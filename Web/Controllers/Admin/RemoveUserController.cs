using System.Security.Claims;
using Infra.DatabaseAdapter;
using Infra.DatabaseAdapter.Helpers;
using Infra.DatabaseAdapter.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Models.RemoveUser;

namespace Web.Controllers.Admin;

public class RemoveUserController : Controller
{
    private const string REM_NAME = "REMOVED";

    private int IdentityId => Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
    private readonly AppDbContext _context;
    private readonly SignInManager<UserModel> _signInManager;
    private readonly UserManager<UserModel> _userManager;


    public RemoveUserController(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager,
        AppDbContext context)
    {
        _context = context;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public int? RemoveRoleId => _context.Roles
        .AsNoTracking()
        .Where(x => x.NormalizedName == "Removed")
        .FirstOrDefault()?.Id ?? null;

    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Index()
    {
        var model = new RemoveUserVm();
        //get roles

        if (RemoveRoleId != null)
        {
            var userList = _context.Users
                .AsNoTracking()
                .Where(x => x.NormalizeName != REM_NAME)
                .Where(x => x.Id != IdentityId)
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = $"{x.Id},{x.NormalizeName}" })
                .ToList();

            var removedUsers = await _context.UserRoles
                .AsNoTracking()
                .Where(x => x.RoleId == RemoveRoleId)
                .Select(x => x.UserId)
                .ToListAsync();

            //ignore removed users
            foreach (var item in userList)
                if (!removedUsers.Contains(int.Parse(item.Value)))
                    model.UserList.Add(item);
        }

        return View(model);
    }


    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeactivateUser(int userId)
    {
        var userDb = await _context.Users
            .Where(x => x.Id == userId)
            .FirstOrDefaultAsync();

        if (RemoveRoleId != null && userDb != null) //add role Removed
        {
            //remove role from db
            _context.UserRoles.Add(new IdentityUserRole<int>() { UserId = userDb.Id, RoleId = RemoveRoleId.Value });
            await _context.SaveChangesAsync();

            // reset user cookies
            await _userManager.UpdateSecurityStampAsync(userDb);
        }


        return Redirect(nameof(Index));
    }


    [HttpPost]
    [Authorize(Roles = "Removed")]
    public async Task<IActionResult> ReactivateMe()
    {
        var role = await _context.UserRoles
            .Where(x => x.UserId == IdentityId && x.RoleId == RemoveRoleId)
            .FirstOrDefaultAsync();

        if (role != null)
        {
            _context.Remove(role);
            await _context.SaveChangesAsync();
        }

        await _signInManager.SignOutAsync();
        return Redirect("/");
    }


    [HttpPost]
    [Authorize(Roles = "Removed")]
    public async Task<IActionResult> RemoveMePersonalData()
    {
        var userDb = await _context.Users
            .Include(x => x.Tutor)
            .Include(x => x.Tutor.About)
            .Include(x => x.Tutor.AvailableTimes)
            .Where(x => x.Id == IdentityId)
            .FirstAsync();


        //remove all roles
        var rolesList = _context.UserRoles.Where(x => x.UserId == IdentityId);
        _context.UserRoles.RemoveRange(rolesList);

        //clear user fields
        userDb.Name = string.Empty;
        userDb.Surname = string.Empty;
        userDb.Patronymic = string.Empty;
        userDb.ProfileEnabled = false;
        userDb.CityId = null;
        userDb.BirthDate = null;
        userDb.FavoriteTutors = new List<TutorModel>();
        userDb.NormalizeName = string.Empty;
        userDb.UserName = null;
        userDb.Email = null;
        userDb.NormalizedEmail = null;
        userDb.NormalizedUserName = null;
        userDb.PasswordHash = null;

        //clear tutor fields
        if (userDb.Tutor != null)
        {
            userDb.Tutor.Address = string.Empty;
            userDb.Tutor.OnlineAccess = false;
            userDb.Tutor.TutorHomeAccess = false;
            userDb.Tutor.StudentHomeAccess = false;
            userDb.Tutor.Descriptions = string.Empty;
            userDb.Tutor.HourRate = 0;
            userDb.Tutor.Subjects = new List<SubjectModel>();

            //remove About
            if (userDb.Tutor.About.Id != 0)
                _context.Remove(userDb.Tutor.About);

            //remove AvailableTimes
            _context.RemoveRange(userDb.Tutor.AvailableTimes);
        }

        //cancel requests
        var newReq = await _context.Requests
            .Where(x => x.Status == LessonRequestStatus.New &&
                        (x.CreatedId == IdentityId || x.Tutor.Id == IdentityId))
            .ToListAsync();
        foreach (var request in newReq)
            request.Status = LessonRequestStatus.Rejected;

        //remove solutions
        var solutions = await _context.Solutions
            .Where(x => x.StudentId == IdentityId)
            .ToListAsync();
        _context.Solutions.RemoveRange(solutions);

        //save changes
        await _context.SaveChangesAsync();

        //logout
        await _signInManager.SignOutAsync();
        return Redirect("/");
    }

    [Authorize(Roles = "Removed")]
    public IActionResult Removed() => View();
}