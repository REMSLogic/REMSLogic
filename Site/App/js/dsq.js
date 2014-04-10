

$(window).bind('content-loaded', function () {

	var $title = $('#wrapper > section > section > header');
	var $section = $($title).closest('section');

	$(window).bind('resize', function () {
		$title.width($section.width());
	});

	$(window).bind('scroll.dsq', function () {
		var sd = $(window).scrollTop();

		if ($section.offset() != null && sd > $section.offset().top) {
			$title.addClass('pinned').css('width', ($section.width() - 2) + 'px');
			$section.css('padding-top', $title.height() + 'px');
		}
		else {
			$title.removeClass('pinned').css('width', 'auto');
			$section.css('padding-top', '0');
		}
	});

	$('.auto-grow').autoGrow();

	$('.form :input').change(function () {
		$(this).addClass('unsaved');
	});

    $('a:not(.ajax-button)').click(function (event) {
        var $form = $('form');
        var $forms = $('.form .unsaved');

		if ($forms.length <= 0)
			return true;

        if ($(event.target).html() !== "Add Item") {
            return true;
        }

        if (confirm('You have unsaved changes.  Would you like to keep your changes?  Click "Ok" to keep them or "Cancel" to discard')) {
            $form.submit();
        }

        return true;
    });

	$('.form .has-children :input').change(function () {

		var $this = $(this);
		updateChildContainer($this);
	});

	$('.formulations-table').dataTable({
		"sPaginationType": "full_numbers",
		"bStateSave": true,
		"iCookieDuration": (60 * 60 * 24 * 30),
		"iDisplayLength": 1000
	});

	$('.versions-table').dataTable({
		"aaSorting": [[1, 'desc']],
		"sPaginationType": "full_numbers",
		"bStateSave": true,
		"iCookieDuration": (60 * 60 * 24 * 30),
		"iDisplayLength": 1000
	});

	$('.dsq-links-table').dataTable({
		"sPaginationType": "full_numbers",
		"bStateSave": true,
		"iCookieDuration": (60 * 60 * 24 * 30),
		"iDisplayLength": 1000
	});

	$('#btnNewVersion').click(function () {
		$("#dialog-confirm").dialog({
			resizable: false,
			height: 240,
			modal: true,
			buttons: {
				"Confirm": function () {
					$('#form-new-version').val('true');
					$('#form-message').val($('#modal-message').val());
					$(this).dialog("close");

					$('#frmEditDrug').submit();
				},
				"Cancel": function () {
					$(this).dialog("close");
				}
			}
		});
	});

	$('#btnApproveVersion').click(function () {
        $("#dialog-approve-confirm").dialog({
			resizable: false,
			height: 240,
			modal: true,
			buttons: {
				"Confirm": function () {
					$(this).dialog("close");

                    $.ajax('/api/Dev/DSQ/Drug/ApproveVersion', {
                        type: 'POST',
                        contentType: 'application/json',
                        data: JSON.stringify({
                            drug_id: $('#form-drug-id').val(),
                            drug_version_id: $('#form-drug-version-id').val(),
                            message: $('#modal-approve-message').val()
                        })
                    });
                },
                "Cancel": function () {
                    $(this).dialog("close");
                }
            }
        });
	});

	$('#btnDenyVersion').click(function () {
		$("#dialog-deny-confirm").dialog({
			resizable: false,
			height: 240,
			modal: true,
			buttons: {
				"Confirm": function () {
					$(this).dialog("close");

					$.ajax('/api/Dev/DSQ/Drug/DenyVersion', {
						type: 'POST',
						contentType: 'application/json',
						data: JSON.stringify({
							drug_id: $('#form-drug-id').val(),
							drug_version_id: $('#form-drug-version-id').val(),
							message: $('#modal-deny-message').val()
						})
					});
				},
				"Cancel": function () {
					$(this).dialog("close");
				}
			}
		});
	});

	$('.dsq-form').each(function () {
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

				if (name.endsWith('[]')) {
					name = name.substring(0, name.length - 2);

					if (typeof postData[name] === 'undefined' || postData[name] == null)
						postData[name] = [];

					postData[name].push(postArray[i].value);
				}
				else
					postData[name] = postArray[i].value;
			}

			var postDataTemp = { questions: [] };

			for (var k in postData) {
				if (k.startsWith('q-')) {
					var name = k.substring(2);
					var id = name;

					var idx = id.indexOf('-');

					if (idx != -1)
						id = id.substring(0, idx);

					postDataTemp.questions.push({
						id: id,
						value: postData[k]
					});
				}
				else
					postDataTemp[k] = postData[k];
			}

			postData = postDataTemp;

			$.ajax(action, {
				type: 'POST',
				contentType: 'application/json',
				data: JSON.stringify(postData),
				error: function (jqXHR, textStatus, errorThrown) {
				},
				success: function (data, textStatus, jqXHR) {
				}
			});

			return false;
		});
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
			$('div.selector, div.radiogroup, div.checkgroup', $e).each(function (idx, e2) {
				var $e2 = $(e2);
				$e2.css('min-height', hStr);

				var oh = $e2.outerHeight();
				if (oh > h) {
					var diff = oh - h;
					var nh = h - diff;

					if (nh !== null && typeof nh !== 'undefined')
						$e2.css('min-height', nh.toString() + 'px');
				}
			});
		});

		$('.to-be-hidden').addClass('hidden').removeClass('to-be-hidden');

		$('.form .has-children :input').each(function () {

			var $this = $(this);

			updateChildContainer($this);
		});

		$(".dsq-accordion").each(function (idx, e) {
			var $e = $(e);

			if (!default_section)
				default_section = 0;
			$e.addClass('accordion').accordion({
				header: 'header',
				heightStyle: 'content',
				active: default_section
			});
		});
	}, 100);
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
	var values = [];

	var $contain = $this.parents('.form-row');
	var name = $this.attr('name');

	if (name.endsWith('[]')) {
		name = name.substr(0, name.length - 2);
	}

	$('.form [name|=' + name + ']').each(function () {
		if (!$(this).is(':checkbox') && !$(this).is(':radio'))
			values.push($(this).val());
		else if ($(this).is(':checked'))
			values.push($(this).val());
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