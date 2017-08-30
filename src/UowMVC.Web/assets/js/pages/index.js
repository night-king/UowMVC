
/*
Lightbox
*/
(function(theme, $) {

	theme = theme || {};

	var instanceName = '__lightbox';

	var PluginLightbox = function($el, opts) {
		return this.initialize($el, opts);
	};

	PluginLightbox.defaults = {
		tClose: 'Close (Esc)', // Alt text on close button
		tLoading: 'Loading...', // Text that is displayed during loading. Can contain %curr% and %total% keys
		gallery: {
			tPrev: 'Previous (Left arrow key)', // Alt text on left arrow
			tNext: 'Next (Right arrow key)', // Alt text on right arrow
			tCounter: '%curr% of %total%' // Markup for "1 of 7" counter
		},
		image: {
			tError: '<a href="%url%">The image</a> could not be loaded.' // Error message when image could not be loaded
		},
		ajax: {
			tError: '<a href="%url%">The content</a> could not be loaded.' // Error message when ajax request failed
		}
	};

	PluginLightbox.prototype = {
		initialize: function($el, opts) {
			if ( $el.data( instanceName ) ) {
				return this;
			}

			this.$el = $el;

			this
				.setData()
				.setOptions(opts)
				.build();

			return this;
		},

		setData: function() {
			this.$el.data(instanceName, this);

			return this;
		},

		setOptions: function(opts) {
			this.options = $.extend(true, {}, PluginLightbox.defaults, opts, {
				wrapper: this.$el
			});

			return this;
		},

		build: function() {
			this.options.wrapper.magnificPopup(this.options);

			return this;
		}
	};

	// expose to scope
	$.extend(theme, {
		PluginLightbox: PluginLightbox
	});

	// jquery plugin
	$.fn.themePluginLightbox = function(opts) {
		return this.each(function() {
			var $this = $(this);

			if ($this.data(instanceName)) {
				return $this.data(instanceName);
			} else {
				return new PluginLightbox($this, opts);
			}

		});
	}

}).apply(this, [ window.theme, jQuery ]);

(function( $ ) {

	'use strict';

	if ( $.isFunction($.fn[ 'magnificPopup' ]) ) {

		$(function() {
			$('[data-plugin-lightbox], .lightbox:not(.manual)').each(function() {
				var $this = $( this ),
					opts = {};

				var pluginOptions = $this.data('plugin-options');
				if (pluginOptions)
					opts = pluginOptions;

				$this.themePluginLightbox(opts);
			});
		});

	}

}).apply(this, [ jQuery ]);


/*
FlotChart (Fire Admin Update)
*/
function randNum(){
	return ((Math.floor( Math.random()* (1+40-0) ) ) + 10)* 10;
	}
	
$(document).ready(function(){
	
	if($("#adminChartUpdate").length)
	{	
		var likes = [[2007, 1+randNum()], [2008, 15+randNum()], [2009, 35+randNum()], [2010, 60+randNum()],[2011, 90+randNum()],[2012, 40+randNum()],[2013, 25+randNum()],[2014, 55+randNum()]];

		var plot = $.plot($("#adminChartUpdate"),
			   [ { data: likes} ], {
				   series: {
					   lines: { show: true,
								lineWidth: 2,
								fill: false, fillColor: { colors: [ { opacity: 0.5 }, { opacity: 0.2 } ] }
							 },
					   points: { show: true, 
								 lineWidth: 1 
							 },
					   shadowSize: 0
				   },
				   grid: { hoverable: true, 
						   clickable: true, 
						   tickColor: "#ECECFB",
						   borderWidth: 0,
						   backgroundColor: '#FFF'
						 
						 },
				   colors: ["#99CCFF"],
					xaxis: {ticks:8, tickDecimals: 0},
					yaxis: {ticks:5, tickDecimals: 0},
					
				 });

		function showTooltip(x, y, contents) {
			$('<div id="tooltip">' + contents + '</div>').css( {
				position: 'absolute',
				display: 'none',
				top: y + 5,
				left: x + 5,
				border: '2px solid #fff',
				padding: '5px',
				'background-color': '#FFBFBF',
				'color': '#fff',
				opacity: 0.90
			}).appendTo("body").fadeIn(200);
		}

		var previousPoint = null;
		$("#adminChartUpdate").bind("plothover", function (event, pos, item) {
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
	
	
	/* ---------- Pie chart (Best Seller) ---------- */
	var data = [
	{ label: "Wordpress",  data: 12},
	{ label: "Prestashop",  data: 27},
	{ label: "CMS",  data: 85},
	{ label: "Jomla",  data: 64},
	{ label: "Drupal",  data: 90},
	{ label: "Admin Template",  data: 112}
	];
	
	if($("#piechart").length)
	{
		$.plot($("#piechart"), data,
		{
			series: {
					pie: {
							show: true
					}
			},
			grid: {
					hoverable: true,
					clickable: true
			},
			legend: {
				show: false
			},
			colors: ["#FF9999", "#FFCC99", "#99CCFF", "#FF7396"]
		});
		
		function pieHover(event, pos, obj)
		{
			if (!obj)
					return;
			percent = parseFloat(obj.series.percent).toFixed(2);
			$("#hover").html('<span style="font-weight: bold; color: '+obj.series.color+'">'+obj.series.label+' ('+percent+'%)</span>');
		}
		$("#piechart").bind("plothover", pieHover);
	}
	
	
		/* ----------Realtime chart (Server Usage) ---------- */

	 // we use an inline data source in the example, usually data would
	// be fetched from a server
	var data = [], totalPoints = 300;
	function getRandomData() {
		if (data.length > 0)
			data = data.slice(1);

		// do a random walk
		while (data.length < totalPoints) {
			var prev = data.length > 0 ? data[data.length - 1] : 50;
			var y = prev + Math.random() * 10 - 5;
			if (y < 0)
				y = 0;
			if (y > 100)
				y = 100;
			data.push(y);
		}

		// zip the generated y values with the x values
		var res = [];
		for (var i = 0; i < data.length; ++i)
			res.push([i, data[i]])
		return res;
	}

	// setup control widget
	var updateInterval = 100;
	$("#updateInterval").val(updateInterval).change(function () {
		var v = $(this).val();
		if (v && !isNaN(+v)) {
			updateInterval = +v;
			if (updateInterval < 1)
				updateInterval = 1;
			if (updateInterval > 2000)
				updateInterval = 2000;
			$(this).val("" + updateInterval);
		}
	});

	
	if($("#realtimeServerUsage").length)
	{
		var options = {
			series: { shadowSize: 1 },
			lines: { fill: true, fillColor: { colors: [ { opacity: 1 }, { opacity: 0.1 } ] }},
			yaxis: { min: 0, max: 100 },
			xaxis: { show: false },
			colors: ["#FFCFBF"],
			grid: {	tickColor: "#ECECFB",
					borderWidth: 0 
			},
		};
		var plot = $.plot($("#realtimeServerUsage"), [ getRandomData() ], options);
		function update() {
			plot.setData([ getRandomData() ]);
			// since the axes don't change, we don't need to call plot.setupGrid()
			plot.draw();
			
			setTimeout(update, updateInterval);
		}

		update();
	}
	
});

/*
Polar (Weekly Stats)
*/	
var Script = function () {

    var polarChartData = [
        {
            value : Math.random(),
            color: "#FF7373"
        },
        {
            value : Math.random(),
            color: "#4CA6FF"
        },
        {
            value : Math.random(),
            color: "#FF99FF"
        },
        {
            value : Math.random(),
            color: "#FF9673"
        },
        {
            value : Math.random(),
            color: "#99B2FF"
        },
        {
            value : Math.random(),
            color: "#FFBFBF"
        }
    ];
    new Chart(document.getElementById("polarArea").getContext("2d")).PolarArea(polarChartData);
	
}();


/*
Radar (Marketplace Area)
*/
var Script = function () {

    var radarChartData = {
        labels : ["","","","","","",""],
        datasets : [
            {
                fillColor : "rgba(220,220,220,0.5)",
                strokeColor : "rgba(220,220,220,1)",
                pointColor : "rgba(220,220,220,1)",
                pointStrokeColor : "#fff",
                data : [65,59,90,81,56,55,40]
            },
            {
                fillColor : "rgba(151,187,205,0.5)",
                strokeColor : "rgba(151,187,205,1)",
                pointColor : "rgba(151,187,205,1)",
                pointStrokeColor : "#fff",
                data : [28,48,40,19,96,27,100]
            }
        ]

    };
    new Chart(document.getElementById("radar").getContext("2d")).Radar(radarChartData);
	
}();




/*
SparklineWeeklyStats
*/
	$("#sparklineBarweeklystats").sparkline(sparklineBarweeklystatsData, {
		type: 'bar',
		width: '120',
		height: '80',
		barColor: '#0088cc',
		negBarColor: '#B20000'
	});
	

/*
 SparklineMyStats
*/

	$("#sparklineLineProfileVisits").sparkline(sparklineLineProfileVisitsData, {
		type: 'bar',
		width: '80',
		height: '38',
		lineColor: '#0088cc'
	});
	$("#sparklineLineProjects").sparkline(sparklineLineProjectsData, {
		type: 'pie',
		width: '80',
		height: '38',
		lineColor: '#0088cc'
	});
	$("#sparklineLineTasks").sparkline(sparklineLineTasksData, {
		type: 'line',
		width: '80',
		height: '38',
		lineColor: '#0088cc'
	});




/*
To Do List
*/
	todoList();
	discussionWidget();
	

/*
Calendar
*/	
	
(function( $ ) {

	'use strict';

	var initCalendar = function() {
		var $calendar = $('#calendar');
		var date = new Date();
		var d = date.getDate();
		var m = date.getMonth();
		var y = date.getFullYear();

		$calendar.fullCalendar({
			header: {
				left: 'title',
				right: 'prev,today,next'
			},

			timeFormat: 'h:mm',

			themeButtonIcons: {
				prev: 'fa fa-caret-left',
				next: 'fa fa-caret-right',
			},

		});

		// FIX INPUTS TO BOOTSTRAP VERSIONS
		var $calendarButtons = $calendar.find('.fc-header-right > span');
		$calendarButtons
			.filter('.fc-button-prev, .fc-button-today, .fc-button-next')
				.wrapAll('<div class="btn-group mt-sm mr-md mb-sm ml-sm"></div>')
				.parent()
				.after('<br class="hidden"/>');

		$calendarButtons
			.not('.fc-button-prev, .fc-button-today, .fc-button-next')
				.wrapAll('<div class="btn-group mb-sm mt-sm"></div>');

		$calendarButtons
			.attr({ 'class': 'btn btn-sm btn-default' });
	};

	$(function() {
		initCalendar();
	});

}).apply(this, [ jQuery ]);


/*
Map Vector (Global Stats)
*/	

(function( $ ) {

	'use strict';

	var initMap = function( $el, options ) {
		var defaults = {
			backgroundColor: null,
			color: '#FFFFFF',
			hoverOpacity: 0.7,
			selectedColor: '#FF7373',
			enableZoom: false,
			borderWidth:1,
			showTooltip: true,
			values: sample_data,
			scaleColors: ['#1AA2E6', '#0088CC'],
			normalizeFunction: 'polynomial'
		};

		$el.vectorMap( $.extend( defaults, options ) );
	};

	$(function() {
		$( '[data-vector-map]' ).each(function() {
			var $this = $(this);
			initMap( $this, ($this.data( 'plugin-options' ) || {}) )
		});
	});

}).apply( this, [ jQuery ]);