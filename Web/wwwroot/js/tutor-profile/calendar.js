const baseUrl = window.location.origin;
let calendarEl = document.getElementById('calendar');
const tutorId = calendarEl.dataset.tutorid;

console.log("Tutor Id", tutorId);
if (tutorId) {
    createCalendar(calendarEl);
}

async function createCalendar(calendarEl) {
    const dates = await getDates();
    const calendarEvents = prepareCalendarEvents(dates);
    console.log("Calendar events", calendarEvents);

    var calendar = new FullCalendar.Calendar(calendarEl, {
        initialDate: Date.now(),
        initialView: 'timeGridWeek',
        themeSystem: 'bootstrap5',
        headerToolbar: {
            left: '',
            center: '',
            right: ''
        },
        selectOverlap: false,
        eventOverlap: false,
        /*eventConstraint: [
            { groupId: 'test', start: '2024-04-15T09:00:00', end: '2024-04-15T12:00:00', display: 'inverse-background' },
            { groupId:'test2', start: '2024-04-16T14:00:00', end: '2024-04-16T17:00:00', display: 'inverse-background' }
        ],
        selectConstraint: [
            { start: '2024-04-15T09:00:00', end: '2024-04-15T12:00:00' }, 
            { start: '2024-04-16T14:00:00', end: '2024-04-16T17:00:00' }
        ],*/
        events: calendarEvents,
        //events:
        //    [
        //        { groupId: 'test', start: '2024-04-15T09:00:00', end: '2024-04-15T12:00:00', backgroundColor: 'red', display:'background' },
        //        /*{ groupId: 'test2', start: '2024-04-16T14:00:00', end: '2024-04-16T17:00:00', display: 'inverse-background' },*/
        //        {
        //            start: '2024-04-15T21:00:00+03:00',
        //            end: '2024-04-15T22:00:00+03:00'
        //        },
        //        /*{
        //            title: 'All Day Event',
        //            start: '2024-04-15'
        //        },
        //        {
        //            title: 'Long Event',
        //            start: '2024-04-15',
        //            end: '2024-04-16'
        //        },*/
        //        /*{
        //            groupId: '999',
        //            title: 'Repeating Event',
        //            start: '2024-04-14T21:00:00+03:00',
        //            editable: false,
        //            type: "unavailable",
        //            //display: 'background',
        //            
        //            selectable: false
        //        },*/
        //        /*{
        //            groupId: '999',
        //            title: 'Repeating Event',
        //            start: '2024-04-08T16:00:00'
        //        },*/
        //        /*{
        //            title: 'Conference',
        //            start: '2024-04-15',
        //            end: '2024-04-17'
        //        },*/
        //        {
        //            title: 'Meeting',
        //            start: '2024-04-16T10:30:00',
        //            end: '2024-04-16T12:30:00'
        //        },
        //        {
        //            title: 'Lunch',
        //            start: '2024-04-17T12:00:00'
        //        },
        //        /*{
        //            title: 'Click for Google',
        //            url: 'https://google.com/',
        //            start: '2024-04-14'
        //        }*/
        //    ],
        height: "auto",
        navLinks: false,
        editable: true,
        selectable: true,
        selectMirror: true,
        allDaySlot: false,
        //slotEventOverlap: false,
        select: addNewEvent,
        eventClick: removeEvent
    });

    calendar.render();

    function addNewEvent(info) {
        console.log("info", info, calendar);
        let t = calendar.addEvent({
            start: info.startStr,
            end: info.endStr,
        });
        //info.event.remove();
        showModal(info.startStr, info.endStr);
        console.log("calendar", calendar, t)


    }

    function removeEvent(info) {
        //info.event.remove();
        console.log(info, info.editable, info.groupId, info.event);
    }
}

async function getDates() {
    try {
        const response = await fetch(baseUrl + '/Lesson/List?UserId=' + tutorId); // -- main
        //const response = await fetch(baseUrl + '/Lesson/List?UserId=5'); // temporary
        const dates = await response.json();
        console.log(dates);
        return dates
    } catch (error) {
        console.error('Error get dates:', error);
        return []
    }
}

function prepareCalendarEvents(dates) {
    console.log("dates", dates);
    const availableDates = dates.filter(date => date.type.toLowerCase() !== "available");
    return availableDates.map(({ startTime, endTime }) => ({
        groupId: "unavailable",
        start: startTime,
        end: endTime,
        display: 'background',
        backgroundColor: 'red'
    }));
}

function showModal(startDate, endDate) {
    console.log("dates ", startDate, endDate)
    const modal = new bootstrap.Modal(document.getElementById("request-modal"), {});
    let startDateInput = document.getElementById('startDate');
    let endDateInput = document.getElementById('endDate');

    startDateInput.value = formatDateForInput(startDate);
    endDateInput.value = formatDateForInput(endDate);

    modal.show();
}

function formatDateForInput(dateString) {
    const dateObject = new Date(dateString);
    const year = dateObject.getFullYear();
    const month = (dateObject.getMonth() + 1).toString().padStart(2, '0');
    const day = dateObject.getDate().toString().padStart(2, '0');
    const hours = dateObject.getHours().toString().padStart(2, '0');
    const minutes = dateObject.getMinutes().toString().padStart(2, '0');

    return `${year}-${month}-${day}T${hours}:${minutes}`;
}