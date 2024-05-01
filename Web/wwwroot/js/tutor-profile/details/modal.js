function submitForm() {
    var subject = document.getElementById('subject').value;

    if (subject === null || subject === "") {
        alert("������ �������!");
        return;
    }

    makePostRequest(subject);
}

function makePostRequest(subject) {
    const baseUrl = window.location.origin;
    const tutorProfileId = document.getElementById('tutorId').value;
    const from = document.getElementById('startDate').value;
    const to = document.getElementById('endDate').value;
    const comment = document.getElementById('comment').value;
    /*subject = "Գ����";*/

    var url = baseUrl + `/LessonRequest/Create?Subject=${encodeURIComponent(subject)}&TutorProfileId=${tutorProfileId}&From=${encodeURIComponent(from)}&To=${encodeURIComponent(to)}&Comment=${encodeURIComponent(comment)}`;

    fetch(url, {
        method: 'POST',
    })
        .then(response => {
            console.log("response", response)
            if (!response.ok) {
                alert("������� ��� �������� ������!");
            } else {
                return response.text();
            }

        })
        .then(data => {
            console.log('Success:', data);
            window.location.reload();
        })
        .catch((error) => {
            console.error('Error:', error);
            alert("������� ��� �������� ������!");
        });
}