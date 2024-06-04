
let CourseData = [];

$(document).ready(function () {
    const struser = localStorage.getItem('user');
    let user = undefined;
    if (struser) {
        user = JSON.parse(localStorage.getItem('user'));
    }

    if (user && user.isAdmin) {
        $('#loadCoursesBtn').show();
    } else {
        $('#loadCoursesBtn').hide();
        alert("You are not authorized to access this page.");
        window.location.href = "login.html";
    }

    $('#loadCoursesBtn').on('click', function () {
        alert("Handler INSERT for `click` called.");
        $.getJSON("../Data/Course.json", function (Data) {
            CourseData.push(Data);
            insertCourses(CourseData[0]);
        });
    });
});

function insertCourses(CourseData) {
    api = "https://localhost:7076/api/Courses";
    var courseDataToSend;
    CourseData.forEach(course => {
        courseDataToSend = {
            id: course.id,
            title: course.title,
            url: course.url,
            rating: course.rating,
            numberOfReviews: course.num_reviews,
            instructorsId: course.instructors_id,
            imageReference: course.image,
            duration: course.duration,
            lastUpdate: course.last_update_date
        };
        ajaxCall("POST", api, JSON.stringify(courseDataToSend), postSCBF, postECBF);
    });
}

function postSCBF(result) {
    console.log(result);
}

function postECBF(err) {
    console.log(err);
}