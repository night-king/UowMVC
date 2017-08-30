
$(document).ready(function(){
	
	/* ---------- Sparkline Charts ---------- */
	//generate random number for charts
	randNum = function(){
		//return Math.floor(Math.random()*101);
		return (Math.floor( Math.random()* (1+40-20) ) ) + 20;
	}

	var chartColours = ['#2FABE9', '#FA5833', '#b9e672', '#bbdce3', '#9a3b1b', '#5a8022', '#2c7282'];

	//sparklines (making loop with random data for all 7 sparkline)
	i=1;
	for (i=1; i<9; i++) {
	 	var data = [[1, 3+randNum()], [2, 5+randNum()], [3, 8+randNum()], [4, 11+randNum()],[5, 14+randNum()],[6, 17+randNum()],[7, 20+randNum()], [8, 15+randNum()], [9, 18+randNum()], [10, 22+randNum()]];
	 	placeholder = '.sparkLineStats' + i;

		if (retina()) {

			$(placeholder).sparkline(data, {
				width: 200,//Width of the chart - Defaults to 'auto' - May be any valid css width - 1.5em, 20px, etc (using a number without a unit specifier won't do what you want) - This option does nothing for bar and tristate chars (see barWidth)
				height: 60,//Height of the chart - Defaults to 'auto' (line height of the containing tag)
				lineColor: '#2FABE9',//Used by line and discrete charts to specify the colour of the line drawn as a CSS values string
				fillColor: '#f2f7f9',//Specify the colour used to fill the area under the graph as a CSS value. Set to false to disable fill
				spotColor: '#467e8c',//The CSS colour of the final value marker. Set to false or an empty string to hide it
				maxSpotColor: '#b9e672',//The CSS colour of the marker displayed for the maximum value. Set to false or an empty string to hide it
				minSpotColor: '#FA5833',//The CSS colour of the marker displayed for the mimum value. Set to false or an empty string to hide it
				spotRadius: 2,//Radius of all spot markers, In pixels (default: 1.5) - Integer
				lineWidth: 1//In pixels (default: 1) - Integer
			});
			
			//only firefox sux in this case
			if (jQuery.browser.mozilla) {
				$(placeholder).css('MozTransform','scale(0.5,0.5)').css('margin-top','-10px');
				$(placeholder).css('height','30px').css('width','100px').css('margin-left','-20px').css('margin-right','40px');
			} else {
				$(placeholder).css('zoom',0.5);
			}

		} else {

			$(placeholder).sparkline(data, {
				width: 100,//Width of the chart - Defaults to 'auto' - May be any valid css width - 1.5em, 20px, etc (using a number without a unit specifier won't do what you want) - This option does nothing for bar and tristate chars (see barWidth)
				height: 30,//Height of the chart - Defaults to 'auto' (line height of the containing tag)
				lineColor: '#2FABE9',//Used by line and discrete charts to specify the colour of the line drawn as a CSS values string
				fillColor: '#f2f7f9',//Specify the colour used to fill the area under the graph as a CSS value. Set to false to disable fill
				spotColor: '#467e8c',//The CSS colour of the final value marker. Set to false or an empty string to hide it
				maxSpotColor: '#b9e672',//The CSS colour of the marker displayed for the maximum value. Set to false or an empty string to hide it
				minSpotColor: '#FA5833',//The CSS colour of the marker displayed for the mimum value. Set to false or an empty string to hide it
				spotRadius: 2,//Radius of all spot markers, In pixels (default: 1.5) - Integer
				lineWidth: 1//In pixels (default: 1) - Integer
			});

		}

	}
	
	/* ---------- Init jQuery Knob - disbaled in IE8, IE7, IE6 ---------- */
	if(jQuery.browser.version.substring(0, 2) == "8.") {
		 
		//disable jQuery Knob
		
	} else {
		
		$('.circleChart').each(function(){

			var circleColor = $(this).parent().css('color');

			$(this).knob({
		        'min':0,
		        'max':100,
		        'readOnly': true,
		        'width': 120,
		        'height': 120,
		        'fgColor': circleColor,
		        'dynamicDraw': true,
		        'thickness': 0.2,
		        'tickColorizeValues': true,
				'skin':'tron'
		    });

		});
		
	}
	
	/*------- Just Gage Init -------*/
	g1=new JustGage({id:"g1",value:67,min:0,max:100,title:"Visitors",label:"per minute"});
	g1a=new JustGage({id:"g1a",value:45,min:0,max:100,title:"Errors",label:"average"});
	g2=new JustGage({id:"g2",value:15,min:0,max:100,title:"Timers",label:""});
	g2a=new JustGage({id:"g2a",value:7,min:0,max:100,title:"Alerts",label:""});
	g2b=new JustGage({id:"g2b",value:22,min:0,max:100,title:"Events",label:""});
	
	setInterval(function(){
		g1.refresh(getRandomInt(50,100));
		g1a.refresh(getRandomInt(50,100));
		g2.refresh(getRandomInt(0,50));
		g2a.refresh(getRandomInt(0,50));
		g2b.refresh(getRandomInt(0,50))
		},2000
	);
	
	/*------- Easy Pie Chart Init -------*/
	$('.percentage').easyPieChart();
    $('.percentage-light').easyPieChart();

    $('.updateEasyPieChart').on('click', function(e) {
      e.preventDefault();
      $('.percentage, .percentage-light').each(function() {
        $(this).data('easyPieChart').update(Math.round(100*Math.random()));
      });
    });    

});


	/*
	Sparkline: Line
	*/
	$("#sparklineLine").sparkline(sparklineLineData, {
		type: 'line',
		width: '80',
		height: '30',
		lineColor: '#0088cc'
	});

	/*
	Sparkline: Bar
	*/
	$("#sparklineBar").sparkline(sparklineBarData, {
		type: 'bar',
		width: '80',
		height: '30',
		barColor: '#0088cc',
		negBarColor: '#B20000'
	});

	/*
	Sparkline: Tristate
	*/
	$("#sparklineTristate").sparkline(sparklineTristateData, {
		type: 'tristate',
		width: '80',
		height: '30',
		posBarColor: '#0088cc',
		negBarColor: '#B20000'
	});

	/*
	Sparkline: Discrete
	*/
	$("#sparklineDiscrete").sparkline(sparklineDiscreteData, {
		type: 'discrete',
		width: '80',
		height: '30',
		lineColor: '#0088cc'
	});

	/*
	Sparkline: Bullet
	*/
	$("#sparklineBullet").sparkline(sparklineBulletData, {
		type: 'bullet',
		width: '80',
		height: '30',
		targetColor: '#ff7f00',
		performanceColor: '#0088cc'
	});

	/*
	Sparkline: Pie
	*/
	$("#sparklinePie").sparkline(sparklinePieData, {
		type: 'pie',
		height: '30',
		barColor: '#0088cc'
	});


/*
Radar
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
Polar
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