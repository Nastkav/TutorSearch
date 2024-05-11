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
        eventClick: removeEvent,
        eventSources: [{events: getEvents}]
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
            from: info.startStr,
            to: info.endStr,
            userid: $("#calendar").data('tutorid'),
            TimeTypes: "Available"
        },
        success: function (data) {
            calEvents = data.map((x) => ({
                start: x.from,
                end: x.to,
                id: x.id,
                title: x.title
            }));
            successCallback(calEvents)
        }
    })
}


function addNewEvent(info) {
    $.ajax({
        url: "/Session/Create",
        type: "POST",
        dataType: 'json',
        data: {
            Type: "Available",
            from: info.startStr,
            to: info.endStr,
            userid: $("#calendar").data('tutorid'),
            TimeTypes: "Available"
        },
        success: function (data) {
            //alert("Відправлено")
            calendar.refetchEvents();
        },
        error: function (data) {
            alert("Помилка")
            calendar.refetchEvents();
        }
    })
}

function removeEvent(info) {
    $.ajax({
        url: "/Session/Delete",
        type: "POST",
        dataType: 'json',
        data: {
            Id: info.event.id,
        },
        success: function (data) {
            //alert("Видалено")
            calendar.refetchEvents();
        },
        error: function (data) {
            alert("Помилка")
            calendar.refetchEvents();
        }
    })
}




