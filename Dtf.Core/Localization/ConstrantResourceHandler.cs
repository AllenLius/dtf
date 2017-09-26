
namespace Dtf.Core
{
    [HandlerName("ConstantResourceHandler")]
    public class ConstantResourceHandler : IResourceHandler
    {
        public string GetObject(string resourceKey)
        {
            return resourceKey;
        }
    }
}
