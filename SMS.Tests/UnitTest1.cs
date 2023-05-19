using Newtonsoft.Json;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;

namespace SMS.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test1()
        {
            try
            {
                var apiKey = "25a61fc938070079d87813edf09c1896";
                var accessKey = "788eae919bdb57143518164ba025be36030851361af52ed9d3189c00c2c95b9c";
                String authHost = "https://opassapi.infocloud.cc/message/send";
                var ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                var currentTime = Convert.ToInt64(ts.TotalMilliseconds).ToString();
                HttpClient client = new HttpClient();
                var requestHeaders = new Dictionary<string, string>();
                //签名算法。⽀持HmacSHA256、HmacMD5、HmacSHA224、HmacSHA384、HmacSHA512
                requestHeaders.Add("x-sign-method", "HmacSHA256");
                requestHeaders.Add("x-api-key", $"{apiKey}");
                //随机数。每次请求随机数建议不相同
                requestHeaders.Add("x-nonce", $"{GetRandom()}");
                //时间戳
                requestHeaders.Add("x-timestamp", $"{currentTime}");
                // client.DefaultRequestHeaders.Host ="recognition.image.myqcloud.com";
                //设置headers请求参数
                foreach (var item in requestHeaders)
                {
                    Console.WriteLine("[{0}]: {1}", item.Key, item.Value);

                    client.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
                //短信接⼝请求参数。参⻅接⼝规范
                var jsArr = new Dictionary<string, string>();
                jsArr.Add("phones", "15086691491");
                jsArr.Add("templateCode", "776896653478080512");
                jsArr.Add("templateParam", "[\"淮安自来水短信平台测试\"]");
                //将headers参与与接⼝报⽂参数合并、排序，并⽣成本次请求的签名信息
                var signDic = jsArr.Union(requestHeaders).ToDictionary(k => k.Key, v => v.Value);
                var dicStr = GetSignContent(signDic);
                var sign = Hmacsha256Encrypt(dicStr, accessKey);
                //在header中设置本次请求签名信息
                client.DefaultRequestHeaders.Add("x-sign", sign);
                HttpContent str = new StringContent(JsonConvert.SerializeObject(jsArr));
                str.Headers.Remove("Content-Type");
                str.Headers.Add("Content-Type", "application/json");
                HttpResponseMessage response = client.PostAsync(authHost, str).Result;
                String result = response.Content.ReadAsStringAsync().Result;

                Assert.Pass();
            }
            catch (Exception exc)
            {
                Assert.Fail();
            }

            //var apiKey = "25a61fc938070079d87813edf09c1896";
            //var accessKey = "788eae919bdb57143518164ba025be36030851361af52ed9d3189c00c2c95b9c";
            //string authHost = "https://opassapi.infocloud.cc/message/send";
            //var ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            //var currentTime = Convert.ToInt64(ts.TotalMilliseconds).ToString();

            //WebRequest request = WebRequest.Create(authHost);
            //request.Method = "POST";
            //var requestHeaders = new Dictionary<string, string>();
            ////签名算法。⽀持HmacSHA256、HmacMD5、HmacSHA224、HmacSHA384、HmacSHA512
            //requestHeaders.Add("x-sign-method", "HmacSHA256");
            //requestHeaders.Add("x-api-key", apiKey);
            ////随机数。每次请求随机数建议不相同
            //requestHeaders.Add("x-nonce", "" + GetRandom() + "");
            ////时间戳
            //requestHeaders.Add("x-timestamp", currentTime);
            //// client.DefaultRequestHeaders.Host ="recognition.image.myqcloud.com";
            ////设置headers请求参数
            //foreach (var item in requestHeaders)
            //{
            //    Console.WriteLine("[{0}]: {1}", item.Key, item.Value);
            //    request.Headers.Add(item.Key, item.Value);
            //}
            ////短信接⼝请求参数。参⻅接⼝规范
            //var jsArr = new Dictionary<string, string>();
            //List<smsParams> smsParams = new List<smsParams>();
            //smsParams.Add(new smsParams
            //{
            //    phones = "15086691491",
            //    templateCode = "776896653478080512",
            //    templateParam = "[\"淮安自来水短信平台测试\"]"
            //});
            //jsArr.Add("phones", "15086691491");
            //jsArr.Add("templateCode", "776896653478080512");
            //jsArr.Add("templateParam", "[\"淮安自来水短信平台测试\"]");
            ////将headers参与与接⼝报⽂参数合并、排序，并⽣成本次请求的签名信息
            //var signDic = jsArr.Union(requestHeaders).ToDictionary(k => k.Key, v => v.Value);
            //var dicStr = GetSignContent(signDic);
            //var sign = Hmacsha256Encrypt(dicStr, accessKey);
            ////在header中设置本次请求签名信息
            //request.Headers.Add("x-sign", sign);

            ////post传参数  
            //string postdata = JsonHelper.ObjectToJson<List<smsParams>>(smsParams, Encoding.UTF8);
            //postdata = postdata.TrimStart('[').TrimEnd(']');
            //byte[] bytes = Encoding.ASCII.GetBytes(postdata);
            //request.Headers.Remove("Content-Type");
            //request.Headers.Add("Content-Type", "application/json");
            //request.ContentLength = postdata.Length;
            //Stream sendStream = request.GetRequestStream();
            //sendStream.Write(bytes, 0, bytes.Length);
            //sendStream.Close();

            ////得到返回值  
            //WebResponse response = request.GetResponse();
            //string OrderQuantity = new StreamReader(response.GetResponseStream(), Encoding.UTF8).ReadToEnd();

        }



        /// <summary>
        /// Get Sign Content
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private static string GetSignContent(IDictionary<string, string> parameters)
        {
            // 第⼀步：把字典按Key的字⺟顺序排序
            IDictionary<string, string> sortedParams = new SortedDictionary<string, string>(parameters);
            IEnumerator<KeyValuePair<string, string>> dem = sortedParams.GetEnumerator();
            // 第⼆步：把所有参数名和参数值串在⼀起
            StringBuilder query = new StringBuilder("");
            while (dem.MoveNext())
            {
                string key = dem.Current.Key;
                string value = dem.Current.Value;
                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
                {
                    query.Append(key).Append("=").Append(value).Append("&");
                }
            }
            string content = query.ToString().Substring(0, query.Length - 1);
            return content;
        }

        /// <summary>
        /// HMACSHA1算法加密
        /// </summary>
        private static string Hmacsha256Encrypt(string encryptText, string encryptKey)
        {
            var encoding = new System.Text.UTF8Encoding();
            StringBuilder builder = new StringBuilder();

            using (var hmacsha256 = new HMACSHA256(encoding.GetBytes(encryptKey)))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(encoding.GetBytes(encryptText));
                for (int i = 0; i < hashmessage.Length; i++)
                {
                    builder.Append(hashmessage[i].ToString("x2"));
                }
                Console.WriteLine(builder);
            }
            return builder.ToString();
        }
        /// <summary>
        /// 获取随机数
        /// </summary>
        private static int GetRandom()
        {
            var random = new Random();
            var rand = random.Next(10000, 999999999);
            return rand;
        }
    }

    public class JsonHelper
    {

        #region 对象类型转换为json 字符
        /// <summary>
        /// 对象类型转换为json 字符
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="jsonObject">待转换实体</param>
        /// <param name="encoding">编码格式</param>
        /// <returns>string</returns>
        public static string ObjectToJson<T>(Object jsonObject, Encoding encoding)
        {
            string result = String.Empty;
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                serializer.WriteObject(ms, jsonObject);
                result = encoding.GetString(ms.ToArray());
            }
            return result;
        }
        #endregion

        #region json字符转换为对象
        /// <summary>
        /// json字符转换为对象
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <param name="encoding">编码格式</param>
        /// <returns>T</returns>
        public static T JsonToObject<T>(string json, Encoding encoding)
        {
            T resultObject = System.Activator.CreateInstance<T>();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(encoding.GetBytes(json)))
            {
                resultObject = (T)serializer.ReadObject(ms);
            }
            return resultObject;
        }
        #endregion

    }


    public class smsParams
    {
        public string phones { get; set; }
        public string templateCode { get; set; }
        public string templateParam { get; set; }
    }

}