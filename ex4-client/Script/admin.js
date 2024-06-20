var coursesFromServer = [];
const udemy = "https://www.udemy.com";
const instructorsAPI = `https://localhost:7076/api/Instructors`;
const apiBaseUrl = "https://localhost:7283/api/Courses";
const displayCourses = $("#displayCourses");

$('document').ready(function () {
    const struser = localStorage.getItem('user');
    let user = undefined;
    //const areLoaded = sessionStorage.getItem('coursesLoaded');
    if (struser) {
        user = JSON.parse(localStorage.getItem('user'));
    }

    //if (areLoaded) {
    //    var coursesLoaded = JSON.parse(sessionStorage.getItem('coursesLoaded'));
    //    console.log(coursesLoaded);
    //}

    if (user && user.isAdmin) {
        addCoursesToDataList();
    }
    else {
        alert("You are not authorized to access this page.");
        window.location.href = "index.html";
    }


    function addCoursesToDataList() {
        ajaxCall('GET', apiBaseUrl, true, getCoursesSCBF, getCoursesECBF);
    }

    function getCoursesSCBF(response) {
        console.log(response);
        addToDataList(response);
    }

    function getCoursesECBF(err) {
        console.log(err);
        alert("Failed to load courses!");
    }

    function addToDataList(data) {
        for (const course of data) {
            const option = document.createElement('option');
            option.value = course.title; // Set the value attribute
            option.textContent = course.title; // Set the displayed text
            courseDataList.appendChild(option);
        }
    }

});

$('#homeBtn').on('click', function () {
    window.location.href = "../Pages/index.html";
});

function addCoursesToDataList() {
    displayCourses.empty();
    api = "https://localhost:7076/api/Courses";
    coursesFromServer.length = 0;
    $.ajax({
        url: api,
        type: 'GET',
        async: false,
        success: function (data) {
            addToDataList(data);
        },
        error: function () {
            alert("Error loading courses.");
        }
    });
}

const courseDataList = document.getElementById("courseDataList");




//change implememtation to get from server - basic idea -->> get course ID from course title, change all other data according to fields and send back
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
                    var durationPattern = /^\s*\d+(\.\d+)?\s*$/;


                    const newTitle = $('#selectedTitle').val();

                    const newDuration = $('#selectedDuration').val();
                    if (!durationPattern.test($('#selectedDuration').val())) {
                        alert("Duration is not valid must be a number!");
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
    ajaxCall('GET', apiBaseUrl, true, getUpdatedCoursesSCBF, getUpdatedCoursesECBF);
}

function getUpdatedCoursesSCBF(response) {
    console.log(response);
    addToDataList(response);
}

function getUpdatedCoursesECBF(err) {
    console.log(err);
    //alert("Failed to load courses!");
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