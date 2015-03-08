using System;
public class BattleEventDie:IEventType
{
	/**
	 * self id 0 for player 1 for enemey
	 **/
	
	public BattleEventDie ()
	{
		m_type = "BATTLEEVENTDIE";
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
	

	public int m_self_id;
	public string  m_type = "BATTLEEVENTDIE";
	
}
