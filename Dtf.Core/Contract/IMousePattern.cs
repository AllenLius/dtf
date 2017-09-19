
using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace Dta.Core
{
    public enum MouseButton
    {
        Left,
        Middle,
        Right
    }

    public interface IMousePattern
    {
        void Click();
        void Click(MouseButton button);
        void MoveTo();
        void Down(MouseButton button);
        void Up(MouseButton button);
    }
}
