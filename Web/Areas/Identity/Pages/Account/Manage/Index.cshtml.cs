#nullable disable

using System.ComponentModel.DataAnnotations;
using Infra.DatabaseAdapter.Models;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Areas.Identity.Models;

namespace Web.Areas.Identity.Pages.Account.Manage;

public class IndexModel : PageModel
{
    private readonly SignInManager<UserModel> _signInManager;
    private readonly UserManager<UserModel> _userManager;

    public IndexModel(
        UserManager<UserModel> userManager,
        SignInManager<UserModel> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [TempData] public string StatusMessage { get; set; }

    [BindProperty] public UserProfileModel Profile { get; set; }

    private async Task LoadAsync(UserModel user)
    {
        var userName = await _userManager.GetUserNameAsync(user);
        var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

        Profile = new UserProfileModel
        {
            Username = userName,
            PhoneNumber = phoneNumber,
            Name = user.Name,
            Surname = user.Surname,
            Patronymic = user.Patronymic,
            BirthDate = user.BirthDate
        };
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");

        await LoadAsync(user);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");

        if (!ModelState.IsValid)
        {
            await LoadAsync(user);
            return Page();
        }

        var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
        if (Profile.PhoneNumber != phoneNumber)
        {
            var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Profile.PhoneNumber);
            if (!setPhoneResult.Succeeded)
            {
                StatusMessage = "Unexpected error when trying to set phone number.";
                return RedirectToPage();
            }
        }

        if (Profile.Name != user.Name)
            user.Name = Profile.Name;
        if (Profile.Surname != user.Surname)
            user.Surname = Profile.Surname;
        if (Profile.Patronymic != user.Patronymic)
            user.Patronymic = Profile.Patronymic;
        if (Profile.BirthDate != user.BirthDate) //TODO: Check correct send data.
            user.BirthDate = Profile.BirthDate;
        await _userManager.UpdateAsync(user);


        await _signInManager.RefreshSignInAsync(user);
        StatusMessage = "Your profile has been updated";
        return RedirectToPage();
    }
}