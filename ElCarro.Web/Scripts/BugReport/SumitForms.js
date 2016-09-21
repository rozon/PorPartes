﻿$(function () {
    $('#BugReportForm').submit(function (e) {
        e.preventDefault(); //prevent the default action

        //grab the form and wrap it with jQuery
        var $form = $(this);

        //send your ajax request
        $.ajax({
            type: $form.prop('method'),
            url: $form.prop('action'),
            data: $form.serialize(),
            dataType: "json",
            traditional: true,
            success: function (response) {
                if (response.status === "error") {
                    for (var c = 0; c < response.errors.length; c++) {
                        var item = $("#" + response.errors[c].ID);
                        item.siblings("span:first").text(response.errors[c].messageError);
                    }
                } else {
                    //var modal = $('#bugs_report');
                    //modal.modal('hide');
                    Materialize.toast(response.message, 4000, 'rounded toast-bug-report');
                }
            },
            error: function (jqXHR, status, exception) {
                console.log(jqXHR);
                console.log(jqXHR.responseText);
            }
        });
    });
});