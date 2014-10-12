using UnityEngine;
using System.Collections;

public class StartViewMgr : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClickStartGame(){
		Debug.Log("start game!");
		Global.It.CreateMainGameView ();
	}

	public void OnClickDeveloperName(){
		Debug.Log("developer name!");
	}
}
