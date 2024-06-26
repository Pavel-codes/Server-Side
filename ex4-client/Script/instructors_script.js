
const apiBaseUrl = "https://proj.ruppin.ac.il/cgroup85/test2/tar1/api/Instructors";
const udemy = "https://www.udemy.com";
let instructorsData = [];
var modal = $('#coursesModal');
var span = $('.close');

$(document).ready(function () {
    function getInstructorsFromDB() {
        ajaxCall('GET', apiBaseUrl, null, getInstructorsSCBF, getInstructorsECBF);
    }

    function renderInstructors(instructors) {
        var instructorsContainer = $('#instructors-container');
        instructors.forEach(function (instructor) {
            var instructorElement = $('<div>');
            instructorElement.append('<h2>' + instructor.name + '</h2>');
            instructorElement.append('<img src="' + instructor.image + '">');
            instructorElement.append('<p>Title: ' + instructor.title + '</p>');
            instructorElement.append('<p>Job: ' + instructor.jobTitle + '</p>');
            instructorElement.append('<button id="' + instructor.id + '">Show courses</button>');

            instructorsContainer.append(instructorElement);

            let instructorBtn = document.getElementById(instructor.id);
            $(instructorBtn).on('click', function () {

                addCoursesToModal(instructorBtn.id);
            });
        });
    }
    modal.css('display', 'none');
    function getInstructorsSCBF(result) {
        renderInstructors(result);
        console.log("Received instructors");
    }

    function getInstructorsECBF(err) {
        console.log(err);
    }

    getInstructorsFromDB();

    span.on('click', function () {
        modal.css('display', 'none');
    });

    $(window).on('click', function (event) {
        if (event.target === $('#coursesModal')[0]) {
            $('#coursesModal').hide();
        }
    });

});

$('#homeBtn').on('click', function () {
    window.location.href = "../Pages/index.html";
});

function addCoursesToModal(buttonId) {
    modal.css('display', 'block');

    //clearModal();
    $('#modal-content').children().slice(1).remove();

    let api = `https://proj.ruppin.ac.il/cgroup85/test2/tar1/api/Courses/searchByInstructorId/${buttonId}`;
    ajaxCall("GET", api, null, getSCBF, getECBF);
}


function getSCBF(result) {
    console.log(result);
    var modalContent = $('#modal-content');
    result.forEach(function (course) {
        var courseElement = $('<div>');
        courseElement.append('<img src="' + course.imageReference + '">');
        courseElement.append('<h2>' + course.title + '</h2>');
        courseElement.append('<p>Instructors ID: ' + course.instructorsId + '</p>');
        courseElement.append('<p>Duration: ' + course.duration + '</p>');
        courseElement.append('<p>Rating: ' + course.rating + '</p>');
        courseElement.append('<p>Number Of Reviews: ' + course.numberOfReviews + '</p>');
        courseElement.append('<p>Last update: ' + course.lastUpdate + '</p>');
        courseElement.append('<p><a href="' + udemy + course.url + '">Link</a></p>');

        modalContent.append(courseElement);
        modalContent.css('width', '50%');
    });
}

function getECBF(err) {
    console.log(err);
    alert("Error adding courses");
}
