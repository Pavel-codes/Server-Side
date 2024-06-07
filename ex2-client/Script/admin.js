var CourseData = [];
const udemy = "https://www.udemy.com";
const instructorsAPI = `https://localhost:7076/api/Instructors`;
var instructorsData = [];


$('document').ready(function () {
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
        window.location.href = "index.html";
    }

    $.getJSON("../Data/Instructor .json", function (data) {
        instructorsData.push(data);
    });
});

$('#homeBtn').on('click', function () {
    window.location.href = "../Pages/index.html";
});

$('#loadCoursesBtn').on('click', function () {
    $.getJSON("../Data/Course.json", function (Data) {
        CourseData.push(Data);
        insertCourses(CourseData[0]);
    });
    insertInstructors();
    setTimeout(addCoursesToDataList, 1000);
    $('#loadCoursesBtn').prop('disabled', true);
});


function insertInstructors() {
    const instructorsAPI = `https://localhost:7076/api/Instructors`;
    var instructorDataToSend;
    console.log(instructorsData);
    instructorsData[0].forEach(instructor => {
        instructorDataToSend = {
            id: instructor.id,
            title: instructor.title,
            name: instructor.display_name,
            image: instructor.image_100x100,
            jobTitle: instructor.job_title
        };
        ajaxCall("POST", instructorsAPI, JSON.stringify(instructorDataToSend), postInstructorsSCBF, postInstructorsECBF);
    })
}


function insertCourses(CourseData) {
    api = "https://localhost:7076/api/Courses";
    var courseDataToSend;
    CourseData.forEach(course => {
        courseDataToSend = {
            id: course.id,
            title: course.title,
            url: udemy + course.url,
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

function postInstructorsSCBF(result) {
    console.log(result);
}

function postInstructorsECBF(err) {
    console.log(err);
}



function postSCBF(result) {
    console.log(result);
}

function postECBF(err) {
    console.log(err);
}

function addCoursesToDataList() {
    api = "https://localhost:7076/api/Courses";
    $.ajax({
        url: api,
        type: 'GET',
        success: function (data) {
            addToDataList(data);
        },
        error: function () {
            alert("Error loading courses.");
        }
    });
}

const courseDataList = document.getElementById("courseDataList");

function addToDataList(data) {
    //courseDataList.innerHTML = ""; // Clear existing options
    for (const course of data) {
        const option = document.createElement('option');
        option.value = course.title; // Set the value attribute
        option.textContent = course.title; // Set the displayed text
        courseDataList.appendChild(option);
    }
}


const displayCourses = $("#displayCourses");

$("#courseNamesList").on('input', function () {
    const courseTitle = $(this).val(); // Get the selected value from the dropdown
    // clear the display area on change
    displayCourses.empty();
    //create new form 
    const editForm = $('<form id="editForm"></form>');
    displayCourses.append(editForm);

    //console.log(CourseData);
    if (CourseData && CourseData.length > 0) {
        CourseData[0].forEach(function (course) {
            const courseId = course.id;
            const courseRating = course.rating;
            const courseReviews = course.num_reviews;
            const courseInstructorID = course.instructors_id;

            if (course.title == courseTitle) {
                console.log("inside if");
                displayCourses.append('<img src=' + course.image + '>');
                displayCourses.append('<p>Course ID: ' + course.id + '</p>');
                displayCourses.append('<p>Instructors ID: ' + course.instructors_id + '</p>'); //
                displayCourses.append('<p>Rating: ' + course.rating + '</p>'); //
                displayCourses.append('<p>Number Of Reviews: ' + course.num_reviews + '</p>'); //

                editForm.append('<label for="title">Title: </label>');
                editForm.append('<input type="text" id="selectedTitle" required><br>');
                editForm.append('<label for="duration">Duration: </label>');
                editForm.append('<input type="text" id="selectedDuration" required><br>');
                editForm.append('<label for="title">Course link: </label>');
                editForm.append('<input type="text" id="selectedUrl" required><br>');
                editForm.append('<label for="title">Image link: </label>');
                editForm.append('<input type="text" id="selectedImageUrl"><br>');
                editForm.append('<button type="submit" id="selectedSubmission">Submit changes</button>');
                displayCourses.append(editForm);

                // Use the submit event of the form
                editForm.on("submit", function (event) {
                    event.preventDefault(); // Prevent the form from submitting the default way

                    var urlPattern = /^(https):\/\/[^ "]+(.com)$/;
                    var imagePattern = /^(https):\/\/[^ "]+(.jpg|.png)$/;
                    var durationPattern = /^\d+(\.\d+)?\s+\w+$/;


                    const newTitle = $('#selectedTitle').val();

                    const newDuration = $('#selectedDuration').val();
                    if (!durationPattern.test($('#selectedDuration').val())) {
                        alert("Duration is not valid");
                        return;
                    }
                    const newUrl = $('#selectedUrl').val();
                    if (!urlPattern.test($('#selectedUrl').val())) {
                        alert("Url Not Valid , Must Use This Structure https://example.com");
                        return;
                    }
                    const newImageUrl = $('#selectedImageUrl').val();
                    if (newImageUrl == "") {
                        console.log(newImageUrl);
                    }
                    else if (!imagePattern.test($('#selectedImageUrl').val())) {
                        alert("Image Reference Not Valid, Must Use This Structure https://example.jpg/png");
                        return;
                    }
                    const newDate = getCurrentDate();
                    const updatedCourseData = {
                        id: courseId,
                        title: newTitle,
                        url: newUrl,
                        rating: courseRating,
                        numberOfReviews: courseReviews,
                        instructorsId: courseInstructorID,
                        imageReference: newImageUrl,
                        duration: newDuration,
                        lastUpdate: newDate
                    };
                    console.log(updatedCourseData);
                    let id = courseId;
                    const api = `https://localhost:7076/api/Courses/${id}`;
                    console.log(api);
                    console.log(courseId);
                    ajaxCall("PUT", api, JSON.stringify(updatedCourseData), getSCBF, getECBF);
                });
            }
        });
    } else {
        console.error("CourseData is empty or not an array");
    }
});


function updateCourseData() {
    const updatedCourseData = [];
    let api = `https://localhost:7076/api/Courses/`;
    $.ajax({
        url: api,
        type: 'GET',
        success: function (data) {
            updatedCourseData.push(data);
            addToDataList(data);
            CourseData = updatedCourseData;
            console.log(CourseData);
        },
        error: function () {
            alert("Error loading courses.");
        }
    });
}

function getSCBF(result) {
    console.log("changed successfully");
    alert("Course changed successfully");
    console.log(result);
}

function getECBF(err) {
    updateCourseData();

    alert("Unable to change");
    console.log(err);
}

$("#coursesBtn").on("click", function () {

    window.location.href = "createEditForm.html";
});


function getCurrentDate() {
    const date = new Date();
    const day = String(date.getDate()).padStart(2, '0');
    const month = String(date.getMonth() + 1).padStart(2, '0'); 
    const year = date.getFullYear();

    return `${day}/${month}/${year}`;
}