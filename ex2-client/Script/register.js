
$(document).ready(function () {
    $('#registerForm').submit(function (event) {
        event.preventDefault();

        const name = $('#name').val();
        const email = $('#email').val();
        const password = $('#password').val();


        const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        if (!emailPattern.test(email)) {
            alert("Invalid email format.");
            return;
        }

        if (password.length < 4) {
            alert("Password must be at least 4 characters long.");
            return;
        }

        const newUser = {
            Name: name,
            Email: email,
            Password: password
        };

        submitToServer(newUser);

        function submitToServer(newUser) {
            let api = `https://localhost:7076/api/Users`;
            ajaxCall("POST", api, JSON.stringify(newUser), postSCBF, postECBF);
            return false;
        }

        function postSCBF(user) {
            alert(user);
            alert("Registration successful.");
            window.location.href = "login.html";

        }

        function postECBF(err) {
            alert("User with this email already exists.");
            console.log(err);
        }
    });
});

// will become redundant - to be removed later!
const loginPageBtn = document.getElementById("loginPageBtn");

loginPageBtn.addEventListener("click", function () {
    window.location.href = "../Pages/login.html";
});
