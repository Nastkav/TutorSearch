using Bogus;

namespace Infra.Helpers;

public class SubjectSet : DataSet
{
    private static readonly string[] SubjectTitles =
    [
        "Англійська",
        "Іспанська",
        "Французька",
        "Німецька",
        "Японська",
        "Італійська",
        "Корейська",
        "Арабська",
        "Китайська (мандарин)",
        "Інформатика",
        "Статистика",
        "Бухгалтерський облік",
        "Хімія",
        "Біологія",
        "Алгебра",
        "Фізика",
        "Історія",
        "Математика",
        "Португальська",
        "Економіка"
    ];

    public string Generate() => Random.ArrayElement(SubjectTitles);

    // public List<string> Generate(int count)
    // {
    //     var subjs = new List<string>();
    //     for (var i = 0; i < count; i++)
    //         subjs.Add(Random.ArrayElement(SubjectTitles));
    //     return subjs;
    // }
}