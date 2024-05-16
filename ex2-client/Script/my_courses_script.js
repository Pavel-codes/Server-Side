$(document).ready(function () {
    // Load courses from server
    $.ajax({
        url: 'get_my_courses.php',
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
            var courseElement = $('<div>');
            courseElement.append('<h2>' + course.title + '</h2>');
            courseElement.append('<p>Instructor: ' + course.instructor + '</p>');
            courseElement.append('<p>Duration: ' + course.duration + '</p>');
            courseElement.append('<p>Rating: ' + course.rating + '</p>');
            courseElement.append('<p><a href="' + course.url + '">Link</a></p>');
            coursesContainer.append(courseElement);
        });
    }
});
