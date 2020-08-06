async function postLogin(appUrl){
    let ednPoint ="users/login";

    let usernameElement = document.getElementById("username-login");
    let passwordElement = document.getElementById("password-login");

    let username = usernameElement.value;
    let password = passwordElement.value;

    let url = `${appUrl}${ednPoint}`;

    let data = {
        username: username,
        password: password,
    };

    let response;

    try{
        response = await fetch(url, {
            method: 'post',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
              },
            body: JSON.stringify(data)
        });
    }catch(err){
        console.error(err);
    }

    if(response.ok){
        let anonymousDataElement = document.getElementById("anonymous-data");
        anonymousDataElement.style.display = "none";

        let loggedInDataElement = document.getElementById("logged-in-data");
        loggedInDataElement.style.display = "inherit";

        /* TODO Add Jwt token to Session Storage */

        let result = response.json();
        console.log(result);

        document
            .getElementById("username-logged-in")
            .textContent = username;
    }else{
        let errorAlert = document.getElementById("alerts");
        errorAlert.innerHTML = '<div class="alert alert-danger fade show m-4" role="alert">Invalid credentials</div>';

        errorAlert.addEventListener("click", () =>  errorAlert.innerHTML = "");
    }

    usernameElement.value = "";
    passwordElement.value = "";
}

async function postRegister(appUrl){
    let ednPoint ="users/register";
    
    let usernameElement = document.getElementById("username-register");
    let passwordElement = document.getElementById("password-register");
    let confirmPasswordElement = document.getElementById("confirm-password-register");
    
    let username = usernameElement.value;
    let password = passwordElement.value;
    let confirmPassword = confirmPasswordElement.value;

    let url = `${appUrl}${ednPoint}`;

    let data = {
        username: username,
        password: password,
        confirmPassword: confirmPassword
    };

    let response;

    try{
        response = await fetch(url, {
            method: 'post',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
              },
            body: JSON.stringify(data)
        });
    }catch(err){
        console.error(err);
    }

    if(response.ok){
        getLogin();
    }else{
        let errorAlert = document.getElementById("alerts");
        errorAlert.innerHTML = '<div class="alert alert-danger fade show m-4" role="alert">Something went wrong try again.</div>';

        errorAlert.addEventListener("click", () =>  errorAlert.innerHTML = "");
    }
}

function getLogin(){
    document
    .getElementById("login-data")
    .style
    .display = "block";

document
    .getElementById("register-data")
    .style
    .display = "none";
}

function getRegister(){
    document
        .getElementById("login-data")
        .style
        .display = "none";

    document
        .getElementById("register-data")
        .style
        .display = "block";
}

function logout(){
    document
        .getElementById("logged-in-data")
        .style
        .display = "none";

    console.log("logout");
}