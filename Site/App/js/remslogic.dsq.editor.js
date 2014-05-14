var REMSLogic = REMSLogic || {};

REMSLogic.DSQEditor = function ($) {
    var _$root;
    var _$textInput;
    
    var _id;
    var _originalText;

    var updateQuestion = function () {
        var newText = _$textInput.val();

        $.ajax({
            url: '/api/Dev/DSQ/Question/Update',
            type: 'POST',
            dataType: 'json',
            data: {
                id: _id,
                text: newText
            }
        }).success(function (data, textSTatus, jqXhr) {
            var $editButton = $('span[data-id="'+data.id+'"].edit-question-button');
            var $text = $editButton.siblings();

            $text.html(data.text);
            _$root.hide();

        }).fail(function (jqXhr, textStatus, errorThrown) {
            alert('Failed to save changes.  Try logging out and back in.');
        });
    };

    var insertEditor = function () {
        $('body').append(
            '<div id="dsq-editor">' +
                '<div><label for="question-text">Question:</label><input id="question-text" type="text" /></div>' +
                '<div class="controls">' +
                    '<input type="button" value="Save" data-action="save">' +
                    '<input type="button" value="Cancel" data-action="cancel">' +
                '</div>' +
            '</div>');

        $root = $('#dsq-editor');

        $questionText = $root.find('#question-text');
        $save = $root.find('input[data-action=save]');
        $cancel = $root.find('input[data-action=cancel]');

        $save.click(function () {
            updateQuestion();
        });

        $cancel.click(function () {
            $root.hide();
        });

        return $root;
    };

    var init = function (id, text) {
        _$textInput = _$root.find('#question-text');
        _originalText = text;
        _id = id;

        _$textInput.val(text);
    }

    var display = function (id, text) {
        _$root = $('#dsq-editor');

        if (_$root.length == 0) {
            _$root = insertEditor();
        }

        init(id, text);
        _$root.show();
    };

    return {
        display: display
    };
};