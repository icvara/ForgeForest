using System.Collections.Generic;
using System;
using UnityEngine;

/// <summary>
/// A static Service locator for registering and Resolving Game Service Interfaces.
/// Registers Services on Awake before they are resolved
/// </summary>
public static class ServiceLocator
{
    private static readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();
    private static readonly Dictionary<Type, List<Action<object>>> _PendingCallbacks = new Dictionary<Type, List<Action<object>>>();

    /// <summary>
    /// Register a service under its interface Type
    /// Fire any callbacks that have been queued before registration.
    /// </summary>
    
    public static void Register<Tservice>(Tservice implementation)where Tservice : class
    {
        //Store a Service from its Type {That being a Class} {can be Upgraded To an Interface based system}
        Type serviceType = typeof(Tservice);

        if (_services.ContainsKey(serviceType))
        {
            Debug.LogWarning($"[ServiceLocator] Overwriting existing Registration for service {serviceType.Name}" + 
                $"Your original service may still have unregister functionality so correct this error");
        }

        _services[serviceType] = implementation;

        ///<summary>
        ///Resolves any methods that tried to resolve from a service type before it was registerd
        ///Fires of all resolve actions queued
        /// </summary>
        
        if(_PendingCallbacks.TryGetValue(serviceType,out List<Action<object>> callbacks))
        {
            foreach(Action<object> callback in callbacks)
            {
                callback(implementation);
            }
        }

        _PendingCallbacks.Remove(serviceType);
    }

    ///<summary>
    ///unregisters a service. Always call this from the providers on Destroy
    /// </summary>

    public static void Unregister<Tservice>() where Tservice : class
    {
        Type serviceType = typeof(Tservice);

        //In are if method we remove the service and if it hasnt been registerd we send a debug
        if (!_services.Remove(serviceType))
        {
            Debug.LogWarning($"[ServiceLocator] tried to Unregister {serviceType.Name} " +
                $"but it was not registerd.");
            
        }
    }

    ///<summary>
    ///Resolves a service immdiately. Returns null and logs a warning if not registerd.
    ///use the callback overload below if your uncertain about the when the service was registered Awake,Start,Enable
    ///That version will store the action untill a service is registered
    /// </summary>
    public static Tservice Get<Tservice>() where Tservice : class
    {
        if(_services.TryGetValue(typeof(Tservice),out object service))
        {
            return service as Tservice;
        }

        Debug.LogWarning($"[ServiceLocator]'{typeof(Tservice).Name}' is not registerd");

        return null;
    }

    ///<summary>
    ///Resolves a service and invokes onResolved immediately if already registerd
    ///or will defer the call until the service has been registered.
    ///returns a cancel handel - store it and invoke it from onDestroy to prevent 
    ///the callback firing on a destroyed consumer if the scene closes before resoulution
    ///</summary>
    public static Action Get<Tservice>(Action<Tservice> onResolved) where Tservice : class
    {
        Type serviceType = typeof(Tservice);

        if(_services.TryGetValue(serviceType,out object service))
        {
            onResolved(service as Tservice);
            return () => { }; //Is the service resolves we return nothing
        }

        //Checks if we have pending callback for that service already if not we add a new list
        if (!_PendingCallbacks.ContainsKey(serviceType))
        {
            _PendingCallbacks[serviceType] = new List<Action<object>>();
        }

        //we wrap the call back and add it to pending callbacks
        Action<object> wrappedCallback = raw => onResolved(raw as Tservice);

        _PendingCallbacks[serviceType].Add(wrappedCallback);

        return () =>
        {
            if (_PendingCallbacks.TryGetValue(serviceType, out List<Action<object>> callbacks))
            {
                callbacks.Remove(wrappedCallback);
            }
        };
    }

    ///<summary>
    ///returns true if a sevice of the given type is
    ///currently registered
    /// </summary>
    public static bool isRegistered<Tservice>() where Tservice : class
    {
        return _services.ContainsKey(typeof(Tservice));
    }

    ///<summary>
    ///clears all services and pending callbacks
    ///useful for full scene reset
    /// </summary>
    public static void Clear()
    {

        _services.Clear();
        _PendingCallbacks.Clear();
    }
}
