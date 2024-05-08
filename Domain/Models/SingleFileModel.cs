using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Domain.Models;

//https://www.c-sharpcorner.com/article/upload-single-or-multiple-files-in-asp-net-core-using-iformfile2/

public class MultipleFiles
{
    public bool IsResponse { get; set; }


    // [Required(ErrorMessage = "Please select files")]
    public List<IFormFile> Files { get; set; } = [];
}