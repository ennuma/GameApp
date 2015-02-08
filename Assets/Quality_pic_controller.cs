using UnityEngine;
using System.Collections;

public class Quality_pic_controller : MonoBehaviour {
	public float time = 0.3f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		time = time - Time.deltaTime;
		if (time <= 0) {
			Destroy(gameObject);		
		}
	}
}
