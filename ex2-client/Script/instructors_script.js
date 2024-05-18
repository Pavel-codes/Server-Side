var instructorData = $.getJSON("../Data/Instructor.json");
console.log(JSON.stringify(instructorData));

$("#insertIntsructorsBtn").on("click", function () {

    alert("Handler for `click` called.");
});

$("#getInstructorsBtn").on("click", function () {
    alert("Handler for `click` called."); 
});


function insertInstructors(instructorsData) {
    $.ajax({
        url: 'insert_instructors.php',
        type: 'POST',
        data: { instructors: instructorsData }, // Assuming instructorsData is already defined
        success: function (response) {
            alert("Instructors inserted successfully!");
        },
        error: function () {
            alert("Error inserting instructors.");
        }
    });
}

// Get all instructors function
function getAllInstructors(instructors) {
    api = "https://localhost:7076/api/Instructors";
    //ajaxCall("GET", api, postSCBF, postECBF)
    $.ajax({
        url: api,
        type: 'GET',
        success: function (data) {
            renderInstructors(data);
        },
        error: function () {
            alert("Error loading courses.");
        }
    });

}



function renderInstructors(instructors) {
    var instructorsContainer = $('#instructors-container');
    instructors.forEach(function (instructor) {
        var instructorElement = $('<div>');
        instructorElement.append('<h2>' + instructor.name + '</h2>');
        instructorElement.append('<p>Email: ' + instructor.email + '</p>');
        
        instructorsContainer.append(instructorElement);
    });
}
