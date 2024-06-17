const apiBaseUrl = "https://localhost:7283/api/Instructors";
let instructorsData = []; 
var modal = $('#coursesModal');
var span = $('.close');

$(document).ready(function () {


    function getInstructorsFromDB() {
        ajaxCall('GET', apiBaseUrl, true, getInstructorsSCBF, getInstructorsECBF);
    }

    function renderInstructors(instructors) {
        var instructorsContainer = $('#instructors-container');
        instructors.forEach(function (instructor) {
            var instructorElement = $('<div>');
            instructorElement.append('<h2>' + instructor.name + '</h2>');
            instructorElement.append('<img src=' + instructor.image + '>');
            instructorElement.append('<p>Title: ' + instructor.title + '</p>');
            instructorElement.append('<p>Job: ' + instructor.jobTitle + '</p>');
            instructorElement.append('<button id="' + instructor.id + '">View more</button>');

            instructorsContainer.append(instructorElement);

            let instructorBtn = document.getElementById(instructor.id);
            console.log(instructorBtn);
            $(instructorBtn).on('click', function () {
                modal.css('display', 'block');
                addCoursesToModal(instructorBtn.id);
            });
        });
    }

    function getInstructorsSCBF(result) {
        renderInstructors(result)
        console.log("Received instructors");

    }

    function getInstructorsECBF(err) {
        console.log(err);
    }

    getInstructorsFromDB();

    span.on('click', function () {
        modal.css('display', 'none');
    });

    $(window).on('click', function (event) {
        if ($(event.target).is(modal)) {
            modal.css('display', 'none');
        }
    });

});

$('#homeBtn').on('click', function () {
    window.location.href = "../Pages/index.html";
});


function viewCourses(element) { // needs fixing
    element.click(function (event) {
        $('#myModal').css('display', 'block');
    });
}

function addCoursesToModal(buttonId) { // not working
    console.log(buttonId);
    //redo the logic to fit the modal
    let api = `https://localhost:7283/api/Courses/searchByInstructorId/${buttonId}`;
    ajaxCall("GET", api, getSCBF, getECBF);
}

function getSCBF(result) {
    alert("Courses added successfully!");
    console.log(result);
}


function getECBF(err) {
    console.log(err);
    alert("Error adding courses");
}




//$("#insertIntsructorsBtn").on("click", function () {

//    console.log("Inserting instructors");
//    insertInstructors(instructorsData);
//});

//$("#getInstructorsBtn").on("click", function () {
//    console.log("Displaying instructors");
//    getAllInstructors();
//});


//function insertInstructors(instructorsData) {
//    //api = `https://localhost:7076/api/Instructors`;
//    var instructorDataToSend;
//    var instructorsToServer = [];
//    instructorsData[0].forEach(instructor => {
//        instructorDataToSend = {
//            id: instructor.id,
//            title: instructor.title,
//            name: instructor.display_name,
//            image: instructor.image_100x100,
//            jobTitle: instructor.job_title
//        };
//        ajaxCall("POST", api, JSON.stringify(instructorDataToSend), postSCBF, postECBF);
//    })
//}

// Get all instructors function




