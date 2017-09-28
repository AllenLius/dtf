
namespace Dtf.Core.Contract
{
    [Pattern("Keyboard")]
    public interface IKeyboardPattern
    {
        void SendKey(string keys);
    }
}
