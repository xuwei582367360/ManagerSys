﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSys.Domain.Shared.Consts
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class Extend
    {
        /// <summary>
        /// 计算日期间隔
        /// </summary>
        /// <param name="tokenCreateDate"></param>
        /// <param name="expiresInSeconds"></param>
        /// <returns></returns>
        public static int GetDateDiff(this DateTime tokenCreateDate, int expiresInSeconds)
        {
            var currentDate = DateTime.Now;
            TimeSpan ts = currentDate.Subtract(tokenCreateDate);
            int remainTime = expiresInSeconds - Convert.ToInt32(ts.TotalSeconds);
            return remainTime;
        }
        /// <summary>
        /// 获取客户端IP
        /// </summary>
        /// <param name="http"></param>
        /// <returns></returns>
        public static string GetClientIp(this HttpContext http)
        {
            string remoteIpAddress = http.Connection.RemoteIpAddress.MapToIPv4().ToString();
            if (http.Request.Headers.ContainsKey("X-Forwarded-For"))
                remoteIpAddress = http.Request.Headers["X-Forwarded-For"];
            return remoteIpAddress;
        }

        /// <summary>
        /// 获取客服端端口
        /// </summary>
        /// <param name="http"></param>
        /// <returns></returns>
        public static int GetClientPort(this HttpContext http)
        {
            var port = http.Connection.RemotePort;
            return port;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="http"></param>
        /// <returns></returns>
        public static string GetUserInfo(this HttpContext http, string key)
        {
            return string.Empty;
        }
        /// <summary>
        /// 获取Max地址
        /// </summary>
        /// <param name="http"></param>
        /// <returns></returns>
        public static string GetMacBySendARP(this HttpContext http)
        {
            IPGlobalProperties iPGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            NetworkInterface[] niscs = NetworkInterface.GetAllNetworkInterfaces();

            //获取本机电脑名
            var hostName = iPGlobalProperties.HostName;
            //获取域名
            var domainName = iPGlobalProperties.DomainName;
            if (niscs == null || niscs.Length < 1)
            {
                return string.Empty;
            }
            var macIp = "";

            foreach (var item in niscs)
            {
                var adapterName = item.Name;
                var description = item.Description;
                var networkInterfaceType = item.NetworkInterfaceType;
                //if (adapterName == "本地连接")

                //{
                PhysicalAddress address = item.GetPhysicalAddress();

                var bytes = address.GetAddressBytes();
                for (int i = 0; i < bytes.Length; i++)
                {
                    macIp += bytes[i].ToString("X2");
                    if (i != bytes.Length - 1)
                    {
                        macIp += "-";
                    }
                }
                break;
                //}
            }
            return macIp;
        }

    }

}
