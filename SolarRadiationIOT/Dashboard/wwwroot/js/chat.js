"use strict";

var connection = new signalR.HubConnectionBuilder()
  .withUrl("http://localhost:5003/commandhub")
  .build();

console.log(connection);
// Funkcija koju trigeruje server
connection.on("ReceivedMsg", function (msg) {
  /*var li = document.createElement("li");
  li.textContent = "TEST";
  document.getElementById("messagesList").appendChild(li);*/

  console.log("callback function call " + msg);
  var nameArr = msg.split(",");

  var item = $(
    "<tr>" +
      "<td>" +
      nameArr[0] +
      "</td>" +
      "<td>" +
      nameArr[1] +
      "</td>" +
      "<td>" +
      nameArr[2] +
      "</td>" +
      "<td>" +
      nameArr[3] +
      "</td>" +
      "<td>" +
      nameArr[4] +
      "</td>" +
      "<td>" +
      nameArr[5] +
      "</td>" +
      "</tr>"
  );

  $(".notifications").prepend(item);
});
// return unixTime + "," + interval + "," + threshold + "," + weatherStatus +
// "," + radiationStatus + "," + periodOfDay;

connection.start().then(function () {
  /*var li = document.createElement("li");
  li.textContent = "Connected!";
  document.getElementById("messagesList").appendChild(li);*/
  console.log("Connection successfull");
});

/*document
  .getElementById("sendButton")
  .addEventListener("click", function (event) {
    connection.invoke("InitCommunication");
  });*/

function initCommunicationSR() {
  connection.invoke("InitCommunication");
}
