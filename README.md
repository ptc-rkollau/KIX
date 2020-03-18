# KIX
Unity3D  Event system


 * KIX - Kickstart your interface experience.
   ------------------------------------------------------------------------------
   Responsible for all things loading, crunching, heavy lifting.
   Responsible for all things event flow.
  
   Executes everything on different threads, but provides the ease
   of main thread availability.
  
   By Using KIX Events you can loosly couple you application components,
   which provides an ease of scalability and adjustability.
 
 
 * LOADING SUPPORT.
   - Loads File as string
   - Loads File as ByteArray
  
 
  @example:
       KIX.Instance.Load<string>( <my_file_path>, out data );
       KIX.Instance.Load<byte[]>( <my_file_path>, out data );
       OR (inside coroutine)
       yield return KIX.Instance.Load<string>( <my_file_path>, out data );
 
 * EVENT SYSTEM SUPPORT.
   - Fires events to all listeners.
   - Fires delayed events to all listeners.
   - Has inheritable / extentable event types.
  
  @example:
    + Listen to KIX events:
      KIX.Instance.Events += OnKIXEvent;
      
    + Fire KIX events:
      KIX.Instance.Fire(new KIXEvent(KIXEvent.DEFAULT, myData), sender);
      KIX.Instance.FireEvent(new KIXEvent(KIXEvent.DEFAULT, myData));    
      KIX.Instance.FireDelayed(new KIXEvent(KIXEvent.DEFAULT, myData), 3000, sender);
     
       
 * LISTEN TO SPECIFIC EVENTS
   - Ability to listen to specific events of type ( AddEventListener )
   - Ability to stop listening to specific events of type ( RemoveEventListener )
   - Base listener class with add and remove all events support
   - Base listener class with add and remove specific events support
  
  @example:
    + Listen to specific KIX events.
       KIX.Instance.AddEventListener( KIXNavEvent.HOME, <my_method> );
       
    + Stop listening to specific KIX events.
       KIX.Instance.RemoveEventListener( KIXNavEvent.HOME, <my_method> );
  
 
 * FIRE TO SPECIFIC EVENTS
    - Ability to fire specific events, only for listeners to that specific event.
      aka addEventListener
    
  @example:
     + Fire specific event:
        KIX.Instance.FireEvent( new KIXEvent( KIXNavEvent.HOME, <possible_data> ) );
        

 *  CREATE CUSTOM EVENTS
    - Ability to create custom events
    - Ability to polymorph base event types class.
    
   @example:
       public class CustomEventTypes : KIXEventType
       {
           public static readonly KIXEventType CUSTOM_TYPE = new KIXEventType(100, "CUSTOM_TYPE");
 
           public CustomEventTypes(int key, string value) : base(key, value){ }
       }
