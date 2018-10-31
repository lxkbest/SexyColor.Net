using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SexyColor.Infrastructure
{
    public class CaiNiaoManager
    {
        //商户ID
        private string EBusinessID = "1293993";
        //API key
        private string AppKey = "83d7cfc3-7d52-4a2d-bdce-e880074a62d7";
        //请求url
        private string ReqURL = "http://api.kdniao.cc/Ebusiness/EbusinessOrderHandle.aspx";

        /// <summary>
        /// 调用即时查询接口
        /// </summary>
        /// <returns></returns>
        public async Task<string> ExecuteInstantQueryAPI(string shipperCode, string logisticCode)
        {
            string requestData = "{'OrderCode':'','ShipperCode':'" + shipperCode + "','LogisticCode':'" + logisticCode + "'}";
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("RequestData", WebUtility.UrlEncode(requestData));
            param.Add("EBusinessID", EBusinessID);
            param.Add("RequestType", "1002");
            string dataSign = Sign(requestData, AppKey, "UTF-8");
            param.Add("DataSign", WebUtility.UrlEncode(dataSign));
            param.Add("DataType", "2");
            var result = await SendPost(ReqURL, GetData(param, "UTF-8"));
            return Encoding.GetEncoding("UTF-8").GetString(result);
        }

        /// <summary>
        /// Post方式提交数据，返回网页的源代码
        /// </summary>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        private async Task<byte[]> SendPost(string url, byte[] byteData)
        {
            byte[] result = null;

            try
            {
                HttpClientHandler handler = new HttpClientHandler();
                using (HttpClient httpClient = new HttpClient(handler))
                {
                    //httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
                    //httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xhtml+xml"));
                    //httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml", 0.9));
                    //httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("image/webp"));
                    //httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*", 0.8));

                    //httpClient.DefaultRequestHeaders.Add("User-Agent", @"Mozilla/5.0 (compatible; Baiduspider/2.0; +http://www.baidu.com/search/spider.html)");
                    //httpClient.DefaultRequestHeaders.Add("Accept", @"text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");

                    HttpResponseMessage responseMessage = null;
                    using (Stream dataStream = new MemoryStream(byteData ?? new byte[0]))
                    {
                        using (HttpContent httpContent = new StreamContent(dataStream))
                        {
                            httpContent.Headers.Add("UserAgent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36");
                            httpContent.Headers.Add("Timeout", (30 * 1000).ToString());
                            httpContent.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                            httpContent.Headers.Add("Content-Length", byteData.Length.ToString());
                            httpContent.Headers.Add("Method", "POST");
                            httpClient.DefaultRequestHeaders.Add("Accept", @"*/*");
                            responseMessage = await httpClient.PostAsync(url, httpContent);
                        }
                    }

                    if (responseMessage != null && responseMessage.StatusCode == HttpStatusCode.OK)
                    {
                        using (responseMessage)
                        {
                            using (Stream responseStream = await responseMessage.Content.ReadAsStreamAsync())
                            {
                                if (responseStream != null)
                                {
                                    byte[] responseData = new byte[responseStream.Length];
                                    responseStream.Read(responseData, 0, responseData.Length);
                                    result = responseData;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="param"></param>
        /// <param name="charset"></param>
        /// <returns></returns>
        private byte[] GetData(Dictionary<string, string> param, string charset)
        {
            StringBuilder postData = new StringBuilder();
            if (param != null && param.Count > 0)
            {
                foreach (var p in param)
                {
                    if (postData.Length > 0)
                    {
                        postData.Append("&");
                    }
                    postData.Append(p.Key);
                    postData.Append("=");
                    postData.Append(p.Value);
                }
            }
            byte[] byteData = Encoding.GetEncoding(charset).GetBytes(postData.ToString());
            return byteData;
        }

        ///<summary>
        ///电商Sign签名
        ///</summary>
        ///<param name="content">内容</param>
        ///<param name="keyValue">Appkey</param>
        ///<param name="charset">URL编码 </param>
        ///<returns>DataSign签名</returns>
        private string Sign(string content, string keyValue, string charset)
        {
            if (keyValue != null)
            {
                return EncryptionUtility.Base64Encode(EncryptionUtility.MD5(content + keyValue, charset), charset);
            }
            return EncryptionUtility.Base64Encode(EncryptionUtility.MD5(content + keyValue, charset), charset);
        }




    }
}
