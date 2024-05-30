let table = new DataTable('.pretty-table', {
    language: {
        "lengthMenu": "Відображати _MENU_ записів на сторінку",
        "zeroRecords": "Нічого не знайдено - вибачте",
        "info": "Показано сторінку _PAGE_ з _PAGES_",
        "infoEmpty": "Немає доступних записів",
        "infoFiltered": "(відфільтровано з _MAX_ загальних записів)",
        "emptyTable": "Немає даних у таблиці",
        "loadingRecords": "Завантаження...",
        "search": "Пошук:",
        "арія": {
            "orderable": "Упорядкувати за цим стовпцем",
            "orderableReverse": "Змінити порядок цього стовпця"
        }
    },
    pagingType: 'simple_numbers',
    scrollCollapse: true,
    deferRender: true,
    scroller: true,
    scrollY: 1000,

});


//load status favorite button
function FavoriteStatus(element, isGet) {
    var urlMethod = isGet ? 'get' : 'post';
    $.ajax({
        url: "/Profile/IsFavorite",
        method: urlMethod,
        dataType: 'json',
        data: {
            UserId: element.dataset.userid,
            TutorId: element.dataset.tutorid,
            Status: element.checked
        },
        success: function (data) {
            element.checked = data.status;
        },
        error: function (r, t, e) {
            element.checked = !element.checked //revert state
            console.log(e)
        }
    });
}


const favoriteElem = document.getElementById("btn-favorite")
if (favoriteElem !== null) {
    FavoriteStatus(favoriteElem, isGet = true)
//onclick for favorite button

    favoriteElem.addEventListener("click", function () {
        FavoriteStatus(this)
    })

}