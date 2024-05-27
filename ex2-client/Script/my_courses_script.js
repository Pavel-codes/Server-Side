var myCourses = [];

const apiBaseUrl = "https://localhost:7076/api/Courses";

$(document).ready(function () {
    loadCourses(apiBaseUrl);

    $('*').not('script, style').css({
        'padding': '5px'
    });
});


function loadCourses(api) {
    function loadCourses(api) {
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

    }


    function renderCourses(coursesList) {
        var coursesContainer = $('#courses-container');
        coursesContainer.empty();
        myCourses = coursesList;
        coursesList.forEach(function (course) {
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
        myCourses = myCourses.filter(courseData => courseData.id !== buttonId);
        const api = `${apiBaseUrl}/${buttonId}`;
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
}