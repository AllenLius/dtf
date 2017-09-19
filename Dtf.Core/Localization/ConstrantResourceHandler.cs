
namespace Dta.Core
{
    [HandlerName("ConstantResourceHandler")]
    public class ConstantResourceHandler : IResourceHandler
    {
        public object GetObject(string resourceKey)
        {
            return resourceKey;
        }
    }
}
