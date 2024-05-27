
let CoursesData = [];
$(document).ready(function () {
    $.getJSON("../Data/Course .json", function (data) { 
        CourseData.push(data); 
    });
    console.log(CourseData);
});


$("#coursesBtn").on("click", function () {

    alert("Handler INSERT for `click` called.");
    window.open("../Pages/createEditForm.html", "_blank");
});



function insertCourses(CourseData) {
    api = "https://localhost:7076/api/Courses";
    var coursestorDataToSend;
    CourseData[0].forEach(Course => {
        CourseDataToSend = {
            id: courseData.id,
            title: courseData.title,
            url: udemy + courseData.url,
            rating: courseData.rating,
            numberOfReviews: courseData.num_reviews,
            instructorsId: courseData.instructors_id,
            imageReference: courseData.image,
            duration: courseData.duration,
            lastUpdate: courseData.last_update_date
        };
        ajaxCall("POST", api, JSON.stringify(CourseDataToSend), postSCBF, postECBF);

    })

}

function postSCBF(result) {
    console.log(result);
}

function postECBF(err) {

    console.log(err);
}