using UnityEngine;
using System.Collections;

public class NetMgr : MonoBehaviour
{
	private string dataString = null;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (dataString != null) {
			Debug.Log(dataString);
			OnMessagReceiveHandlerMethod("returnData");		
		}
	}

	public void postData(string data){
		dataString = data;
	}

	private void OnMessagReceiveHandlerMethod(string data){
		Global.It.ProcessMsg ("wow");
		data = null;
	}
}

