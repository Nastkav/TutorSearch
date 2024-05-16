using System.ComponentModel.DataAnnotations;

namespace Infra.DatabaseAdapter.Helpers;

public enum SolutionStatus
{
    [Display(Name = "До виконання")] Todo,
    [Display(Name = "На Перевирці")] Review,
    [Display(Name = "Виконано")] Completed
}