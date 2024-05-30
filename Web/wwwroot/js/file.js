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
    document.getElementById('uploadform-file').onchange = function () {
        $(".alert").alert('close');
        var postData = new FormData($(this.parentElement)[0]);
        $.ajax({
            url: this.parentElement.action,
            type: "POST",
            data: postData,
            processData: false,
            contentType: false,
            success: function (data, text) {
                appendAlert('Файл успішно завантажено на  сервер', 'success')
            },
            error: function (request, status, error) {

                var msg = "Помилка під час завантаження файлу, спробуйте надіслати інший файл"
                if (request !== null && request.responseJSON !== null && request.responseJSON.message !== null)
                    msg += ": " + request.responseJSON.message;

                appendAlert(msg + "\n", 'danger')
            }
        });
    };
