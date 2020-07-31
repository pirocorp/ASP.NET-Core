const appUrl = "https://localhost:5001/api/";
let interval;

let sendButton = document.getElementById("send");
sendButton.addEventListener("click", () => sendMessage(appUrl));

let loginButton = document.getElementById("login");
loginButton.addEventListener("click", () => getLogin(appUrl));

let registerButton = document.getElementById("register");
registerButton.addEventListener("click", () => getRegister(appUrl));

let logoutButtonElement = document.getElementById("logout");
logoutButtonElement.addEventListener("click", () => logout());

document.addEventListener("DOMContentLoaded", function() {  
    getLogin();  
    interval = setInterval(() => renderMessages(appUrl), 1000);    
});