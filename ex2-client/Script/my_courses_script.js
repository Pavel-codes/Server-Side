var myCourses = [];
//const udemy = "https://www.udemy.com";

$(document).ready(function () {
    // Load courses from server
    api = "https://localhost:7076/api/Courses";
    //ajaxCall("GET", api, postSCBF, postECBF)
    $.ajax({
        url: api,
        type: 'GET',
        success: function (data) {
            renderCourses(data);
        },
        error: function () {
            alert("Error loading courses.");
        }
    });

    // Render courses
    function renderCourses(coursesList) {
        var coursesContainer = $('#courses-container');
        coursesList.forEach(function (course) {
            myCourses.push(course);
            var courseElement = $('<div>');
            courseElement.append('<img src=' + course.imageReference + '>');
            courseElement.append('<h2>' + course.title + '</h2>');
            courseElement.append('<p>Instructors ID: ' + course.instructorsId + '</p>');
            courseElement.append('<p>Duration: ' + course.duration + '</p>');
            courseElement.append('<p>Rating: ' + course.rating + '</p>');
            courseElement.append('<p><a href="' + course.url + '">Link</a></p>');
            courseElement.append('<button id="' + course.id + '">Remove Course</button>');
            coursesContainer.append(courseElement);
        });
    }
});

document.addEventListener('click', function (event) {
    // Check if the clicked element is a button
    if (event.target.tagName.toLowerCase() === 'button') {
        const buttonId = event.target.id;
        removeCourse(buttonId);
        console.log("Button clicked with ID:", buttonId);
    }
});


function removeCourse(buttonId) {
    // Assuming allData is an array of objects representing courses
    console.log(buttonId);
    myCourses.forEach(courseData => {
        if (buttonId == courseData.id) {
            api = "https://localhost:7076/api/Courses/" + courseData.id;
            ajaxCall("DELETE", api, null, deleteSCBF, deleteECBF)
        }
        else {
            console.log("not found");
        }
    });
}


function deleteSCBF(result) {
    alert("Course removed");
    console.log(result);
}

function deleteECBF(err) {
    alert("error occured - indicator, course was still deleted"); // jumps to error function because of catch handling
    console.log(err);
}
