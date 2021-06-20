function addTestData() {
  var item = $(
    "<tr>" +
      '<th scope="row">2</th>' +
      "<td>Jacob</td>" +
      "<td>Thornton</td>" +
      "<td>@fat</td>" +
      "<td>@mdo</td>" +
      "<td>@mdo</td>" +
      "<td>@mdo</td>" +
      "</tr>"
  );

  $(".notifications").append(item);
}

function startSystem() {
  fetch("http://localhost:5004/sensor/start", {
    method: "GET",
  }).then((p) => console.log("System started"));
}

function stopSystem() {
  fetch("http://localhost:5004/sensor/stop", {
    method: "GET",
  }).then((p) => console.log("System stopped"));
}
