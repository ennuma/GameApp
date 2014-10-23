using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EventMgr : MonoBehaviour
{
	private Dictionary<string, List<System.Action<IEventType>>> m_EventFunctionMap;
	private Queue<IEventType> queue1;
	private Queue<IEventType> queue2;
	private float timeout = 100;
	//cursor
	private Queue<IEventType> currentQueue;
	public static EventMgr It;
	// Use this for initialization
	void Awake(){
		m_EventFunctionMap = new Dictionary<string, List<System.Action<IEventType>>> ();
		queue1 = new Queue<IEventType> ();
		queue2 = new Queue<IEventType> ();
		
		currentQueue = queue1;

		It = this;
		Debug.Log("init");
	}

	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		float currentTime = Time.time;
		Queue<IEventType> thisqueue = currentQueue;
		Queue<IEventType> nextQueue = switchCurrentQueue();
		currentQueue = nextQueue;
		while (thisqueue.Count!=0) {
			IEventType evnt = thisqueue.Dequeue();

			if(Time.time - currentTime > timeout){
				nextQueue.Enqueue(evnt);
				continue;
			}
			Debug.Log(((Event_Test)evnt).position);
			Debug.Log(m_EventFunctionMap.Keys.ToString());
			List<System.Action<IEventType>> callback_list = m_EventFunctionMap[evnt.type];
			if(callback_list == null){
				Debug.Log("Event is not registered yet");
			}
			foreach(System.Action<IEventType> callback in callback_list){
				callback(evnt);
			}
		}
	}
	
	private	Queue<IEventType> switchCurrentQueue(){
		if (currentQueue == queue1) {
			return queue2;		
		}else{
			return queue1;
		}
	}

	public void queueEvent(IEventType evnt)
	{
		if (currentQueue == null) {
			Debug.Log("currentQueue is point to no where!");		
		}
		currentQueue.Enqueue (evnt);
	}

	public void register(IEventType evnt, System.Action<IEventType> callback){
		if (m_EventFunctionMap == null) {
			Debug.Log("Event Function Map is not init yet!");		
		}
		if (m_EventFunctionMap.ContainsKey (evnt.type)) {
			//event type contained in map
			List<System.Action<IEventType>> callback_list = m_EventFunctionMap[evnt.type];
			callback_list.Add(callback);
		}else{
			Debug.Log("in register new event");
			List<System.Action<IEventType>> callback_list = new List<System.Action<IEventType>>();
			callback_list.Add(callback);
			m_EventFunctionMap.Add(evnt.type,callback_list);
		}
	}
}

