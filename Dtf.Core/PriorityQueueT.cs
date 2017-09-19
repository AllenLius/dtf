using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dta.Core
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class PriorityQueue<T>
    {
        private List<int> m_priorityList = new List<int>();
        private List<T> m_itemList = new List<T>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="priority">1 is the highest priorty</param>
        public void Enqueue(T item, int priority)
        {
            int index = m_priorityList.FindIndex(r => r > priority);
            if (index < 0)
            {
                m_priorityList.Add(priority);
                m_itemList.Add(item);
            }
            else
            {
                m_priorityList.Insert(index, priority);
                m_itemList.Insert(index, item);
            }
        }

        public T Dequeue()
        {
            if (!m_priorityList.Any())
            {
                throw new Exception("Queue is empty!");
            }
            T item = m_itemList.First();
            m_priorityList.RemoveAt(0);
            m_itemList.RemoveAt(0);
            return item;
        }

        public int Count
        {
            get
            {
                return m_priorityList.Count;
            }
        }
    }
}
