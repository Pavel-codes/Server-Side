var coursesData = [];
const udemy = "https://www.udemy.com"; 
                                       

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
            addCoursClick(courseElement);
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
    window.open("../Pages/MyCourses.html", "_blank"); 
});


const instructorsBtn = document.getElementById("instructorsBtn");


instructorsBtn.addEventListener("click", function () {
    window.open("../Pages/instructorsPage.html", "_blank");


});

const loginbtn = document.getElementById("loginbtn"); 

loginbtn.addEventListener("click", function () { 
    window.open("../Pages/login.html", "_blank"); 
});


const Registerbtn = document.getElementById("Registerbtn");

Registerbtn.addEventListener("click", function () { 
    window.open("../Pages/register.html", "_blank"); 
});

const Adminbtn = document.getElementById("Adminbtn");


Adminbtn.addEventListener("click", function () {
    window.open("../Pages/admin.html", "_blank");


});


function postSCBF(result) {
    if (!result) alert("Course is already in database"); // 
    console.log(result);
}


function postECBF(err) {

    console.log(err);
}


function isLoggedIn() {
    return localStorage.getItem('user') !== null;
}

function addCoursClick(element) {
    element.click(function (event) {

        if (event.target.tagName.toLowerCase() === 'button') {
            const buttonId = event.target.id;
            console.log("Button clicked with ID:", buttonId);

            if (isLoggedIn()) {
                addCourse(buttonId);
                alert("Course was added");
                console.log("User is logged in. Adding course.");
            } else {
                console.log("User not logged in. Redirecting to login.");
                alert("Please login or register to add courses.");
                window.location.href = "login.html";
            }
        }
    });
}



function addCourse(buttonId) {
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
            console.log(courseDataToSend) // test
            
            api = "https://localhost:7076/api/Courses"; 
            
            ajaxCall("POST", api, JSON.stringify(courseDataToSend), postSCBF, postECBF)

        }
        else {
            console.log("not found");
        }
    });
}
