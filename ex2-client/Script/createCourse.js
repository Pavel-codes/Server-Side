const apiBaseUrl = "https://localhost:7076/api/Courses";

$('#homeBtn').on('click', function () {
    window.location.href = "../Pages/index.html";
});


$("#createCourseForm").submit(function (event) {
    event.preventDefault();
    var newCourse = {
        id: $("#id").val(),
        title: $("#title").val(),
        url: $("#url").val(),
        rating: 0.0,
        numberOfReviews: 0,
        instructorsId: $("#instructorsId").val(),
        imageReference: $("#image").val(),
        duration: $("#duration").val(),
        lastUpdate: new Date().toISOString() // change the date format
    }
    ajaxCall("POST", `${apiBaseUrl}/NewCourse`, JSON.stringify(newCourse), getSCBF, getECBF);
  
});


function getSCBF(result) {
    console.log(result);
}

function getECBF(err) {
    if (err.status == 200)
        alert("Course Seccesfully Added");
    if (err.status == 404)
        alert("Course already exists/Instructor not Exist");
}