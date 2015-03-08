using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour {
	//Action mode will be what kind of AI you want to use
	/* 0 for no action, always miss
	 * 1 be always attack
	 * 2 be always defense
	 * 3 be always qi
	 */
	private int action_mode = 4;
	//Initialize to miss rate of 10, range from 0 to 100, 100 for 100% misses, we can change this later
	private int miss_rate = 0;
	//List to keep track of the probability to pick each actions. index from 0 to 4
	private List<int> prob_list = new List<int>();
	private int action_type;
	// Use this for initialization
	void Start () {
		Debug.Log ("Enemy ai start");

		//Request for a enemy turn info
		System.Action<IEventType> callback = Enemy_Turn_Info_Handler;
		EventMgr.It.register(new BattleTurnInfoEvent(),callback);

		action_type = 0;
		System.Action<IEventType> callback1 = QueryEventInfoHandler;

		EventMgr.It.register(new BattleTurnStartEvent(),callback1);
		//Initialize the list with everything equal probability
		for(int i = 0;i < 5;i ++){
			prob_list.Add(20);
		}
	}
	
	//Queue Events when decision made
	
	// Update is called once per frame
	void Update () {
		
	}
	private void Enemy_Turn_Info_Handler(IEventType evnt){
		//Base on probability of missing
		//Debug.Log ("in enemy battle turn infor handler");
		if (Random.Range (0, 100) < miss_rate) {
			action_type = 3;
			return;
		}
		BattleTurnInfoEvent m_evnt = evnt as BattleTurnInfoEvent;
		Dictionary<string, int> self_dic = new Dictionary<string, int>();
		Dictionary<string, int> enemy_dic = new Dictionary<string, int>();
		
		self_dic = m_evnt.dictionary[0];
		enemy_dic = m_evnt.dictionary[1];
		
		int attack_damage = self_dic["attack_damage"];//damage taken at the end
		int defense_value = self_dic ["defense_value"];//damage blocked
		int current_turn_action = self_dic ["current_turn_action"];
		int qi = self_dic ["qi"];
		int blood = self_dic["blood"];
		int level = self_dic["level"];

		int e_attack_damage = enemy_dic["attack_damage"];//damage taken at the end
		int e_defense_value = enemy_dic ["defense_value"];//damage blocked
		int e_current_turn_action = enemy_dic ["current_turn_action"];
		int e_qi = enemy_dic ["qi"];
		int e_blood = enemy_dic["blood"];
		int e_level = enemy_dic["level"];

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
			//Miss
		case 3:
			break;
			//Qi
		case 4:
			break;
		}
		
		/* We have different decisions here, assuming the enemy right now only knows the action of the previous round. We want it to make a 
		 * decison to what next action it will perform.
		 * Here's some different approach:
		 * 1. always attack
		 * 2. always defense
		 * 3. always qi
		 * 4. attack and defense and qi, equal probability to do certain actions.
		 * 5. look at user's action, if defense, more likely to attack, therefore defense, if attack, then more likely to defense or qi, use qi
		 * 
		 */

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
			//Miss
		case 3:
			break;
			//Qi
		case 4:
			break;
		}
		/* 0 for no action, always miss
		 * 1 be always attack
		 * 2 be always defense
		 * 3 be always qi
		 * 4 be random
		 * 5 for whenever there's qi, use skill, otherwise, use attack
		 */
		switch(action_mode){
			case 0:
			action_type = 3;
				break;
			case 1:
			action_type = 0;
				break;
			case 2:
			action_type = 1;
				break;
			case 3:
			action_type = 4;
				break;
			//Randomly pick action between attack, defense and qi
			case 4:
				int tmp_action = Random.Range (0,2);
				if(tmp_action == 2){
					tmp_action = 4;
				}
			action_type = tmp_action;
				break;
			case 5:
				if(e_qi > 0){
				action_type = 2;
				}
				else{
				action_type = 0;
				}
				break;
			case 6:
				setActionProbabilityList(30,30,0,0,40);
				action_type = pickActionBasedOnProbability();
				break;
		}
	}

	private void QueryEventInfoHandler(IEventType evnt){
		string tmp = "";
		switch (action_type) {
			//Attack
		case 0:
			BattleEventAttack enemy = new BattleEventAttack ();
			enemy.self_id = 1;
			enemy.rhythm_quality = 1;
			EventMgr.It.queueEvent (enemy);
			tmp = "attack";
			break;
			//Defense
		case 1:
			BattleEventDefense enemy1 = new BattleEventDefense ();
			enemy1.self_id = 1;
			enemy1.rhythm_quality = 1;
			EventMgr.It.queueEvent (enemy1);
			tmp = "defense";
			break;
			//Skill
		case 2:
//			BattleEventMiss enemy2 = new BattleEventMiss ();
//			enemy2.self_id = 1;
//			enemy2.rhythm_quality = 1;
//			EventMgr.It.queueEvent (enemy2);
			break;
			//Miss
		case 3:
			BattleEventMiss enemy3 = new BattleEventMiss ();
			enemy3.self_id = 1;
			enemy3.rhythm_quality = 1;
			EventMgr.It.queueEvent (enemy3);
			tmp = "miss";
			break;
			//Qi
		case 4:
			BattleEventQi enemy4 = new BattleEventQi ();
			enemy4.self_id = 1;
			enemy4.rhythm_quality = 1;
			EventMgr.It.queueEvent (enemy4);
			tmp = "qi";
			break;
		}
		Debug.Log ("Action for enemy is: " + tmp, gameObject);

	}
	private void setActionProbabilityList(int attack_prob, int defense_prob, int skill_prob, int miss_prob, int qi_prob){
		prob_list [0] = attack_prob;
		prob_list [1] = defense_prob;
		prob_list [2] = skill_prob;
		prob_list [3] = miss_prob;
		prob_list [4] = qi_prob;
	}
	/**
	 * Function to determine which action to call next based on some probability distribution
	 **/
	private int pickActionBasedOnProbability(){
		//If it's more than 5 element, we return -1 to indicate can't do anything
		if (prob_list.Count != 5) {
			return -1;
		}
		int random_num = Random.Range (0, 100);
		int count = 0;
		int index = 0;
		foreach(int x in prob_list){
			count += x;
			if(random_num <= count){
				return index;
			}
			else{
				index++;
			}
		}
		//return -1 to indicate some errors have occured.
		return -1;
	}
}



