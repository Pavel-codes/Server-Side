﻿var CourseData = [];
const udemy = "https://www.udemy.com"; 

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

});

$('#loadCoursesBtn').on('click', function () { // need to add control so the button cannot be pressed multiple times
    alert("Handler INSERT for `click` called.");
    $.getJSON("../Data/Course.json", function (Data) {
        CourseData.push(Data);
        insertCourses(CourseData[0]);
        addCoursesToDataList();
    });
    $('#loadCoursesBtn').prop('disabled', true);
});


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
    courseDataList.innerHTML = ""; // Clear existing options
    for (const course of data) {
        const option = document.createElement('option');
        option.value = course.title; // Set the value attribute
        option.textContent = course.title; // Set the displayed text
        courseDataList.appendChild(option);
    }
}


const displayCourses = $("#displayCourses");

$("#courseNamesList").on('change', function () {
    //addCoursesToDataList();
    const courseTitle = $(this).val(); // Get the selected value from the dropdown
    displayCourses.innerHTML = "";
    const editForm = $("#editForm");
    console.log(CourseData);
    if (CourseData && CourseData.length > 0) {
        CourseData[0].forEach(function (course) {
            const courseId = course.id;
            const courseRating = course.rating;
            const courseReviews = course.num_reviews;
            const courseInstructorID = course.instructors_id;
            const courseImgRef = course.image;

            console.log(courseId);

            if (course.title == courseTitle) {
                displayCourses.append('<img src=' + course.image + '>');
                
                editForm.append('<label for="title">Title: </label>');
                editForm.append('<input type="text" id="selectedTitle" required><br>');
               
                displayCourses.append('<p>Course ID: ' + course.id + '</p>');
                displayCourses.append('<p>Instructors ID: ' + course.instructors_id + '</p>'); //
                editForm.append('<label for="duration">Duration: </label>');
                editForm.append('<input type="text" id="selectedDuration" required><br>');
                displayCourses.append('<p>Rating: ' + course.rating + '</p>'); //
                displayCourses.append('<p>Number Of Reviews: ' + course.num_reviews + '</p>'); //
                editForm.append('<label for="title">Url: </label>');
                editForm.append('<input type="text" id="selectedUrl" required><br>');
                editForm.append('<button id="selectedSubmission">Submit changes</button>'); 
                displayCourses.append(editForm);


                $("#selectedSubmission").on("click", function (course) {
                    const newTitle = $('#selectedTitle').val();
                    const newDuration = $('#selectedDuration').val();
                    const newUrl = $('#selectedUrl').val();
                    const newDate = new Date().toISOString();
                    const updatedCourseData = {
                        id: courseId,
                        title: newTitle,
                        url: newUrl,
                        rating: courseRating,
                        numberOfReviews: courseReviews,
                        instructorsId: courseInstructorID,
                        imageReference: courseImgRef,
                        duration: newDuration,
                        lastUpdate: newDate
                    };
                    console.log(updatedCourseData);
                    //updateCourse(courseId, updatedCourseData);
                    let api = `https://localhost:7076/api/Courses/${courseId}`;
                    ajaxCall("PUT", `${api}/`, JSON.stringify(updatedCourseData), getSCBF, getECBF);
                });
            }
        });
    } else {
        console.error("CourseData is empty or not an array"); // Handle empty or invalid CourseData
    }
});

function getSCBF(result) {
    console.log("changed successfully");
    alert("Course changed successfully");
    console.log(result);
}

function getECBF(err) {
    alert("Unable to change");
    console.log(err);
}

//function updateCourse(courseId, updatedData) {

//    $.ajax({
//        url: `https://localhost:7076/api/Courses/${courseId}`,
//        type: 'PUT',
//        dataType: 'json', // Expect JSON response from server
//        data: JSON.stringify(updatedData), // Convert updated data to JSON string
//        success: function (response) {
//            console.log("Course updated successfully:", response);
//        },
//        error: function (error) {
//            console.error("Error updating course:", error);
//        }
//    });
//}


$("#coursesBtn").on("click", function () {

    alert("Handler INSERT for `click` called.");
    insertCourses(CourseData);
    window.open("../Pages/createEditForm.html", "_blank");
});
