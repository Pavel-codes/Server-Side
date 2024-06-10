var coursesData = [];
const udemy = "https://www.udemy.com";

//localStorage.clear();

$(document).ready(function () {

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
            courseElement.append('<p>Number Of Reviews: ' + course.num_reviews + '</p>');
            courseElement.append('<p><a href="' + udemy + course.url + '">Link</a></p>');
            courseElement.append('<button id="' + course.id + '">Add Course</button>');
            coursesContainer.append(courseElement);
            addCourseClick(courseElement);
        });
    }


    $('*').not('script, style').css({
        'padding': '5px',
        'margin-top': '5px',
        'margin-bottom': '5px'
    });
});

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




if (!isLoggedIn()) {
    $('#logoutBtn').hide();
    $('#loginBtn').show();
    $('#Registerbtn').show();
    $('#myCourses').hide();
}
else {
    $('#logoutBtn').show();
    $('#loginBtn').hide();
    $('#Registerbtn').hide();
    $('#myCourses').show();
}

function isLoggedIn() {
    return localStorage.getItem('user') !== null;
}

function addCourseClick(element) {
    element.click(function (event) {

        if (event.target.tagName.toLowerCase() === 'button') {
            const buttonId = event.target.id;
            console.log("Button clicked with ID:", buttonId);

            if (isLoggedIn()) {
                const user = JSON.parse(localStorage.getItem('user')); //
                addCourse(buttonId, user.id);
            }
            else {
                console.log("User not logged in. Redirecting to login.");
                alert("Please login or register to add courses.");
                window.location.href = "login.html";
            }
        }
    });
}

function addCourse(buttonId, userId) {
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
                numberOfReviews: courseData.num_reviews,
                instructorsId: courseData.instructors_id,
                imageReference: courseData.image,
                duration: courseData.duration,
                lastUpdate: courseData.last_update_date
            };

            const api = `https://localhost:7076/api/Courses/addCourseToUser/${userId}`;

            ajaxCall("POST", api, JSON.stringify(courseDataToSend), postSCBF, postECBF)

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
}