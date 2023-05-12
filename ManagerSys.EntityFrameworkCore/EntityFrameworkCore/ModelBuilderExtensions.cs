using ManagerSys.Domain.Shared.Common;
using ManagerSys.Domian.Business;
using ManagerSys.Domian.Schedule;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
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
        public static ModelBuilder SeedBusData(this ModelBuilder builder)
        {
            string remark = "seed by efcore auto migration";
            Console.WriteLine(remark);
            builder.Entity<SystemUserEntity>().HasData
                (
                new SystemUserEntity()
                {
                    Id = 1,
                    UserCode = "001",
                    UserName = "admin",
                    RealName = "admin",
                    Phone="15086691491",
                    Email= "15086691491@qq.com",
                    LastLoginTime=null,
                    CreateUserCode="",
                    CreateUser= "",
                    UpdateUserCode= "",
                    UpdateUser = "",
                    UpdateTime=null,
                    CreateTime = DateTime.Now,
                    Status = (int)SystemUserStatus.Available,                   
                    Password = SecurityHelper.MD5("111111"),
                    IsDeleted = false
                }
                );
            return builder;
        }

        public static ModelBuilder SeedScheData(this ModelBuilder builder) 
        {
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
        /// 字段类型适配
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static ModelBuilder FixColumnsDataType<T>(this ModelBuilder builder) where T : class
        {
            var props = typeof(T).GetProperties();
            foreach (var item in props)
            {
                if (item.PropertyType == typeof(string))
                {
                    builder.Entity<T>(builer =>
                    {
                        var att = item.GetCustomAttributes().FirstOrDefault(att => att.GetType() == typeof(ColumnAttribute));
                        if (att != null)
                        {
                            var columnAtt = att as ColumnAttribute;
                            var type = columnAtt.TypeName;
                            if (!string.IsNullOrEmpty(type))
                            {
                                if ("" == "MySQL")
                                {
                                    builer.Property(item.Name).HasColumnType(type.Replace("varchar(max)", "longtext"));
                                }
                                if ("11" == "")
                                {
                                    builer.Property(item.Name).HasColumnType(type.Replace("varchar(max)", "text"));
                                }
                            }
                        }
                    });
                }
            }
            return builder;
        }
    }

}
