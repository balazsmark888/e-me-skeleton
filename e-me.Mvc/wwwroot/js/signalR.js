"use strict";

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/testhub")
    .build();

connection.on("receiveMessage", (message) => {
    var div = document.createElement("div");
    div.innerHTML = message + "<hr/>";
    document.getElementById("messages").appendChild(div);
})

connection.start().catch((err) => {
    console.error(err.message);
});

document.getElementById("sendButton").addEventListener("click", (event) => {
    var message = document.getElementById("message").value;
    connection.invoke("Announce", message).catch((err) => {
        console.error(err.message);
    });
    event.preventDefault();
});