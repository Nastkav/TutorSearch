using System.ComponentModel.DataAnnotations;

namespace Infra.DatabaseAdapter.Helpers;

public enum LessonRequestStatus
{
    [Display(Name = "Новий")] New,
    [Display(Name = "Схвалено")] Approved,
    [Display(Name = "Відхилено")] Rejected
}