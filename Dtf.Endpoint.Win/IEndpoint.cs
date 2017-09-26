namespace Dtf.Endpoint.Win
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Dtf.Core;
    using System.ServiceModel;

    [ServiceContract]
    public  interface IEndpoint
    {
        [OperationContract]
        T QueryInterface<T>();
    }
}
