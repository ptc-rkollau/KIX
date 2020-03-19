/*
 * KIX - Kickstart your interface experience.
 * ------------------------------------------------------------------------------
 * Current Version 1.0.1 - March 2020
 * ------------------------------------------------------------------------------
 * 
 * Responsible for all things loading, crunching, heavy lifting.
 * Responsible for all things event flow.
 * 
 * Executes everything on different threads, but provides the ease
 * of main thread availability.
 * 
 * By Using KIX Events you can loosly couple you application components,
 * which provides an ease of scalability and adjustability.
 *
 * 
 * 
 * ------------------------------------------------------------------------------
 * 
 * LOADING SUPPORT.
 * - Loads File as string
 * - Loads File as ByteArray
 * 
 * @author : Robin Kollau ( rkollau@ptc.com )
 * @version: 1.0.0
 * @date   : 08 July 2019
 *
 * @example:
 *      KIX.Instance.Load<string>( <my_file_path>, out data );
 *      KIX.Instance.Load<byte[]>( <my_file_path>, out data );
 *
 *      OR (inside coroutine)
 *
 *      yield return KIX.Instance.Load<string>( <my_file_path>, out data );
 *
 * ------------------------------------------------------------------------------
 * 
 * EVENT SYSTEM SUPPORT.
 *  - Fires events to all listeners.
 *  - Fires delayed events to all listeners.
 *  - Has inheritable / extentable event types.
 * 
 * @author : Robin Kollau ( rkollau@ptc.com )
 * @version: 1.0.0
 * @date   : 25 October 2019 
 * 
 * 
 * @example:
 *   + Listen to KIX events:
 *     KIX.Instance.Events += OnKIXEvent;
 *     
 *   + Fire KIX events:
 *     KIX.Instance.Fire(new KIXEvent(KIXEvent.DEFAULT, myData), sender);
 *     KIX.Instance.FireEvent(new KIXEvent(KIXEvent.DEFAULT, myData));    
 *     KIX.Instance.FireDelayed(new KIXEvent(KIXEvent.DEFAULT, myData), 3000, sender);
 *    
 *      
 *      
 * ------------------------------------------------------------------------------ 
 * 
 * LISTEN TO SPECIFIC EVENTS
 *  - Ability to listen to specific events of type ( AddEventListener )
 *  - Ability to stop listening to specific events of type ( RemoveEventListener )
 *  - Base listener class with add and remove all events support
 *  - Base listener class with add and remove specific events support
 * 
 * @author : Robin Kollau ( rkollau@ptc.com )
 * @version: 1.0.0
 * @date   : 11 December 2019 
 * 
 * 
 * @example:
 *   + Listen to specific KIX events.
 *      KIX.Instance.AddEventListener( KIXNavEvent.HOME, <my_method> );
 *      
 *   + Stop listening to specific KIX events.
 *      KIX.Instance.RemoveEventListener( KIXNavEvent.HOME, <my_method> );
 * 
 * 
 * ------------------------------------------------------------------------------
 * FIRE TO SPECIFIC EVENTS
 *   - Ability to fire specific events, only for listeners to that specific event.
 *     aka addEventListener
 *   
 *  @author : Robin Kollau ( rkollau@ptc.com )
 *  @version: 1.0.0
 *  @date   : 11 December 2019 
 *  
 * @example:
 *    + Fire specific event:
 *       KIX.Instance.FireEvent( new KIXEvent( KIXNavEvent.HOME, <possible_data> ) );
 *       
 * ------------------------------------------------------------------------------ 
 * 
 *  CREATE CUSTOM EVENTS
 *   - Ability to create custom events
 *   - Ability to polymorph base event types class.
 *   
 *  @author : Robin Kollau
 *  @version: 1.0.0
 *  @date   : 09 March 2020 
 *   
 *   
 *  @example:
 *      public class CustomEventTypes : KIXEventType
 *      {
 *          public static readonly KIXEventType CUSTOM_TYPE = new KIXEventType(100, "CUSTOM_TYPE");
 *
 *          public CustomEventTypes(int key, string value) : base(key, value){ }
 *      }
 *  
 *  
 *  
 *  
 */
using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections;

public class KIX : UnityEngine.CustomYieldInstruction
{
    //events.
    public delegate void OnEvents(KIXEvent evt);
    public OnEvents Events;


    //privates.
    private static readonly KIX instance = new KIX();
    private bool isWorking;
    private Dictionary<string, List<Action<KIXEvent>>> listeners_ = new Dictionary<string, List<Action<KIXEvent>>>();



    /// <summary>
    /// Instance
    /// Singleton constructor
    /// </summary>
    public static KIX Instance { get { return instance; } }


    #region KIX_EVENT_SYSTEM
    /// <summary>
    /// Add Event Listener
    /// Adds an event listener to specific KIX events of type.
    /// </summary>
    /// <param name="evtType">KIXEventType</param>
    /// <param name="method">Action<KIXEvent></param>
    public void AddEventListener(KIXEventType evtType, Action<KIXEvent> method)
    {
        if (!listeners_.ContainsKey(evtType.ToString())) listeners_[evtType.ToString()] = new List<Action<KIXEvent>>();
        listeners_[evtType.ToString()].Add(method);
    }


    /// <summary>
    /// Add Event Listener
    /// Adds an event listener to specific KIX events of type.
    /// </summary>
    /// <param name="evtType">KIXEventType</param>
    /// <param name="method">Action<KIXEvent></param>
    public void AddEventListener(string evtType, Action<KIXEvent> method)
    {
        if (!listeners_.ContainsKey(evtType)) listeners_[evtType] = new List<Action<KIXEvent>>();
        listeners_[evtType].Add(method);
    }

    /// <summary>
    /// Remove Event Listener
    /// Removes an event listener to specific KIX events of type.
    /// </summary>
    /// <param name="evtType">KIXEventType</param>
    /// <param name="method">Action<KIXEvent></param>
    public void RemoveEventListener(KIXEventType evtType, Action<KIXEvent> method)
    {
        listeners_[evtType.ToString()]?.Remove(method);
        if (listeners_[evtType.ToString()].Count == 0)
            listeners_.Remove(evtType.ToString());
    }

    /// <summary>
    /// Fire
    /// Fires an KIXEvent
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void Fire(KIXEvent e, object sender = null)
    {
        OnEvent(e, sender);
        if (listeners_.ContainsKey(e.Type.ToString()))
            FireEvent(e);
    }

    /// <summary>
    /// Fire Delayed
    /// Fires an KIXEvent after a defined delay in ms.
    /// </summary>
    /// <param name="e">KIXEvent to fire</param>
    /// <param name="delayInMS">float</param>
    /// <param name="sender">object</param>
    public void FireDelayed(KIXEvent e, int delayInMS, UnityEngine.MonoBehaviour sender)
    {
        sender.StartCoroutine(FireDelayedEvent(e, delayInMS));
    }

    /// <summary>
    /// Fire Delayed Event
    /// Fires an Event after a MS delay
    /// </summary>
    /// <param name="evt">KIXEvent</param>
    /// <param name="delayInMS">int</param>
    /// <returns></returns>
    private static IEnumerator FireDelayedEvent(KIXEvent evt, int delayInMS)
    {
        int timer = delayInMS / 10;
        while (timer > 0)
        {
            yield return new UnityEngine.WaitForSeconds(0.01f);
            --timer;
        }
        KIX.Instance.FireEvent(evt);
    }


    /// <summary>
    /// Fire Event
    /// Fires an KIXEvent to listeners that registered to that
    /// specific event.
    /// </summary>
    /// <param name="evt">KIXEvent</param>
    public void FireEvent(KIXEvent evt)
    {
        if (!listeners_.ContainsKey(evt.Type.ToString())) KIX.Instance.Fire(evt, this);
        else
        {
            var list = listeners_[evt.Type.ToString()].ToArray();
            for (int i = 0; i < list.Length; ++i) list[i](evt);
        }
       

    }
    #endregion


    #region KIX_LOAD_SYSTEM
    /// <summary>
    /// Load
    /// Generic load  method for all things loading.
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    /// <param name="url">string</param>
    ///<returns>T<returns>
    public object LoadFile<T>(string url, out T output)
    {
        OnEvent(new KIXEvent(KIXEventType.START));
        isWorking = true;
        Type gt = typeof(T);
        T result = default(T);
        object o = new object();

        Thread t = null;
        //load based on type.
        if (gt == typeof(string)) t = Execute(() => { o = LoadString(url); });
        else if (gt == typeof(byte[])) t = Execute(() => { o = LoadBytes(url); });

        //wait.
        t.Join();
        // while (isWorking) { }
        result = (T)Convert.ChangeType(o, typeof(T));
        output = result;
        OnEvent(new KIXEvent(KIXEventType.STOP));
        return result;
    }

    /// <summary>
    /// Load URL ASYNC
    /// Loads url data async.
    /// </summary>
    /// <typeparam name="TResult">output type</typeparam>
    /// <param name="url">string</param>
    /// <returns></returns>
    public async Task<string> LoadURLAsync<TResult>(string url)
    {
        Uri uri = new Uri(url);
        using (var client = new HttpClient())
        {
            var msg = await client.GetAsync(uri);
            if (!msg.IsSuccessStatusCode) throw new Exception(KIX.CreateKIXMSG(msg.ReasonPhrase));
            string t = await msg.Content.ReadAsStringAsync();
            return t;
        }
    }
    #endregion


    #region KIX_RUN
    /// <summary>
    /// Run
    /// runs a task in a thread and waits for it to finish.
    /// </summary>
    /// <param name="task"></param>
    /// <returns>object</returns>
    public object Run(Action task)
    {
        OnEvent(new KIXEvent(KIXEventType.START));
        isWorking = true;
        Thread t = Execute(task);
        t.Join();
        // while (isWorking) { }
        OnEvent(new KIXEvent(KIXEventType.STOP));
        return new object();
    }
    #endregion


    //----------------------------------------------------
    #region PUBLICS
    /// <summary>
    /// keepWaiting
    /// Returns True if MetaLoader is in an Operation.
    /// Returns False if MetaLoader is Idle.
    /// Can be used to wait for the loader to complete inside a coroutine.
    /// </summary>
    public override bool keepWaiting { get { return isWorking; } }

    /// <summary>
    /// IsWorking
    /// Returns True if MetaLoader is in an Operation.
    /// Returns False if MetaLoader is Idle.
    /// Can be used to wait for the loader to complete.
    /// </summary>
    public bool IsWorking { get { return isWorking; } }

    /// <summary>
    /// Debugging
    /// Get / Set debug messages on or of.
    /// </summary>
    public static bool Debugging { get; set; }
    #endregion

    #region PRIVATES
    /// <summary>
    /// Execute
    /// Executes a task in a thread.
    /// </summary>
    /// <param name="task">Action</param>
    private Thread Execute(Action task)
    {
        Thread t = new Thread(() =>
        {
            task();
            isWorking = false;
        });
        t.Start();
        return t;
    }


    /// <summary>
    /// OnEvent
    /// Fired when an KIX event has occured.
    /// </summary>
    /// <param name="e"></param>
    private void OnEvent(KIXEvent e, object sender = null)
    {
        var handler = Events;
        handler?.Invoke(e);
    }
    #endregion

    #region LOADING
    /// <summary>
    /// Load Text File
    /// Loads a text file
    /// </summary>
    /// <param name="path">string</param>
    /// <returns>string</returns>
    private string LoadString(string path)
    {
        StreamReader reader = new StreamReader(path);
        string s = reader.ReadToEnd();
        reader.Close();
        return s;
    }


    /// <summary>
    /// Load Bytes
    /// Loads a byte array from file.
    /// </summary>
    /// <param name="path">string</param>
    /// <returns>byte[]</returns>
    private byte[] LoadBytes(string path)
    {
        byte[] b = new byte[1];
        return b;
    }
    #endregion

    #region LOGGING
    public static void Log(string msg)
    {
        if (KIX.Debugging)
            UnityEngine.Debug.Log(KIX.CreateKIXMSG(msg));
    }
    public static string CreateKIXMSG(string msg)
    {
        return "[KIX] - " + msg;
    }
    #endregion
}



#region KIX Events & Types

public struct KIXData
{
    public object Value;
}



/*
 * KIX Event
 * Communication body for KIX occurences.
 *
 */
public class KIXEvent : EventArgs
{
    public string Type { private set; get; }
    public KIXData Data { private set; get; }

    public KIXEvent(KIXEventType t, KIXData data = new KIXData() )
    {
        Type = t.Value;
        Data = data;
    }
    public KIXEvent(string t, KIXData data = new KIXData())
    {
        Type = t;
        Data = data;
    }
}


/*
 *  Enum Base Type
 *  Base type for expandable enum objects.
 * 
 * 
 */
public abstract class EnumBaseType<T> where T : EnumBaseType<T>
{
    protected static List<T> enumValues = new List<T>();

    public readonly int Key;
    public readonly string Value;

    public EnumBaseType(int key, string value)
    {
        Key = key;
        Value = value;
        enumValues.Add((T)this);
    }

    protected static ReadOnlyCollection<T> GetBaseValues()
    {
        return enumValues.AsReadOnly();
    }

    protected static T GetBaseByKey(int key)
    {
        foreach (T t in enumValues)
        {
            if (t.Key == key) return t;
        }
        return null;
    }

    protected static T GetBaseByValue(string value)
    {
        foreach (T t in enumValues)
        {
            if (t.Value == value) return t;
        }
        return null;
    }

    public override string ToString()
    {
        return Value;
    }
}


/*
 *  KIX Event Type
 *  Inheritable enum for all KIX events.
 *  Based on EnumBaseType
 * 
 */
[System.Serializable]
public class KIXEventType : EnumBaseType<KIXEventType>
{
    //event types.
    public static readonly KIXEventType DEFAULT = new KIXEventType(0, "DEFAULT");
    public static readonly KIXEventType START = new KIXEventType(1, "START");
    public static readonly KIXEventType STOP = new KIXEventType(2, "STOP");
    public static readonly KIXEventType BUSY = new KIXEventType(3, "BUSY");

    public KIXEventType(int key, string value) : base(key, value) { }

    public static ReadOnlyCollection<KIXEventType> GetValues()
    {
        return GetBaseValues();
    }

    public static KIXEventType GetByKey(int key)
    {
        return GetBaseByKey(key);
    }

    public static KIXEventType GetByValue(string value)
    {
        return GetBaseByValue(value);
    }

}



/*
 * I KIX Listener
 * Listener Interface for KIX Event receivers
 * 
 */
public interface IKIXListener
{
    void OnKIXEvent(KIXEvent e);
}

/**
 *  KIX Listener
 *  Add functionality to enable system to start and stop listening to KIXEventsSystem.
 * 
 */
public class KIXListener : UnityEngine.MonoBehaviour, IKIXListener
{

    /// <summary>
    /// Add Event Listener
    /// Add a listener for specific KIX events
    /// and relays it to a method proxy.
    /// </summary>
    /// <param name="evtType">KIXEventType</param>
    /// <param name="proxy">Action<KIXEvent></param>
    public void AddEventListener(KIXEventType evtType, Action<KIXEvent> proxy)
    {
        KIX.Instance.AddEventListener(evtType, proxy);
    }
    public void AddEventListener(string evtType, Action<KIXEvent> proxy)
    {
        KIX.Instance.AddEventListener(evtType, proxy);
    }

    /// <summary>
    /// Remove Event Listener
    /// Removes a listener for specific KIX events
    /// </summary>
    /// <param name="evtType">KIXEventType</param>
    /// <param name="proxy">Action<KIXEvent></param>
    public void RemoveEventListener(KIXEventType evtType, Action<KIXEvent> proxy)
    {
        KIX.Instance.RemoveEventListener(evtType, proxy);
    }



    /// <summary>
    /// Add Event Listener
    /// Add a listener for all KIX events
    /// and relays it to a method proxy.
    /// </summary>
    public void AddGeneralEventListener()
    {
        KIX.Instance.Events += OnKIXEvent;
    }

    /// <summary>
    /// Remove General Event Listener
    /// Removes a general listener for all KIX events
    /// </summary>
    /// <param name="proxy">Action<KIXEvent></param>
    public void RemoveGeneralEventListener()
    {
        KIX.Instance.Events -= OnKIXEvent;
    }

    /// <summary>
    /// On KIX Event
    /// Proxy method for all KIX events.
    /// and relays it to a method proxy.
    /// </summary>
    /// <param name="evt">KIXEvent</param>
    public virtual void OnKIXEvent(KIXEvent evt)
    {
        throw new NotImplementedException("OnKIXEvent has not yet been implmentented, please override and imnplement OnKIXEvent( KIXEvent evt );");
    }
}

/*
 * I KIX Dispatcher
 * Dispatcher Interface for KIX Event dispatchers
 * 
 */
public interface IKIXDispatcher
{
    void FireKIXEvent(KIXEvent evt);
}

/**
 *  KIX Dispatcher
 *  Add functionality to enable system to fire events to KIX Event System.
 * 
 */
public class KIXDispatcher : UnityEngine.MonoBehaviour, IKIXDispatcher
{
    public void FireKIXEvent(KIXEvent evt)
    {
        KIX.Instance.FireEvent(evt);
    }
}
#endregion