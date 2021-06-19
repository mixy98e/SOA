let dataArr = [
  {
    lineColor: "#D83A56",
    yValueFormatString: "",
    xValueFormatString: "YYYY",
    type: "spline",
    dataPoints: [],
  },
  {
    // temperature
    lineColor: "#233E8B",
    yValueFormatString: "",
    xValueFormatString: "YYYY",
    type: "spline",
    dataPoints: [],
  },
  {
    // pressure
    lineColor: "#4CA1A3",
    yValueFormatString: "",
    xValueFormatString: "YYYY",
    type: "spline",
    dataPoints: [],
  },
  {
    // humidity
    lineColor: "#FFC074",
    yValueFormatString: "",
    xValueFormatString: "YYYY",
    type: "spline",
    dataPoints: [],
  },
  {
    // wind direction
    lineColor: "#511281",
    yValueFormatString: "",
    xValueFormatString: "YYYY",
    type: "spline",
    dataPoints: [],
  },
  {
    // speed
    lineColor: "#A5E1AD",
    yValueFormatString: "",
    xValueFormatString: "YYYY",
    type: "spline",
    dataPoints: [],
  },
  {
    // time sun rise
    lineColor: "#F7FD04",
    yValueFormatString: "",
    xValueFormatString: "YYYY",
    type: "spline",
    dataPoints: [],
  },

  {
    // time sun set
    lineColor: "#FC5404",
    yValueFormatString: "#,### Units",
    xValueFormatString: "YYYY",
    type: "spline",
    dataPoints: [],
  },
];

let finalArray = [];

function dravSplineChart(title, dataArray) {
  var chart = new CanvasJS.Chart("chartContainer", {
    animationEnabled: true,
    theme: "light2",
    title: {
      text: title,
    },
    axisY: {
      title: "Value",
      valueFormatString: "",
      suffix: "",
      //   stripLines: [
      //     {
      //       value: 12506000, // aditional function parameter
      //       label: "Average",
      //     },
      //   ],
    },
    data: dataArray,
    // data: [
    //   {
    //     lineColor: "cyan",
    //     yValueFormatString: "#,### Units",
    //     xValueFormatString: "YYYY",
    //     type: "spline",
    //     dataPoints: [
    //       { x: new Date(1624105623), y: 12506000 },
    //       { x: new Date(1624105725), y: 12798000 },
    //     ],
    //   },
    // ],
  });
  chart.render();
}

let data1 = [
  {
    lineColor: "#FC5404",
    yValueFormatString: "#,### Units",
    xValueFormatString: "YYYY",
    type: "spline",
    dataPoints: [
      { x: new Date(1624105623), y: 1.2 },
      { x: new Date(1624105725), y: 1.5 },
    ],
  },
  {
    lineColor: "#FC5404",
    yValueFormatString: "#,### Units",
    xValueFormatString: "YYYY",
    type: "spline",
    dataPoints: [
      { x: new Date(1624105623), y: 2.2 },
      { x: new Date(1624105725), y: 2.5 },
    ],
  },
];
dravSplineChart("Chart tester", data1);

function fetchSensorData() {
  fetch("http://localhost:5004/data/all", {
    method: "GET",
  }).then((p) =>
    p.json().then((data) => {
      console.log("usaspoasdoasdo", data);

      data.forEach((el) => {
        //console.log(el["radiation"]);
        dataArr[0].dataPoints.push({
          // unix timestamp
          x: new Date(el["unixTime"]),
          y: el["radiation"],
        });
        dataArr[1].dataPoints.push({
          // radiation
          x: new Date(el["unixTime"]),
          y: el["temperature"],
        });
        dataArr[2].dataPoints.push({
          // pressure
          x: new Date(el["unixTime"]),
          y: el["pressure"],
        });
        dataArr[3].dataPoints.push({
          // humidity
          x: new Date(el["unixTime"]),
          y: el["humidity"],
        });
        dataArr[4].dataPoints.push({
          // wind direction
          x: new Date(el["unixTime"]),
          y: el["windDirection"],
        });
        dataArr[5].dataPoints.push({
          // speed
          x: new Date(el["unixTime"]),
          y: el["speed"],
        });
        dataArr[6].dataPoints.push({
          // sun rise
          x: new Date(el["unixTime"]),
          y: el["timeSunRise"],
        });
        dataArr[7].dataPoints.push({
          // sun set
          x: new Date(el["unixTime"]),
          y: el["timeSunSet"],
        });
      });
    })
  );

  console.log(dataArr);
}

function buttonSelection(caller) {
  console.log("asdasdsasad");
  console.log(caller);

  let btnGroup = document.querySelectorAll(".btn-check");
  //console.log(btnGroup);
  finalArray = [];
  btnGroup.forEach((el, index) => {
    //console.log(el.checked, index);
    if (el.checked === true) finalArray.push(dataArr[index]);
  });

  console.log(dataArr);
  console.log(finalArray);
  dravSplineChart("Solar Radiation IOT", finalArray);
}

function onLoadBody() {
  fetchSensorData();
}

/*  DataBase data model (c# model)

        public ObjectId Id { get; set; }
        public int UnixTime { get; set; }
        public float Radiation { get; set; }        color D83A56
        public float Temperature { get; set; }      color FFC074
        public float Pressure { get; set; }         color 233E8B
        public float Humidity { get; set; }         color 4CA1A3
        public float WindDirection { get; set; }    color 511281
        public float Speed { get; set; }            color A5E1AD
        public string TimeSunRise { get; set; }     color F7FD04
        public string TimeSunSet { get; set; }      color FC5404
*/
