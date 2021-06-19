"use strict";

var connection = new signalR.HubConnectionBuilder()
  .withUrl("http://localhost:5003/commandhub")
  .build();

console.log(connection);
// Funkcija koju trigeruje server
connection.on("ReceiveMessage", function () {
  /*var li = document.createElement("li");
  li.textContent = "TEST";
  document.getElementById("messagesList").appendChild(li);*/

  console.log("callback funkcija pozvana");
});

connection.start().then(function () {
  /*var li = document.createElement("li");
  li.textContent = "Connected!";
  document.getElementById("messagesList").appendChild(li);*/
  console.log("konekcija uspesna");
});

/*document
  .getElementById("sendButton")
  .addEventListener("click", function (event) {
    connection.invoke("InitCommunication");
  });*/

function initCommunicationSR() {
  console.log("uso");
  connection.invoke("InitCommunication");
  console.log("izaso");
}
