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
        editable: false,
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
    var strStart = new Date(info.start - info.start.getTimezoneOffset() * 60000).toISOString()
    var strEnd = new Date(info.end - info.end.getTimezoneOffset() * 60000).toISOString()
    $.ajax({
        url: "/Session/List",
        type: "GET",
        dataType: 'json',
        data: {
            from: strStart,
            to: strEnd,
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
    var endTimeDiff = 0;
    if (info.start.getDate() !== info.end.getDate())
        endTimeDiff = 1;
    var endTime = new Date(info.end - endTimeDiff - info.start.getTimezoneOffset() * 60000).toISOString()
    $.ajax({
        url: "/Session/Create",
        type: "POST",
        dataType: 'json',
        async: false,
        data: {
            Type: "Available",
            from: info.startStr,
            to: endTime.toString(),
            userid: $("#calendar").data('tutorid'),
            TimeTypes: "Available"
        },
        success: function (data) {
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
            calendar.refetchEvents();
        },
        error: function (data) {
            alert("Помилка")
            calendar.refetchEvents();
        }
    })
}




