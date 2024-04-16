var calendarEl = document.getElementById('calendar');
var calendar = new FullCalendar.Calendar(calendarEl, {
    initialDate: Date.now(),
    initialView: 'timeGridWeek',
    themeSystem: 'bootstrap5',
    headerToolbar: {
        left: '',
        center: '',
        right: ''
    },
    height: "auto",
    navLinks: false,
    editable: true,
    selectable: true,
    selectMirror: true,
    allDaySlot: false,
    slotEventOverlap: false,
    select: addNewEvent,
    eventClick: removeEvent
});


function addNewEvent(info) {
    calendar.addEvent({
        start: info.startStr,
        end: info.endStr,
    });

}

function removeEvent(info) {
    info.event.remove();
}

calendar.render();
