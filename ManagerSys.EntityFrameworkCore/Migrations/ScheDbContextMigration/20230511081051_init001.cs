using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagerSys.EntityFrameworkCore.Migrations.ScheDbContextMigration
{
    public partial class init001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScheduleDelayeds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SourceApp = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Topic = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContentKey = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DelayTimeSpan = table.Column<int>(type: "int", nullable: false),
                    DelayAbsoluteTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExecuteTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinishTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    FailedRetrys = table.Column<int>(type: "int", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NotifyUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NotifyDataType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NotifyBody = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreateUserCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: true),
                    UpdateUserCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleDelayeds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleExecutors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreateUserCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: true),
                    UpdateUserCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleExecutors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleHttpOptions",
                columns: table => new
                {
                    ScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Method = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Headers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateUserCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: true),
                    UpdateUserCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleHttpOptions", x => x.ScheduleId);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleKeepers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreateUserCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: true),
                    UpdateUserCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleKeepers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleLocks",
                columns: table => new
                {
                    ScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    LockedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LockedNode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateUserCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: true),
                    UpdateUserCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleLocks", x => x.ScheduleId);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleReferences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChildId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateUserCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: true),
                    UpdateUserCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleReferences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MetaType = table.Column<int>(type: "int", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RunLoop = table.Column<bool>(type: "bit", nullable: false),
                    CronExpression = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AssemblyName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ClassName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CustomParamsJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastRunTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NextRunTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalRunCount = table.Column<int>(type: "int", nullable: false),
                    CreateUserCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: true),
                    UpdateUserCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleTraces",
                columns: table => new
                {
                    TraceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Node = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ElapsedTime = table.Column<double>(type: "float", nullable: false),
                    Result = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateUserCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: true),
                    UpdateUserCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleTraces", x => x.TraceId);
                });

            migrationBuilder.CreateTable(
                name: "ServerNodes",
                columns: table => new
                {
                    NodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NodeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NodeType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MachineName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AccessProtocol = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Host = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AccessSecret = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    MaxConcurrency = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateUserCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: true),
                    UpdateUserCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerNodes", x => x.NodeId);
                });

            migrationBuilder.CreateTable(
                name: "SystemConfigs",
                columns: table => new
                {
                    key = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Group = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    IsReuired = table.Column<bool>(type: "bit", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateUserCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: true),
                    UpdateUserCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemConfigs", x => x.key);
                });

            migrationBuilder.CreateTable(
                name: "TraceStatistics",
                columns: table => new
                {
                    DateNum = table.Column<int>(type: "int", nullable: false),
                    DateStamp = table.Column<long>(type: "bigint", nullable: false),
                    Success = table.Column<int>(type: "int", nullable: false),
                    Fail = table.Column<int>(type: "int", nullable: false),
                    Other = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    CreateUserCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: true),
                    UpdateUserCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TraceStatistics", x => x.DateNum);
                });

            migrationBuilder.InsertData(
                table: "SystemConfigs",
                columns: new[] { "key", "CreateTime", "CreateUser", "CreateUserCode", "Group", "Id", "IsReuired", "Name", "Remark", "Sort", "UpdateTime", "UpdateUser", "UpdateUserCode", "Value" },
                values: new object[,]
                {
                    { "Assembly_ImagePullPolicy", new DateTime(2023, 5, 11, 16, 10, 51, 45, DateTimeKind.Local).AddTicks(8623), null, null, "程序集配置", null, true, "文件包拉取策略", "Always-总是拉取，IfNotPresent-本地没有时拉取，默认是Always", 1, null, null, null, "Always" },
                    { "DelayTask_DelayPattern", new DateTime(2023, 5, 11, 16, 10, 51, 45, DateTimeKind.Local).AddTicks(8629), null, null, "延时任务配置", null, true, "延迟模式", "Relative-相对时间，Absolute-绝对时间，默认值是Relative", 1, null, null, null, "Relative" },
                    { "DelayTask_RetrySpans", new DateTime(2023, 5, 11, 16, 10, 51, 45, DateTimeKind.Local).AddTicks(8632), null, null, "延时任务配置", null, true, "回调失败重试间隔", "回调失败重试间隔时间(s)，会随着重试次数递增，默认值是10秒", 3, null, null, null, "10" },
                    { "DelayTask_RetryTimes", new DateTime(2023, 5, 11, 16, 10, 51, 45, DateTimeKind.Local).AddTicks(8630), null, null, "延时任务配置", null, true, "回调失败重试次数", "回调失败重试次数，默认值是3", 2, null, null, null, "3" },
                    { "Email_FromAccount", new DateTime(2023, 5, 11, 16, 10, 51, 45, DateTimeKind.Local).AddTicks(8619), null, null, "邮件配置", null, true, "发件人账号", "邮箱账号", 3, null, null, null, "" },
                    { "Email_FromAccountPwd", new DateTime(2023, 5, 11, 16, 10, 51, 45, DateTimeKind.Local).AddTicks(8621), null, null, "邮件配置", null, true, "发件人账号密码", "登录密码或授权码等", 4, null, null, null, "" },
                    { "Email_SmtpPort", new DateTime(2023, 5, 11, 16, 10, 51, 45, DateTimeKind.Local).AddTicks(8617), null, null, "邮件配置", null, true, "邮件服务器端口", "smtp端口号", 2, null, null, null, "" },
                    { "Email_SmtpServer", new DateTime(2023, 5, 11, 16, 10, 51, 45, DateTimeKind.Local).AddTicks(8603), null, null, "邮件配置", null, true, "邮件服务器", "smtp服务器地址", 1, null, null, null, "" },
                    { "Http_RequestTimeout", new DateTime(2023, 5, 11, 16, 10, 51, 45, DateTimeKind.Local).AddTicks(8625), null, null, "HTTP配置", null, true, "请求超时时间", "单位是秒，默认值是10", 1, null, null, null, "10" },
                    { "System_WorkerUnHealthTimes", new DateTime(2023, 5, 11, 16, 10, 51, 45, DateTimeKind.Local).AddTicks(8627), null, null, "系统配置", null, true, "Worker允许无响应次数", "健康检查失败达到最大次数会被下线剔除，默认值是3", 1, null, null, null, "3" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduleDelayeds");

            migrationBuilder.DropTable(
                name: "ScheduleExecutors");

            migrationBuilder.DropTable(
                name: "ScheduleHttpOptions");

            migrationBuilder.DropTable(
                name: "ScheduleKeepers");

            migrationBuilder.DropTable(
                name: "ScheduleLocks");

            migrationBuilder.DropTable(
                name: "ScheduleReferences");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "ScheduleTraces");

            migrationBuilder.DropTable(
                name: "ServerNodes");

            migrationBuilder.DropTable(
                name: "SystemConfigs");

            migrationBuilder.DropTable(
                name: "TraceStatistics");
        }
    }
}
