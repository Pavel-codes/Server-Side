﻿var coursesData = [];
const udemy = "https://www.udemy.com";

$(document).ready(function () {
    // Load courses from JSON file

    $.getJSON("../Data/Course.json", function (data) {
        renderCourses(data);
    });


    // Render courses
    function renderCourses(courses) {
        var coursesContainer = $('#courses-container');
        courses.forEach(function (course) {
            coursesData.push(course);
            var courseElement = $('<div>');
            courseElement.append('<img src=' + course.image + '>');
            courseElement.append('<h2>' + course.title + '</h2>');
            courseElement.append('<p>Instructors ID: ' + course.instructors_id + '</p>');
            courseElement.append('<p>Duration: ' + course.duration + '</p>');
            courseElement.append('<p>Rating: ' + course.rating + '</p>');
            courseElement.append('<p><a href="' + udemy + course.url + '">Link</a></p>');
            courseElement.append('<button id="' + course.id + '">Add Course</button>');
            coursesContainer.append(courseElement);
        });
    }

});

const myCoursesBtn = document.getElementById("myCourses");
//myCoursesBtn.onclick = window.open("../Pages/MyCourses.html", "_blank");

myCoursesBtn.addEventListener("click", function () {
    window.open("../Pages/MyCourses.html", "_blank");
});


const instructorsBtn = document.getElementById("instructorsBtn");
//myCoursesBtn.onclick = window.open("../Pages/MyCourses.html", "_blank");

instructorsBtn.addEventListener("click", function () {
    window.open("../Pages/instructorsPage.html", "_blank");
});


function sendSomething() { // strictly for testing POST
    const courseDataToSend = {
        id: 1234,
        title: "title",
        url: "url",
        rating: 3.33333333,
        numberOfReviews: 1234678,
        instructorsId: 111,
        imageReference: 'https://img-c.udemycdn.com/course/750x422/567828_67d0.jpg',
        duration: '11 hours',
        lastUpdate: '11/11/11'
    };

    api = "https://localhost:7076/api/Courses";
    ajaxCall("POST", api, JSON.stringify(courseDataToSend), postSCBF, postECBF)

}

//console.log(coursesData);

function postSCBF(result) {
    if (!result) alert("Course is already in database");
    console.log(result);
}

function postECBF(err) {

    console.log(err);
}

document.addEventListener('click', function (event) {
    // Check if the clicked element is a button
    if (event.target.tagName.toLowerCase() === 'button') {
        const buttonId = event.target.id;
        addCourse(buttonId);
        console.log("Button clicked with ID:", buttonId);
    }
});


function addCourse(buttonId) {
    // Assuming allData is an array of objects representing courses
    //console.log("works!"); - done
    console.log(buttonId);
    var courseDataToSend;
    coursesData.forEach(courseData => {
        if (buttonId == courseData.id) {
            console.log("inside if statement"); // done
            courseDataToSend = {
                id: courseData.id,
                title: courseData.title,
                url: udemy + courseData.url,
                rating: courseData.rating,
                abc: courseData.num_reviews,
                instructorsId: courseData.instructors_id,
                imageReference: courseData.image,
                duration: courseData.duration,
                lastUpdate: courseData.last_update_date
            };
            console.log(courseDataToSend) // test
            api = "https://localhost:7076/api/Courses";
            ajaxCall("POST", api, JSON.stringify(courseDataToSend), postSCBF, postECBF)

        }
        else {
            console.log("not found");
        }
    });
}
