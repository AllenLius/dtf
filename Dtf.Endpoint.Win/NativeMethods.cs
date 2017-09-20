
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Dtf.Endpoint.Win
{
    public class NativeMethods
    {
        internal delegate bool EnumResNameProcDelegate(IntPtr hMoudle, IntPtr lpszType, IntPtr lpszName, IntPtr lParam);
        
        [DllImport("user32.dll")]
        internal static extern int WaitForInputIdle(IntPtr hProcess, int dwMilliseconds);

        [DllImport("kernel32.dll")]
        internal static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hFile, int dwFlags);

        [DllImport("kernel32.dll")]
        internal static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport("user32.dll")]
        internal static extern int LoadString(IntPtr hInst, uint uID, StringBuilder lpBuf, int nMaxBuffer);

        [DllImport("user32.dll")]
        internal static extern IntPtr LoadMenu(IntPtr hInst, IntPtr menuName);

        [DllImport("user32.dll")]
        internal static extern int GetMenuString(IntPtr hMenu, uint uId, StringBuilder str, int nMaxCount, uint uFlag);

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool EnumResourceNames(IntPtr hModule, IntPtr lpszType, EnumResNameProcDelegate EnumResNameProc, IntPtr lParam);

        [DllImport("kernel32.dll")]
        internal static extern IntPtr FindResource(IntPtr hModule, IntPtr lpName, IntPtr lpType);

        [DllImport("kernel32.dll")]
        internal static extern IntPtr LoadResource(IntPtr hModule, IntPtr hResInfo);

        [DllImport("kernel32.dll")]
        internal static extern IntPtr LockResource(IntPtr hResData);

        [DllImport("kernel32.dll")]
        internal static extern int SizeofResource(IntPtr hModule, IntPtr hResInfo);

        public enum ResourceType
        {
            RT_ACCELERATOR = 9, //Accelerator table. 
            RT_DIALOG = 5, // Dialog box. 
            RT_HTML = 23, // HTML, //. 
            RT_MENU = 4, // Menu resource. 
            RT_STRING = 6 // String-table entry. 
        }



        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, int flags);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);


        [Flags]
        public enum ForeGroundColor
        {
            //Black = 0x0000,
            Blue = 0x0001,
            Green = 0x0002,
            Cyan = 0x0003,
            Red = 0x0004,
            Magenta = 0x0005,
            Yellow = 0x0006,
            Grey = 0x0007,
            White = 0x0008
        }



        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        internal static extern int SendInput(uint nInputs, ref INPUT pInputs, int cbSize);

        [DllImport("user32.dll")]
        internal static extern int AttachThreadInput(int idAttach, int idAttachTo, int fAttach);

        [DllImport("user32.dll")]
        internal static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        [StructLayout(LayoutKind.Sequential)]
        internal struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public int mouseData;
            public int dwFlags;
            public int time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct HARDWAREINPUT
        {
            int uMsg;
            short wParamL;
            short wParamH;
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct INPUT
        {
            [FieldOffset(0)]
            public int type;
            [FieldOffset(4)]
            public MOUSEINPUT mi;
            [FieldOffset(4)]
            public KEYBDINPUT ki;
            [FieldOffset(4)]
            public HARDWAREINPUT hi;
        }


    }
}
