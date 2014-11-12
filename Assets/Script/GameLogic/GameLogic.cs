using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GameLogic : MonoBehaviour {

	private RiderLogic player;
	private ActorLogic enemy;
	//private List<ActorLogic> actors = new List<ActorLogic>();
	// Use this for initialization
	void Start () {
		System.Action<IEventType> atkCallback = atkHandler;
		EventMgr.It.register (new BattleEventAttack (), atkCallback);

		System.Action<IEventType> defCallback = defHandler;
		EventMgr.It.register (new BattleEventDefense (), defCallback);

		System.Action<IEventType> turnendCallback = turnendHandler;
		EventMgr.It.register (new BattleTurnEndEvent (), turnendHandler);

		//rider
		player = new RiderLogic();
		player.init (50, 2, 0, 0, 1, 1);
		//rider
		//enemy = new ActorLogic (50, 2, 0, 0, 1, 1);
		//test
		/**
		BattleEventAttack atk = new BattleEventAttack ();
		atk.rhythm_quality = 0;
		atk.self_id = 0;
		Debug.Log("queue atk");
		EventMgr.It.queueEvent (atk);
		**/
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void atkHandler(IEventType evnt){
		Debug.Log("atk event received");
		BattleEventAttack m_evnt = evnt as BattleEventAttack;
		int quality = m_evnt.rhythm_quality;
		int id = m_evnt.self_id;
		int dmg = 0;
		if (id == 0) {
			dmg = player.getValueForSkill ("attack", 0, quality);		
		} else {
				
		}
		Debug.Log ("damage is : "+ dmg.ToString());

	}

	private void defHandler(IEventType evnt){
		Debug.Log("def event received");
		BattleEventDefense m_evnt = evnt as BattleEventDefense;
		
	}

	private void turnendHandler(IEventType evnt){
		Debug.Log("def event received");
		BattleTurnEndEvent m_evnt = evnt as BattleTurnEndEvent;
	}

	private void snedTurnStart(){
		BattleTurnStartEvent evnt = new BattleTurnStartEvent();
		EventMgr.It.queueEvent (evnt);
	}

	private void sendTurnInfo(){
		BattleTurnInfoEvent evnt = new BattleTurnInfoEvent();
		EventMgr.It.queueEvent (evnt);
	}
}
