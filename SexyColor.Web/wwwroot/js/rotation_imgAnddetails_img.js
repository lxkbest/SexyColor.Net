/*编辑图片回调函数*/
function success(data) {
    fnAjaxSuccessPrompt(data);
}
/*编辑错误回调函数*/
function error(data) {
    swal({
        title: '系统异常！',
        timer: 1500,
        showConfirmButton: false,
        type: 'error'
    });
}

/*添加图片错误回调函数*/
function addImgError() {
    swal({
        title: '系统异常！',
        timer: 1500,
        showConfirmButton: false,
        type: 'error'
    });
}
//添加商品成功后回调函数
function addImgSuccess(data) {
    fnAjaxSuccessPrompt(data);
}

function fnAjaxSuccessPrompt(data) {
    var msg = data.messageContent;
    var $operation = $('#ajaxDialog').data("operation");

    if (data.messageType == 1) {
        $operation.triggerHandler("click");
        swal({
            title: msg,
            timer: 1500,
            showConfirmButton: false,
            type: 'success'
        });
    } else {
        swal({
            title: msg,
            timer: 1500,
            showConfirmButton: false,
            type: 'error'
        });
    }
}

/*添加图片*/
var G_$imgBox = $("#jsImgBox");
var IMGCOUNT = 1;
G_$imgBox.on("change", ".jsImgInput", function () {
    IMGCOUNT++;
    var $img = $("<img />"),
        $close = $("<div class=\"fa fa-close text-danger _close jsSingleDelete \"></div>");
    imagePreview(this, $img[0]);
    $(this).closest(".jsImgInputBox").hide().closest(".jsAddImgprevBox").append($img, $close);
    fnCreateImgInput();
});

//创建图片add
function fnCreateImgInput() {
    var strVar = "";
    strVar += "<div class=\"inline-block jsAddImgprevBox img_prev_box\">\n";
    strVar += "	<div class=\"jsImgInputBox\">\n";
    strVar += "		<label for=\"" + IMGCOUNT + "\" class=\"add_label\">+<\/label>\n";
    strVar += "		<input id=\"" + IMGCOUNT + "\" class=\"jsImgInput\" name=\"addImgFiles\" type=\"file\" style=\"display:none;\" mulpitle/>\n";
    strVar += "	<\/div>\n";
    strVar += "<\/div>\n";
    G_$imgBox.append(strVar);
}

/* 添加-取消图片 */
$("#jsCancelAddImg").click(function () {
    $(".jsAddImgprevBox").not(":last-child").remove();
});

var G_editImg = $("#jsEditImg"),
    G_EditImgBox = $("#jsEditImgBox"),
    G_editImgInput = $("#editImgInput");
/*取消单张图片*/
G_$imgBox.on("click", ".jsSingleDelete", function () {
    $(this).parents(".jsAddImgprevBox").remove();
});

/*编辑*/
$(".jsEdit").click(function () {
    var imgUrl = $(this).prevAll("img").attr("src"),
        id = $(this).data("id"),
        number = $(this).data("number");
    G_editImg.attr("src", imgUrl);
    $("#hideId").val(id);
    $("#number").val(number);
    G_EditImgBox.show();
});
/*取消编辑*/
$("#jsCancelEditImg").click(function () {
    G_EditImgBox.hide();
    G_editImgInput.val("");
});

/*编辑预览图片*/
G_editImgInput.change(function () {
    imagePreview(this, G_editImg[0]);
});

/*删除图片*/
$(".jsDeleteEditImg").click(function () {
    var url = $(this).data("url");
    sweetAlert({
        title: "您确定要删除该张图片吗?",
        type: "warning",
        showCancelButton: true,
        closeOnConfirm: false
    }, function () {
        //确定后回调函数
        $.post({
            url: url,
            success: function (data) {
                fnAjaxSuccessPrompt(data);
            },
            error: function () {
                swal({
                    title: "系统异常！",
                    timer: 1500,
                    showConfirmButton: false,
                    type: 'error'
                });
            }
        });
    });
});

