using Qiniu.CDN;
using Qiniu.CDN.Model;
using Qiniu.Common;
using Qiniu.Http;
using Qiniu.IO;
using Qiniu.IO.Model;
using Qiniu.Util;
using System;
using System.IO;
using System.Net;

namespace SexyColor.Infrastructure
{
    /// <summary>
    /// 七牛数据存储管理
    /// </summary>
    public class QiniuManager
    {
        private readonly string AK = "ZVywTHmDbjSKDg-JdJ4lq8_2RPl-3SD-R3DCYi-w";
        private readonly string SK = "bG65S-znJgltTSRBvIfq5xyq7ub7rwVCvhw_lal0";
        private string scope = "test";
        private static bool paused = false;

        public string Scope { get => scope; set => scope = value; }

        public QiniuManager()
        {
            //默认华南区
            Config.SetZone(ZoneID.CN_South, false);
        }

        public QiniuManager(string bucket)
        {
            Config.AutoZone(AK, bucket, false);
        }

        public string GenerateUploadToken(string json)
        {
            Mac mac = new Mac(AK, SK);
            string token = Auth.CreateUploadToken(mac, json);
            return token;
        }


        public string InitPolicyByBucket(string bucket, string saveKey = "", int expires = 3600, int delDays = 0)
        {
            PutPolicy putPolicy = new PutPolicy();
            // 如果需要设置为"覆盖"上传(如果云端已有同名文件则覆盖)，请使用 SCOPE = "BUCKET:KEY"
            if (!string.IsNullOrWhiteSpace(saveKey))
                putPolicy.Scope = bucket + ":" + saveKey;
            else
                putPolicy.Scope = bucket;
            // 上传策略有效期(对应于生成的凭证的有效期) 
            putPolicy.SetExpires(expires);
            // 上传到云端多少天后自动删除该文件，如果不设置（即保持默认默认）则不删除
            if (delDays > 0)
                putPolicy.DeleteAfterDays = delDays;

            return putPolicy.ToJsonString();
        }

        public string InitPolicyByScope(string scope, int expires = 3600, int delDays = 0)
        {
            PutPolicy putPolicy = new PutPolicy();
            putPolicy.Scope = scope;
            // 上传策略有效期(对应于生成的凭证的有效期) 
            putPolicy.SetExpires(expires);
            // 上传到云端多少天后自动删除该文件，如果不设置（即保持默认默认）则不删除
            if (delDays > 0)
                putPolicy.DeleteAfterDays = delDays;

            return putPolicy.ToJsonString();
        }

        /// <summary>
        /// 简单上传(同步)
        /// </summary>
        /// <param name="localFile"></param>
        /// <param name="saveKey"></param>
        /// <param name="putPolicy"></param>
        public void EasyUpload(string localFile, string saveKey, PutPolicy putPolicy = null)
        {
            var json = InitPolicyByScope(scope);
            UploadManager um = new UploadManager();

            if (putPolicy != null)
                json = putPolicy.ToJsonString();
            HttpResult result = um.UploadFile(localFile, saveKey, GenerateUploadToken(json));

            Console.WriteLine(result);
        }


        /// <summary>
        /// 简单上传(异步)
        /// </summary>
        public async void EasyUploadAsync(string localFile, string saveKey, PutPolicy putPolicy = null)
        {
            var json = InitPolicyByScope(scope);
            UploadManager um = new UploadManager();

            if (putPolicy != null)
                json = putPolicy.ToJsonString();
            HttpResult result = await um.UploadFileAsync(localFile, saveKey, GenerateUploadToken(json));
            Console.WriteLine(result);
        }

        /// <summary>
        /// 字节上传
        /// </summary>
        /// <param name="localFile"></param>
        /// <param name="saveKey"></param>
        /// <param name="putPolicy"></param>
        public void ByteUpload(byte[] data, string saveKey, PutPolicy putPolicy = null)
        {
            var json = InitPolicyByScope(scope);
            FormUploader fu = new FormUploader();
            if (putPolicy != null)
                json = putPolicy.ToJsonString();
            HttpResult result = fu.UploadData(data, saveKey, GenerateUploadToken(json));
            Console.WriteLine(result);
        }

        /// <summary>
        /// 数据流上传（异步）
        /// </summary>
        /// <param name="localFile"></param>
        /// <param name="saveKey"></param>
        /// <param name="putPolicy"></param>
        public async void StreamUpload(string url, string saveKey, PutPolicy putPolicy = null)
        {
            var json = InitPolicyByScope(scope);
            UploadManager um = new UploadManager();

            if (putPolicy != null)
                json = putPolicy.ToJsonString();

            string token = GenerateUploadToken(json);
            try
            {
                var wReq = WebRequest.Create(url) as HttpWebRequest;
                var resp = await wReq.GetResponseAsync() as HttpWebResponse;
                using (var stream = resp.GetResponseStream())
                {
                    FormUploader fu = new FormUploader();
                    var result = fu.UploadStream(stream, saveKey, token);
                    Console.WriteLine(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// 大文件上传
        /// </summary>
        public void BigFileUpload(string localFile, string saveKey, PutPolicy putPolicy = null)
        {
            var json = InitPolicyByScope(scope);
            string recordFile = $@"C:\Users\Administrator\Desktop\20170515151515.record";
            string token = GenerateUploadToken(json);
            ResumableUploader ru = new ResumableUploader(true, ChunkUnit.U1024K);

            // 最大尝试次数
            int maxTry = 10;
            // 使用默认进度处理，使用自定义上传控制            
            // UploadProgressHandler uploadProgressHandler = new UploadProgressHandler(ResumableUploader.DefaultUploadProgressHandler);

            UploadProgressHandler uploadProgressHandler = new UploadProgressHandler(CustomProgressHandler);

            HttpResult result = ru.UploadFile(localFile, saveKey, token, recordFile, uploadProgressHandler);
            Console.WriteLine(result);
        }

        private static void CustomProgressHandler(long uploadedBytes, long totalBytes)
        {
            var result = $"{uploadedBytes}/{totalBytes}";
            Console.WriteLine(result);
        }
 

        public void CdnRefresh(string url)
        {
            Mac mac = new Mac(AK, SK);
            CdnManager cdnMgr = new CdnManager(mac);
            string[] urls = new string[] { url };
            var result = cdnMgr.RefreshUrls(urls);
            Console.WriteLine(result);
        }

        /// <summary>
        /// 生成时间戳防盗链接
        /// </summary>
        public void HotLink()
        {
            Mac mac = new Mac(AK, SK);
            CdnManager cdnMgr = new CdnManager(mac);
            TimestampAntiLeechUrlRequest request = new TimestampAntiLeechUrlRequest();
            request.Host = "http://your-host";
            request.Path = "/path/";
            request.File = "file-name";
            request.Query = "?version=1.1";
            request.SetLinkExpire(600); // 设置有效期，单位:秒
            string prefLink = cdnMgr.CreateTimestampAntiLeechUrl(request);
            Console.WriteLine(prefLink);
        }
    }
}
