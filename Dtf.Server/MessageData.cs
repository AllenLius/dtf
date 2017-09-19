using System;
using System.Diagnostics;
using System.Threading;

namespace HttpMessageProxy
{
    /// <summary>
    /// all member is not thread safe!
    /// </summary>
    public class MessageData
    {
        private const int WaitTime = 20 * 1000;
        private string m_data;
        private bool m_dataValid;
        private volatile int pending;
        private object m_get = new object();
        private object m_set = new object();
        private DataWait<bool> m_dataWait = new DataWait<bool>();
        private AutoResetEvent m_lock = new AutoResetEvent(true);
        private AutoResetEvent m_dataValidEvent = new AutoResetEvent(false);
        private AutoResetEvent m_dataInValidEvent = new AutoResetEvent(true);

        public MessageData()
        {
            m_dataWait.Data = false;
        }

        public bool GetData(out string data)
        {
            data = null;
            lock (m_get)
            {
                if (m_dataWait.WaitUtil(true, WaitTime))
                {
                    data = m_data;
                    Debug.Assert(!string.IsNullOrEmpty(data));
                    return true;
                }
            }
            return false;
        }

        //public void SetDataValid()
        //{
        //    Console.WriteLine("SetDataValid");
        //    m_dataValidEvent.Set();
        //}

        //public void SetDataInvalid()
        //{
        //    Console.WriteLine("SetDataValid");
        //    m_dataValidEvent.Set();
        //}

        public bool SetData(string data)
        {
            lock (m_set)
            {
                if (m_dataWait.WaitUtil(false, WaitTime))
                {
                    Debug.Assert(!string.IsNullOrEmpty(data));
                    m_data = data;
                    return true;
                }
            }
            return false;
        }

        public bool DataValid
        {
            set
            {
                Console.WriteLine("data valid:{0}", value);
                m_dataWait.Data = value;
            }
        }
    }
}
