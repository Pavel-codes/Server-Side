
//var myCourses = [];
var userCourses = [];
var user = JSON.parse(localStorage.getItem('user'));
var id = user.id;

const apiBaseUrl = "https://proj.ruppin.ac.il/cgroup85/test2/tar1/api";

$(document).ready(function () {

    function getUserCoursesFromDB() {
        //let api = apiBaseUrl + "/Courses/UserCourse/" + user.id;
        let api = `https://proj.ruppin.ac.il/cgroup85/test2/tar1/api/Courses/UserCourse/${user.id}`;
        console.log(api);
        ajaxCall('GET', api, true, getCoursesSCBF, getCoursesECBF);
    }

    function getCoursesSCBF(response) {
        console.log(response);
        renderCourses(response);
    }

    function getCoursesECBF(err) {
        console.log(err);
        alert("Your list is empty!");
    }

    function renderCourses(coursesList) {
        var coursesContainer = $('#courses-container');
        userCourses = coursesList;
        coursesContainer.empty();
        coursesList.forEach(function (course) {
            var courseElement = $('<div>');
            courseElement.append('<img src="' + course.imageReference + '">');
            courseElement.append('<h2>' + course.title + '</h2>');
            courseElement.append('<p>Instructors ID: ' + course.instructorsId + '</p>');
            courseElement.append('<p>Duration: ' + course.duration + '</p>');
            courseElement.append('<p>Rating: ' + course.rating.toFixed(2) + '</p>');
            courseElement.append('<p><a href="' + course.url + '">Link</a></p>');
            courseElement.append('<button id="' + course.id + '">Remove Course</button>');
            coursesContainer.append(courseElement);
        });
    }

    getUserCoursesFromDB();

    $('*').not('script, style').css({
        'padding': '5px'
    });
});
function renderFilteredCourses(coursesList) {
    var coursesContainer = $('#courses-container');
    userCourses = coursesList;
    coursesContainer.empty();
    coursesList.forEach(function (course) {
        var courseElement = $('<div>');
        courseElement.append('<img src="' + course.imageReference + '">');
        courseElement.append('<h2>' + course.title + '</h2>');
        courseElement.append('<p>Instructors ID: ' + course.instructorsId + '</p>');
        courseElement.append('<p>Duration: ' + course.duration + '</p>');
        courseElement.append('<p>Rating: ' + course.rating.toFixed(2) + '</p>');
        courseElement.append('<p><a href="' + course.url + '">Link</a></p>');
        courseElement.append('<button id="' + course.id + '">Remove Course</button>');
        coursesContainer.append(courseElement);
    });
}

$('#homeBtn').on('click', function () {
    window.location.href = "../Pages/index.html";
});

document.addEventListener('click', function (event) {
    if (event.target.tagName.toLowerCase() === 'button' && event.target.id != "apply-rating-filter" && event.target.id != "apply-duration-filter") {
        const buttonId = event.target.id;
        removeCourse(user.id, buttonId);

    }
});

function removeCourse(userId, courseId) {
    const api = `${apiBaseUrl}/Courses/deleteByCourseFromUserList/${userId}?coursid=${courseId}`;
    $.ajax({
        url: api,
        type: 'DELETE',
        success: deleteSCBF,
        error: deleteECBF
    });
}

function deleteSCBF(result) {
    alert("Course removed");
    console.log(result);
    location.reload(); //reload page to clear deleted course
}

function deleteECBF(err) {
    alert("Error occurred - course may have been deleted.");
    console.log(err);
    //loadCourses(apiBaseUrl);
}

const applyDurationFilterButton = document.getElementById("apply-duration-filter");

applyDurationFilterButton.addEventListener("click", function () {
    const userId = getUserId();
    console.log("Duration filter applied!");
    filterByDuration(userId);
});

function filterByDuration(userId) {
    const fromDuration = parseFloat($('#duration-from').val());
    const toDuration = parseFloat($('#duration-to').val());
    if (fromDuration < toDuration) {
        $.ajax({
            url: `${apiBaseUrl}/Courses/searchByDurationForUser/${userId}?fromDuration=${fromDuration}&toDuration=${toDuration}`,
            type: 'GET',
            success: function (data) {
                renderFilteredCourses(data);
            },
            error: function () {
                $('#courses-container').empty();
                console.log("Error fetching courses by duration.");
            }
        });
    }
    else {
        alert("Invalid duration input!");
    }
}

function getUserId() {
    return id;
}

const applyRatingFilterButton = document.getElementById("apply-rating-filter");

applyRatingFilterButton.addEventListener("click", function () {
    console.log("Rating filter applied!");
    const userId = getUserId(); // Assuming you have a function to get the user ID
    filterByRating(userId);
});

function filterByRating(userId) {
    const fromRating = parseFloat($('#rating-from').val());
    const toRating = parseFloat($('#rating-to').val());
    console.log(toRating);
    if (fromRating < toRating) {
        $.ajax({
            url: `${apiBaseUrl}/Courses/searchByRatingForUser/${userId}?fromRating=${fromRating}&toRating=${toRating}`,
            type: 'GET',
            success: function (data) {
                renderFilteredCourses(data);
            },
            error: function () {
                $('#courses-container').empty();
                console.log("Error fetching courses by rating.");
            }
        });
    }
    else {
        alert("Invalid rating input!");
    }
}
