!function ($) {

    $(function() {


        // tooltip
        $('[data-toggle="tooltip"]').tooltip();

        // custom scroll bars @ https://github.com/malihu/malihu-custom-scrollbar-plugin
        $(window).load(function(){
            $(".bk-docs-scroll").mCustomScrollbar({
                setHeight: 300,
                theme: "dark-3"

            });

            $(".bk-docs-scroll-hidden").mCustomScrollbar({
                autoHideScrollbar: true,
                setHeight: 300,
                theme: "dark-3"

            });
        });

        if ($("#gmap-marker").length > 0) {
            function initialize() {
                var mapOptions = {
                    center: new google.maps.LatLng(-6.2297465,106.829518),
                    zoom: 10,
                    mapTypeId: google.maps.MapTypeId.ROADMAP
                };
                map = new google.maps.Map(document.getElementById("gmap-marker"), mapOptions);

                var marker = new google.maps.Marker({
                    position: mapOptions.center,
                    map: map,
                    title:"Hello World!"
                });
            }

            google.maps.event.addDomListener(window, 'load', initialize);
            google.maps.event.addDomListener(window, "resize", function() {
                var center = map.getCenter();
                google.maps.event.trigger(map, "resize");
                map.setCenter(center);
            });
        }

        // skycons
        //http://stackoverflow.com/questions/24572100/skycons-cant-display-the-same-icon-twice
        var skycons = new Skycons({"color": "#27ae60"}),
            list  = [
                "clear-day", "clear-night", "partly-cloudy-day",
                "partly-cloudy-night", "cloudy", "rain", "sleet", "snow", "wind",
                "fog"
            ],
            i;

        for(i = list.length; i--; ) {
            var weatherType = list[i],
                elements = document.getElementsByClassName( weatherType );
            for (e = elements.length; e--;){
                skycons.set( elements[e], weatherType );
            }
        }

        skycons.play();


        // animations
        function testAnim(target, x) {
            console.log(target);
            $(target).removeClass().addClass(x + ' animated').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function(){
                $(this).removeClass();
            });
        }

        $('.animated > .panel > .panel-body').click(function(e){
            e.preventDefault();
            var anim = $(this).parent().parent().data('animate');
            testAnim($(this).parent().parent(), anim);
        });


    });

}(window.jQuery);