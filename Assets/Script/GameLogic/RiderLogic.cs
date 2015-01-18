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
		this.isDead = false;

		initSkill ();
	}

	private void initSkill()
	{
		List<List<int>> atk = buildSkill("3 2 1 0 4 3 2 0 5 4 3 0 6 5 4 0 8 7 6 0 10 9 8 0 12 11 10 0 14 13 12 0 16 15 14 0 20 18 16 0");
		skillDic.Add ("attack", atk);

		List<List<int>> def = buildSkill("3 2 1 0 4 3 2 0 5 4 3 0 6 5 4 0 8 7 6 0 10 9 8 0 12 11 10 0 14 13 12 0 16 15 14 0 20 18 16 0");
		skillDic.Add ("defend", def);	

		List<List<int>> nuzhan = buildSkill("10 6 4 0 11 7 5 0 12 8 6 0 13 9 7 0 15 11 9 0 17 13 11 0 19 15 13 0 21 17 15 0 23 19 17 0 27 23 21 0");
		skillDic.Add ("nuzhan", nuzhan);
	}

	public override int getValueForSkill(string skillname, int level, int quality)
	{

		return skillDic [skillname] [level] [quality];
	}
}

