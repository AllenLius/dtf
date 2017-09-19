
namespace Dta.Core
{
    public interface IApp
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="arguments"></param>
        /// <returns>app id</returns>
        void Launch();
        void Close();
    }
}
