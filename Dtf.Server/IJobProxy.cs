
namespace Dta.Server
{
    public interface IJobProxy
    {
        [Restful("POST", "/{client}/*", ParameterStyle.Uri)]
        void ExecuteJob(string client);

        [Restful("GET", "/{client}/", ParameterStyle.Uri)]
        void GetJobWait(string client);

        [Restful("PUT", "/{client}/", ParameterStyle.Uri)]
        void SendResult(string client);
    }
}
