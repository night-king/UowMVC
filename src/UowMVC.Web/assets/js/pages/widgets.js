/*
FlotChart
*/
function randNum(){
	return ((Math.floor( Math.random()* (1+40-0) ) ) + 10)* 10;
	}


$(document).ready(function(){
	
	if($("#dotChart1").length)
	{	
		var likes = [[1, 5+randNum()], [2, 10+randNum()], [3, 40+randNum()], [4, 60+randNum()],[5, 90+randNum()],[6, 40+randNum()],[7, 25+randNum()],[8, 35+randNum()]];

		var plot = $.plot($("#dotChart1"),
			   [ { data: likes, label: "Profit"} ], {
				   series: {
					   lines: { show: true,
								lineWidth: 2,
								fill: false, fillColor: { colors: [ { opacity: 0.5 }, { opacity: 0.2 } ] }
							 },
					   points: { show: true, 
								 lineWidth: 3 
							 },
					   shadowSize: 0
				   },
				   grid: { hoverable: true, 
						   clickable: true, 
						   tickColor: "#fff",
						   borderWidth: 0
						 
						 },
				   colors: ["#75b9e6"],
					xaxis: {ticks:20, tickDecimals: 0},
					yaxis: {ticks:7, tickDecimals: 0},
					
				 });

		function showTooltip(x, y, contents) {
			$('<div id="tooltip">' + contents + '</div>').css( {
				position: 'absolute',
				display: 'none',
				top: y + 5,
				left: x + 5,
				border: '1px solid #fdd',
				padding: '2px',
				'background-color': '#dfeffc',
				opacity: 0.80
			}).appendTo("body").fadeIn(200);
		}

		var previousPoint = null;
		$("#dotChart1").bind("plothover", function (event, pos, item) {
			$("#x").text(pos.x.toFixed(2));
			$("#y").text(pos.y.toFixed(2));

				if (item) {
					if (previousPoint != item.dataIndex) {
						previousPoint = item.dataIndex;

						$("#tooltip").remove();
						var x = item.datapoint[0].toFixed(0),
							y = item.datapoint[1].toFixed(0);

						showTooltip(item.pageX, item.pageY,
									item.series.label + " of " + x + " = " + y);
					}
				}
				else {
					$("#tooltip").remove();
					previousPoint = null;
				}
		});
	
	}
	
	function randNumTW(){
		return ((Math.floor( Math.random()* (1+40-20) ) ) + 20);
	}
	
});

$(document).ready(function(){
	
	if($("#dotChart2").length)
	{	
		var likes = [[2001, 1+randNum()], [2002, 15+randNum()], [2003, 35+randNum()], [2004, 60+randNum()],[2005, 90+randNum()],[2006, 40+randNum()],[2007, 25+randNum()],[2008, 55+randNum()]];

		var plot = $.plot($("#dotChart2"),
			   [ { data: likes} ], {
				   series: {
					   lines: { show: true,
								lineWidth: 2,
								fill: false, fillColor: { colors: [ { opacity: 0.5 }, { opacity: 0.2 } ] }
							 },
					   points: { show: true, 
								 lineWidth: 3 
							 },
					   shadowSize: 0
				   },
				   grid: { hoverable: true, 
						   clickable: true, 
						   tickColor: "#368ee0",
						   borderWidth: 0
						 
						 },
				   colors: ["#fffff"],
					xaxis: {ticks:20, tickDecimals: 0},
					yaxis: {ticks:7, tickDecimals: 0},
					
				 });

		function showTooltip(x, y, contents) {
			$('<div id="tooltip">' + contents + '</div>').css( {
				position: 'absolute',
				display: 'none',
				top: y + 5,
				left: x + 5,
				border: '1px solid #fdd',
				padding: '2px',
				'background-color': '#dfeffc',
				opacity: 0.80
			}).appendTo("body").fadeIn(200);
		}

		var previousPoint = null;
		$("#dotChart2").bind("plothover", function (event, pos, item) {
			$("#x").text(pos.x.toFixed(2));
			$("#y").text(pos.y.toFixed(2));

				if (item) {
					if (previousPoint != item.dataIndex) {
						previousPoint = item.dataIndex;

						$("#tooltip").remove();
						var x = item.datapoint[0].toFixed(0),
							y = item.datapoint[1].toFixed(0);

						showTooltip(item.pageX, item.pageY,
									item.series.label + " of " + x + " = " + y);
					}
				}
				else {
					$("#tooltip").remove();
					previousPoint = null;
				}
		});
	
	}
	
	function randNumTW(){
		return ((Math.floor( Math.random()* (1+40-20) ) ) + 20);
	}
	
});

/*
Sparkline: Bar
*/
	$("#sparklineBar").sparkline(sparklineBarData, {
		type: 'bar',
		width: '120',
		height: '80',
		barColor: '#0088cc',
		negBarColor: '#B20000'
	});
/*
To Do List
*/
	todoList();
	discussionWidget();