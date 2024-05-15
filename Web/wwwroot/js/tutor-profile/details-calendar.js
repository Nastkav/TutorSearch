createCalendar()

function createCalendar() {
    calElement = document.getElementById('calendar')
    calendar = new FullCalendar.Calendar(calElement, {
        initialDate: Date.now(),
        initialView: 'timeGridWeek',
        themeSystem: 'bootstrap4',
        headerToolbar: {
            left: '',
            center: '',
            right: ''
        },
        selectOverlap: false,
        eventOverlap: false,
        height: "auto",
        navLinks: false,
        editable: true,
        selectable: true,
        selectMirror: true,
        allDaySlot: false,
        firstDay: 1,
        select: addNewEvent,
        eventSources: getEvents
    });
    calendar.render();
}

//Отримання списку подій
function getEvents(info, successCallback, failureCallback) {
    $.ajax({
        url: "/Session/List",
        type: "GET",
        dataType: 'json',
        data: {
            from: info.start.valueOf(),
            to: info.end.valueOf(),
            userid: $("#calendar").data('tutorid')
        },
        success: function (data) {
            availableDates = data.filter(date => date.type.toLowerCase() !== "available");
            calEvents = availableDates.map(({from, to}) => ({
                groupId: "игын",
                start: from,
                end: to,
                display: 'background',
                backgroundColor: 'red'
            }));
            successCallback(calEvents)
        }
    })
}

//Додання нового запису
function addNewEvent(info) {
    var userid = $("#calendar").data('userid')
    if (userid > 0) {
        let start = new Date(info.start)
        let end = new Date(info.end)
        start.setMinutes(start.getMinutes() - start.getTimezoneOffset());
        end.setMinutes(end.getMinutes() - end.getTimezoneOffset());
        document.requestForm.timeFrom.value = start.toISOString().slice(0, -1);
        document.requestForm.timeTo.value = end.toISOString().slice(0, -1);

        $('#requestModal').modal('show');
    } else {
        $('#auth-modal').modal('show');
    }
}


//зберегти в базу
async function send_request() {
    formData = {
        TutorId: document.requestForm.tutorProfileId.value,
        SubjectId: document.requestForm.subject.value,
        From: document.requestForm.timeFrom.value,
        To: document.requestForm.timeTo.value,
        Comment: document.requestForm.tutorComment.value ?? ""
    }
    console.log(formData)
    $.ajax({
        type: "POST",
        url: document.requestForm.action,
        data: formData,
        success: function () {
            $('#requestModal').modal('hide');
        }, error: function (data) {
            alert(data.responseJSON.join('\n')); // show response from the php script.
        }
    })
}

