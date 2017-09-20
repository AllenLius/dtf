using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Dtf.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class ResourceManager
    {
        static Dictionary<string, IResourceHandler> _resourceHandlers = new Dictionary<string, IResourceHandler>();

        static ResourceManager()
        {
            var asm = Assembly.GetExecutingAssembly();
            var handlers = asm.GetTypes().Where(t => Attribute.IsDefined(t, typeof(HandlerNameAttribute))).ToArray();
            RegisterResourceHandler(handlers);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="resourceKey">[HANDLER_TYPE]|[RESOURCE_KEY]</param>
        /// <returns></returns>
        public static object GetObject(string handlerType, string resourceKey)
        {
            IResourceHandler handler = null;
            if (!_resourceHandlers.TryGetValue(handlerType, out handler))
            {
                throw new Exception(string.Format("The handler {0} not registered!", handlerType));
            }
            return handler.GetObject(resourceKey);
        }

        public static void RegisterResourceHandler(params Type[] handlerTypes)
        {
            foreach (var type in handlerTypes)
            {
                if (!typeof(IResourceHandler).IsAssignableFrom(type))
                {
                    throw new Exception(string.Format("Type {0} must implement {1}!", type.Name, typeof(IResourceHandler).Name));
                }
                var handlerNameAttribute = type.GetCustomAttributes(typeof(HandlerNameAttribute), true).FirstOrDefault() as HandlerNameAttribute;
                string handlerName = handlerNameAttribute == null ? type.Name : handlerNameAttribute.Name;
                if (!_resourceHandlers.ContainsKey(handlerName))
                {
                    IResourceHandler handler = Activator.CreateInstance(type) as IResourceHandler;
                    _resourceHandlers.Add(handlerName, handler);
                }
            }
        }
    }
}
