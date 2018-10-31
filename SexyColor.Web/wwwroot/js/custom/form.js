(function ($) {
    $(document).ready(function () {
        //ajaxForm
        $('form[plugin="ajaxForm"]').livequery(function () {
            var data = parseObject($(this).attr("data"));

            data.beforeSubmit = function (arr, $form, options) {
                if ($.watermark) {
                    $form.find("input:text,textarea").each(function () {
                        $.watermark._hide($(this));
                    });
                }
                if (!$form.valid()) {
                    return false;
                }

                var beforeSubmitFn = eval(data.beforeSubmitFn);
                if (beforeSubmitFn)
                    beforeSubmitFn(arr, $form, options);
            };

            data.error = function (response, statusText, xhr, $form) {

                var errorFn = eval(data.errorFn);
                if (errorFn) {
                    errorFn(response.responseText, statusText, xhr, $form);
                }
            };

            data.success = function (response, statusText, xhr, $form) {

                var successFn = eval(data.successFn);
                if (successFn) {
                    $.ajaxSetup({ cache: false });
                    successFn(response, statusText, xhr, $form);
                }
            };
            $(this).ajaxForm(data);
        });


        $("button[url]").on("click", function () {
            var url = $(this).attr("url");
            if (url)
                window.location.href = url;
        });
        $("form").livequery(function () { $.validator.unobtrusive.parse(document); });



        /*Begin表单内容保存提示插件
        *表单有录入内容但未保存时，用户离开页面提示
        *调用示例：('form').enable_changed_form_confirm("您确定不保存就离开页面吗?");
        */
        //解决IE下 javascript:void(0)会触发window.onbeforeunload事件的问题
        $("a[href='javascript:void(0)']").on("click", function (e) {
            e.preventDefault();
        });
        $("a[href='javascript:void(0);']").on("click", function (e) {
            e.preventDefault();
        });
        $("a[href='javascript:;']").on("click", function (e) {
            e.preventDefault();
        });
        $.fn.enable_changed_form_confirm = function (prompt) {
            var _f = this;
            $('input:text,input:password, textarea', this).each(function () {
                $(this).attr('_value', $(this).val());
            });

            $('input:checkbox,input:radio', this).each(function () {
                var _v = this.checked ? 'on' : 'off';
                $(this).attr('_value', _v);
            });

            $('select', this).each(function () {
                $(this).attr('_value', this.options[this.selectedIndex].value);
            });

            $(this).submit(function () {
                window.onbeforeunload = null;
            });

            window.onbeforeunload = function () {
                if (is_form_changed(_f)) {
                    return prompt;
                }
            }
        }

        function is_form_changed(f) {
            var changed = false;
            $('input:text,input:password,textarea', f).each(function () {
                var _v = $(this).attr('_value');
                if (typeof (_v) == 'undefined') _v = '';
                if (_v != $(this).val()) changed = true;
            });

            $('input:checkbox,input:radio', f).each(function () {
                var _v = this.checked ? 'on' : 'off';
                if (_v != $(this).attr('_value')) changed = true;
            });

            $('select', f).each(function () {
                var _v = $(this).attr('_value');
                if (typeof (_v) == 'undefined') _v = '';
                if (_v != this.options[this.selectedIndex].value) changed = true;
            });
            return changed;
        }

    });
})(jQuery);


//转为对象类型
function parseObject(value) {
    if (value == null)
        return null;
    return eval("(" + value + ")");
}