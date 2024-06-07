const apiBaseUrl = "https://localhost:7076/api/Courses";
const udemy = "https://www.udemy.com";

$('#homeBtn').on('click', function () {
    window.location.href = "../Pages/index.html";
});

$("#createCourseForm").submit(function (event) {
    event.preventDefault();

    if ($('#id').val() < 1 || $('#id').val() > 2147483647) {
        alert("Course Id Not Valid");
        return;
    }

    //url validation atart with http or https and end with .com
    var urlPattern = /^(https):\/\/[^ "]+(.com)$/;
    if (!urlPattern.test($('#url').val())) {
        alert("Url Not Valid , Must Use This Structure https://example.com");
        return;
    }

 

    if ($('#instructorsId').val() < 1 && $('#instructorsId').val() > 2147483647) {
        alert("Instructors Id Not Valid");
        return;
    }
  
    var imagePattern = /^(https):\/\/[^ "]+(.jpg|.png)$/;
    if (!imagePattern.test($('#image').val())) {
        alert("Image Reference Not Valid, Must Use This Structure https://example.jpg/png");
        return;
    }
  
    if (isNaN($('#duration').val())) {
        alert("Duration Not Valid, Must Be A Number");
        return;
    }

    var newCourse = {
        id: $("#id").val(),
        title: $("#title").val(),
        url: $("#url").val(),
        rating: 0.0,
        numberOfReviews: 0,
        instructorsId: $("#instructorsId").val(),
        imageReference: $("#image").val(),
        duration: $("#duration").val(),
        lastUpdate: getCurrentDate() // change the date format - fixed
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


function getCurrentDate() {
    const date = new Date();
    const day = String(date.getDate()).padStart(2, '0');
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const year = date.getFullYear();

    return `${day}/${month}/${year}`;
}