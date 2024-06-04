var myCourses = [];
var userCourses = [];
var user = JSON.parse(localStorage.getItem('user'));
var id = user.id;

const apiBaseUrl = "https://localhost:7076/api/Users";

$(document).ready(function () {
    loadCourses(apiBaseUrl);

    $('*').not('script, style').css({
        'padding': '5px'
    });
});

$('#homeBtn').on('click', function () {
    window.location.href = "../Pages/index.html";
});

function loadCourses(api) {
<<<<<<< HEAD
    function loadCourses(api) { // same function twice? need to check
        $.ajax({
            url: api, //.https://localhost:7076/api/Courses
            type: 'GET',
            success: function (data) {
                renderCourses(data);
            },
            error: function () {
                alert("Error loading courses.");
            }
        });
=======
    $.ajax({
        url: `https://localhost:7076/api/Users/${id}`, //.https://localhost:7076/api/Courses
        type: 'GET',
        success: function (data) {
>>>>>>> main

            myCourses.push(data);
            setTimeout(renderCourses, 1000);
            
        },
        error: function () {
            alert("Error loading courses.");
        }
    });
}


function renderCourses() {
    var coursesContainer = $('#courses-container');
    userCourses = myCourses[0].myCourses;
    coursesContainer.empty();
    //myCourses = coursesList;
    userCourses.forEach(function (course) {
        var courseElement = $('<div>');
        courseElement.append('<img src="' + course.imageReference + '">');
        courseElement.append('<h2>' + course.title + '</h2>');
        courseElement.append('<p>Instructors ID: ' + course.instructorsId + '</p>');
        courseElement.append('<p>Duration: ' + course.duration + '</p>');
        courseElement.append('<p>Rating: ' + course.rating + '</p>');
        courseElement.append('<p><a href="' + course.url + '">Link</a></p>');
        courseElement.append('<button id="' + course.id + '">Remove Course</button>');
        coursesContainer.append(courseElement);
    });
}   


    document.addEventListener('click', function (event) {
        if (event.target.tagName.toLowerCase() === 'button' && event.target.id != "apply-rating-filter" && event.target.id != "apply-duration-filter") {
            const buttonId = event.target.id;
            removeCourse(buttonId);
            console.log("Button clicked with ID:", buttonId);
        }
    });

    const applyRatingFilterButton = document.getElementById("apply-rating-filter");

    applyRatingFilterButton.addEventListener("click", function () {

        console.log("Rating filter applied!");
        filterByRating();

    });

    const applyDurationFilterButton = document.getElementById("apply-duration-filter");

    applyDurationFilterButton.addEventListener("click", function () {
        console.log("Rating filter applied!"); // Example action
        filterByDuration();
    });


    function removeCourse(buttonId) {
        console.log(userCourses);
        const api = `${apiBaseUrl}/${buttonId}`;
        $.ajax({
            url: api,
            type: 'DELETE',
            contentType: 'application/json',
            data: JSON.stringify(userCourses),  // Convert user data to JSON string
            success: deleteSCBF,
            error: deleteECBF
        });
    }

    function deleteSCBF(result) {
        alert("Course removed");
        console.log(result);
        loadCourses(apiBaseUrl);
    }

    function deleteECBF(err) {
        alert("Error occurred - course may have been deleted.");
        console.log(err);
        loadCourses(apiBaseUrl);
    }



    function filterByDuration() {
        const fromDuration = parseFloat($('#duration-from').val());
        const toDuration = parseFloat($('#duration-to').val());

        $.ajax({
            url: `${apiBaseUrl}/search?fromDuration=${fromDuration}&toDuration=${toDuration}`,
            type: 'GET',
            success: function (data) {
                renderCourses(data);
            },
            error: function () {
                console.log("Error fetching courses by duration.");
            }
        });
    }


function filterByRating() {
    const fromRating = parseFloat($('#rating-from').val());
    const toRating = parseFloat($('#rating-to').val());

    $.ajax({
        url: `${apiBaseUrl}/searchByRouting/fromRating/${fromRating}/toRating/${toRating}`,
        type: 'GET',
        success: function (data) {
            renderCourses(data);
        },
        error: function () {
            console.log("Error fetching courses by rating.");
        }
    });
}