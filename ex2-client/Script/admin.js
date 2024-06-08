var CourseData = [];
var coursesFromServer = [];
const udemy = "https://www.udemy.com";
const instructorsAPI = `https://localhost:7076/api/Instructors`;
var instructorsData = [];



$('document').ready(function () {
    const struser = localStorage.getItem('user');
    let user = undefined;
    const areLoaded = sessionStorage.getItem('coursesLoaded');
    if (struser) {
        user = JSON.parse(localStorage.getItem('user'));
    }

    if (areLoaded) {
        var coursesLoaded = JSON.parse(sessionStorage.getItem('coursesLoaded'));
        console.log(coursesLoaded);
    }

    if (user && user.isAdmin) {
        if (coursesLoaded) {
            $('#loadCoursesBtn').hide();
            addCoursesToDataList();
            return;
        }
        $('#loadCoursesBtn').show();
    }
    else {
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
        sessionStorage.setItem('coursesLoaded', true);
        $('#loadCoursesBtn').hide();
        
    });
    insertInstructors();
    setTimeout(addCoursesToDataList, 1000);
});


function insertInstructors() {
    const instructorsAPI = `https://localhost:7076/api/Instructors`;
    var instructorDataToSend;
    instructorsData[0].forEach(instructor => {
        instructorDataToSend = {
            id: instructor.id,
            title: instructor.title,
            name: instructor.display_name,
            image: instructor.image_100x100,
            jobTitle: instructor.job_title
        };
        ajaxCall("POST", instructorsAPI, JSON.stringify(instructorDataToSend), false, postInstructorsSCBF, postInstructorsECBF); //async
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
        ajaxCall("POST", api, JSON.stringify(courseDataToSend), false, postCoursesSCBF, postCoursesECBF);
    });
}

function postInstructorsSCBF(result) {
    console.log(result);
}

function postInstructorsECBF(err) {
    console.log(err);
}


function postCoursesSCBF(result) {
    console.log(result);
}

function postCoursesECBF(err) {
    console.log(err);
}

function addCoursesToDataList() {
    displayCourses.empty();
    api = "https://localhost:7076/api/Courses";
    coursesFromServer.length = 0;
    $.ajax({
        url: api,
        type: 'GET',
        async: false,
        success: function (data) {
            coursesFromServer.push(data);
            addToDataList(data);
        },
        error: function () {
            alert("Error loading courses.");
        }
    });
}

const courseDataList = document.getElementById("courseDataList");

function addToDataList(data) {
    //displayCourses.empty();
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
    const editForm = $('<form id="editForm"></form>');
    displayCourses.append(editForm);
    if (coursesFromServer && coursesFromServer.length > 0) {
        coursesFromServer[0].forEach(function (course) {
            const courseId = course.id;
            const courseRating = course.rating;
            const courseReviews = course.numberOfReviews;
            const courseInstructorID = course.instructorsId;

            if (course.title == courseTitle) {
                displayCourses.append('<img src=' + course.imageReference + '>');
                displayCourses.append('<p>Course ID: ' + course.id + '</p>');
                displayCourses.append('<p>Instructors ID: ' + course.instructorsId + '</p>'); //
                displayCourses.append('<p>Rating: ' + course.rating + '</p>'); //
                displayCourses.append('<p>Number Of Reviews: ' + course.numberOfReviews + '</p>'); //

                editForm.append('<label for="Title">Title: </label>');
                editForm.append('<input type="text" id="selectedTitle" required><br>');
                editForm.append('<label for="Duration">Duration: </label>');
                editForm.append('<input type="text" id="selectedDuration" required><br>');
                editForm.append('<label for="Course link">Course link: </label>');
                editForm.append('<input type="text" id="selectedUrl" required><br>');
                editForm.append('<label for="Image link">Image link: </label>');
                editForm.append('<input type="text" id="selectedImageUrl"><br>');
                editForm.append('<button type="submit" id="selectedSubmission">Submit changes</button>');
                displayCourses.append(editForm);

                // Use the submit event of the form
                editForm.on("submit", function (event) {
                    event.preventDefault();

                    var urlPattern = /^(https):\/\/[^ "]+(.com)$/;
                    var imagePattern = /^(https):\/\/[^ "]+(.jpg|.png)$/;
                    var durationPattern = /^\d+(\.\d+)?\s+\w+$/;


                    const newTitle = $('#selectedTitle').val();

                    const newDuration = $('#selectedDuration').val();
                    if (!durationPattern.test($('#selectedDuration').val())) {
                        alert("Duration is not valid Must Use This Structure: [number] [text]");
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
                    ajaxCall("PUT", api, JSON.stringify(updatedCourseData), putSCBF, putECBF);
                });
            }
        });
    } else {
        console.error("CourseData is empty or not an array");
    }
});



function updateCourseData() {
    removeAllOptionsFromDataList(); // trying
    coursesFromServer.length = 0;
    let api = `https://localhost:7076/api/Courses/`;
    $.ajax({
        url: api,
        type: 'GET',
        async: false,
        success: function (data) {
            coursesFromServer.push(data);
            addToDataList(data);
        },
        error: function () {
            alert("Error loading courses.");
        }
    });
    //location.reload();
}

function putSCBF(result) {
    console.log("changed successfully!");
    alert("Course changed successfully!");
    displayCourses.empty();
    updateCourseData();
    console.log(result);
}

function putECBF(err) {
    console.log(err);
    alert("Unable to update.");
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

function removeAllOptionsFromDataList() {
    var dataList = document.getElementById('courseDataList');
    var options = dataList.getElementsByTagName('option');
    while (options.length > 0) {
        dataList.removeChild(options[0]);
    }
}