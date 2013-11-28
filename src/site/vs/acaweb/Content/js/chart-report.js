var bardata = {
    labels: ["January", "February", "March", "April", "May", "June", "July"],
    datasets: [
		{
		    fillColor: "rgba(220,220,220,0.5)",
		    strokeColor: "rgba(220,220,220,1)",
		    data: [65, 59, 90, 81, 56, 55, 40]
		},
		{
		    fillColor: "rgba(151,187,205,0.5)",
		    strokeColor: "rgba(151,187,205,1)",
		    data: [28, 48, 40, 19, 96, 27, 100]
		}
    ]
};
var piedata = [
	{
	    value: 30,
	    color: "#F38630"
	},
	{
	    value: 50,
	    color: "#E0E4CC"
	},
	{
	    value: 100,
	    color: "#69D2E7"
	}
];
var barCtx = document.getElementById("bars").getContext("2d");
var pieCtx = document.getElementById("pie").getContext("2d");
//new Chart(barCtx).Bar(bardata);
//new Chart(pieCtx).Pie(piedata);

$.get(base_url + "Report/ChartData", function (json) {
    // + "?startdate=" + parseDateField("#startDate") + "&to=" + parseDateField("#endDate"), function (data) {
    var c1 = new Chart(barCtx).Bar(json.bar);
    var c2 = new Chart(pieCtx).Pie(json.pie.data);
    for (var idx in json.pie.labels) {
        $("#piePiecesNames").append("<li style='color:"+json.pie.colors[idx]+"'>" + json.pie.labels[idx] + "</li>");
    }
    //new Chart(pieCtx).Pie(data.pie);
}, "json");