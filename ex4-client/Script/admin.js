
var coursesFromServer = [];
const udemy = "https://www.udemy.com";
//const instructorsAPI = `https://localhost:7076/api/Instructors`;
const apiBaseUrl = "https://localhost:7283/api/Courses";
const displayCourses = $("#displayCourses");

$('document').ready(function () {
    const struser = localStorage.getItem('user');
    let user = undefined;
    //const areLoaded = sessionStorage.getItem('coursesLoaded');
    if (struser) {
        user = JSON.parse(localStorage.getItem('user'));
    }

    if (user && user.isAdmin) {
        addCoursesToDataList();
    }
    else {
        alert("You are not authorized to access this page.");
        window.location.href = "index.html";
    }

    $('#dataTableForm').hide();
    function addCoursesToDataList() {
        ajaxCall('GET', apiBaseUrl, true, getCoursesSCBF, getCoursesECBF);
    }

    function getCoursesSCBF(response) {
        console.log("Received courses!");
        console.log(response);
        coursesFromServer = response;
        addToDataList(response);
        populateDataTable(response);
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
    //////////////////////////////////// testing /////////////////////////////////
    function populateDataTable(courses) {
        $('#coursesDataTable').DataTable({
            data: courses,
            pageLength: 10,
            columns: [
                {
                    data: null,
                    title: "Actions",
                    render: function (data, type, row, meta) {
                        return '<button class="editBtn">Edit</button>' +
                            '<button class="deleteBtn">Delete</button>' +
                            '<button class="viewBtn">View</button>';
                    }
                },

                
                { data: "title", title: "Course" },
                {data: "id", title: "ID" },
                { data: "url", title: "Course Link" },
                { data: "rating", title: "Rating" },
                { data: "numberOfReviews", title: "Reviews" },
                { data: "instructorsId", title: "Instructors Id" },
                { data: "duration", title: "Duration" },
                {data :"imageReference", title: "Image Reference"},
                { data: "lastUpdate", title: "Last Update" },

                {
                    data: "isActive",
                    title: "Active",
                    render: function (data, type, row, meta) {
                        return '<input type="checkbox"' + (data ? ' checked="checked"' : '') + ' />';
                    }
                }
            ],
            destroy: true // Allow reinitialization of the table
        });
    }

    //function when i clicked delete button Delete the course from the display and from the dtatabase
    $('#coursesDataTable').on('click', '.deleteBtn', function () {
        if (!confirm("Are you sure you want to delete this course?")) return;
        var table = $('#coursesDataTable').DataTable();
        var data = table.row($(this).parents('tr')).data();
        var id = data.id;
        var api = `${apiBaseUrl}/DeleteCourse/${id}`;
        ajaxCall("DELETE", api, null, deleteSCBF, deleteECBF);
    });

    function deleteSCBF(result) {
        console.log("Deleted successfully!");
        alert("Course deleted successfully!");
        populateDataTable(result)
        
    }

    function deleteECBF(err) {
        console.log(err);
        alert("Unable to delete.");
    }

    $('#showDataTable').on('click', function(){
        $('#dataTableForm').show();
    });

    $('#buttonUpload').on('click', function () {
        var data = new FormData();
        var files = $("#files").get(0).files;

        // Add the uploaded file to the form data collection  
        if (files.length > 0) {
            for (f = 0; f < files.length; f++) {
                data.append("files", files[f]);
            }
        }

        api = "https://localhost:7283/api/Courses/uploadFiles";
        

        // Ajax upload  
        $.ajax({
            type: "POST",
            url: api,
            contentType: false,
            processData: false,
            data: data,
            success: showImages,
            error: error
        });

        return false;
    });
    //checking
    

    function showImages(data) {
        console.log(data);
        var imageFolder = "https://localhost:7283/Images/";
        var imgStr = "";

        if (Array.isArray(data)) {
            for (var i = 0; i < data.length; i++) {
                src = imageFolder + data[i];
                imgStr += `<img src='${src}'/>`;
            }
        } else { // just in case you have an api returning a single string
            src = imageFolder + data;
            imgStr = `<img src='${src}'/>`;
        }

        document.getElementById("ph").innerHTML = imgStr;
    }

});


function error(data) {
    console.log(data);
}

$('#homeBtn').on('click', function () {
    window.location.href = "../Pages/index.html";
});



const courseDataList = document.getElementById("courseDataList");

var selectedCourse = {};


//change implememtation to get from server - basic idea -->> get course ID from course title, change all other data according to fields and send back
$("#courseNamesList").on('input', function () {
    const courseTitle = $(this).val(); // Get the selected value from the dropdown
    // clear the display area on change
    displayCourses.empty();
    const editForm = $('<form id="editForm"></form>');
    displayCourses.append(editForm);

    selectedCourse = coursesFromServer.find(course => course.title === courseTitle);

    if (courseTitle != '') {
        const courseId = selectedCourse.id;
        const courseRating = selectedCourse.rating;
        const courseReviews = selectedCourse.numberOfReviews;
        const courseInstructorID = selectedCourse.instructorsId;
        const courseisActive = selectedCourse.isActive;

        displayCourses.append('<img src=' + selectedCourse.imageReference + '>');
        displayCourses.append('<p>Course ID: ' + courseId + '</p>');
        displayCourses.append('<p>Instructors ID: ' + courseInstructorID + '</p>');
        displayCourses.append('<p>Rating: ' + courseRating + '</p>');
        displayCourses.append('<p>Number Of Reviews: ' + courseReviews + '</p>');

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

            var urlPattern = /^(https):\/\/www\.[^\s"]+\.[^\s"]+$/;
            var imagePattern = /^(https):\/\/www\.[^\s"]+(\.jpg|\.png)$/;
            var durationPattern = /^\s*\d+(\.\d+)?\s*$/;

            const newTitle = $('#selectedTitle').val();

            const newDuration = $('#selectedDuration').val();
            if (!durationPattern.test($('#selectedDuration').val())) {
                alert("Duration is not valid must be a number!");
                return;
            }
            const newUrl = $('#selectedUrl').val();
            if (!urlPattern.test($('#selectedUrl').val())) {
                alert("Url Not Valid , Must Use This Structure https://www.example.com");
                return;
            }
            const newImageUrl = $('#selectedImageUrl').val();
            if (newImageUrl == "") {
            }
            else if (!imagePattern.test($('#selectedImageUrl').val())) {
                alert("Image Reference Not Valid, Must Use This Structure https://www.example.jpg/png");
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
                lastUpdate: newDate,
                isActive: courseisActive
            };
            //let id = courseId;
            const api = `https://localhost:7283/api/Courses/${courseId}`;
            ajaxCall("PUT", api, JSON.stringify(updatedCourseData), putSCBF, putECBF);
        });
    }
});

function updateCourseData() {
    removeAllOptionsFromDataList(); // trying
    coursesFromServer.length = 0;
    ajaxCall('GET', apiBaseUrl, true, getUpdatedCoursesSCBF, getUpdatedCoursesECBF);
}

function getUpdatedCoursesSCBF(response) {
    console.log("Updating list");
    coursesFromServer = response;
    $('#courseNamesList').val('');
    addToDataList(response);
}

function addToDataList(data) {
    for (const course of data) {
        const option = document.createElement('option');
        option.value = course.title; // Set the value attribute
        option.textContent = course.title; // Set the displayed text
        courseDataList.appendChild(option);
    }
}

function getUpdatedCoursesECBF(err) {
    console.log(err);
}

function putSCBF(result) {
    console.log("changed successfully!");
    alert("Course changed successfully!");
    displayCourses.empty();
    updateCourseData();
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
