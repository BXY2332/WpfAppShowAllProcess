using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WPFShowAllPeocessApp
{
    internal static class Dllimport
    {
        public const uint STATUS_SUCCESS = 0x00000000;
        public const uint STATUS_INFO_LENGTH_MISMATCH = 0xC0000004;

        [DllImport("ntdll.dll", CharSet = CharSet.Auto)]
        public static extern uint NtQuerySystemInformation(SYSTEM_INFORMATION_CLASS systeminfomationclaa, IntPtr systeminfomation, int systeminfomationLength, out int returnLength);

        public enum SYSTEM_INFORMATION_CLASS
        {
            SystemBasicInformation = 0x00, //系统基本信息，如处理器数量 返回结构 SYSTEM_BASIC_INFORMATION
            SystemPerformanceInformation = 0x02, //系统性能统计信息 返回结构 SYSTEM_PERFORMANCE_INFORMATION
            SystemProcessorPerformanceInformation = 0x04, //系统处理器性能信息 返回结构 SYSTEM_PROCESSOR_PERFORMANCE_INFORMATION 
            SystemProcessInformation = 0x05, //系统进程和线程信息 返回结构 SYSTEM_PROCESS_INFORMATION
            SystemCallCountInformation = 0x07, //系统调用计数信息
            SystemConfigurationInformation = 0x08, //系统配置信息
            SystemModuleInformation = 0x11, //系统模块（内核驱动程序）信息 RTL_PROCESS_MODULES
            SystemFileCacheInformation = 0x21, //系统文件缓存信息 SYSTEM_FILECACHE_INFORMATION
            SystemInterruptInformation = 0x23, //系统中断信息 SYSTEM_INTERRUPT_INFORMATION
            SystemLookasideInformation = 0x45, //系统 Lookaside 列表信息
            SystemPolicyInformation = 0x46, //系统策略信息
            SystemRegistryQuotaInformation = 0x47, //系统注册表配额信息
            SystemTimeZoneInformation = 0x48, //系统时区信息 TIME_ZONE_INFORMATION
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct UNICODE_STRING
        {
            public ushort Length;
            public ushort MaximumLength;
            public IntPtr Buffer;
        }
        public struct SYSTEM_PROCESS_INFORMATION
        {
            public uint NextEntryOffset;    // ULONG
            public uint NumberOfThreads;    // ULONG
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 48)]
            public byte[] Reserved1;        // SYSTEM_TIME + etc
            public UNICODE_STRING ImageName;
            public int BasePriority;        // KPRIORITY
            public IntPtr UniqueProcessId;  // HANDLE
            public IntPtr InheritedFromUniqueProcessId;
            public uint HandleCount;        // ULONG
            public uint SessionId;          // ULONG
            public UIntPtr PageDirectoryBase; // SIZE_T
            public UIntPtr PeakVirtualSize;
            public UIntPtr VirtualSize;
            public uint PageFaultCount;
            public UIntPtr PeakWorkingSetSize;
            public UIntPtr WorkingSetSize;
            public UIntPtr QuotaPeakPagedPoolUsage;
            public UIntPtr QuotaPagedPoolUsage;
            public UIntPtr QuotaPeakNonPagedPoolUsage;
            public UIntPtr QuotaNonPagedPoolUsage;
            public UIntPtr PagefileUsage;
            public UIntPtr PeakPagefileUsage;
            public UIntPtr PrivatePageCount;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public long[] Reserved7;
        }
        public enum SystemProcessName
        {
            // 系统核心进程 
            [Description("空闲/无名进程")]
            NoName, // 对应 "NO NAME"

            [Description("系统进程")]
            System,

            [Description("注册表")]
            Registry,

            [Description("本地会话管理器子系统")]
            Smss,

            [Description("客户端服务端运行时子系统")]
            Csrss,

            [Description("Windows 初始化应用程序")]
            Wininit,

            [Description("服务控制管理器")]
            Services,

            [Description("本地安全机构子系统")]
            Lsass,

            [Description("通用服务宿主 (多个Windows服务)")]
            Svchost,

            [Description("字体驱动程序主机")]
            Fontdrvhost,

            [Description("Windows Defender 核心服务")]
            MpDefenderCoreService,

            [Description("Windows Defender 反恶意软件服务")]
            MsMpEng,

            [Description("Windows Shell 体验主机")]
            Sihost,

            [Description("Windows 登录应用程序")]
            Winlogon,

            [Description("桌面窗口管理器")]
            Dwm,

            [Description("任务主机")]
            Taskhostw,

            [Description("资源管理器 (桌面 Shell)")]
            Explorer,

            [Description("输入法 (中文)")]
            ChsIME,

            [Description("后台代理程序")]
            RuntimeBroker,

            [Description("Windows 搜索索引器")]
            SearchIndexer,

            [Description("打印后台处理程序")]
            Spoolsv,

            [Description("内存压缩")]
            MemoryCompression,

            [Description("Realtek 音频服务")]
            RtkAudioService64,

            [Description("英特尔动态音频扩展 API")]
            DAX3API,

            [Description("VMware 网络服务")]
            Vmnetdhcp,

            [Description("VMware 认证服务")]
            VmwareAuthd,

            [Description("VMware USB仲裁服务")]
            VmwareUsbarbitrator64,

            [Description("NVIDIA 显示容器")]
            NVDisplayContainer,

            [Description("Realtek 音频管理器")]
            RAVBg64,

            [Description("微软 Edge 浏览器")]
            Msedge,

            [Description("Visual Studio 开发环境")]
            Devenv,

            [Description("Obsidian 笔记")]
            Obsidian,

            [Description("Clash 客户端")]
            ClashforWindows,

            [Description("Intel CP 保护机制服务")]
            IntelCpHDCPSvc,

            [Description("Intel HECI 服务")]
            IntelCpHeciSvc,

            [Description("Intel 图形用户界面服务")]
            IgfxCUIService,

            [Description("NVIDIA 显示容器")]
            NvDisplayContainer,

            [Description("无线局域网扩展性模块")]
            Wlanext,

            [Description("SQL Server 卷影副本写入服务")]
            Sqlwriter,

            [Description("Intel ME JHI 服务")]
            JhiService,

            [Description("Intel Graphics Command Center 服务")]
            OneAppIGCCWinService,

            [Description("VMware NAT 服务")]
            Vmnat,

            [Description("Windows 管理规范注册服务")]
            WmiRegistrationService,

            [Description("COM Surrogate/DLL 宿主")]
            Dllhost,

            [Description("系统聚合主机")]
            AggregatorHost,

            [Description("Windows Defender 网络检测系统")]
            NisSrv,

            [Description("安全健康服务")]
            SecurityHealthService,

            [Description("Intel 图形执行模块")]
            IgfxEM,

            [Description("Microsoft Edge 更新服务")]
            MicrosoftEdgeUpdate,

            [Description("文本服务和输入语言管理器")]
            Ctfmon,

            [Description("开始菜单体验主机")]
            StartMenuExperienceHost,

            [Description("Windows 搜索应用程序")]
            SearchApp,

            [Description("文本输入主机")]
            TextInputHost,

            [Description("系统设置")]
            SystemSettings,

            [Description("VMware 托盘图标")]
            VmwareTray,

            [Description("Windows 组件包服务")]
            CompPkgSrv,

            [Description("Windows Search 协议主机")]
            SearchProtocolHost,

            [Description("Windows Search 筛选主机")]
            SearchFilterHost,
        }
        public static string GetDescription(this Enum value)
        {
            // 1. 获取枚举类型
            Type type = value.GetType();

            // 2. 获取当前枚举成员的信息
            FieldInfo fieldInfo = type.GetField(value.ToString());

            if (fieldInfo != null)
            {
                // 3. 查找是否有 DescriptionAttribute
                DescriptionAttribute attribute = Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute)) as DescriptionAttribute;

                // 4. 如果找到特性，返回描述；否则返回枚举名（默认值）
                return attribute?.Description ?? value.ToString();
            }

            return value.ToString();
        }
    }
}