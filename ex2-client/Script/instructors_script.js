const api = `https://localhost:7076/api/Instructors`;
let instructorsData = []; 

$(document).ready(function () {
    $.getJSON("../Data/Instructor .json", function (data) { 
        instructorsData.push(data); 
    });
    //console.log(instructorsData);
});

$('#homeBtn').on('click', function () {
    window.location.href = "../Pages/index.html";
});

$("#insertIntsructorsBtn").on("click", function () {

    alert("Handler INSERT for `click` called.");
    insertInstructors(instructorsData);
});

$("#getInstructorsBtn").on("click", function () {
    alert("Handler GET for `click` called.");
    getAllInstructors();
});

$(document).ready(function () {
    $('#insertIntsructorsBtn').click(function () {
        $(this).prop('disabled', true); 
    });

    $('#getInstructorsBtn').click(function () {
        $(this).prop('disabled', true); // Disable the button
        // Your logic for getting all instructors
    });
});

function insertInstructors(instructorsData) {
    //api = `https://localhost:7076/api/Instructors`;
    var instructorDataToSend;
    instructorsData[0].forEach(instructor => {
        instructorDataToSend = {
            id: instructor.id,
            title: instructor.title,
            name: instructor.display_name,
            image: instructor.image_100x100,
            jobTitle: instructor.job_title
        };
        ajaxCall("POST", api, JSON.stringify(instructorDataToSend), postSCBF, postECBF);

    })
}

function postSCBF(result) {
    console.log(result);
}

function postECBF(err) {

    console.log(err);
}

// Get all instructors function
function getAllInstructors() {
    //ajaxCall("GET", api, postSCBF, postECBF)
    $.ajax({
        url: api,
        type: 'GET',
        success: function (data) {
            renderInstructors(data);
        },
        error: function () {
            alert("Error loading instructors.");
        }
    });
}


function renderInstructors(instructors) {
    var instructorsContainer = $('#instructors-container');
    instructors.forEach(function (instructor) {
        var instructorElement = $('<div>');
        instructorElement.append('<h2>' + instructor.name + '</h2>');
        instructorElement.append('<img src=' + instructor.image + '>');
        instructorElement.append('<p>ID: ' + instructor.id + '</p>');
        instructorElement.append('<p>Title: ' + instructor.title + '</p>');
        instructorElement.append('<p>Job: ' + instructor.jobTitle + '</p>');
        
        instructorsContainer.append(instructorElement);
    });
}
