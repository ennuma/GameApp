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
using System.Collections.Generic;

/**
 * What the server send for rendering animation and results calculation
 * dictionary contains
 * {
 * hero1: 
 *  {
 *    attack_damage:1; int //damage taken at the end
 *    defense_value:2; int //damage blocked
 *    current_turn_action: int (attack: 0, defense: 1, skill: 2, miss: 3, qi: 4)
 *    qi: int (0 - max)
 *    blood: int (0 - max)
 *    level: int (0 - level_max)
 *    
 *    debuff_state(0 - 5): 0 for poision etc.
 *    skill: (0 - 10) (look up from table)
 *  }
 * hero2:
 *  {
 *  }
 * }
 * 
 **/
public class BattleTurnInfoEvent:IEventType
{
	public Dictionary<int, Dictionary<string, int>> dictionary =
		new Dictionary<int, Dictionary<string, int>>();
	public string m_type = "BATTLETURNINFOEVENT";

	public BattleTurnInfoEvent ()
	{
		//type = "battle turn infor event";
	}
	public string type
	{
		get { return m_type; }
		set { m_type = value; }
	}
}

