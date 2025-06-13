using System.Collections.Generic;
using UnityEngine;

namespace GHEvtSystem
{
    public class Event
    {

    }

    public class EventDispatcher
    {
        private static EventDispatcher _instance = null;

        private EventDispatcher()
        {

        }

        public static EventDispatcher Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EventDispatcher();
                }
                return _instance;
            }
        }

        public delegate void EventDelegate<T>(T e) where T : Event;

        private Dictionary<System.Type, System.Delegate> m_eventDelegates =
            new Dictionary<System.Type, System.Delegate>();

        public void AddListener<T>(EventDelegate<T> listener) where T : Event
        {
            System.Type type = typeof(T);
            System.Delegate del;

            if (m_eventDelegates.TryGetValue(type, out del))
            {
                del = System.Delegate.Combine(del, listener);
                m_eventDelegates[type] = del;
            }
            else
            {
                m_eventDelegates.Add(type, listener);
            }
        }

        public void RemoveListener<T>(EventDelegate<T> listener) where T : Event
        {
            System.Delegate del;

            if (m_eventDelegates.TryGetValue(typeof(T), out del))
            {
                System.Delegate newDel = System.Delegate.Remove(del, listener);

                if (newDel == null)
                {
                    m_eventDelegates.Remove(typeof(T));
                }
                else
                {
                    m_eventDelegates[typeof(T)] = newDel;
                }
            }
        }

        public void RaiseEvent<T>(T evtData) where T : Event
        {
            System.Delegate del;
            if (m_eventDelegates.TryGetValue(typeof(T), out del))
            {
                EventDelegate<T> callback = del as EventDelegate<T>;
                if (callback != null)
                {
                    callback(evtData);
                }
            }
        }
    }
}