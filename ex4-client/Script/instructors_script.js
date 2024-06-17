const apiBaseUrl = "https://localhost:7283/api/Instructors";
let instructorsData = []; 


$(document).ready(function () {


    function getInstructorsFromDB() {
        ajaxCall('GET', apiBaseUrl, true, postSCBF, postECBF);
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
            viewCourses(instructorBtn);
        });
    }

    function postSCBF(result) {
        renderInstructors(result)
        console.log("Received instructors");

    }

    function postECBF(err) {
        console.log(err);
    }

    getInstructorsFromDB();

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

    //redo the logic to fit the modal
    console.log(buttonId);
    var courseDataToSend;
    coursesData.forEach(courseData => {
        if (buttonId == courseData.id) {
            courseDataToSend = {
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

            const api = `https://localhost:7283/api/Courses/addCourseToUser/${userId}`;

            ajaxCall("POST", api, JSON.stringify(courseDataToSend), postSCBF, postECBF)

        }
        else {
            console.log("not found");
        }
    });
}

function postSCBF(result) {
    alert("Course added successfully!");
    console.log(result);
}


function postECBF(err) {
    alert("Course was already added.");
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




