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
