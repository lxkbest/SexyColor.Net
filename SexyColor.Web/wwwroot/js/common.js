/**

*/
$(function () {

    /**/
    var G_$table = $(".jambo_table");

    /*table单机选中CheckBox*/
    //G_$table.find("tr").click(function () {
    //    var $check = $(this).find(".icheckbox_flat-green input.flat");
    //    $check.iCheck('toggle');
    //});

    //表单重置清空
    $('.jsEmpty').click(function () {
        var $form = $(this).parents('form');
        $form.find('input').not(':button, :submit, :reset, :hidden').val('');
        $form.find('select option[value=""]').prop("selected", true);
    });


    //全选
    $('.allCheck').on('ifClicked', function (event) {
        var items = document.getElementsByName('customCheck');
        for (var i = 0; i < items.length; i++) {
            if (items[i].type == 'checkbox') {
                if (this.checked)
                    $(items[i]).iCheck('uncheck');
                else {
                    $(items[i]).iCheck('check');
                }
            }
        }
    });

});//ready end


//校验至少选择一位用户 2017-5-3 10:44:11
function verify() {
    if ($("input[name='customCheck']:checked").length <= 0) {
        swal({
            title: '至少选择一位用户！',
            timer: 1500,
            showConfirmButton: false,
            type: 'error'
        });
        return false;
    } return true;
}

/*图片上传预览*/
function imagePreview(input, img) {
    var files = input.files;
    for (var i = 0; i < files.length; i++) {//预览新添加的图片
        var file = files[i];
        var imageType = /^image\//;
        if (!imageType.test(file.type)) {
            swal("请选择图片类型文件上传！", "", "error");
        } else if ((file.size / 1024 / 1024) > 2) {
            swal("请选择小于2M的图片！", "", "error");
        } else {
            img.file = file;
            var reader = new FileReader();
            reader.onload = (function (aImg) {
                return function (e) {
                    aImg.src = e.target.result;
                };
            })(img);
            reader.readAsDataURL(file);
        }
    }
}

/**
 * 全局ajax工具类
 */
var AjaxUtility = {};

/**
 * 加载分布页到modal模态框
 * @param {object} _this 当前被点击的元素
 * @param {string} modalClass 改变模态框大小的class
 */
AjaxUtility.loadPartialPageModal = function (_this, modalClass) {
    var url = $(_this).data("url"),
        modalClass = "modal-dialog ";

    arguments.length === 1 && (modalClass += "modal-lg");
    arguments.length === 2 && (modalClass += arguments[1].toString());

    $.ajax({
        method: "GET",
        url: url,
        success: function (data) {
            if (data.messageType <= 0) {
                swal(data.messageContent, "", "error");
                return;
            }
            $("#dialog").removeClass().addClass(modalClass).html(data);
            $('#ajaxDialog').modal('show');
        },
        error: function () {
            swal("系统异常", "", "error");
        }
    });

}



