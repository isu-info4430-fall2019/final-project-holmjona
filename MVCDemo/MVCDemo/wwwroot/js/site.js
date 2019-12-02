// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$("#filterText").keyup(function(e) {
    var tBox = $(this);
    var addy = tBox.data("url");
    var text = tBox.val();
    $.ajax({
        url: addy
        ,method: "POST"
        , data: { searchtext: text }
        , success: function(rdata) {
            var lst = $("<ul>");
            //var lst = document.createElement("ul");
            $.each(rdata.data, function(ndx, obj) {
                var li = $("<li>").text(obj.text + " - " + obj.id)
                    .data("id", obj.id).data("text", obj.text);
                li.click(function() {
                    var btn = $(this);
                    $("#filterID").val(btn.data("id"));
                    $("#filterText").val(btn.data("text"));
                });
                lst.append(li);
            });
            $("#results").empty().append(lst);
        }
    });
});


$("#btnFilter").click(function() {
    var obj = $(this);
    var idToFind = parseInt($("#filterID").val());
    if (!isNaN(idToFind) && idToFind > 0) {
        var action = obj.data("action");
        var actionParts = action.split("|");
        var path = actionParts[0] + "/GetFor" + actionParts[1];
        $.ajax({
            url: path
            , method: "POST"
            , data: { id: idToFind }
            , success: function(data) {
                $("table tbody").empty().append(data);
            }
        });
    }
    return false;
});

$("#loginForm").mouseenter(function () {
    var frm = $(this).find(".form");
    frm.show(200);
    fillUserName();
});
$("#loginForm").mouseleave(function () {
    var frm = $(this).find(".form");
    frm.hide(200);
});

$("#loginForm form").submit(function () {
    setUserName();
    //return false;
});

function fillUserName() {
    if (localStorage) {
        if (typeof localStorage.username !== "undefined") {
            $("#txtUserName").val(localStorage.username);
            $("#chkRememberMe").prop("checked",true);
        }
    }
}

function setUserName() {
    if (localStorage) {
        var chk = $("#chkRememberMe");
        if (chk.is(":checked")) {
            localStorage.username = $("#txtUserName").val();
        } else {
            localStorage.removeItem("username");
        }
    }
}