using UnityEngine;
using System.Collections;

public class ProtocalMgr : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void ProcessData(string data){
		if (data.Equals ("receiveData")) {
			Global.It.CreateMainGameView();		
		}
	}
}

