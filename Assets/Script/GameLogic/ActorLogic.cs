using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
public class ActorLogic
{
	public int health;
	public int atk;
	public int def;
	public int qi;
	public int level;
	public int id;
	public string character;
	public Boolean isDead;

	//value reset for each turn
	public int dmgTaken = 0;
	public int dmgBlocked = 0;
	public int currentTurnAction = -1;
	public int deltaqi = 0;
	public int deltalevel = 0;

	public ActorLogic()
	{
	}

	public void init(int health2, int atk2, int def2, int qi2, int level2, int id2)
	{
		this.health = health2;
		this.atk = atk2;
		this.def = def2;
		this.level = level2;
		this.qi = qi2;
		this.id = id2;
		this.isDead = false;
		
		//buildSkill ();
	}
	
	protected  List<List<int>> buildSkill(string str)
	{
		string[] charlist = str.Split ();
		List<List<int>> ret = new List<List<int>> ();
		for (int i = 0; i < charlist.Length/4; i++) {
			int perfect = int.Parse(charlist[i*4]);
			int great = int.Parse(charlist[i*4+1]);
			int normal = int.Parse(charlist[i*4+2]);
			int bad = int.Parse(charlist[i*4+3]);

			List<int> skill = new List<int>();
			skill.Add(perfect);
			skill.Add(great);
			skill.Add(normal);
			skill.Add(bad);
			ret.Add(skill);
		}

		return ret;
	}

	public virtual int getValueForSkill(string skillname, int level, int quality)
	{

		return 0;
	}

	public void update()
	{
		//Debug.Log (String.Format("damage taken is {0}, damage blocked is {1}", dmgTaken.ToString(), dmgBlocked.ToString()));
		int delta = dmgTaken-dmgBlocked;
		if (delta < 0) {
			delta = 0;		
		}
		this.health -= delta;
		if (health <= 0) {
			isDead = true;		
		}

		qi += deltaqi;

		//dmgTaken = 0;
		//dmgBlocked = 0;
		//currentTurnAction = -1;
		//deltaqi = 0;
		//deltalevel = 0;
	}

	public void reset()
	{
		dmgTaken = 0;
		dmgBlocked = 0;
		currentTurnAction = -1;
		deltaqi = 0;
		deltalevel = 0;
	}

	public Dictionary<string, int> convertToDictionary()
	{
		Dictionary<string, int> ret = new Dictionary<string, int> (){
			{"attack_damage",dmgTaken},
			{"defense_value",dmgBlocked},
			{"current_turn_action",currentTurnAction},
			{"qi",qi},
			{"blood",health},
			{"level",level},
			{"debuff_state", 0},
			{"skill",0}
		};
		return ret;
	}
}

