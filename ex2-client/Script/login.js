const apiBaseUrl = "https://localhost:7076/api/Users";

$(document).ready(function () {
    $('#loginForm').submit(function (event) {
        event.preventDefault();

        const email = $('#email').val();
        const password = $('#password').val();


        const loginData = {
            Email: email,
            Password: password
        };

        submitToServer(loginData);
        function submitToServer(loginData) {

            let api = apiBaseUrl + '/login';
            ajaxCall('POST', api, JSON.stringify(loginData), postSCBF, postECBF);
        }

        function postSCBF(response) {
            console.log(response);
            if (response) {
                localStorage.setItem('user', JSON.stringify(response));
                alert("Login successful.");
                window.location.href = "index.html";
            }
            else {
                postECBF();
            }
            
        }

        function postECBF() {
            alert("Invalid email or password.");
        }
    });
});

// will become redundant - to be removed later!
const registerPageBtn = document.getElementById("registerPageBtn"); 

registerPageBtn.addEventListener("click", function () {
    window.location.href = "../Pages/register.html";
});

