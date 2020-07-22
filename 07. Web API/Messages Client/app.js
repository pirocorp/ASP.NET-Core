const appUrl = "https://localhost:5001/api/";
let currentUsername = null;
let endPointMessages = "messages";
let interval;

let username = null;

async function loadData(endPoint) {
    try{
        let response = await fetch(`${appUrl}${endPoint}`);
        let data = await response.json();

        return data;
    }catch(err){
        console.error(err);
    }
};

async function renderMessages() {
    let messages = await loadData(endPointMessages)

    let messagesElement = document.getElementById("messages");
    messagesElement.innerHTML = "";

    for(let message of messages){
        messagesElement.innerHTML 
            += `<div class="message d-flex justify-content-start"><strong>${message.user}</strong>: ${message.content}</div>`
    }

    window.scrollTo(0, messagesElement.scrollHeight);
};

async function createMessage(opts) {
    try{
        let url = `${appUrl}${endPointMessages}`;
        let response = await fetch(url, {
            method: 'post',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
              },
            body: JSON.stringify(opts)
        });    
    }catch(err){
        console.error(err);
    }
};

function sendMessage(){
    if(username === null){
        console.error("empty user");
        return;
    }

    let messageElement = document.getElementById("message");
    let messageText = messageElement.value;

    if(messageText === "" || messageText === null){
        console.log("Empty message");
        return;
    }

    let message = {
        user: username,
        content: messageText
    };

    messageElement.value = "";
    createMessage(message);
}

function chooseUsername(){
    let usernameElement = document.getElementById("username");
    username = usernameElement.value;
    usernameElement.value = "";

    let usernameChoiceElement = document.getElementById("username-choice");
    usernameChoiceElement.textContent = username;

    let resetDataElement = document.getElementById("reset-data");
    resetDataElement.style.display = 'inherit';

    let chooseDataElement = document.getElementById("choose-data");
    chooseDataElement.style.display = 'none';
}

function resetUsername(){
    username = null;

    let usernameChoiceElement = document.getElementById("username-choice");
    usernameChoiceElement.textContent = username;

    let resetDataElement = document.getElementById("reset-data");
    resetDataElement.style.display = 'none';

    let chooseDataElement = document.getElementById("choose-data");
    chooseDataElement.style.display = 'inherit';
}

document.addEventListener("DOMContentLoaded", function() {
    let resetDataElement = document.getElementById("reset-data");
    resetDataElement.style.display = 'none';
    
    interval = setInterval("renderMessages()", 1000);    
});

let chooseButton = document.getElementById("choose");
chooseButton.addEventListener("click", chooseUsername);

let resetButton = document.getElementById("reset");
resetButton.addEventListener("click", resetUsername);

let sendButton = document.getElementById("send");
sendButton.addEventListener("click", sendMessage);