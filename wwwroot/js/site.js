// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(() => {
    $("#reservation").hide();
    $("#reservation-line").hide();
    $("#btn-reserve").click(() => {
        $("#reservation").show();
        $("#reservation-line").show();
        $("#btn-reserve").hide();
    });
    $("#cancel-reservation").click(() => {
        $("#btn-reserve").show();
        $("#reservation").hide();
        $("#reservation-line").hide();
    });
})
