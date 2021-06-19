/*function drawLineChart() {
  var chart = new CanvasJS.Chart("chartContainer", {
    animationEnabled: true,
    theme: "dark1",
    title: {
      text: "Simple Line Chart",
    },
    data: [
      {
        type: "line",
        indexLabelFontSize: 20,
        dataPoints: [
          { y: 450 },
          { y: 414 },
          {
            y: 520,
            indexLabel: "\u2191 highest",
            markerColor: "red",
            markerType: "triangle",
          },
          { y: 460 },
          { y: 450 },
          { y: 500 },
          { y: 480 },
          { y: 480 },
          {
            y: 410,
            indexLabel: "\u2193 lowest",
            markerColor: "DarkSlateGrey",
            markerType: "cross",
          },
          { y: 500 },
          { y: 480 },
          { y: 510 },
        ],
      },
    ],
  });
  chart.render();
}*/

function dravSplineChart() {
  var chart = new CanvasJS.Chart("chartContainer", {
    animationEnabled: true,
    title: {
      text: "Music Album Sales by Year",
    },
    axisY: {
      title: "Units Sold",
      valueFormatString: "#0,,.",
      suffix: "mn",
      stripLines: [
        {
          value: 3366500,
          label: "Average",
        },
      ],
    },
    data: [
      {
        yValueFormatString: "#,### Units",
        xValueFormatString: "YYYY",
        type: "spline",
        dataPoints: [
          { x: new Date(2002, 0), y: 2506000 },
          { x: new Date(2003, 0), y: 2798000 },
          { x: new Date(2004, 0), y: 3386000 },
          { x: new Date(2005, 0), y: 6944000 },
          { x: new Date(2006, 0), y: 6026000 },
          { x: new Date(2007, 0), y: 2394000 },
          { x: new Date(2008, 0), y: 1872000 },
          { x: new Date(2009, 0), y: 2140000 },
          { x: new Date(2010, 0), y: 7289000 },
          { x: new Date(2011, 0), y: 4830000 },
          { x: new Date(2012, 0), y: 2009000 },
          { x: new Date(2013, 0), y: 2840000 },
          { x: new Date(2014, 0), y: 2396000 },
          { x: new Date(2015, 0), y: 1613000 },
          { x: new Date(2016, 0), y: 2821000 },
          { x: new Date(2017, 0), y: 2000000 },
        ],
      },
    ],
  });
  chart.render();
}
dravSplineChart();

function drawPieChart() {
  var chart = new CanvasJS.Chart("chartContainer2", {
    theme: "dark1",
    animationEnabled: true,
    title: {
      text: "Shares of Electricity Generation by Fuel",
    },
    subtitles: [
      {
        text: "United Kingdom, 2016",
        fontSize: 16,
      },
    ],
    data: [
      {
        type: "pie",
        indexLabelFontSize: 18,
        radius: 80,
        indexLabel: "{label} - {y}",
        yValueFormatString: '###0.0"%"',
        click: explodePie,
        dataPoints: [
          { y: 42, label: "Gas" },
          { y: 21, label: "Nuclear" },
          { y: 24.5, label: "Renewable" },
          { y: 9, label: "Coal" },
          { y: 3.1, label: "Other Fuels" },
        ],
      },
    ],
  });
  chart.render();

  function explodePie(e) {
    for (var i = 0; i < e.dataSeries.dataPoints.length; i++) {
      if (i !== e.dataPointIndex) e.dataSeries.dataPoints[i].exploded = false;
    }
  }
}

drawPieChart();
