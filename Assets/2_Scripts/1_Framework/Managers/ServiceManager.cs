using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watenk;

/// <summary> Class to store managers / single classes in. </summary>
public interface IServiceManager
{
	public void Add<T>(T service);
	public void Remove<T>(T service);
	public T Get<T>();
}

/// <summary> Class to store managers / single classes in. </summary>
public class ServiceManager : ASingleton<ServiceManager>, IServiceManager, IUpdateable, IFixedUpdateable
{
	private Dictionary<System.Type, object> services = new Dictionary<System.Type, object>();
	private List<IUpdateable> updateables = new List<IUpdateable>();
	private List<IFixedUpdateable> fixedUpdateables = new List<IFixedUpdateable>();

	//----------------------------------------

	public void Update()
	{
		foreach (IUpdateable updateable in updateables) updateable.Update();
	}

	public void FixedUpdate()
	{
		foreach (IFixedUpdateable fixedUpdateable in fixedUpdateables) fixedUpdateable.FixedUpdate(); 
	}
	
	public void Add<T>(T service)
	{
		if (services.ContainsKey(typeof(T)))
		{
		 	DebugUtil.ThrowError("ServiceManager already contains the " + typeof(T).Name + " service");
		 	return;	
		}
		
		services.Add(typeof(T), service);

		if (service is IUpdateable) updateables.Add((IUpdateable)service);
		if (service is IFixedUpdateable) fixedUpdateables.Add((IFixedUpdateable)service);
	}
	
	public void Remove<T>(T service)
	{
		if (!services.ContainsKey(typeof(T)))
		{
		 	DebugUtil.ThrowError("ServiceManager doesn't contain the " + typeof(T).Name + " service");
		 	return;	
		}
		
		services.Remove(typeof(T));

		if (service is IUpdateable) updateables.Remove((IUpdateable)service);
		if (service is IFixedUpdateable) fixedUpdateables.Remove((IFixedUpdateable)service);
	}

	public T Get<T>()
	{
		services.TryGetValue(typeof(T), out object service);

		if (service == null) DebugUtil.ThrowError(typeof(T).Name + " Sevice not found"); 

		return (T)service;
	}
}