let CourseData = [];
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
        //    CourseData.push(data);
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
        console.log(option);
        courseDataList.appendChild(option);
    }
}

const courseSelected = document.getElementById("courseSelected")



$("#courseNamesList").on('change', function () {

    console.log(CourseData);
});



$("#coursesBtn").on("click", function () {

    alert("Handler INSERT for `click` called.");
    insertCourses(CourseData);
    window.open("../Pages/createEditForm.html", "_blank");
});
