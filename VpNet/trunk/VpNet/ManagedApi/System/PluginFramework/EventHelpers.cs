using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace VpNet.PluginFramework
{
    //--------------------------------------------------------------------------------
    static public class EventHelpers
    {
        static global::System.Collections.Generic.Dictionary<Type, List<FieldInfo>> dicEventFieldInfos = new global::System.Collections.Generic.Dictionary<Type, List<FieldInfo>>();

        static BindingFlags AllBindings
        {
            get { return BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static; }
        }

        //--------------------------------------------------------------------------------
        static List<FieldInfo> GetTypeEventFields(Type t)
        {
            if (dicEventFieldInfos.ContainsKey(t))
                return dicEventFieldInfos[t];
            var lst = new List<FieldInfo>();
            BuildEventFields(t, lst);
            dicEventFieldInfos.Add(t, lst);
            return lst;
        }

        //--------------------------------------------------------------------------------
        static void BuildEventFields(Type t, List<FieldInfo> lst)
        {
            foreach (EventInfo ei in t.GetEvents(AllBindings))
            {
                Type dt = ei.DeclaringType;
                FieldInfo fi = dt.GetField(ei.Name, AllBindings);
                if (fi != null)
                    lst.Add(fi);
            }
        }

        //--------------------------------------------------------------------------------
        static EventHandlerList GetStaticEventHandlerList(Type t, object obj)
        {
            MethodInfo mi = t.GetMethod("get_Events", AllBindings);
            return (EventHandlerList)mi.Invoke(obj, new object[] { });
        }

        //--------------------------------------------------------------------------------
        public static void RemoveAllEventHandlers(object obj) { RemoveEventHandler(obj, ""); }

        //--------------------------------------------------------------------------------
        public static void RemoveEventHandler(object obj, string EventName)
        {
            if (obj == null)
                return;

            Type t = obj.GetType();
            IEnumerable<FieldInfo> event_fields = GetTypeEventFields(t);
            EventHandlerList static_event_handlers = null;

            foreach (FieldInfo fi in event_fields)
            {
                if (EventName != "" && string.Compare(EventName, fi.Name, true) != 0)
                    continue;

                // After hours and hours of research and trial and error, it turns out that
                // STATIC Events have to be treated differently from INSTANCE Events...
                if (fi.IsStatic)
                {
                    // STATIC EVENT
                    if (static_event_handlers == null)
                        static_event_handlers = GetStaticEventHandlerList(t, obj);

                    object idx = fi.GetValue(obj);
                    Delegate eh = static_event_handlers[idx];
                    if (eh == null)
                        continue;

                    Delegate[] dels = eh.GetInvocationList();
                    if (dels == null)
                        continue;

                    EventInfo ei = t.GetEvent(fi.Name, AllBindings);
                    foreach (Delegate del in dels)
                        ei.RemoveEventHandler(obj, del);
                }
                else
                {
                    // INSTANCE EVENT
                    EventInfo ei = t.GetEvent(fi.Name, AllBindings);
                    if (ei != null)
                    {
                        object val = fi.GetValue(obj);
                        Delegate mdel = (val as Delegate);
                        if (mdel != null)
                        {
                            foreach (Delegate del in mdel.GetInvocationList())
                                ei.RemoveEventHandler(obj, del);
                        }
                    }
                }
            }
        }

        //--------------------------------------------------------------------------------
    }
}
