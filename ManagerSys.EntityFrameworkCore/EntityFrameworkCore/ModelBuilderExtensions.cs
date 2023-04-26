﻿using ManagerSys.Domain.Shared.Common;
using ManagerSys.Domian.Business;
using ManagerSys.Domian.Schedule;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSys.EntityFrameworkCore
{
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// 生成种子数据
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static ModelBuilder SeedData(this ModelBuilder builder)
        {
            string remark = "seed by efcore auto migration";

            builder.Entity<SystemUserEntity>().HasData
                (
                new SystemUserEntity()
                {
                    Id = 1,
                    CreateTime = DateTime.Now,
                    Status = 1,
                    UserName = "admin",
                    RealName = "admin",
                    Password = SecurityHelper.MD5("111111")
                }
                );

            builder.Entity<SystemConfigEntity>().HasData
                (
                new SystemConfigEntity()
                {
                    CreateTime = DateTime.Now,
                    IsReuired = true,
                    Key = "Email_SmtpServer",
                    Name = "邮件服务器",
                    Value = "",
                    Group = "邮件配置",
                    Remark = "smtp服务器地址",
                    Sort = 1
                },
                new SystemConfigEntity()
                {
                    CreateTime = DateTime.Now,
                    IsReuired = true,
                    Key = "Email_SmtpPort",
                    Name = "邮件服务器端口",
                    Value = "",
                    Group = "邮件配置",
                    Remark = "smtp端口号",
                    Sort = 2
                },
                new SystemConfigEntity()
                {
                    CreateTime = DateTime.Now,
                    IsReuired = true,
                    Key = "Email_FromAccount",
                    Name = "发件人账号",
                    Value = "",
                    Group = "邮件配置",
                    Remark = "邮箱账号",
                    Sort = 3
                },
                new SystemConfigEntity()
                {
                    CreateTime = DateTime.Now,
                    IsReuired = true,
                    Key = "Email_FromAccountPwd",
                    Name = "发件人账号密码",
                    Value = "",
                    Group = "邮件配置",
                    Remark = "登录密码或授权码等",
                    Sort = 4
                },
                new SystemConfigEntity()
                {
                    CreateTime = DateTime.Now,
                    IsReuired = true,
                    Key = "Assembly_ImagePullPolicy",
                    Name = "文件包拉取策略",
                    Value = "Always",
                    Group = "程序集配置",
                    Remark = "Always-总是拉取，IfNotPresent-本地没有时拉取，默认是Always",
                    Sort = 1
                },
                new SystemConfigEntity()
                {
                    CreateTime = DateTime.Now,
                    IsReuired = true,
                    Key = "Http_RequestTimeout",
                    Name = "请求超时时间",
                    Value = "10",
                    Group = "HTTP配置",
                    Remark = "单位是秒，默认值是10",
                    Sort = 1
                },
                new SystemConfigEntity()
                {
                    CreateTime = DateTime.Now,
                    IsReuired = true,
                    Key = "System_WorkerUnHealthTimes",
                    Name = "Worker允许无响应次数",
                    Value = "3",
                    Group = "系统配置",
                    Remark = "健康检查失败达到最大次数会被下线剔除，默认值是3",
                    Sort = 1
                },
                new SystemConfigEntity()
                {
                    CreateTime = DateTime.Now,
                    IsReuired = true,
                    Key = "DelayTask_DelayPattern",
                    Name = "延迟模式",
                    Value = "Relative",
                    Group = "延时任务配置",
                    Remark = "Relative-相对时间，Absolute-绝对时间，默认值是Relative",
                    Sort = 1
                },
                new SystemConfigEntity()
                {
                    CreateTime = DateTime.Now,
                    IsReuired = true,
                    Key = "DelayTask_RetryTimes",
                    Name = "回调失败重试次数",
                    Value = "3",
                    Group = "延时任务配置",
                    Remark = "回调失败重试次数，默认值是3",
                    Sort = 2
                },
                new SystemConfigEntity()
                {
                    CreateTime = DateTime.Now,
                    IsReuired = true,
                    Key = "DelayTask_RetrySpans",
                    Name = "回调失败重试间隔",
                    Value = "10",
                    Group = "延时任务配置",
                    Remark = "回调失败重试间隔时间(s)，会随着重试次数递增，默认值是10秒",
                    Sort = 3
                }
                );
            return builder;
        }

        /// <summary>
        /// 应用数据库
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static DbContextOptionsBuilder UseDatabase(this DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(conn);
            return builder;
        }
    }

}
