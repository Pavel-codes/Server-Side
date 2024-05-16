function insertInstructors() {
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
function getAllInstructors() {
    // Add a timeout of 2 seconds (2000 milliseconds)
    setTimeout(function () {
        $.getJSON("/api/instructors", function (data) {
            // Render instructors
        });
    }, 2000); 
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
