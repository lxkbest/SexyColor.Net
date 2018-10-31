using System;
using SexyColor.CommonComponents;
using SexyColor.BusinessComponents;
using SexyColor.Infrastructure;
using Qiniu.Common;
using System.IO;
using System.Threading.Tasks;

namespace SexyColor.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //简单上传-测试通过
            //QiniuManager qiniu = new QiniuManager();
            //var savekey = "20170515/20170515151515.png";
            //qiniu.Scope = $"test:{savekey}";
            //qiniu.EasyUpload(@"C:\Users\Administrator\Desktop\CA73787285B22BE96F44314F4B718F7A.png", savekey);

            //简单上传（异步）-测试通过
            //QiniuManager qiniu = new QiniuManager();
            //var savekey = "20170515/20170515151515.png";
            //qiniu.Scope = $"test:{savekey}";
            //qiniu.EasyUploadAsync(@"C:\Users\Administrator\Desktop\CA73787285B22BE96F44314F4B718F7A.png", savekey);

            //字节上传-测试通过
            //QiniuManager qiniu = new QiniuManager();
            //var savekey = "20170515/20170515151515.jpg";
            //qiniu.Scope = $"test:{savekey}";
            //var localFile = @"C:\Users\Administrator\Desktop\IMG_1195.jpg";
            //byte[] data = File.ReadAllBytes(localFile);
            //qiniu.ByteUpload(data, savekey);

            //数据流上传-测试通过
            //QiniuManager qiniu = new QiniuManager();
            //var savekey = "20170515/201705151817.png";
            //qiniu.Scope = $"test:{savekey}";
            //qiniu.StreamUpload("http://common2.qyerstatic.com/login/project/login/images/web_login_bg.jpg", savekey);

            //刷新缓存-测试通过
            //QiniuManager qiniu = new QiniuManager();
            //qiniu.CdnRefresh("http://opzera7uv.bkt.clouddn.com/20170515/20170515151515.png");

            //大文件断点续传-测试通过
            //QiniuManager qiniu = new QiniuManager();
            //var savekey = "20170515/20170515151515.png";
            //qiniu.Scope = $"test:{savekey}";
            //qiniu.BigFileUpload(@"C:\Users\Administrator\Desktop\SUISDHFIUSDHIHBDBJOIDFJVOJHN.jpg", savekey);
            //qiniu.CdnRefresh("http://opzera7uv.bkt.clouddn.com/20170515/20170515151515.png");

            //var guid = Guid.NewGuid();
            //var result = Utility.GetDisplayByEnumMemberInfo(typeof(OrderPayType), OrderPayType.AliPay);
            //var result = Utility.GetDisplayByEnumMemberInfo(typeof(OrderPayType), Enum.Parse(typeof(OrderPayType), 1.ToString()));

            //订单号生成测试
            //for (int i = 0; i < 100; i++)
            //{
            //    var dateCode = DateTime.Now.ToString("yyMMdd");
            //    var resultCode = Utility.GenerateOrderNumber("10", "1679139677825".Substring("1679139677825".Length - 3, 3));
            //    Console.WriteLine(resultCode);
            //}

            //菜鸟裹裹API测试
            CaiNiaoManager cainiao = new CaiNiaoManager();
            Task<string> task = cainiao.ExecuteInstantQueryAPI("HTKY", "70576309191543");
            var s = task.Result;
            Console.WriteLine(task.Result);
            //Console.WriteLine("Hello World!");
            Console.Read();
        }
    }
}