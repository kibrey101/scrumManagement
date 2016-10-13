$(document).ready(function () {
    $("#checkSprint").prop("checked", false);
    $(".projectSprintDD").prop('disabled', false);
    $("#checkSprint").change(function () {
        if ($(this).is(':checked')) {
            $(".projectSprintDD").prop('disabled', true);
            $(".projectSprintDD").val('');
        }
        else {
            $(".projectSprintDD").prop('disabled', false);
           
        }
       
    });
});