using UnityEngine;
using System.Collections;

public class Event_Test : IEventType
{
	public Vector3 position;
	string m_type = "TESTEVENT";
	
	public string type
	{
		get { return m_type; }
		set { m_type = value; }
	}
}

