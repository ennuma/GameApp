using System;
using System.Collections.Generic;
using System.Collections;
public class BattleAIDecisionEvent:BattleEvent
{
	/**
	 * self id
	 * rhythm_quality: perfect(0)/great(1)/normal(2)/bad(3)
	 **/
	
	public BattleAIDecisionEvent ()
	{
		m_type = "BATTLEAIDECISIONEVENT";
	}
	public string type	
	{
		get { return m_type; }
		set { m_type = value; }
		
	}
	
	public int self_id {
		get	{return m_self_id;}
		set {m_self_id = value;}
	}
	
	public int enemy_decision{
		get	{return m_enemy_decision;}
		set {m_enemy_decision = value;}
	}
	
	public int player_decision{
		get	{return m_player_decision;}
		set {m_player_decision = value;}
	}
	
	public int m_self_id;
	public int m_enemy_decision;
	public int m_player_decision;
	public string  m_type = "BATTLEAIDECISIONEVENT";
	
}
