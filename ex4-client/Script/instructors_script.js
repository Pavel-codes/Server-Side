const api = `https://localhost:7076/api/Instructors`;
let instructorsData = [];

$(document).ready(function () {
    $.getJSON("../Data/Instructor.json", function (data) {
        instructorsData = data; // Update instructorsData directly
        renderInstructors(instructorsData); // Render instructors on document ready
    });

    $('#homeBtn').on('click', function () {
        window.location.href = "../Pages/index.html";
    });
});

// Render instructors
function renderInstructors(instructors) {
    var instructorsContainer = $('#instructors-container');
    instructors.forEach(function (instructor) {
        var instructorElement = $('<div>');
        instructorElement.append('<h2>' + instructor.name + '</h2>');
        instructorElement.append('<button class="show-courses-btn" data-instructor-id="' + instructor.id + '">Show Courses</button>'); // Add the button
        instructorsContainer.append(instructorElement);
    });
}

// Show courses button click event
$(document).on('click', '.show-courses-btn', function () {
    var instructorId = $(this).data('instructor-id');
    // Make AJAX call to fetch courses of this instructor
    var apiUrl = `URL_TO_FETCH_COURSES_BY_INSTRUCTOR?id=${instructorId}`;
    $.ajax({
        url: apiUrl,
        type: 'GET',
        success: function (data) {
            // Render the retrieved courses in a modal view or a new page
            renderInstructorCourses(data);
        },
        error: function () {
            console.log("Error fetching instructor courses.");
        }
    });
});

// Function to render instructor courses
function renderInstructorCourses(courses) {
    $('#modal-container').empty(); // Assuming you have a modal container
    courses.forEach(function (course) {
        // Render each course in the modal container
        $('#modal-container').append('<p>' + course.title + '</p>');
    });
    $('#modal').show(); // Show the modal
}
