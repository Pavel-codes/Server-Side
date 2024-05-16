$(document).ready(function () {
    // Load courses from JSON file

    $.getJSON("../Data/Course.json", function (data) {
        renderCourses(data);
    });


    const udemy = "https://www.udemy.com";
    var allData = [];
    // Render courses
    function renderCourses(courses) {
        var coursesContainer = $('#courses-container');
        courses.forEach(function (course) {
            allData.push(course);
            var courseElement = $('<div>');
            courseElement.append('<img src=' + course.image + '>');
            courseElement.append('<h2>' + course.title + '</h2>');
            courseElement.append('<p>Instructor: ' + course.instructor + '</p>');
            courseElement.append('<p>Duration: ' + course.duration + '</p>');
            courseElement.append('<p>Rating: ' + course.rating + '</p>');
            courseElement.append('<p><a href="' + udemy + course.url + '">Link</a></p>');
            courseElement.append('<button id="' + course.id + 'onclick="addCourse(button.id)">Add Course</button>');
            coursesContainer.append(courseElement);
        });
    }

    console.log(allData); //stores all data

    // Add course function
    function addCourse(buttonID) {
        // Assuming allData is an array of objects representing courses

        allData.forEach(courseData => {
            if (!(buttonID === courseData.id)) {
                $.ajax({
                    url: 'https://localhost:7076/api/Courses',
                    type: 'GET',
                    data: JSON.stringify(courseData),
                    contentType: "application/json",
                    success: function (response) {
                        if (response.status === "success") {
                            console.log("Course added successfully:", courseData.name || courseData.title); // Use name or title for logging
                        } else if (response.status === "duplicate" || response.message === "Course already exists") {
                            console.warn("Course already exists:", courseData.name || courseData.title);
                        } else {
                            console.error("Error adding course:", response.message);
                        }
                    },
                    error: function () {
                        console.error("Error adding course. Please try again later.");
                    }
                });
            }
        });
    }
});
