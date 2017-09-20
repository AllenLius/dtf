using System;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;

namespace Dtf.Server
{
    public enum JobStatus
    {
        Idle,
        Scheduled,
        Running,
        Finished
    }

    public class Job
    {
        private ManualResetEvent m_jobStatusChanged = new ManualResetEvent(false);
        private JobStatus m_status;
        private volatile int m_pendingWait;

        public Job()
        {
            InvokeLock = new object();
            ExecuteLock = new object();
            PutResultLock = new object();
        }

        public bool Wait(JobStatus status, TimeSpan timeout)
        {
            DateTime startTime = DateTime.Now;
            if (Status == status)
            {                
                return true;
            }
            while (true)
            {
                m_pendingWait++;
                m_jobStatusChanged.WaitOne(timeout);
                m_pendingWait--;
                if (m_pendingWait == 0)
                {
                    m_jobStatusChanged.Reset();
                }      
                if (Status == status)
                {
                    return true;
                }
                if (DateTime.Now - startTime > timeout)
                {
                    return false;
                }                          
            }
        }

        public JobStatus Status
        {
            get
            {
                return m_status;
            }
            set
            {
                m_status = value;
                m_jobStatusChanged.Set();
            }
        }
        public object InvokeLock { get; set; }
        public object ExecuteLock { get; set; }
        public object PutResultLock { get; set; }

        public string Message { get; set; }
    }
}
