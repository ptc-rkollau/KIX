# KIX - Kickstart your interface experience.

   ------------------------------------------------------------------------------
   Unity3D Event system.<br />
   Responsible for all things loading, crunching, heavy lifting.<br />
   Responsible for all things event flow.
  
   Executes everything on different threads, but provides the ease
   of main thread availability.
  
   By Using KIX Events you can loosly couple you application components,
   which provides an ease of scalability and adjustability.
 
 
**LOADING SUPPORT**
   - Loads File as string
   - Loads File as ByteArray
  
 
   @example:<br />
       KIX.Instance.Load<string>( <my_file_path>, out data );<br />
       KIX.Instance.Load<byte[]>( <my_file_path>, out data );<br />
       OR (inside coroutine)<br />
       yield return KIX.Instance.Load<string>( <my_file_path>, out data );<br />
 
**EVENT SYSTEM SUPPORT**
   - Fires events to all listeners.
   - Fires delayed events to all listeners.
   - Has inheritable / extentable event types.
  
  @example:<br />
    + Listen to KIX events:<br />
         KIX.Instance.Events += OnKIXEvent;<br /><br />
    + Fire KIX events:<br />
         KIX.Instance.Fire(new KIXEvent(KIXEvent.DEFAULT, myData), sender);<br />
         KIX.Instance.FireEvent(new KIXEvent(KIXEvent.DEFAULT, myData)); <br />   
         KIX.Instance.FireDelayed(new KIXEvent(KIXEvent.DEFAULT, myData), 3000, sender);<br /><br />
     
       
**LISTEN TO SPECIFIC EVENTS**
   - Ability to listen to specific events of type ( AddEventListener )
   - Ability to stop listening to specific events of type ( RemoveEventListener )
   - Base listener class with add and remove all events support
   - Base listener class with add and remove specific events support
  
   @example:<br />
    + Listen to specific KIX events:<br />
         KIX.Instance.AddEventListener( KIXNavEvent.HOME, <my_method> );<br /><br />
    + Stop listening to specific KIX events:<br />
         KIX.Instance.RemoveEventListener( KIXNavEvent.HOME, <my_method> );<br /><br />
  
 
**FIRE TO SPECIFIC EVENTS**<br />
    - Ability to fire specific events, only for listeners to that specific event.
      aka addEventListener<br /><br />
    
  @example:<br />
    + Fire specific event:<br />
         KIX.Instance.FireEvent( new KIXEvent( KIXNavEvent.HOME, <possible_data> ) );
        

**CREATE CUSTOM EVENTS**<br />
    - Ability to create custom events<br />
    - Ability to polymorph base event types class.<br />
    
   @example:<br />
       public class CustomEventTypes : KIXEventType<br />
       {<br />
           public static readonly KIXEventType CUSTOM_TYPE = new KIXEventType(100, "CUSTOM_TYPE");<br />
           public CustomEventTypes(int key, string value) : base(key, value){ }<br />
       }<br />
