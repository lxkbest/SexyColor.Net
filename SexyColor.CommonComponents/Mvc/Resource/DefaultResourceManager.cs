using System;

namespace SexyColor.CommonComponents
{
    public class DefaultResourceManager : ResourceManager
    {
        const string LibraryPath = "~/lib";
        const string ScriptPath = "~/js";
        const string StylePath = "~/css";
        const string VendorsPath = "~/vendors";
        protected override void InitScript(Func<string, ResourceHelper> script)
        {

            script("jquery")
               .Include($"{LibraryPath}/jquery/dist/jquery.js", $"{LibraryPath}/jquery/dist/jquery.min.js");
 
            script("vendors-jquery")
               .Include($"{VendorsPath}/jquery/dist/jquery.js", $"{VendorsPath}/jquery/dist/jquery.min.js").RequiredAtFoot();

            script("livequery")
                .Include($"{ScriptPath}/liveQuery/jquery.livequery.js", $"{ScriptPath}/liveQuery/jquery.livequery.js").RequiredAtFoot();

            script("ajaxform")
                .Include($"{ScriptPath}/ajaxForm/jquery.form.js", $"{ScriptPath}/ajaxForm/jquery.form.min.js").RequiredAtFoot();

            script("validate")
                .Include($"{LibraryPath}/jquery-validation/dist/jquery.validate.js", $"{LibraryPath}/jquery-validation/dist/jquery.validate.min.js").RequiredAtFoot()
                .Include($"{LibraryPath}/jquery-validation-unobtrusive/jquery.validate.unobtrusive.js", $"{LibraryPath}/jquery-validation-unobtrusive/jquery.validate.unobtrusive.min.js").RequiredAtFoot();

            script("vendors-bootstrap")
               .Include($"{VendorsPath}/bootstrap/dist/js/bootstrap.js", $"{VendorsPath}/bootstrap/dist/js/bootstrap.min.js").RequiredAtFoot();
            script("vendors-fastclick")
               .Include($"{VendorsPath}/fastclick/lib/fastclick.js", $"{VendorsPath}/fastclick/lib/fastclick.js").RequiredAtFoot();
            script("vendors-nprogress")
               .Include($"{VendorsPath}/nprogress/nprogress.js", $"{VendorsPath}/nprogress/nprogress.js").RequiredAtFoot();
            script("custom")
                .Include($"{ScriptPath}/custom.js", $"{ScriptPath}/custom.min.js")
                .Include($"{ScriptPath}/custom/form.js", $"{ScriptPath}/custom/form.js")
                .Include($"{ScriptPath}/common.js", $"{ScriptPath}/common.js");

            script("vendors-bootstrap-table")
                .Include($"{VendorsPath}/bootstrap-table-develop/dist/bootstrap-table.js", $"{VendorsPath}/bootstrap-table-develop/dist/bootstrap-table.min.js")
                .Include($"{VendorsPath}/bootstrap-table-develop/dist/locale/bootstrap-table-zh-CN.js", $"{VendorsPath}/bootstrap-table-develop/dist/locale/bootstrap-table-zh-CN.min.js");

            script("vendors-iCheck")
                .Include($"{VendorsPath}/iCheck/icheck.min.js", $"{VendorsPath}/iCheck/icheck.min.js").RequiredAtFoot();

            script("vendors-bootstrap-datetime-picker")
                .Include($"{VendorsPath}/moment/min/moment-with-locales.min.js", $"{VendorsPath}/moment/min/moment-with-locales.min.js")
                .Include($"{VendorsPath}/bootstrap-datetime-picker/bootstrap-datetimepicker.min.js", $"{VendorsPath}/bootstrap-datetime-picker/bootstrap-datetimepicker.min.js").RequiredAtFoot();

            script("vendors-sweetalert")
               .Include($"{VendorsPath}/sweetalert/sweetalert.min.js", $"{VendorsPath}/sweetalert/sweetalert.min.js").RequiredAtFoot();

            script("Jcrop")
                .Include($"{LibraryPath}/Jcrop/js/Jcrop.min.js", $"{LibraryPath}/Jcrop/js/Jcrop.js");
        }

        protected override void InitStyle(Func<string, ResourceHelper> style)
        {
            

            style("vendors-bootstrap")
                .Include($"{VendorsPath}/bootstrap/dist/css/bootstrap.css", $"{VendorsPath}/bootstrap/dist/css/bootstrap.min.css").RequiredAtHead();

            style("vendors-font-awesome")
                .Include($"{VendorsPath}/font-awesome/css/font-awesome.css", $"{VendorsPath}/font-awesome/css/font-awesome.min.css").RequiredAtHead();

            style("vendors-nprogress")
                .Include($"{VendorsPath}/nprogress/nprogress.css", $"{VendorsPath}/nprogress/nprogress.css").RequiredAtHead();

            style("custom")
                .Include($"{StylePath}/custom.css", $"{StylePath}/custom.min.css")
                .Include($"{StylePath}/custom-extend.css", $"{StylePath}/custom-extend.css");


            style("vendors-bootstrap-table")
                .Include($"{VendorsPath}/bootstrap-table-develop/dist/bootstrap-table.css", $"{VendorsPath}/bootstrap-table-develop/distbootstrap-table.min.css");

            style("vendors-iCheck")
               .Include($"{VendorsPath}/iCheck/skins/flat/green.css", $"{VendorsPath}/iCheck/skins/flat/green.css").RequiredAtHead();

            style("vendors-bootstrap-datetime-picker")
               .Include($"{VendorsPath}/bootstrap-datetime-picker/bootstrap-datetimepicker.min.css", $"{VendorsPath}/bootstrap-datetime-picker/bootstrap-datetimepicker.min.css").RequiredAtHead();

            style("vendors-sweetalert")
               .Include($"{VendorsPath}/sweetalert/sweetalert.css", $"{VendorsPath}/sweetalert/sweetalert.css").RequiredAtHead();

            style("Jcrop")
                .Include($"{LibraryPath}/Jcrop/css/Jcrop.min.css", $"{LibraryPath}/Jcrop/css/Jcrop.css");
        }
    }
}
