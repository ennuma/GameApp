using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GameLogic : MonoBehaviour {

	//instance for player
	private RiderLogic player;
	//instance for enemy
	private RiderLogic enemy;
	//dictionary for actormap, map id to actorlogic. 0 for player, 1 for enemy
	private Dictionary<int,ActorLogic> actormap = new Dictionary<int,ActorLogic>();
	//info
	private Dictionary<int,Dictionary<string,int>> info = new Dictionary<int,Dictionary<string,int>>();
	//
	private int playerReady = 0;
	private int enemyReady = 0;

	void Start () {
		System.Action<IEventType> atkCallback = atkHandler;
		EventMgr.It.register (new BattleEventAttack (), atkCallback);

		System.Action<IEventType> defCallback = defHandler;
		EventMgr.It.register (new BattleEventDefense (), defCallback);

		System.Action<IEventType> missCallback = missHandler;
		EventMgr.It.register (new BattleEventMiss (), missCallback);

		System.Action<IEventType> turnendCallback = turnendHandler;
		EventMgr.It.register (new BattleTurnEndEvent (), turnendHandler);

		//rider
		player = new RiderLogic();
		player.init (50, 2, 0, 0, 1, 1);
		//rider
		enemy = new RiderLogic();
		enemy.init (50, 2, 0, 0, 1, 1);
		//test
		actormap.Add (0, player);
		actormap.Add (1, enemy);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/**
	Method Name: defHandler
	Description: handler attack event
	 **/
	private void atkHandler(IEventType evnt){
		Debug.Log("atk event received");
		BattleEventAttack m_evnt = evnt as BattleEventAttack;

		int quality = m_evnt.rhythm_quality;
		int id = m_evnt.self_id;
		int dmg = 0;
		dmg = actormap [id].getValueForSkill ("attack", 0, quality);

		//need to fix in later version this damage is not thread safe
		int enemyid = -1;
		if (id == 0) {
			enemyid = 1;
		} else {
			enemyid=0;
		}
		ActorLogic from = actormap [id];
		ActorLogic target = actormap [enemyid];

		from.currentTurnAction = 0;
		target.dmgTaken = dmg;
		Debug.Log ("damage is : "+ dmg.ToString());

		tryEndTurn ();

	}

	/**
	Method Name: defHandler
	Description: handler defend event
	 **/
	private void defHandler(IEventType evnt){
		Debug.Log("def event received");
		BattleEventDefense m_evnt = evnt as BattleEventDefense;


		int quality = m_evnt.rhythm_quality;
		int id = m_evnt.self_id;
		Debug.Log (id.ToString ());
		int dmg = 0;
		dmg = actormap [id].getValueForSkill ("defend", 0, quality);
		
		//need to fix in later version this damage is not thread safe
		int enemyid = -1;
		if (id == 0) {
			enemyid = 1;
		} else {
			enemyid=0;
		}
		ActorLogic from = actormap [id];
		ActorLogic target = actormap [enemyid];
		
		from.currentTurnAction = 1;
		from.dmgBlocked = dmg;
		Debug.Log ("defend is : "+ dmg.ToString());

		tryEndTurn ();
	}

	/**
	Method Name: missHandler
	Description: handler defend event
	 **/
	private void missHandler(IEventType evnt){
		Debug.Log("miss event received");
		BattleEventMiss m_evnt = evnt as BattleEventMiss;
		
		
		int quality = m_evnt.rhythm_quality;
		int id = m_evnt.self_id;
		//int dmg = 0;
		//dmg = actormap [id].getValueForSkill ("defend", 0, quality);
		
		//need to fix in later version this damage is not thread safe
		int enemyid = -1;
		if (id == 0) {
			enemyid = 1;
		} else {
			enemyid=0;
		}
		ActorLogic from = actormap [id];
		ActorLogic target = actormap [enemyid];
		
		from.currentTurnAction = 3;
		//from.dmgBlocked = dmg;
		//Debug.Log ("defend is : "+ dmg.ToString());
		
		tryEndTurn ();
	}

	/**
	Method Name: turnendHandler
	Description: receive this event means the corresponding player is ready for next
					turn. also try start next turn
	 **/
	private void turnendHandler(IEventType evnt){
		Debug.Log("turn end event received");
		BattleTurnEndEvent m_evnt = evnt as BattleTurnEndEvent;

		int id = m_evnt.self_id;
		if (id == 0) {
			playerReady = 1;		
		}else{
			enemyReady = 1;
		}

		tryStartTurn ();
	}

	/**
	Method Name: sendTurnStart
	Description: send the start information to client side for starting a new turn
					A client must receive this event to make new input 
	 **/
	private void sendTurnStart(){
		BattleTurnStartEvent evnt = new BattleTurnStartEvent();
		EventMgr.It.queueEvent (evnt);
	}

	/**
	Method Name: sendTurnInfo
	Description: send the useful information to client side for rendering effect
	 **/
	private void sendTurnInfo(){
		BattleTurnInfoEvent evnt = new BattleTurnInfoEvent();

		//update current logic state
		foreach(ActorLogic ac in actormap.Values){
			ac.update();
		}

		//add info for each player
		info.Add (0, player.convertToDictionary ());
		info.Add (1, enemy.convertToDictionary ());
		//deep copy current info dictionary
		evnt.dictionary = new Dictionary<int, Dictionary<string, int>>(info);
	
		//clear info data dictionary
		info.Clear ();
		//queue turninfoevent
		EventMgr.It.queueEvent (evnt);
		Debug.Log("0 : ");
		foreach (string k in evnt.dictionary[0].Keys) {

						Debug.Log (k);
			Debug.Log(evnt.dictionary[0][k]);
		}
		Debug.Log("1 : ");
		foreach (string k in evnt.dictionary[1].Keys) {

			Debug.Log (k);
			Debug.Log(evnt.dictionary[1][k]);
		}

		player.reset ();
		enemy.reset ();
		//player.currentTurnAction = -1;
		//enemy.currentTurnAction = -1;
	}

	/**
	Method Name : tryStartTurn
	Description : this method is used for star turn, each time game logic receive turnendevent
 					the corresponding actor is marked ready to start next turn. This method is 
					being called in **turnendHandler**
	 **/
	private void tryStartTurn ()
	{
		if (playerReady==1 && enemyReady==1) {
			sendTurnStart();		
		}
	}

	/**
	Method Name : tryEndTurn
	Description : this method is used for end turn, each time game logic receive any actionevent(atk,def)
 					the corresponding actor is marked ready to render effect. This method is 
					being called in **any actionevent handler**
					After this being called, game logic will queue infodataEvent to the eventmgr and ready
					for client side to display animation
	 **/
	private void tryEndTurn ()
	{
		if (player.currentTurnAction != -1 && enemy.currentTurnAction != -1) {
			sendTurnInfo();		
		}
	}

}
