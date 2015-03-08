using UnityEngine;
using System.Collections;

public class checkManager : MonoBehaviour {

	public RythmeManager RManager;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {	
		if (RManager.enableAI) {
			gameObject.SetActive(true);
			Debug.Log("set true");
		} else {
			gameObject.SetActive(false);
			Debug.Log("set false");
		}
	}
}
