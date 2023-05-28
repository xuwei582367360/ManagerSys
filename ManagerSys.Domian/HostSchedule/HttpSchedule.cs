using ManagerSys.Domain.Shared.QuartzNet.Base;
using ManagerSys.Domian.HostSchedule.Jobs;
using ManagerSys.Domian.Schedule;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSys.Domian.HostSchedule
{
    public class HttpSchedule : IHosSchedule
    {
        public ScheduleEntity Schedule { get; set; }
        //public Dictionary<string, object> CustomParams { get; set; }
        //public List<KeyValuePair<string, string>> Keepers { get; set; }
        //public Dictionary<Guid, string> Children { get; set; }
        public ScheBaseEntity RunnableInstance { get; set; }

        public CancellationTokenSource CancellationTokenSource { get; set; }

        public void CreateRunnableInstance(ScheduleOperation context)
        {
            RunnableInstance = new HttpTask(context.HttpOption);
        }

        public Type GetQuartzJobType()
        {
            return typeof(HttpJob);
        }

        public void Dispose()
        {
            RunnableInstance.Dispose();
            RunnableInstance = null;
        }
    }
    public class HttpTask : ScheBaseEntity
    {
        private readonly ScheduleHttpOptionEntity _option;

        private readonly TimeSpan _timeout = TimeSpan.FromSeconds(10);

        private const string HEADER_TIMEOUT = "sm-timeout";

        private readonly Dictionary<string, object> _headers;


        public HttpTask(ScheduleHttpOptionEntity httpOption)
        {
            if (httpOption != null)
            {
                _option = httpOption;

                _headers = HosScheduleFactory.ConvertParamsJson(httpOption.Headers);

                if (_headers.ContainsKey(HEADER_TIMEOUT) && int.TryParse(_headers[HEADER_TIMEOUT].ToString(), out int result) && result > 0)
                {
                    _timeout = TimeSpan.FromSeconds(result);
                }
                else
                {
                    int config = 300;//ConfigurationCache.GetField<int>("Http_RequestTimeout");
                    if (config > 0)
                    {
                        _timeout = TimeSpan.FromSeconds(config);
                    }
                }

                string requestBody = string.Empty;
                string url = httpOption.RequestUrl;
                if (httpOption.ContentType == "application/json")
                {
                    requestBody = httpOption.Body?.Replace("\r\n", "");
                }
                else if (httpOption.ContentType == "application/x-www-form-urlencoded")
                {
                    var formData = HosScheduleFactory.ConvertParamsJson(httpOption.Body);
                    requestBody = string.Join('&', formData.Select(x => $"{x.Key}={System.Net.WebUtility.UrlEncode(x.Value.ToString())}"));
                    if (httpOption.Method.ToLower() == "get" && formData.Count > 0)
                    {
                        url = $"{httpOption.RequestUrl}?{requestBody}";
                    }
                }
                _option.RequestUrl = url;
                _option.Body = requestBody;
            }
        }


        public override void Run(TaskContext context)
        {
            if (_option == null) return;
            //context.WriteLog($"即将请求：{_option.RequestUrl}");

            DoRequest(context).Wait(CancellationToken);

        }

        private async Task DoRequest(TaskContext context)
        {
            //using (var scope = new ScopeDbContext())
            //{
            //    var httpClient = scope.GetService<IHttpClientFactory>().CreateClient();

            //    foreach (var item in _headers)
            //    {
            //        httpClient.DefaultRequestHeaders.Add(item.Key, item.Value.ToString());
            //    }

            //    httpClient.Timeout = _timeout;

            //    var httpRequest = new HttpRequestMessage
            //    {
            //        Content = new StringContent(_option.Body ?? string.Empty, System.Text.Encoding.UTF8, _option.ContentType),
            //        Method = new HttpMethod(_option.Method),
            //        RequestUri = new Uri(_option.RequestUrl)
            //    };

            //    var response = await httpClient.SendAsync(httpRequest, CancellationToken);
            //    if (response.IsSuccessStatusCode)
            //    {
            //        context.WriteLog($"请求结束，响应码：{response.StatusCode.GetHashCode().ToString()}，响应内容：{(response.Content.Headers.GetValues("content-type").Any(x => x.Contains("text/html")) ? "html文档" : await response.Content.ReadAsStringAsync())}");
            //    }
            //    response.Dispose();
            //}

        }
    }
}
