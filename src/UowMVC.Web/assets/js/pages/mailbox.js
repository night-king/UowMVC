
// Mailbox
(function( $ ) {

	'use strict';

	$(function() {
		$('[data-mailbox]').each(function() {
			var $this = $( this );

			$this.themeMailbox();
		});
	});

}).apply(this, [ jQuery ]);

// Mailbox
(function(theme, $) {

	theme = theme || {};

	var instanceName = '__mailbox';

	var capitalizeString = function( str ) {
    	return str.charAt( 0 ).toUpperCase() + str.slice( 1 );
	}

	var Mailbox = function($wrapper) {
		return this.initialize($wrapper);
	};

	Mailbox.prototype = {
		initialize: function($wrapper) {
			if ( $wrapper.data( instanceName ) ) {
				return this;
			}

			this.$wrapper = $wrapper;

			this
				.setVars()
				.setData()
				.build()
				.events();

			return this;
		},

		setVars: function() {
			this.view = capitalizeString( this.$wrapper.data( 'mailbox-view' ) || "" );

			return this;
		},

		setData: function() {
			this.$wrapper.data(instanceName, this);

			return this;
		},

		build: function() {

			if ( typeof this[ 'build' + this.view ] === 'function' ) {
				this[ 'build' + this.view ].call( this );
			}


			return this;
		},

		events: function() {
			if ( typeof this[ 'events' + this.view ] === 'function' ) {
				this[ 'events' + this.view ].call( this );
			}

			return this;
		},

		buildCompose: function() {
			this.buildComposer();
		},

		buildComposer: function() {
			this.$wrapper.find( '#compose-field' ).summernote({
				height: 250,
				toolbar: [
					['style', ['style']],
					['font', ['bold', 'italic', 'underline', 'clear']],
					['fontname', ['fontname']],
					['color', ['color']],
					['para', ['ul', 'ol', 'paragraph']],
					['height', ['height']],
					['table', ['table']],
					['insert', ['link', 'picture', 'video']],
					['view', ['fullscreen']],
					['help', ['help']]
				]
			});
		}
	};

	// jquery plugin
	$.fn.themeMailbox = function(opts) {
		return this.each(function() {
			var $this = $(this);

			if ($this.data(instanceName)) {
				return $this.data(instanceName);
			} else {
				return new Mailbox($this);
			}

		});
	}

}).apply(this, [ window.theme, jQuery ]);


