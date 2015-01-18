// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System;
using UnityEngine;
using System.Collections.Generic;

public class BattleEventListener: MonoBehaviour
{
	public UILabel self_blood;
	public UILabel self_level;
	public UILabel self_defense;
	public UILabel self_attack;

	public UILabel enemy_blood;
	public UILabel enemy_level;
	public UILabel enemy_defense;
	public UILabel enemy_attack;
	public BattleEventListener ()
	{
//		EventMgr.It.queueEvent (new BattleTurnInfoEvent ());
	}
	void Start () {
		System.Action<IEventType> callback = Battle_Turn_Info_Handler;
		EventMgr.It.register(new BattleTurnInfoEvent(),callback);

		EventMgr.It.queueEvent (new BattleTurnInfoEvent ());
		self_blood.text = "12";
		self_level.text = "12";
		self_attack.text = "12";
		self_defense.text = "12";
		
		enemy_blood.text = "10";
		enemy_level.text = "10";
		enemy_defense.text = "10";
		enemy_attack.text = "10";
	}

	/**
	 * In charge of handling all UI events related to the battle_turn_info
	 **/
	private void Battle_Turn_Info_Handler(IEventType evnt){
		Debug.Log ("in battle turn infor handler");
		BattleTurnInfoEvent m_evnt = evnt as BattleTurnInfoEvent;
		Dictionary<string, int> self_dic = new Dictionary<string, int>();
		Dictionary<string, int> enemy_dic = new Dictionary<string, int>();

		self_dic = m_evnt.dictionary[0];
		enemy_dic = m_evnt.dictionary[1];

		int attack_damage = self_dic["attack_damage"];//damage taken at the end
		Debug.Log ("attack damage" + attack_damage);
		int defense_value = self_dic ["defense_value"];//damage blocked
		int current_turn_action = self_dic ["current_turn_action"];
		Debug.Log ("current turn action is " + current_turn_action);
		int qi = self_dic ["qi"];
		int blood = self_dic["blood"];
		int level = self_dic["level"];
	
		switch (current_turn_action) {
			//Attack
			case 0:
				break;
			//Defense
			case 1:
				break;
			//Skill
			case 2:
				break;
		}


		int e_attack_damage = enemy_dic["attack_damage"];//damage taken at the end
		int e_defense_value = enemy_dic ["defense_value"];//damage blocked
		int e_current_turn_action = enemy_dic ["current_turn_action"];
		int e_qi = enemy_dic ["qi"];
		int e_blood = enemy_dic["blood"];
		int e_level = enemy_dic["level"];
		switch (e_current_turn_action) {
			//Attack
			case 0:
				break;
			//Defense
			case 1:
				break;
			//Skill
			case 2:
				break;
		}
		Debug.Log ("self blood" + blood);

		self_blood.text = "" + blood;
		self_level.text = "" + level;
		self_attack.text = "" + attack_damage;
		self_defense.text = "" + defense_value;
		
		enemy_blood.text = "" + e_blood;
		enemy_level.text = "" + e_level;
		enemy_defense.text = "" + e_defense_value;
		enemy_attack.text = "" + e_attack_damage;


		//End of battle, send battle turn end event
		Send_Battle_Turn_End_Event ();
	}
	private void Send_Battle_Turn_End_Event(){
		//Query self battle turn end event
		EventMgr.It.queueEvent (new BattleTurnEndEvent());
		BattleTurnEndEvent b = new BattleTurnEndEvent ();
		b.self_id = 1;
		//Query enemy battle turn end event
		EventMgr.It.queueEvent (b);
	}
	//Set functions for later use
	public void setBlood(int value){
		self_blood.text = "" + value;
	}
	public void setLevel(int value){
		self_level.text = "" + value;
	}
	public void setAttack(int value){
		self_attack.text = "" + value;
	}
	public void setDefense(int value){
		self_defense.text = "" + value;
	}
	public void setEnemyBlood(int value){
		enemy_blood.text = "" + value;
	}
	public void setEnemyLevel(int value){
		enemy_level.text = "" + value;
	}
	public void setEnemyDefense(int value){
		enemy_defense.text = "" + value;
	}
	public void setEnemyAttack(int value){
		enemy_attack.text = "" + value;
	}
}


