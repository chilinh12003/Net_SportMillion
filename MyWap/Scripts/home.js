/// <reference path="jquery-2.1.1.min.js" />

$(document).ready(function ()
{
    $(".icon-menu").click(function ()
    {
        if ($(".menu").css("display") == "block")
        {
            $(".menu").css("display", "none");
        }
        else
        {
            $(".menu").css("display", "block");
        }
    });
});
