$(document).ready(function () {
    $('#loading-div-background').hide();
});

$('#search-input').keyup(searchButtonDisabler);

$('#search-form :checkbox').each(function() {
    $(this).change(searchButtonDisabler);
});


$('#search-form').submit(function (e) {
    e.preventDefault();
});

$('#search-button').click(function () {
    $('#loading-div-background').show();
    $.ajax({
        type: 'POST',
        url: '/home/search',
        data: $('#search-form').serialize(),
        processData: true,
        success: function (data) {
            $('#results').html(data);
        }
    }).done(function () {
        $('#loading-div-background').hide();
    });
});

function checkSearchType(el) {
    $('#search-engines :input').prop('disabled', el.value === "offline");
    searchButtonDisabler();
}

function searchButtonDisabler() {
    if ($('#search-form :checkbox:checked').length > 0 || $('#radio-offline').is(':checked')) {
        $('#search-button').prop('disabled', $('#search-input').val() === "");
    } else {
        $('#search-button').prop('disabled', true);
    }
}
    
