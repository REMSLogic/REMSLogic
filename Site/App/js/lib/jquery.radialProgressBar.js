(function ($) {

	var name = 'radialProgressBar';
	var cssClasses = {
		main: name,
		percent: 'percent',
		fill: 'fill',
		slice: 'slice',
		pie: 'pie',
		gt50: 'gt50'
	};

	$.fn[name] = function (method) {
		// Method calling logic
		if (methods[method]) {
			return methods[method].apply(this, Array.prototype.slice.call(arguments, 1));
		} else if (typeof method === 'object' || !method) {
			return methods.init.apply(this, arguments);
		} else {
			$.error('Method ' + method + ' does not exist on jQuery.' + name);
		}
	};

	var defaults = {
		showPercentage: true,
		fill: false,
		color: '#CCC',
		value: 0
	};

	var methods = {
		init: function (options) {

			var opts = $.extend(defaults, options);

			if (opts.value > 1)
				opts.value = 1;
			else if (opts.value < 0)
				opts.value = 0;

			return this.each(function () {

				var $this = $(this);
				var data = $this.data(name);

				if (!data) {
					$this.addClass(cssClasses.main);
					$this.css({
						fontSize: $this.width()
					});
					$this.data(name, opts);
					if (opts.showPercentage) {
						$this.find('.' + cssClasses.percent).show();
					}
					if (opts.fill) {
						$this.addClass(cssClasses.fill);
					}
					$(this)[name]('update');
				}
			});
		},
		getValue: function () {

			var $this = $(this);
			var data = $this.data(name);

			if (data)
				return data.value;
			else
				return null;
		},
		reset: function () {
			return this.each(function () {

				var $this = $(this);
				var data = $this.data(name);

				if (data) {
					data.value = percent;
					$this.data(name, data);

					$(this)[name]('update');
				}
			});
		},
		setValue: function (percent) {

			return this.each(function () {

				var $this = $(this);
				var data = $this.data(name);

				if (percent > 1)
					percent = 1;
				else if (percent < 0)
					percent = 0;

				if (data) {
					data.value = percent;
					$this.data(name, data);

					$(this)[name]('update');
				}
			});
		},
		update: function () {

			return this.each(function () {

				var $this = $(this);
				var data = $this.data(name);

				if (data) {
					var percent = data.value * 100;

					$this.html('<div class="' + cssClasses.percent + '"></div><div class="' + cssClasses.slice + (percent > 50 ? ' ' + cssClasses.gt50 : '') + '"><div class="' + cssClasses.pie + '"></div>' + (percent > 50 ? '<div class="' + cssClasses.pie + ' ' + cssClasses.fill + '"></div>' : '') + '</div>');
					var deg = 360 / 100 * percent;
					$this.find('.' + cssClasses.slice + ' .' + cssClasses.pie).css({
						'-moz-transform': 'rotate(' + deg + 'deg)',
						'-webkit-transform': 'rotate(' + deg + 'deg)',
						'-o-transform': 'rotate(' + deg + 'deg)',
						'transform': 'rotate(' + deg + 'deg)'
					});
					$this.find('.' + cssClasses.percent).html(Math.round(percent) + '%').hide();
					if (data.showPercentage) {
						$this.find('.' + cssClasses.percent).show();
					}
					if ($this.hasClass(cssClasses.fill)) {
						$this.find('.' + cssClasses.slice + ' .' + cssClasses.pie).css({ backgroundColor: data.color });
					}
					else {
						$this.find('.' + cssClasses.slice + ' .' + cssClasses.pie).css({ borderColor: data.color });
					}
				}
			});
		}
	};

})(jQuery);