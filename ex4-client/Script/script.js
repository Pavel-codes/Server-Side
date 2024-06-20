﻿var coursesData = [];
const udemy = "https://www.udemy.com";
const apiBaseUrl = "https://localhost:7283/api/Courses";
var modal = $('#coursesModal');
var span = $('.close');
var instructors = [];
var user = JSON.parse(localStorage.getItem('user'));

$(document).ready(function () {

    function getInstructors() {
        let api = "https://localhost:7283/api/Instructors/";
        ajaxCall('GET', api, true, getInstructorSCBF, getInstructorECBF);
    }

    function getInstructorSCBF(response) {
        instructors.push(response);
    }

    function getInstructorECBF(err) {
        console.log(err);
    }

    getInstructors();

    function getCoursesFromDB() {
        ajaxCall('GET', apiBaseUrl, true, getCoursesSCBF, getCoursesECBF);
    }
    
    function getCoursesSCBF(response) {
        console.log(response);
        renderCourses(response);
    }

    function getCoursesECBF(err) {
        console.log(err);
        alert("Failed to load courses!");
    }

    // Render courses
    function renderCourses(courses) {
        var coursesContainer = $('#courses-container');
        var courseInstructorName;
        courses.forEach(function (course) {
            coursesData.push(course); // do not remove 
            courseInstructorName = instructors[0].find(instructor => instructor.id == course.instructorsId);

            var courseElement = $('<div>');
            courseElement.append('<img src=' + course.imageReference + '>');
            courseElement.append('<h2>' + course.title + '</h2>');
            courseElement.append('<p>By: ' + courseInstructorName.name + '</p>'); // created with array - workaround
            var showMoreBtn = $('<button id="' + course.instructorsId + '">Show more courses of this instructor</button>');
            var addCourseBtn = $('<button id="' + course.id + '">Add Course</button>');
            courseElement.append(showMoreBtn);
            courseElement.append('<p>Duration: ' + course.duration + '</p>');
            courseElement.append('<p>Rating: ' + course.rating + '</p>');
            courseElement.append('<p>Number Of Reviews: ' + course.numberOfReviews + '</p>');
            courseElement.append('<p>Last update: ' + course.lastUpdate + '</p>');
            courseElement.append('<p><a href="' + udemy + course.url + '">Link</a></p>');
            courseElement.append(addCourseBtn);

            coursesContainer.append(courseElement);

            // Attach the click event using addCourseClick directly
            addCourseClick(addCourseBtn);

            showMoreBtn.on('click', function () {
                addCoursesToModal(this.id);
            });
        });
    }

    // still need to fix login
    function isLoggedIn() {
        return localStorage.getItem('user') !== null;
    }

    function addCourseClick(button) {
        button.on('click', function (event) {
            if (event.target.tagName.toLowerCase() === 'button') {
                const buttonId = event.target.id;
                console.log("Button clicked with ID:", buttonId);

                if (isLoggedIn()) {
                    const user = JSON.parse(localStorage.getItem('user'));
                    addCourse(buttonId, user.id);
                } else {
                    console.log("User not logged in. Redirecting to login.");
                    alert("Please login or register to add courses.");
                    window.location.href = "login.html";
                }
            }
        });
    }

    getCoursesFromDB();
});


modal.css('display', 'none');
span.on('click', function () {
    modal.css('display', 'none');
});

$(window).on('click', function (event) {
    if (event.target === $('#coursesModal')[0]) {
        $('#coursesModal').hide();
    }
});
function addCoursesToModal(buttonId) {
    modal.css('display', 'block');

    //clearModal();
    $('#modal-content').children().slice(1).remove();

    let api = `https://localhost:7283/api/Courses/searchByInstructorId/${buttonId}`;
    ajaxCall("GET", api, null, getInstructorCoursesSCBF, getInstructorCoursesECBF);
}

function getInstructorCoursesSCBF(result) {
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

function getInstructorCoursesECBF(err) {
    console.log(err);
    alert("Error adding courses");
}

const myCoursesBtn = document.getElementById("myCourses");

myCoursesBtn.addEventListener("click", function () {
    window.location.href = "MyCourses.html";
});


const instructorsBtn = document.getElementById("instructorsBtn");

instructorsBtn.addEventListener("click", function () {
    window.location.href = "instructorsPage.html";

});

const loginBtn = document.getElementById("loginBtn");

loginBtn.addEventListener("click", function () {
    window.location.href = "login.html";
});

const logoutbtn = document.getElementById("logoutBtn");

logoutBtn.addEventListener("click", function () {
    localStorage.clear();
    window.location.reload();
});


const Registerbtn = document.getElementById("Registerbtn");

Registerbtn.addEventListener("click", function () {
    window.location.href = "register.html";
});

const Adminbtn = document.getElementById("Adminbtn");


Adminbtn.addEventListener("click", function () {
    window.location.href = "admin.html";

});

//check
if (user && !user.isAdmin) {
    $('#logoutBtn').show();
    $('#loginBtn').hide();
    $('#Registerbtn').hide();
    $('#myCourses').show();
    $('#Adminbtn').hide();

}

else if (user && user.isAdmin)
{
    $('#logoutBtn').show();
    $('#loginBtn').hide();
    $('#Registerbtn').hide();
    $('#myCourses').show();
    $('#Adminbtn').show();
}
else {
    $('#logoutBtn').hide();
    $('#loginBtn').show();
    $('#Registerbtn').show();
    $('#myCourses').hide();
    $('#Adminbtn').hide();
}


//possibly need to change implementation to use database instead of list
function addCourse(buttonId, userId) { 
    console.log(buttonId);
    var courseDataToSend;
    coursesData.forEach(courseData => {
        if (buttonId == courseData.id) {
            //console.log("inside if statement");
            
            courseDataToSend = {
                id: courseData.id,
                title: courseData.title,
                url: udemy + courseData.url,
                rating: courseData.rating,
                numberOfReviews: courseData.numberOfReviews,
                instructorsId: courseData.instructorsId,
                imageReference: courseData.imageReference,
                duration: courseData.duration,
                lastUpdate: courseData.lastUpdate
            };
            console.log(courseDataToSend, userId);


            ajaxCall("POST", `${apiBaseUrl}/addCourseToUser/${userId}`, JSON.stringify(courseDataToSend), postSCBF, postECBF)

        }
        else {
            console.log("not found");
        }
    });
}

function postSCBF(result) {
    alert("Course added successfully!");
    console.log(result);
}


function postECBF(err) {
    alert("Course was already added.");
    console.log(err);
}