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

	public virtual int getValueForSkill(string skillname, int quality, int level)
	{

		return 0;
	}
}

