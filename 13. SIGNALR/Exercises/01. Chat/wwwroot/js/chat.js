let connection = new signalR.HubConnectionBuilder().withUrl("/chat").build();

// The Remote Procedure that will be called from the back-end
connection.on("ReceiveMessage", function(user, message) {
    let msg = message
        .replace(/&/g, "&amp;")
        .replace(/</g, "&lt;")
        .replace(/>/g, "&gt;");

    let encodedMsg = "[" + user + "]: " + msg;

    let messageElement = document.createElement("h3");
    messageElement.textContent = encodedMsg;

    document.getElementById("messageList").appendChild(messageElement);
});

// An error handler for connection errors
connection.start().catch(function(err) {
    return console.error(err.toString());
});

// Remote Procedure Call this will call SendMessage in the back-end
document.getElementById("sendButton").addEventListener("click", function(event) {
    let user = document.getElementById("userInput").value;
    let message = document.getElementById("messageInput").value;

    connection.invoke("SendMessage", user, message).catch(function(err) {
        return console.error(err.toString());
    });

    event.preventDefault();
});