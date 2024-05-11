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
    document.requestForm.timeFrom.value = info.start.toISOString().replace(/.\d+Z$/g, "")
    document.requestForm.timeTo.value = info.end.toISOString().replace(/.\d+Z$/g, "")
    $('#requestModal').modal('show');
}


//зберегти в базу
async function send_request(e) {
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
        success: function (data) {
            $('#requestModal').modal('hide');
        }, error: function (data) {
            alert(data.responseJSON.join('\n')); // show response from the php script.
        }
    })
}

