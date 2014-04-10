// HTML5 placeholder plugin version 0.3
// Enables cross-browser* html5 placeholder for inputs, by first testing
// for a native implementation before building one.
//
// USAGE:
//$('input[placeholder]').placeholder();

(function ($) {

	$.fn.placeholder = function (options) {
		return this.each(function () {
			if (!("placeholder" in document.createElement(this.tagName.toLowerCase()))) {
				var $this = $(this);
				var placeholder = $this.attr('placeholder');
				!$this.val() && $this.val(placeholder).data('color', $this.css('color')).css('color', '#aaa');
				$this
          .focus(function () { if ($.trim($this.val()) === placeholder) { $this.val('').css('color', $this.data('color')) } })
          .blur(function () { if (!$.trim($this.val())) { $this.val(placeholder).data('color', $this.css('color')).css('color', '#aaa'); } });
			}
		});
	};
})(jQuery);


if (Object.defineProperty) {
	Object.defineProperty(Array.prototype, "remove", {
		enumerable: false,
		value: function (item) {
			for (var i = 0; i < this.length; i++) {
				if (this[i] === item) {
					this.splice(i, 1);
					return item;
				}
			}

			return false;
		}
	});
}
else {
	// Removes first instance of item
	Array.prototype.remove = function (item) {
		for (var i = 0; i < this.length; i++) {
			if (this[i] === item) {
				this.splice(i, 1);
				return item;
			}
		}

		return false;
	};
}

// detect if browser supports transition, currently checks for webkit, moz, opera, ms
var cssTransitionsSupported = false;
(function () {
	var div = document.createElement('div');
	div.innerHTML = '<div style="-webkit-transition:color 1s linear;-moz-transition:color 1s linear;-o-transition:color 1s linear;-ms-transition:color 1s linear;-khtml-transition:color 1s linear;transition:color 1s linear;"></div>';
	cssTransitionsSupported = (div.firstChild.style.webkitTransition !== undefined) || (div.firstChild.style.MozTransition !== undefined) || (div.firstChild.style.OTransition !== undefined) || (div.firstChild.style.MsTransition !== undefined) || (div.firstChild.style.KhtmlTransition !== undefined) || (div.firstChild.style.Transition !== undefined);
	delete div;
})();

var validation_rules = {
	time: {
		selector: "[type=time]",
		fun: function (value, element, values) {
			return (/^\d\d:\d\d$/).test(value);
		},
		l10n: {
			en: "Please supply a valid time"
		}
	},
	equals_other: {
		selector: "[equalTo]",
		fun: function (value, element, values) {
			var name = element.attr("equalTo"),
				field = element.parents('form').children(':input[name=' + name + ']');
			return input.val() === field.val() ? true : [name];
		},
		ignore: {
			'jquery.validate': true
		}
	},
	minlength: {
		selector: "[minlength]",
		fun: function (value, element, values) {
			var min = element.attr("minlength");

			return value.length >= min ? true : {
				en: "Please provide at least " + min + " character" + (min > 1 ? "s" : "")
			};
		},
		ignore: {
			'jquery.validate': true
		}
	}
};

// perform JavaScript after the document is scriptable.
$(document).ready(function () {

	for (var n in validation_rules) {
		var item = validation_rules[n];
		if (!item.selector) {
			if (window.debug)
				console.log('No selector given for validation rule [' + n + '].');

			continue;
		}

		if ($.tools) {
			if (item.ignore && item.ignore['jquery.tools'])
				continue;

			var l10n = null;

			if (item.l10n && item.l10n.en)
				l10n = item.l10n.en;

			if (item.fun)
				$.tools.validator.fn(item.selector, l10n, function (input, value) {
					item.fun(value, input);
				});

			if (item.l10n)
				$.tools.validator.localizeFn(item.selector, item.l10n);
		}
		else if ($.validator) {
			if (item.ignore && item.ignore['jquery.validate'])
				continue;

			var l10n = 'Invalid value.';

			if (item.l10n && item.l10n.en)
				l10n = item.l10n.en;

			if (item.fun) {
				$.validator.addMethod(n, item.fun, l10n);
			}
		}
	}

	$().UItoTop({ easingType: 'easeOutQuart' });

	$(window).bind('load resize', function () {
		var section = $('#wrapper > section > section');
		if (section.css('position') == 'absolute' && section.css('left') != 0) {
			if (location.hash != '#menu') {
				section.css('left', 0);
			} else {
				section.css('left', '100%');
			}
		} else {
			section.show();
		}
	});

	if (!location.href.match(/App\/Login(\.aspx)?$/i) && (!location.hash || location.hash == '#menu')) {
		location.hash = $('.drilldownMenu .current a').attr("href");
	} else {
		$(window).trigger("hashchange");
	}

	$("#wrapper > section > aside > nav").length && $("#wrapper > section > aside > nav").each(function () {
		var base = $(this);
		$(this).drillDownMenu();
	});

	$('.showmenu').click(function () {
		$('#wrapper > section > section').animate({ left: "100%" }, 300, "easeInOutQuart", function () {
			$(this).hide();
		});
	});

	var target = ".login-box";

	$('input[placeholder]', target).placeholder();

	if (!Modernizr.inputtypes.date) {
		$("input[type=date]", target).each(function () {

			var $this = $(this);
			var val = null;

			if ($this.val())
				val = $this.val();

			$(this).datepicker({
				dateFormat: 'm/d/yy',
				selectors: true,
				changeMonth: true,
				changeYear: true,
				val: val
			});
		});
	}

	if ($.fn.uniform)
		$("input:checkbox,input:radio,select,input:file", target).uniform();

	if ($.tools && $.fn.validator) {
		/**
		* setup the validators
		*/
		$(".has-validation", target).validator({
			position: 'bottom left',
			offset: [5, 0],
			messageClass: 'form-error',
			message: '<div><em/></div>'// em element is the arrow
		}).attr('novalidate', 'novalidate');
	}
	else if ($.validator) {
		$('.has-validation', target).validate({
			errorClass: 'invalid'
		});
	}

	$(document).ajaxComplete(function (e, xhr, opts) {
		var ct = xhr.getResponseHeader('Content-Type');

		if (xhr.statusText === 'abort')
			return;

		if (xhr.status != 200 && window.growl_create) {
			window.growl_create("sticky-container", { title: 'Error', text: 'An error has occured while processing your request. Please try again later.' }, { expires: false, speed: 500 });
		}
		if (ct == 'application/json' || ct == 'application/json; charset=utf-8') {
			var data = JSON.parse(xhr.responseText);

			if (!data || (data.Error && !data.Growl)) {
				var msg = 'An error has occured while submitting your data. Please try again later.';
				if (data && data.Message)
					msg = data.Message;

				window.growl_create("sticky-container", { title: 'Error', text: msg }, { expires: false, speed: 500 });
				return;
			}
			else if (data.Error && data.Growl) {
				window.growl_create(data.Growl.Type + '-container', data.Growl.Vars, data.Growl.Opts);
				return;
			}
			else if (data.Growl)
				window.growl_create(data.Growl.Type + '-container', data.Growl.Vars, data.Growl.Opts);

			if (data.Actions && data.Actions.length > 0) {
				for (var i = 0; i < data.Actions.length; i++) {
					var a = data.Actions[i];
					if (a.Type == 'remove')
						$(a.Ele).remove();
					else if (a.Type == 'show')
						$(a.Ele).show();
					else if (a.Type == 'hide')
						$(a.Ele).hide();
					else if (a.Type == 'enable')
						$(a.Ele).removeClass('disabled');
					else if (a.Type == 'back')
						history.go(-1);
					else if (a.Type == 'reset-unsaved')
						$('.unsaved').removeClass('unsaved');
				}
			}

			if (data.Redirect) {
				if (data.Redirect.Hash)
					window.location.hash = data.Redirect.Hash;
				if (data.Redirect.Url)
					window.location.href = data.Redirect.Url;
			}
		}
	});
});

$(window).bind('drilldown', function () {
	var target = "#main-content";

	$(".tabs > ul", target).tabs({

	});
	$(".accordion", target).accordion({
		header: 'header',
		heightStyle: 'content',
		active: 0
	});

	$('input[placeholder]', target).placeholder();

	if (!Modernizr.inputtypes.date) {
		$("input[type=date]", target).each(function () {
			var $this = $(this);
			var val = null;

			if ($this.val())
				val = $this.val();

			$(this).datepicker({
				dateFormat: 'm/d/yy',
				selectors: true,
				changeMonth: true,
				changeYear: true,
				val: val
			});
		});
	}

	$("input[type=file]", target).each(function () {
		var $this = $(this);
		var p = $this.parents('.form-input');

		var formData = {};
		if ($this.attr('data-form-data'))
			formData = JSON.parse($this.attr('data-form-data'));

		formData.type = $this.attr('data-type');
		$this.fileupload({
			dataType: 'json',
			url: '/api/Admin/Files/Upload',
			formData: formData,
			done: function (e, data) {
				if ($this.attr('data-update-field'))
					$('#' + $this.attr('data-update-field')).val(data.result.Result);

				if ($this.attr('data-callback') && window[$this.attr('data-callback')])
					window[$this.attr('data-callback')](data);
			}
		});
	});

	$("input:checkbox,input:radio,select,input:file", target).uniform();

	/**
	* fix uniform uploader
	*/
	$('.uploader .filename', target).click(function () {
		$('input:file', $(this).parent()).click();
	});

	/**
	* setup the validators
	*/
	if ($.tools) {
		$(".has-validation", target).validator({
			position: 'bottom left',
			offset: [5, 0],
			messageClass: 'form-error',
			message: '<div><em/></div>'// em element is the arrow
		}).attr('novalidate', 'novalidate');
	}
	else if ($.validator) {
		$('.has-validation', target).validate({
			errorClass: 'invalid'
		});
	}

	/**
	* setup messages
	*/
	$('.message.closeable').each(function () {
		var message = $(this);
		message.prepend('<span class="message-close"></span>');
		$('.message-close', message).click(function () {
			message.fadeOut();
		});
	});

	/**
	* Setup tooltips // Not used atm
	*/
	/*
	if ($.fn.tooltip)
	{
	$('.has-tooltip').tooltip({
	effect: 'slide', offset: [-14, 0], position: 'top center', layout: '<div><em/></div>',
	onBeforeShow: function () {
	this.getTip().each(function () {
	if ($.browser.msie) {
	PIE.attach(this);
	}
	});
	},
	onHide: function () {
	this.getTip().each(function () {
	if ($.browser.msie) {
	PIE.detach(this);
	}
	});
	}
	}).dynamic({
	bottom: { direction: 'down', bounce: true }
	});
	}*/

	$('.back-button').click(function () {
		history.go(-1);
		return false;
	});

	$('.ajax-button', target).each(function () {
		var $this = $(this);

		$this.click(function () {
			var action = $(this).attr('href');
			var confirmTxt = $(this).attr('data-confirmtext');

			if (confirmTxt && confirmTxt != '') {
				var a = confirm(confirmTxt);
				if (a) {
					$.get(action, {}, function () {
						//$(window).trigger('hashchange');
					});
				}
			}
			else {
				$.get(action, {}, function () {
					//$(window).trigger('hashchange');
				});
			}

			return false;
		});
	});

	$('.ajax-form', target).each(function () {
		var $this = $(this);

		$this.submit(function () {
			if ($(this).is('.has-validation') && !$(this).valid())
				return false;

			if ($('input.file-id[type=hidden][value=""]', this).length > 0) {
				window.growl_create("sticky-container", { title: 'Error', text: 'Not all files have been uploaded. Please try again in a few seconds.' }, { expires: false, speed: 500 });
				return false;
			}
			var action = $(this).attr('action');
			var postArray = $(this).serializeArray();
			var postData = {};

			for (var i = 0; i < postArray.length; i++) {
				var name = postArray[i].name;

				if (name.substring(name.length - 2) == '[]') {
					name = name.substring(0, name.length - 2);

					if (postData[name] == null)
						postData[name] = [];
					else if (typeof (postData[name]) != 'object') {
						console.log('[ERROR] Invalid post data! Post value "' + name + '" already contains a value, and it should be an array!');
						return false;
					}

					postData[name].push(postArray[i].value);
				}
				else {
					postData[name] = postArray[i].value;
				}
			}

			$.ajax(action, {
				type: 'POST',
				data: JSON.stringify(postData),
				processData: false,
				contentType: 'application/json',
				error: function (jqXHR, textStatus, errorThrown) {
				},
				success: function (data, textStatus, jqXHR) {
				}
			});

			return false;
		});
	});

	if ($.fn.iTextClear) {
		$("input[type=text], input[type=password], input[type=url], input[type=email], input[type=number], textarea", '.form').each(function () {
			var $this = $(this);

			if ($this.parents('.dataTables_wrapper').length > 0)
				return;

			$this.iTextClear();
		});
	}
});

$(window).bind("hashchange", function (e) {

	if (window.location.href.indexOf('Login.aspx') !== -1)
		return;

	h = location.hash;
	if (h && h != '#menu') {
		var qs = "";
		if (h.indexOf('?') >= 0) {
			qs = '&' + h.substr(h.indexOf('?') + 1);
			h = h.substr(0, h.indexOf('?'));
		}
		h = h.replace(/^\#/, "");
		if (h == 'undefined')
			return;
		link = "/App/Page.aspx?v=" + h + qs,
        id = (h + qs).replace(/[\/\.&=]+/gi, "-");
		$('#' + id).length && $('#' + id).remove();
		$.ajax(link, {
			/*dataType: "html",*/
			cache: false,
			success: function (data, textStatus, jqXHR) {
				var loc = jqXHR.getResponseHeader('Location');
				if (loc)
					console.log('Location = [' + loc + ']');

				if (typeof (data) == "object")
					return;

				return pageDownloaded(data, id);
			},
			complete: function (jqXHR, textStatus) {
			}
		});
	}
});

function pageDownloaded(data, id) {
	if ($.fn.dataTable) {
		var tables = $.fn.dataTable.fnTables();
		if (tables.length > 0) {
			for (var i = 0; i < tables.length; i++)
				$(tables[i]).dataTable().fnDestroy();
		}
	}

	$(window).trigger('content-unloaded').unbind('content-unloaded');

	var target = "#main-content", div = $('<div style="left: 100%" id="' + id + '">' + data + '</div>').appendTo($(target));
	title = $(div).find("h1.page-title").html();
	$("#wrapper > section > section > header h1").html(title);

	if ($('#wrapper > section > section').css('position') == 'absolute') {
		$("> div:last", target).css({ left: 0, position: 'relative' }).siblings().remove();
		$('#wrapper > section > section').show().animate({ left: 0 }, 300, "easeInOutQuart", function () { $(this).css('left', 0); });
	} else {
		$("> div", target).animate({ left: "-=100%" }, "slow", "easeInOutQuart", function () {
			$(this).css('left', 0);
			$("> div:last", target).css({ position: 'relative' }).siblings().remove();
		});
	}

	$(window).trigger('drilldown').trigger('content-loaded').unbind('content-loaded');
}