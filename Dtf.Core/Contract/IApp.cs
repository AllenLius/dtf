
namespace Dtf.Core
{
    public interface IApp
    {
        /// <summary>
        /// Launch application
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="arguments"></param>
        /// <returns>app id</returns>
        void Launch();

        /// <summary>
        /// Close application
        /// </summary>
        void Close();
    }
}
