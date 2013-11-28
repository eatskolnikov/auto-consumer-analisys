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

$(document).ready(function() {
    $.getJSON(base_url + "?from=" + parseDateField("#startDate") + "&to=" + parseDateField("#endDate"), function(data) {
        new Chart(barCtx).Bar(data.bar);
        new Chart(pieCtx).Pie(data.pie);
    });
});