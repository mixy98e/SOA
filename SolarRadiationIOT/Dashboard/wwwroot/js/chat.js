"use strict";

var connection = new signalR.HubConnectionBuilder()
  .withUrl("http://localhost:5003/commandhub")
  .build();

// Funkcija koju trigeruje server
connection.on("ReceiveMessage", function () {
  var li = document.createElement("li");
  li.textContent = "TEST";
  document.getElementById("messagesList").appendChild(li);
});

connection.start().then(function () {
  var li = document.createElement("li");
  li.textContent = "Connected!";
  document.getElementById("messagesList").appendChild(li);
});

document
  .getElementById("sendButton")
  .addEventListener("click", function (event) {
    connection.invoke("InitCommunication");
  });

function initCommunicationSR() {
  connection.invoke("InitCommunication");
}
