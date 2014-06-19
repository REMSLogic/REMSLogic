

$(function () {
    $("#available-drugs").sortable({
        connectWith: ".connectedDrugSelection",
        start: function (event, ui) {
            ui.item.startPos = ui.item.index();
        },
        stop: handleDrop
    }).disableSelection();

    $("#selected-drugs").sortable({
        connectWith: ".connectedDrugSelection",
        start: function (event, ui) {
            ui.item.startPos = ui.item.index();
        },
        stop: handleDrop
    }).disableSelection();

    $('#available-drugs-filter').keyup(function () {
        UpdateAvailFilter();
    });

    $('#my-drugs-filter').keyup(function () {
        UpdateMyFilter();
    });

    $('.eoc-filter-avail img').click(function () {
        $(this).toggleClass('off');
        UpdateAvailFilter();
    }).each(function () {
        $(this).addClass('off');
    });

    $('.eoc-filter-my img').click(function () {
        $(this).toggleClass('off');
        UpdateMyFilter();
    }).each(function () {
        $(this).addClass('off');
    });

    $('.addDrugList').on('click', 'i', function () {
        $drug = $(this);
        var id = parseInt($drug.attr('id').toString().substring(3));
        $listItem = $(eval('drug' + id.toString()));

        if ($drug.hasClass("myDrug-remove")) { //Remove From My Drugs
            if (id) {
                $.ajax({
                    url: "/api/Common/DrugList/RemoveDrugFromList",
                    type: 'POST',
                    dataType: 'json',
                    data: { 'id': id },
                    success: function (response) {
                        $drug.toggleClass("fa-plus-circle fa-times-circle");
                        $drug.toggleClass("myDrug-remove selectDrug-add");
                        $listItem.appendTo('#available-drugs');
                    }
                });
            }
        }
        else if ($drug.hasClass("selectDrug-add")) { //Add to My Drugs
            if (id) {
                $drug.toggleClass("fa-plus-circle fa-times-circle");
                $drug.toggleClass("myDrug-remove selectDrug-add");
                $listItem.appendTo('#selected-drugs');
                $index = $listItem.index();
                $.ajax({
                    url: "/api/Common/DrugList/AddDrugToList",
                    type: 'POST',
                    dataType: 'json',
                    data: {
                        'id': id
                    }
                });
            }
        }
    });
});



function UpdateAvailFilter() {
    var v = $('#available-drugs-filter').val().toLowerCase();

    $('#available-drugs li').show().each(function () {
        var $this = $(this);

        var drug = $('span.drugName', $this);

        if (drug.html().toLowerCase().indexOf(v) == -1)
            $this.hide();
    });

    $('.eoc-filter-avail img:not(.off)').each(function () {
        $('#available-drugs li:not([data-' + $(this).attr('data-eoc') + '])').hide();
    });
}

function UpdateMyFilter() {
    var v = $('#my-drugs-filter').val().toLowerCase();

    $('#selected-drugs li').show().each(function () {
        var $this = $(this);

        var drug = $('span.drugName', $this);

        if (drug.html().toLowerCase().indexOf(v) == -1)
            $this.hide();
    });

    $('.eoc-filter-my img:not(.off)').each(function () {
        $('#selected-drugs li:not([data-' + $(this).attr('data-eoc') + '])').hide();
    });
}

function ClearAvailDrugListFilter() {
    $('.eoc-filter-avail img').each(function () { $(this).addClass('off'); });
    $('#available-drugs-filter').val('');
    UpdateAvailFilter();
}

function ClearMyDrugListFilter() {
    $('.eoc-filter-my img').each(function () { $(this).addClass('off'); });
    $('#my-drugs-filter').val('');
    UpdateMyFilter();
}

function handleDrop(event, ui) {
    var $from = $(this);
    var $to = $(ui.item[0].parentElement);

    if ($from.attr('id') == $to.attr('id') && $from.attr('id') == 'available-drugs')
        return;

    console.log(event);
    console.log(ui);

    $('#my-drugs-filter').val('').keyup();

    var drugId = $(ui.item[0]).attr('data-drug-id');
    $drug = $(ui.item[0].children[0].children[0]);
    //alert($drug.html());
    //alert($(ui.item[0].children[0]));
    if ($from.attr('id') == 'selected-drugs' && $to.attr('id') == 'selected-drugs')		// Reordering Drugs
    {
        $.ajax({
            url: "/api/Prescriber/Drug/Reorder",
            type: 'POST',
            dataType: 'json',
            data: {
                'id': drugId,
                'fromPosition': ui.item.startPos + 1,
                'toPosition': ui.item.index() + 1
            }
        });
    }
    else if ($to.attr('id') == 'available-drugs')										// Removing Drug
    {
        $drug.toggleClass("fa-plus-circle fa-times-circle");
        $drug.toggleClass("myDrug-remove selectDrug-add");
        //console.log('Removing drug with ID ' + drugId.toString());
        $.ajax({
            url: "/api/Common/DrugList/RemoveDrugFromList",
            type: 'POST',
            dataType: 'json',
            data: {
                'id': drugId
            }
        });
    }
    else if ($to.attr('id') == 'selected-drugs')										// Adding Drug
    {
        $drug.toggleClass("fa-plus-circle fa-times-circle");
        $drug.toggleClass("myDrug-remove selectDrug-add");
        //console.log('Adding drug with ID ' + drugId.toString());
        $.ajax({
            url: "/api/Common/DrugList/AddDrugToList",
            type: 'POST',
            dataType: 'json',
            data: {
                'id': drugId
            }
        });
    }
}