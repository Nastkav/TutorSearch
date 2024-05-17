function onclickFileDelete(item) {
    $(".alert").alert('close');
    $.ajax({
        url: item.formAction,
        method: 'post',
        dataType: 'json',
        data: {FileId: item.dataset.filename},
        success: function (data, text) {
            appendAlert('Файл успішно видалено з  сервер', 'success')
        },
        error: function (request, status, error) {
            appendAlert('Помилка під час видаленя файлу:\n' + request, 'danger')
        }
    });
}

//https://coreui.io/bootstrap/docs/components/alerts/#live-example
const alertPlaceholder = document.getElementById('liveAlertPlaceholder')
const appendAlert = (message, type) => {
    const wrapper = document.createElement('div')
    wrapper.innerHTML = [
        `<div class="alert alert-${type} alert-dismissible" role="alert">`,
        `   <div>${message}</div>`,
        '   <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>',
        '</div>'
    ].join('')
    alertPlaceholder.append(wrapper)
}
var uploadForm = document.getElementById('uploadform');
if (uploadForm !== null && uploadForm !== undefined)
    document.getElementById('uploadform').addEventListener('submit', function (event) {
        event.preventDefault(); // Запобігання надсиланню форми
        $(".alert").alert('close');
        var postData = new FormData($("#uploadform")[0]);
        $.ajax({
            url: event.target.action,
            type: "POST",
            data: postData,
            processData: false,
            contentType: false,
            success: function (data, text) {
                appendAlert('Файл успішно завантажено на  сервер', 'success')
            },
            error: function (request, status, error) {
                appendAlert('Помилка під час завантаження файлу, спробуйте надіслати інший файл:\n' + error, 'danger')
            }
        });
    });