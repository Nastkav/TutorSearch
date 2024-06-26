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
    var strStart = new Date(info.start - info.start.getTimezoneOffset() * 60000).toISOString()
    var strEnd = new Date(info.end - info.end.getTimezoneOffset() * 60000).toISOString()

    $.ajax({
        url: "/Session/List",
        type: "GET",
        dataType: 'json',
        data: {
            from: strStart,
            to: strEnd,
            userid: $("#calendar").data('tutorid')
        },
        success: function (data) {
            availableDates = data.filter(date => date.type.toLowerCase() !== "available");
            calEvents = availableDates.map(({from, to}) => ({
                groupId: "busy",
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
        //'2024-04-23T05:30:00+03:00' --> '2024-04-23T05:30:00'
        if (info != null) {
            document.requestForm.timeFrom.value = info.startStr.slice(0, -6)
            document.requestForm.timeTo.value = info.endStr.slice(0, -6)
        }

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

