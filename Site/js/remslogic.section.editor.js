var REMSLogic = REMSLogic || {};

REMSLogic.SectionEditor = function ($) {
    var updateSection = function (sectionId, drugId, isEnabled) {
        var $editButton = $('span[data-section-id="' + sectionId + '"].disable-section-button');
        var $editLabel = $editButton.siblings();

        $.ajax({
            url: '/api/Dev/DSQ/SectionSettings/Update',
            type: 'POST',
            dataType: 'json',
            data: {
                sectionId: sectionId,
                drugId: drugId,
                isEnabled: $editButton.html() === 'Enable'
            }
        }).success(function (data, textSTatus, jqXhr) {
            $editButton.html(data.isEnabled ? 'Disable' : 'Enable');

            if (data.isEnabled) {
                $editLabel.removeClass('form-label-hidden-section');
            }
            else {
                $editLabel.addClass('form-label-hidden-section');
            }

        }).fail(function (jqXhr, textStatus, errorThrown) {
            alert('Failed to save changes.  Try logging out and back in.');
        });
    };

    return {
        updateSection: updateSection
    };
}