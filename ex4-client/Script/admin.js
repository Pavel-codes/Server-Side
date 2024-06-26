
var coursesFromServer = [];
const udemy = "https://www.udemy.com";
//const instructorsAPI = `https://localhost:7076/api/Instructors`;
const apiBaseUrl = "https://proj.ruppin.ac.il/cgroup85/test2/tar1/api/Courses";
const displayCourses = $("#displayCourses");
const statusChangeUrl = 'https://proj.ruppin.ac.il/cgroup85/test2/tar1/api/Courses/ChangeActiveStatus/';
const uploadPath = "https://proj.ruppin.ac.il/cgroup85/test2/tar1/Images/";
const uploadApi = "https://proj.ruppin.ac.il/cgroup85/test2/tar1/api/Courses/uploadFiles";

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
    $('#hideDataTable').hide();
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

    function populateDataTable(courses) {
        $('#coursesDataTable').DataTable({
            data: courses,
            pageLength: 10,
            columns: [

                {
                    data: "title",
                    title: "Course",
                    render: function (data, type, row, meta) {
                        return '<input type="text" class="editedTitle" id="editedTitle' + row.id + '" value="' + data + '" data-course-id="' + row.id + '" required>';
                    }
                },
                { data: "id", title: "ID" },
                {
                    data: "url",
                    title: "Course Link",
                    render: function (data, type, row, meta) {
                        return '<p><a href="' + udemy + data + '">Link</a></p>'
                    }
                },
                {
                    data: "rating",
                    title: "Rating",
                    render: function (data, type, row, meta) {
                        return '<p>' + data.toFixed(2) + '</p>'
                    }
                },
                { data: "numberOfReviews", title: "Reviews"},
                { data: "instructorsId", title: "Instructors Id" },
                { data: "duration", title: "Duration" },
                {
                    data: "imageReference",
                    title: "Image Reference",
                    render: function (data, type, row, meta) {
                        return '<img src=' + data + '>';
                    }
                },
                { data: "lastUpdate", title: "Last Update" },

                {
                    data: "isActive",
                    title: "Active",
                    render: function (data, type, row, meta) {
                        return '<input type="checkbox" class="isActiveCheckbox" id="isActive' + meta.row + '" data-course-id="' + row.id + '"' + (data ? ' checked="checked"' : '') + ' />';
                    }
                }
            ],
            destroy: true // Allow reinitialization of the table
        });
    }

    $('#showDataTable').on('click', function () {
        $('#showDataTable').hide();
        $('#hideDataTable').show();
        $('#dataTableForm').show();
    });

    $('#hideDataTable').on('click', function () {
        $('#showDataTable').show();
        $('#hideDataTable').hide();
        $('#dataTableForm').hide();
    });
    function imageSent(data) {
        console.log(data);
    }

    $('#dataTableForm').on('change', '.editedTitle', function () {
        var input = $(this);
        var newValue = input.val();
        var courseId = input.data('course-id'); // Get the ID

        console.log(newValue);
        console.log(courseId);

        // AJAX PUT call for title change
        $.ajax({
            url: `${apiBaseUrl}/ChangeCourseTitle/${courseId}/${newValue}`,
            type: 'PUT',
            contentType: 'application/json',
            data: null,
            success: function (response) {
                console.log('Title updated successfully', response);
                $('#overlayMessage').fadeIn();
            },
            error: function (xhr, status, error) {
                console.error('Error updating title', error);
            }
        });
    });

    // Close the overlay message
    $('#closeOverlay').on('click', function () {
        $('#overlayMessage').fadeOut();
    });

    // Event delegation for isActive checkbox change
    $('#dataTableForm').on('change', '.isActiveCheckbox', function () {
        var checkbox = $(this);
        var isActive = checkbox.is(':checked');
        var courseId = checkbox.data('course-id'); // Get the course ID from the data attribute

        // AJAX PUT call for isActive change
        $.ajax({
            url: `${apiBaseUrl}/ChangeActiveStatus/${courseId}`, 
            type: 'PUT',
            contentType: 'application/json',
            data: JSON.stringify({
                isActive: isActive
            }),
            success: function (response) {
                console.log('isActive status updated successfully', response);
                $('#overlayMessage').fadeIn();
            },
            error: function (xhr, status, error) {
                console.error('Error updating isActive status', error);
            }
        });
    });
});

function imageSent(response) {
    console.log(response);
}

function error(data) {
    console.log(data);
}

$('#homeBtn').on('click', function () {
    window.location.href = "../Pages/index.html";
});


const courseDataList = document.getElementById("courseDataList");

var selectedCourse = {};

$("#courseNamesList").on('input', function () {
    const courseTitle = $(this).val(); // Get the selected value from the dropdown
    var uploadFileName;
    // clear the display area on change
    displayCourses.empty();
    //$('#displayCourses').children().slice(0, -1).remove();
    const editForm = $('<form id="editForm"></form>');
    displayCourses.append(editForm);

    selectedCourse = coursesFromServer.find(course => course.title === courseTitle);

    if (courseTitle != '' && selectedCourse.id) {
        const courseId = selectedCourse.id;
        const courseRating = selectedCourse.rating;
        const courseReviews = selectedCourse.numberOfReviews;
        const courseInstructorID = selectedCourse.instructorsId;
        const courseisActive = selectedCourse.isActive;

        displayCourses.append('<img src=' + selectedCourse.imageReference + '>');
        displayCourses.append('<p>Course ID: ' + courseId + '</p>');
        displayCourses.append('<p>Instructors ID: ' + courseInstructorID + '</p>');
        displayCourses.append('<p>Rating: ' + courseRating.toFixed(2) + '</p>');
        displayCourses.append('<p>Number Of Reviews: ' + courseReviews + '</p>');

        editForm.append('<label for="Title">Title: </label>');
        editForm.append('<input type="text" id="selectedTitle" required><br>');
        editForm.append('<label for="Duration">Duration: </label>');
        editForm.append('<input type="text" id="selectedDuration" required><br>');
        editForm.append('<label for="Course link">Course link: </label>');
        editForm.append('<input type="text" id="selectedUrl" required><br>');
        editForm.append('<label for="Image link">Image link: </label>');
        editForm.append('<input type="text" id="selectedImageUrl"><br>');
        editForm.append('<label for="file upload">Or upload image</label>');
        editForm.append('<input type="file" id="imageFile" name="files" accept=".jpg,.png"/>');
        editForm.append('<button type="submit" id="selectedSubmission">Submit changes</button>');
        displayCourses.append(editForm);

        editForm.on('change', '#imageFile', function () {
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
            var newImageUrl = $('#selectedImageUrl').val();
            if (newImageUrl == "" && uploadFileName) {
                newImageUrl = uploadPath + uploadFileName;
                
            }
            else if (!imagePattern.test($('#selectedImageUrl').val()) && $('#selectedImageUrl').val() !== "") {
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
            const api = `https://proj.ruppin.ac.il/cgroup85/test2/tar1/api/Courses/${courseId}`;
            ajaxCall("PUT", api, JSON.stringify(updatedCourseData), putSCBF, putECBF);
        });
    }
});

function updateCourseData() {
    removeAllOptionsFromDataList();
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
