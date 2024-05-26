$(document).ready(function () {
    $('#loginForm').submit(function (event) {
        event.preventDefault();

        const email = $('#email').val();
        const password = $('#password').val();

        if (email === '' || password === '') {
            alert("All fields are mandatory.");
            return;
        }

        const loginData = {
            Email: email,
            Password: password
        };

        const api = 'https://localhost:7076/api/Users'; 
        ajaxCall('POST', api, JSON.stringify(loginData), loginSuccess, loginError);


        function loginSuccess(response) {
            localStorage.setItem('user', JSON.stringify(response));
            alert("Login successful.");
            window.location.href = "index.html";
        }

        function loginError() {
            alert("Invalid email or password.");
        }
    });
});


