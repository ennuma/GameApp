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

public class BattleEventDefense:BattleEvent
{
	/**
	 * self id
	 * rhythm_quality: perfect(0)/great(1)/normal(2)/bad(3)
	 **/
	public string m_type = "BATTLEEVENTDEFENCE";
	public BattleEventDefense ()
	{
		//type = "battle event defense";
	}
	public string type
	{
		get { return m_type; }
		set { m_type = value; }
	}
	public int self_id;
	public int rhythm_quality;
	
}