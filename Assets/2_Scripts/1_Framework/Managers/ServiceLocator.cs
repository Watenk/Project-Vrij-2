using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watenk;

public class ServiceLocator : ASingleton<ServiceLocator>, IServiceLocator
{
	private Dictionary<System.Type, object> services = new Dictionary<System.Type, object>();
	private List<IUpdateable> updateables = new List<IUpdateable>();
	private List<IFixedUpdateable> fixedUpdateables = new List<IFixedUpdateable>();
	
	public ServiceLocator()
	{
		Add<EventManager>();
		Add<AudioManager>();
		AddMonobehaviour<TimerManager>();
	}
	
	public T Get<T>()
	{
		services.TryGetValue(typeof(T), out object service);

		if (service == null) DebugUtil.ThrowError(typeof(T).Name + " Sevice not found");

		return (T)service;
	}

	private T Add<T>() where T : new()
	{
		T service = new T();
		
		if (services.ContainsKey(service.GetType())) DebugUtil.ThrowError(typeof(T).Name + " Service already exists");
		services.Add(typeof(T), service);

		if (service is IUpdateable) { updateables.Add((IUpdateable)service); }
		if (service is IFixedUpdateable) { fixedUpdateables.Add((IFixedUpdateable)service); }
		return service;
	}
	
	private T AddMonobehaviour<T>() where T : MonoBehaviour
	{
		GameObject monobehaviour = new GameObject();
		T service = monobehaviour.AddComponent<T>();
		
		if (services.ContainsKey(service.GetType())) DebugUtil.ThrowError(typeof(T).Name + " Service already exists");
		services.Add(typeof(T), service);

		if (service is IUpdateable) { updateables.Add((IUpdateable)service); }
		if (service is IFixedUpdateable) { fixedUpdateables.Add((IFixedUpdateable)service); }
		return service;
	}
	
	private void Remove<T>(T service)
	{
		if (!services.ContainsKey(service.GetType())) DebugUtil.ThrowError(typeof(T).Name + " Service doesn't exists");
		services.Remove(typeof(T));

		if (service is IUpdateable) { updateables.Remove((IUpdateable)service); }
		if (service is IFixedUpdateable) { fixedUpdateables.Remove((IFixedUpdateable)service); }
	}
}
