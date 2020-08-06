let endPointMessages = "messages";

async function loadData(appUrl) {
    try{
        let response = await fetch(`${appUrl}${endPointMessages}`);
        let data = await response.json();

        return data;
    }catch(err){
        console.error(err);
    }
};

async function renderMessages(appUrl) {
    let messages = await loadData(appUrl)

    let messagesElement = document.getElementById("messages");
    messagesElement.innerHTML = "";

    for(let message of messages){
        messagesElement.innerHTML 
            += `<div class="message d-flex justify-content-start"><strong>${message.user}</strong>: ${message.content}</div>`
    }

    messagesElement.scrollTo(0, messagesElement.scrollHeight);
};

async function createMessage(appUrl, message) {
    try{
        let url = `${appUrl}${endPointMessages}`;
        let response = await fetch(url, {
            method: 'post',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
              },
            body: JSON.stringify(message)
        });    
    }catch(err){
        console.error(err);
    }
};

function sendMessage(appUrl){
    let messageElement = document.getElementById("message");
    let messageText = messageElement.value;

    let usernameElement = document.getElementById("username-logged-in");
    let username = usernameElement.textContent;

    if(messageText === "" || messageText === null){
        console.error("Empty message");
        return;
    }

    let message = {
        content: messageText,
        username: username
    };

    messageElement.value = "";
    createMessage(appUrl, message);
}