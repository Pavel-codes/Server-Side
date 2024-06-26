
const apiBaseUrl = "https://localhost:7283/api/Courses";
const udemy = "https://www.udemy.com";
const uploadPath = "https://localhost:7283/Images/";
const uploadApi = "https://localhost:7283/api/Courses/uploadFiles";

$('#homeBtn').on('click', function () {
    window.location.href = "../Pages/index.html";
});
$('#returnToPanel').on('click', function () {
    window.location.href = "../Pages/admin.html";
});

var uploadFileName;

const createCourseForm = $("#createCourseForm");
$("#createCourseForm").submit(function (event) {
    event.preventDefault();
    
    var newImageUrl;
    if ($('#id').val() < 1 || $('#id').val() > 2147483647) {
        alert("Course Id Not Valid");
        return;
    }

    var urlPattern = /^(https):\/\/www\.[^\s"]+\.[^\s"]+$/;
    if (!urlPattern.test($('#url').val())) {
        alert("Url Not Valid , Must Use This Structure https://www.example.com");
        return;
    }

    if ($('#instructorsId').val() < 1 && $('#instructorsId').val() > 2147483647) {
        alert("Instructors Id Not Valid");
        return;
    }

    var imagePattern = /^(https):\/\/www\.[^\s"]+(\.jpg|\.png)$/;

    if (!imagePattern.test($('#image').val()) && $('#image').val().trim() !== "") {
        alert("Image Reference Not Valid, Must Use This Structure https://www.example.jpg/png");
        return;
    }
    else if ($('#image').val() == "" && uploadFileName) {
        newImageUrl = uploadPath + uploadFileName;
    }
    else {
        newImageUrl = $("#image").val();
    }

    var durationPattern = /^\s*\d+(\.\d+)?\s*$/;
    if (!durationPattern.test($('#duration').val())) {
        alert("Duration is not valid must be a number!");
        return;
    }

    var newCourse = {
        id: 0,
        title: $("#title").val(),
        url: $("#url").val(),
        rating: 0.0,
        numberOfReviews: 0,
        instructorsId: $("#instructorsId").val(),
        imageReference: newImageUrl,
        duration: $("#duration").val() + " total hours",
        lastUpdate: getCurrentDate(),
        isAdmin : true
    };
    console.log(newCourse);
    ajaxCall("POST", `${apiBaseUrl}/NewCourse`, JSON.stringify(newCourse), postSCBF, postECBF);

});

createCourseForm.on('change', '#imageFile', function () {
    var fileInput = $(this)[0];
    uploadFileName = fileInput.files[0].name; // Get the selected file
    var data = new FormData();
    var files = $("#imageFile").get(0).files;

    // Add the uploaded file to the form data collection  
    if (files.length > 0) {
        for (f = 0; f < files.length; f++) {
            data.append("files", files[f]);
        }
    }

    // Ajax upload  
    $.ajax({
        type: "POST",
        url: uploadApi,
        contentType: false,
        processData: false,
        data: data,
        success: imageSent,
        error: error
    });

    return false;

});

function imageSent(response) {
    console.log(response);
}

function error(data) {
    console.log(data);
}


function postSCBF(result) {
    alert("Course Successfully Added");
    clearFields();
    console.log(result);
}

function clearFields() {
    $('#title').val('');
    $('#url').val('');
    $('#instructorsId').val('');
    $('#image').val('');
    $('#duration').val('');
}

function postECBF(err) {
    console.log(err);
    alert("Course already exists/Instructor not Exist");
}


function getCurrentDate() {
    const date = new Date();
    const day = String(date.getDate()).padStart(2, '0');
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const year = date.getFullYear();

    return `${day}/${month}/${year}`;
}
