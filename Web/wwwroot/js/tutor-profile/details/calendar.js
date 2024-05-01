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
        events: calendarEvents,
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
        let event = calendar.addEvent({
            start: info.startStr,
            end: info.endStr,
        });
        console.log("calendar", calendar, event, info);
        const url = `${baseUrl}/Lesson/Create?Type=Available&From=${info.startStr.split('+')[0]}&To=${info.endStr.split('+')[0]}`;

        addAvailableTime(url)
            .then(() => {
                window.location.reload();
            })
            .catch((error) => {
                alert(error.message);
                event.remove();
            })
    }

    function removeEvent(info) {
        console.log(info, info.editable, info.groupId, info.event);
        const url = `${baseUrl}/Lesson/Delete?Id=${info.Id}`;
        deleteEvent(url)
            .then(() => {
                info.event.remove();
            })
            .catch((error) => {
                alert(error.message);
            })
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

async function addAvailableTime(url = '') {
    try {
        const response = await fetch(url, { method: 'POST' });
        if (!response.ok) {
            throw new Error('   !');
        }
        const data = await response.text();
        console.log('Success:', data);
        return
    } catch (error) {
        console.error('Error:', error);
        throw error
    }
}

async function deleteEvent(url = '') {
    try {
        const response = await fetch(url, { method: 'POST' });
        if (!response.ok) {
            throw new Error('   !');
        }
        const data = await response.text();
        console.log('Success:', data);
        return
    } catch (error) {
        console.error('Error:', error);
        throw error
    }
}

function prepareCalendarEvents(dates) {
    console.log("dates", dates);
    const availableDates = dates.filter(date => date.type.toLowerCase() === "available");
    return availableDates.map(({ startTime, endTime }) => ({
        groupId: "available",
        start: startTime,
        end: endTime,
    }));
}


