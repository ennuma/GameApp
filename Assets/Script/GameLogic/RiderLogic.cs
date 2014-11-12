using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class RiderLogic: ActorLogic
{
	Dictionary<string,List<List<int>>> skillDic = new Dictionary<string, List<List<int>>>();
	public RiderLogic()
	{
	}

	public void init(int health2, int atk2, int def2, int qi2, int level2, int id2)
	{
		this.health = health2;
		this.atk = atk2;
		this.def = def2;
		this.qi = qi2;
		this.level = level2;
		this.id = id2;
		this.character = "rider";
		initSkill ();
	}

	private void initSkill()
	{
		List<List<int>> atk = buildSkill("3 2 1 0 4 3 2 0 5 4 3 0 6 5 4 0 8 7 6 0 10 9 8 0 12 11 10 0 14 13 12 0 16 15 14 0 20 18 16 0");
		skillDic.Add ("attack", atk);

		List<List<int>> def = buildSkill("3 2 1 0 4 3 2 0 5 4 3 0 6 5 4 0 8 7 6 0 10 9 8 0 12 11 10 0 14 13 12 0 16 15 14 0 20 18 16 0");
		skillDic.Add ("defend", def);	
	}

	public override int getValueForSkill(string skillname, int quality, int level)
	{

		return skillDic [skillname] [level] [quality];
	}
}

