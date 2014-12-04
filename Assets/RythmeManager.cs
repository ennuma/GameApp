using UnityEngine;
using System.Collections;

public class RythmeManager : MonoBehaviour {

	public RythmBehavior rythmBehavior;
	private float intervalTime;
	private float dropTime;
	private float creatTime;
	private int rythmeCount;


	// Use this for initialization
	void Start () {
		//get interval time (second) from Database
		intervalTime = 5;
		//get dropTime from DATABASE 
		dropTime = 10;
		System.Action<IEventType> callback = turn_Start;
		EventMgr.It.register(new BattleTurnStartEvent(),callback);
		BattleTurnStartEvent btse = new BattleTurnStartEvent ();
		EventMgr.It.queueEvent (btse);
	}
	
	// Update is called once per frame
	void Update () {
		if (rythmeCount < 4) {
			creatTime += Time.deltaTime;
			if (creatTime>intervalTime) {
				rythmBehavior.createNewRythmObj (dropTime);
				creatTime = 0;
				rythmeCount ++;
			}
		}
	}

	void turn_Start(IEventType turnStartEvent){
		Debug.Log("in RythmeManager turn_Start: \n");
		creatTime = 0;
		rythmeCount = 0;

	}
}
