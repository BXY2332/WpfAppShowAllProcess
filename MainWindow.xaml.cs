using System.Runtime.InteropServices;
using System.Windows;
using static WPFShowAllPeocessApp.Dllimport;

namespace WPFShowAllPeocessApp
{
    public partial class MainWindow : Window
    {
        public class processInfo
        {
            public string Name { get; set; }
            public int PID { get; set; }
            public int Index { get; set; }
            public string ChineseName
            {
                get
                {
                    string rawName = this.Name;
                    string normalizedName;
                    if (rawName.Contains(" "))
                    {
                        normalizedName = rawName.Replace(" ", "");
                    }
                    else
                    {
                        normalizedName = rawName.Replace(".exe", "", StringComparison.OrdinalIgnoreCase);
                    }
                    normalizedName = normalizedName.ToLower();

                    try
                    {
                        SystemProcessName enumValue = (SystemProcessName)Enum.Parse(typeof(SystemProcessName), normalizedName, true);
                        return enumValue.GetDescription();
                    }
                    catch (ArgumentException)
                    {
                        return rawName;
                    }
                }
            }
        }
        public List<processInfo> GetProcessList()
        {
            List<processInfo> allProcessList = new List<processInfo>();

            int bufferSize = 0;
            IntPtr buffer = IntPtr.Zero;
            uint status;

            status = NtQuerySystemInformation(SYSTEM_INFORMATION_CLASS.SystemProcessInformation, buffer, bufferSize, out bufferSize);
            while (status == STATUS_INFO_LENGTH_MISMATCH)
            {
                if (bufferSize != 0)
                {
                    Marshal.FreeHGlobal(buffer);
                }
                buffer = Marshal.AllocHGlobal(bufferSize);
                status = NtQuerySystemInformation(SYSTEM_INFORMATION_CLASS.SystemProcessInformation, buffer, bufferSize, out bufferSize);
            }
            if (status != STATUS_SUCCESS)
            {
                Marshal.FreeHGlobal(buffer);
                return allProcessList;
            }

            IntPtr currentPointer = buffer;
            ulong nextEntryOffset = 0;
            int index = 1;

            while (true)
            {
                string processName;
                SYSTEM_PROCESS_INFORMATION systemProcessInfomation = (SYSTEM_PROCESS_INFORMATION)Marshal.PtrToStructure(currentPointer, typeof(SYSTEM_PROCESS_INFORMATION));
                if (systemProcessInfomation.ImageName.Length > 0 && systemProcessInfomation.ImageName.Buffer != IntPtr.Zero)
                {
                    processName = Marshal.PtrToStringAuto(systemProcessInfomation.ImageName.Buffer, systemProcessInfomation.ImageName.Length / 2);
                }
                else
                {
                    processName = "NO NAME";
                }
                allProcessList.Add(new processInfo
                {
                    Index = index++,
                    Name = processName,
                    PID = (int)systemProcessInfomation.UniqueProcessId
                });
                nextEntryOffset = systemProcessInfomation.NextEntryOffset;
                if (nextEntryOffset == 0)
                {
                    break;
                }
                currentPointer = IntPtr.Add(currentPointer, (int)nextEntryOffset);
            }

            Marshal.FreeHGlobal(buffer);

            return allProcessList;
        }
        public MainWindow()
        {
            InitializeComponent();
            showProcesslistbox.ItemsSource = GetProcessList();
        }
    }
}