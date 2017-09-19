
using Dta.Core;
using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace Dta.Endpoint.Win
{
    public class Mouse
    {
        public static void Click(MouseButton button)
        {
            Down(button);
            Up(button);
        }

        public static void Move(int x, int y)
        {
            NativeMethods.SetCursorPos(x, y);
        }

        public static void Down(MouseButton button)
        {
            int nFlag = 0x0;
            switch (button)
            {
                case MouseButton.Left:
                    nFlag = 0x2;
                    break;
                case MouseButton.Right:
                    nFlag = 0x8;
                    break;
                case MouseButton.Middle:
                    nFlag = 0x20;
                    break;
            }


            NativeMethods.MOUSEINPUT mi = new NativeMethods.MOUSEINPUT(); ;
            mi.dwFlags = nFlag;

            NativeMethods.INPUT input = new NativeMethods.INPUT();
            input.mi = mi;
            input.type = 0x0;

            int result = NativeMethods.SendInput(1, ref input, Marshal.SizeOf(input));
            if (result == 0)
            {
                throw new Exception("SendInput Error!");
            }
        }

        public static void Up(MouseButton button)
        {
            int nFlag = 0x0;
            switch (button)
            {
                case MouseButton.Left:
                    nFlag = 0x4;
                    break;
                case MouseButton.Right:
                    nFlag = 0x10;
                    break;
                case MouseButton.Middle:
                    nFlag = 0x40;
                    break;
            }


            NativeMethods.MOUSEINPUT mi = new NativeMethods.MOUSEINPUT(); ;
            mi.dwFlags = nFlag;

            NativeMethods.INPUT input = new NativeMethods.INPUT();
            input.mi = mi;
            input.type = 0x0;

            NativeMethods.SendInput(1, ref input, Marshal.SizeOf(input));
        }

    }
}
