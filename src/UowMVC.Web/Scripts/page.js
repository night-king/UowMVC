function iframeload() {
    setTimeout(function () {
        document.getElementById("iframepage-load").style.display = "none";
        document.getElementById("iframepage-load-text").style.display = "none";
    }, 50);
}

function openPage(url, isMustSelected) {
    if (isMustSelected) {
        url += url.indexOf("?") > 0 ? "&id=" + selectedId : "?id=" + selectedId;
    }
    location.href = url;
}

function openDialog(a, title, url, w, h, isMustSelected) {
    $('#mod-window .modal-title').text(title);
    $('#mod-window .modal-body').height(h);
    $('#mod-window .modal-dialog').width(w);
    if (isMustSelected) {
        url += (url.indexOf("?") > 0 ? "&id=" + selectedId : "?id=" + selectedId) + "&ids=" + selectedIds;
    }
    var height = parseInt(h / 2) - 50;
    $('.modal-body').empty().append('<div align="center" style="width:99.9%;height:100%">' +
       '<img id="iframepage-load" src="../../Images/loading-small.gif" style="padding-top:' + height + 'px;display:block"/> <div id="iframepage-load-text"  style="margin-top:10px;display:block">加载中....</div>' +
       '<iframe id="iframepage" src="' + url + '"  onload="iframeload()" width="100%"  height="100%" style="background-color:transparent" frameborder="0" scrolling="auto" marginheight="0" marginwidth="0"> </iframe>' +
       '</div>');
    $('#mod-window').modal({
        keyboard: true,
    });
}

function closeDialog(a) {
    $(this.window.parent.document).find("#mod-window .close").click();
}
var selectedIdArray = [];
var selectedId;
var selectedIds;

$(function () {
    $('.single-select-table tr').click(function () {
        var tr = $(this);
        var itemid = $(this).attr("itemid");
        if (!itemid) {
            itemid = $(this).find("td").eq(0).text().trim();
        }
        $('.single-select-table tbody tr').removeClass("active").attr("state", 0);
        $('.single-select-table tbody tr').find('.btn[requireid=true]').attr("disabled", "disabled");
        $(tr).addClass("active").attr("state", 1);
        selectedId = itemid;
        $('.page-toolbars .btn[requireid=true]').removeAttr("disabled");
        $(tr).find('.btn').removeAttr("disabled");
        console.log(selectedId);
    });

    $(".multi-check-all").click(function (e) {
        var $input = $(this);
        var ischecked = $(this).is(":checked");
        if (ischecked) {
            $('.mult-select-table tbody tr td input').prop("checked", true);
            $(".mult-select-table tr").addClass("active");
            $(".mult-select-table tr").attr("state", 1);
            $(".mult-select-table tr").find('.btn').removeAttr("disabled");
            selectedIds = "";
            $.each($(".mult-select-table tbody tr"), function (i, item) {
                var itemid = $(item).attr("itemId");
                selectedIdArray.push(itemid);
            });
            jQuery.unique(selectedIdArray);
            for (var i = 0; i < selectedIdArray.length; i++) {
                selectedIds += selectedIdArray[i] + ",";
            }
            if (selectedIdArray.length > 0) {
                selectedId = selectedIdArray[0];
                $('#deleteSelected').removeAttr("disabled");
                $('.page-toolbars .btn[requireid=true]').removeAttr("disabled");
            } else {
                $('#deleteSelected').attr("disabled", "disabled");
                $('.page-toolbars .btn[requireid=true]').attr("disabled", "disabled");
            }
            console.log("selectedId:" + selectedIds);
        }
        else {
            $('.mult-select-table  tbody  tr  td  input').prop("checked", false);
            $(".mult-select-table tr").removeClass("active");
            $(".mult-select-table tr").removeAttr("state", 0);
            $(".mult-select-table tr").find('.btn').attr("disabled", "disabled");
            selectedIdArray = [];
            selectedIds = "";
            console.log("selectedId:" + selectedIds);
            if (selectedIdArray.length > 0) {
                selectedId = selectedIdArray[0];
                $('#deleteSelected').removeAttr("disabled");
                $('.page-toolbars .btn[requireid=true]').removeAttr("disabled");
            } else {
                $('#deleteSelected').attr("disabled", "disabled");
                $('.page-toolbars .btn[requireid=true]').attr("disabled", "disabled");
            }
        }
    });
    $('.mult-select-table tbody tr').click(function (e) {
        if ($(e.target).hasClass("btn")) {
            return;
        }
        var $input=$(this).find('input').eq(0);
        var ischecked = $input.is(":checked");
        var itemid = $(this).attr("itemid");
        if (!ischecked) {
            $input.attr("checked","checked");
            $(".mult-select-table tr[itemid=" + itemid + "]").addClass("active");
            $(".mult-select-table tr[itemid=" + itemid + "]").attr("state", 1);
            $(".mult-select-table tr[itemid=" + itemid + "]").find('.btn').removeAttr("disabled");
            selectedIdArray.push(itemid);
            selectedIds = "";
            jQuery.unique(selectedIdArray);
            for (var i = 0; i < selectedIdArray.length; i++) {
                selectedIds += selectedIdArray[i] + ",";
            }
            console.log("selectedId:" + selectedIds);
            if (selectedIdArray.length > 0) {
                selectedId = itemid;
                $('#deleteSelected').removeAttr("disabled");
                $('.page-toolbars .btn[requireid=true]').removeAttr("disabled");
            } else {
                $('#deleteSelected').attr("disabled", "disabled");
                $('.page-toolbars .btn[requireid=true]').attr("disabled", "disabled");
            }
        }
        else {
            $input.removeAttr("checked");
            $(".mult-select-table tr[itemid=" + itemid + "]").removeClass("active");
            $(".mult-select-table tr[itemid=" + itemid + "]").attr("state", 0);
            $(".mult-select-table tr[itemid=" + itemid + "]").find('.btn').attr("disabled","disabled");
            for (var i = 0; i < selectedIdArray.length; i++) {
                if (selectedIdArray[i] == itemid) {
                    selectedIdArray.splice(i, 1);
                    break;
                }
            }
            selectedIds = "";
            for (var i = 0; i < selectedIdArray.length; i++) {
                selectedIds += selectedIdArray[i] + ",";
            }
            if (selectedIdArray.length > 0) {
                selectedId = selectedIdArray[0];
                $('#deleteSelected').removeAttr("disabled");
                $('.page-toolbars .btn[requireid=true]').removeAttr("disabled");
            } else {
                $('#deleteSelected').attr("disabled", "disabled");
                $('.page-toolbars .btn[requireid=true]').attr("disabled", "disabled");
            }
            console.log("selectedId:" + selectedIds);
        }
    });
    $("#icon-window .ifa").click(function () {
        var iconDiv = $(window.document).find("." + icon_display_div);
        if (window.parent && (!iconDiv || iconDiv.length == 0)) {
            iconDiv = $(window.parent.document).find("." + icon_display_div);
        }
        if (window.frames[0] && (!iconDiv || iconDiv.length == 0)) {
            iconDiv = $(window.frames[0].document).find("." + icon_display_div);
        }
        if (!iconDiv) {
            return;
        }
        var icon = this.outerHTML;
        $(iconDiv).html(icon).val(icon);;
    });
})
var icon_display_div = "icon-display-div";
function openIconWindow() {
    var iconDiv = $(window.document).find("#icon-window");
    if (!iconDiv || iconDiv.length == 0) {
        iconDiv = $(window.parent.document).find("#icon-window");
    }

    if (!iconDiv) {
        return;
    }
    $(iconDiv).css("display", "block");
}

function closeIconWindow() {
    var iconDiv = $(window.document).find("#icon-window");
    if (!iconDiv || iconDiv.length == 0) {
        iconDiv = $(window.parent.document).find("#icon-window");
    }
    if (!iconDiv) {
        return;
    }
    $(iconDiv).css("display", "none");
}

function checkall(checkbox,table) {
    var ischecked = $(checkbox).is(":checked");
    if (ischecked) {
        $("#"+table).find('tbody > tr > td > input').prop("checked", true);
        $("#" + table).find("tr").addClass("active").attr("state", 1);
    }
    else {
        $("#" + table).find('tbody > tr > td > input').prop("checked", false);
        $("#" + table).find("tr").removeClass("active").attr("state", 0);
    }
}

function changeCheck(input, table) {
    var ischecked = $(input).is(":checked");
    if (ischecked) {
        $(input).parent().parent().addClass("active").attr("state", 1);
    }
    else {
        $(input).parent().parent().removeClass("active").attr("state", 0);
    }
}

function changeRowCheck(row, table) {
    var checkBox =  $(row).find(".checkbox").eq(0);
    var ischecked = $(checkBox).is(":checked");
    if (!ischecked) {
        $(checkBox).prop("checked", true);
        $(row).addClass("active").attr("state", 1);
    }
    else {
        $(checkBox).prop("checked", false);
        $(row).removeClass("active").attr("state", 0);
    }
}
