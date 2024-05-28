const apiBaseUrl = "https://localhost:7076/api/Courses";


$("#createCourseForm").submit(function () {
    alert("Handler Submit for `click` called.")
    let newCourse = {
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
    return false;
});


function getSCBF(result) {
    console.log(result);
}

function getECBF(err) {
    console.log(err);
}