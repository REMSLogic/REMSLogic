

$(window).bind('content-loaded', function () {

    var $title = $('#wrapper > section > section > header');
    var $section = $($title).closest('section');

    $(window).bind('resize', function () {
        $title.width($section.width());
    });

    $(window).bind('scroll.dsq', function () {
        var sd = $(window).scrollTop();

        if ($section.offset() != null && sd > $section.offset().top) {
            $title.addClass('pinned').css('width', ($section.width()-2)+'px');
            $section.css('padding-top', $title.height()+'px');
        }
        else {
            $title.removeClass('pinned').css('width', 'auto');
            $section.css('padding-top', '0');
        }
    });

    setTimeout(function () {
        $('.form-input').each(function (idx, e) {

            var $e = $(e);
            var $lbl = $e.parent().children('.form-label');

            var h = $lbl.outerHeight();

            if (h == null)
                return;

            var hStr = h.toString() + 'px';

            $e.css('min-height', hStr);
        });

        $('.to-be-hidden').addClass('hidden').removeClass('to-be-hidden');

        $('.form .has-children .form-info').each(function () {

            var $this = $(this);

            updateChildContainer($this);
        });


    }, 100);

    $(".dsq-accordion").each(function (idx, e) {
        var $e = $(e);

        $e.addClass('accordion').accordion({
            header: 'header',
            heightStyle: 'content',
            active: 0
        });
    });

    var $vc = $('.versions-container');
    var $vt = $vc.find('.versions-table');
    var vt_shown = false;

    $vt.hide();

    $vc.find('h2.click-to-expand').click(function () {
        if (vt_shown)
            $vt.slideUp();
        else
            $vt.slideDown();

        vt_shown = !vt_shown;
    });
});

$(window).bind('content-unloaded', function () {
    var $title = $('#wrapper > section > section > header');
    var $section = $($title).closest('section');

    $section.css('padding-top', '0');
    $title.removeClass('pinned').css('width', 'auto');

    $(window).unbind('resize');
    $(window).unbind('scroll.dsq');
});


function updateChildContainer($this) {
	console.log($this.attr('name'));
	var values = [];

	var $contain = $this.parents('.form-row');
	var name = $this.attr('name');

	if (name.endsWith('[]')) {
		name = name.substr(0, name.length - 2);
	}

	$('.form [name|=' + name + ']').each(function () {
		values.push($(this).text());
	});

	var shown = false;

	$('.form [data-parent-id=' + $contain.attr('data-id') + ']').each(function () {
		$(this).hide();

		var Q = $(this).attr('data-parent-checks');

		if (Q == null || Q == '')
			return;

		var show = question_eval.evaluate(Q, values);

		if (show) {
			shown = true;
			$(this).show();
		}
	});

	var id = '#form-' + $this.attr('name') + '-children';

	if (shown) {
		$(id).show();
	} else {
		$(id).hide();
	}
}

if (typeof String.prototype.startsWith != 'function') {
	String.prototype.startsWith = function (str) {
		return this.slice(0, str.length) == str;
	};
}

if (typeof String.prototype.endsWith != 'function') {
	String.prototype.endsWith = function (str) {
		return this.slice(-str.length) == str;
	};
}

if (typeof Array.prototype.peek != 'function') {
	Array.prototype.peek = function () {
		return this[this.length - 1];
	}
}

var question_eval = {
	operators: ['(', ')', '&', '|'],
	evaluate: function (Q, values) {
		Q = question_eval.parseQ(Q);
		P = question_eval.convertToPostfix(Q);
		var stack = [];

		for (var i = 0; i < P.length; i++) {
			if (question_eval.operators.indexOf(P[i]) == -1) {
				stack.push(values.indexOf(P[i]) >= 0);
			}
			else {
				var v2 = stack.pop();
				var v1 = stack.pop();
				var v = null;

				if (P[i] == '&')
					v = v1 && v2;
				else if (P[i] == '|')
					v = v1 || v2;

				stack.push(v);
			}
		}

		return stack.pop();
	},
	convertToPostfix: function (Q) {
		var stack = [];

		var P = [];

		for (var i = 0; i < Q.length; i++) {
			if (question_eval.operators.indexOf(Q[i]) == -1) {
				P.push(Q[i]);
			}
			else if (Q[i] == "(") {
				stack.push(Q[i]);
			}
			else if (Q[i] == ")") {
				while (stack.peek() != '(')
					P.push(stack.pop());

				stack.pop();
			}
			else {
				if (stack.length == 0 || stack.peek() == '(')
					stack.push(Q[i]);
				else {
					while (stack.length != 0 && stack.peek() != '(')
						P.push(stack.pop());

					stack.push(Q[i]);
				}
			}
		}

		while (stack.length > 0)
			P.push(stack.pop());

		return P;
	},
	parseQ: function (Q) {
		var ret = [];
		var idx = 0;
		var ss = '';
		var inStr = false;

		while (idx < Q.length) {
			if (Q[idx] == '"') {
				if (inStr) {
					ret.push(ss);
					ss = '';
					inStr = false;
				}
				else {
					inStr = true;
				}
			}
			else if (Q[idx] == '\\') {
				idx++;
				ss = ss + Q[idx];
			}
			else if (question_eval.operators.indexOf(Q[idx]) != -1) {
				ret.push(ss);
				ret.push(Q[idx])
				ss = '';
			}
			else {
				ss = ss + Q[idx];
			}

			idx++;
		}

		if (ss != null && ss != '')
			ret.push(ss);

		var temp = ret;
		ret = [];

		for (var i = 0; i < temp.length; i++) {
			if (!temp[i] || temp[i].trim() == '')
				continue;
			ret.push(temp[i].trim());
		}

		return ret;
	}
};