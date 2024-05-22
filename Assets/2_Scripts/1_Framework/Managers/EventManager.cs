using System;
using System.Collections.Generic;
using UnityEngine;
using Watenk;

public enum Event
{
	// Swarm
	OnPlayerHealth,
}

// TODO: use params to generalize the paramers
/// <summary> A global eventManager that supports up to 2 parameters </summary>
public class EventManager
{
	private Dictionary<EventManagerKey, object> eventManagers = new Dictionary<EventManagerKey, object>();

	//--------------------------------------------------

	// No Parameters
	public void AddListener(Event eventName, Action listener){
		Get().AddListener(eventName, listener);
	}

	public void RemoveListener(Event eventName, Action listener){
		Get().RemoveListener(eventName, listener);
	}

	public void Invoke(Event eventName){
		Get().Invoke(eventName);
	}

	private EventManagerNoParameter Get(){
		Type type = typeof(EventManagerNoParameter);
		EventManagerKey key = new EventManagerKey(type);

		if (eventManagers.ContainsKey(key)){
			return eventManagers[key] as EventManagerNoParameter;
		}
		else{
			EventManagerNoParameter newEventManager = new EventManagerNoParameter();
			eventManagers.Add(key, newEventManager);
			return newEventManager;
		}
	}

	// One Parameter
	public void AddListener<T>(Event eventName, Action<T> listener){
		Get<T>().AddListener(eventName, listener);
	}

	public void RemoveListener<T>(Event eventName, Action<T> listener){
		Get<T>().RemoveListener(eventName, listener);
	}

	public void Invoke<T>(Event eventName, T eventParam){
		Get<T>().Invoke(eventName, eventParam);
	}

	private EventManagerOneParameter<T> Get<T>(){
		Type type = typeof(T);
		EventManagerKey key = new EventManagerKey(type);

		if (eventManagers.ContainsKey(key)){
			return eventManagers[key] as EventManagerOneParameter<T>;
		}
		else{
			EventManagerOneParameter<T> newEventManager = new EventManagerOneParameter<T>();
			eventManagers.Add(key, newEventManager);
			return newEventManager;
		}
	}

	// Two Parameter
	public void AddListener<T, U>(Event eventName, Action<T, U> listener){
		Get<T, U>().AddListener(eventName, listener);
	}

	public void RemoveListener<T, U>(Event eventName, Action<T, U> listener){
		Get<T, U>().RemoveListener(eventName, listener);
	}

	public void Invoke<T, U>(Event eventName, T eventParam1, U eventParam2){
		Get<T, U>().Invoke(eventName, eventParam1, eventParam2);
	}

	private EventManagerTwoParameters<T, U> Get<T, U>(){
		Type type1 = typeof(T);
		Type type2 = typeof(U);
		EventManagerKey key = new EventManagerKey(type1, type2);

		if (eventManagers.ContainsKey(key)){
			return eventManagers[key] as EventManagerTwoParameters<T, U>;
		}
		else{
			EventManagerTwoParameters<T, U> newEventManager = new EventManagerTwoParameters<T, U>();
			eventManagers.Add(key, newEventManager);
			return newEventManager;
		}
	}
}

public class EventManagerKey{
	public Type type1;
	public Type type2;

	//-------------------------

	public EventManagerKey(Type type1){
		this.type1 = type1;
		this.type2 = null;
	}

	public EventManagerKey(Type type1, Type type2){
		this.type1 = type1;
		this.type2 = type2;
	}

	public override bool Equals(object obj){
		if (obj == null || GetType() != obj.GetType()){
			return false;
		}

		EventManagerKey other = (EventManagerKey)obj;

		if (type1 != other.type1){
			return false;
		}

		if (type2 != null){
			return type2.Equals(other.type2);
		}

		if (other.type2 != null){
			return false;
		}

		return true;
	}

	public override int GetHashCode(){
		unchecked{
			int hash = 17;
			hash = hash * 23 + type1.GetHashCode();
			if (type2 != null){
				hash = hash * 23 + type2.GetHashCode();
			}
			return hash;
		}
	}
}

public class EventManagerNoParameter{
	private Dictionary<Event, Action> eventDictionary = new Dictionary<Event, Action>();

	//---------------------------------------------------

	public void AddListener(Event eventName, Action listener){
		if (eventDictionary.TryGetValue(eventName, out Action currentEvent)){
			currentEvent += listener;
			eventDictionary[eventName] = currentEvent;
		}
		else{
			currentEvent += listener;
			eventDictionary.Add(eventName, currentEvent);
		}
	}

	public void RemoveListener(Event eventName, Action listener){
		if (eventDictionary.TryGetValue(eventName, out Action currentEvent)){
			currentEvent -= listener;
			eventDictionary[eventName] = currentEvent;
		}
	}

	public void Invoke(Event eventName){
		if (eventDictionary.TryGetValue(eventName, out Action thisEvent)){
			thisEvent.Invoke();
		}
		#if UNITY_EDITOR
			else{
				Debug.LogWarning("Tried to invoke event " + eventName + " but there are no listeners");
			}
		#endif
	}
}

public class EventManagerOneParameter<T>{
	private Dictionary<Event, Action<T>> eventDictionary = new Dictionary<Event, Action<T>>();

	//---------------------------------------------------

	public void AddListener(Event eventName, Action<T> listener){
		Action<T> currentEvent;
		if (eventDictionary.TryGetValue(eventName, out currentEvent)){
			currentEvent += listener;
			eventDictionary[eventName] = currentEvent;
		}
		else{
			currentEvent += listener;
			eventDictionary.Add(eventName, currentEvent);
		}
	}

	public void RemoveListener(Event eventName, Action<T> listener){
		Action<T> currentEvent;
		if (eventDictionary.TryGetValue(eventName, out currentEvent)){
			currentEvent -= listener;
			eventDictionary[eventName] = currentEvent;
		}
	}

	public void Invoke(Event eventName, T eventParam){
		Action<T> currentEvent = null;
		if (eventDictionary.TryGetValue(eventName, out currentEvent)){
			currentEvent.Invoke(eventParam);
		}
		#if UNITY_EDITOR
			else{
				Debug.LogWarning("Tried to invoke event " + eventName + " but there are no listeners");
			}
		#endif
	}
}

public class EventManagerTwoParameters<T, U>{
	private Dictionary<Event, Action<T, U>> eventDictionary = new Dictionary<Event, Action<T, U>>();

	//---------------------------------------------------

	public void AddListener(Event eventName, Action<T, U> listener){
		Action<T, U> currentEvent;
		if (eventDictionary.TryGetValue(eventName, out currentEvent)){
			currentEvent += listener;
			eventDictionary[eventName] = currentEvent;
		}
		else{
			currentEvent += listener;
			eventDictionary.Add(eventName, currentEvent);
		}
	}

	public void RemoveListener(Event eventName, Action<T, U> listener){
		Action<T, U> currentEvent;
		if (eventDictionary.TryGetValue(eventName, out currentEvent)){
			currentEvent -= listener;
			eventDictionary[eventName] = currentEvent;
		}
	}

	public void Invoke(Event eventName, T eventParam1, U eventParam2){
		Action<T, U> currentEvent = null;
		if (eventDictionary.TryGetValue(eventName, out currentEvent)){
			currentEvent.Invoke(eventParam1, eventParam2);
		}
		#if UNITY_EDITOR
			else{
				Debug.LogWarning("Tried to invoke event " + eventName + " but there are no listeners");
			}
		#endif
	}
}
