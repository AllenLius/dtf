
using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace Dtf.Core
{
    public enum MouseButton
    {
        Left,
        Middle,
        Right
    }

    [Pattern("Mouse")]
    public interface IMousePattern
    {
        void Click();
        void Click(MouseButton button);
        void MoveTo();
        void Down(MouseButton button);
        void Up(MouseButton button);
    }
}
