# KIX - Kickstart your Unity3D appflow experience.
  ------------------------------------------------------------------------------
 current version: 1.0.0
<br />

   ------------------------------------------------------------------------------
   Unity3D Event system.<br />
   Responsible for all things loading, crunching, heavy lifting.<br />
   Responsible for all things event flow.
  
   Executes everything on different threads, but provides the ease
   of main thread availability.
  
   By Using KIX Events you can loosly couple you application components,
   which provides an ease of scalability and adjustability.<br /><br />
 
**LOADING SUPPORT**
   - Loads File as string
   - Loads File as ByteArray
  
 
   @example:<br />
   &ensp;&ensp;&ensp;```KIX.Instance.Load<string>( <my_file_path>, out data );```<br />
   &ensp;&ensp;&ensp;```KIX.Instance.Load<byte[]>( <my_file_path>, out data );```<br />
   &ensp;&ensp;OR (inside coroutine)<br />
   &ensp;&ensp;&ensp;```yield return KIX.Instance.Load<string>( <my_file_path>, out data );```<br /><br /><br />
 
**EVENT SYSTEM SUPPORT**
   - Fires events to all listeners.
   - Fires delayed events to all listeners.
   - Has inheritable / extentable event types.
  
  @example:<br />
    + Listen to KIX events:<br />
   &ensp;&ensp;&ensp;```KIX.Instance.Events += OnKIXEvent;```<br /><br />
    + Fire KIX events:<br />
    &ensp;&ensp;&ensp;```KIX.Instance.Fire(new KIXEvent(KIXEvent.DEFAULT, myData), sender);```<br />
    &ensp;&ensp;&ensp;```KIX.Instance.FireEvent(new KIXEvent(KIXEvent.DEFAULT, myData));```<br />
    &ensp;&ensp;&ensp;```KIX.Instance.FireDelayed(new KIXEvent(KIXEvent.DEFAULT, myData),3000, sender);```<br /><br /><br />
     
       
**LISTEN TO SPECIFIC EVENTS**
   - Ability to listen to specific events of type ( AddEventListener )
   - Ability to stop listening to specific events of type ( RemoveEventListener )
   - Base listener class with add and remove all events support
   - Base listener class with add and remove specific events support
  
   @example:<br />
    + Listen to specific KIX events:<br />
    &ensp;&ensp;&ensp;```KIX.Instance.AddEventListener( KIXEvent.DEFAULT, <my_method> );```<br /><br />
    + Stop listening to specific KIX events:<br />
    &ensp;&ensp;&ensp;```KIX.Instance.RemoveEventListener( KIXEvent.DEFAULT, <my_method> );```<br /><br /><br />
  
 
**FIRE TO SPECIFIC EVENTS**<br />
    - Ability to fire specific events, only for listeners to that specific event.
      aka addEventListener<br /><br />
    
  @example:<br />
    + Fire specific event:<br />
    &ensp;&ensp;&ensp;```KIX.Instance.FireEvent( new KIXEvent( KIXEvent.DEFAULT, <possible_data> ) );```
        <br /><br /><br />

**CREATE CUSTOM EVENTS**<br />
    - Ability to create custom events<br />
    - Ability to polymorph base event types class.<br />
    
   @example:<br />
       public class CustomEventTypes : KIXEventType<br />
       {<br />
       &ensp;&ensp;&ensp;public static readonly KIXEventType CUSTOM_TYPE = new KIXEventType(100, "CUSTOM_TYPE");<br />
       &ensp;&ensp;&ensp;public CustomEventTypes(int key, string value) : base(key, value){ }<br />
       }<br />
