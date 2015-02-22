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
		else if(time <= 0.1f){
			transform.localScale -= new Vector3(0.1f,0.1f,0);
		}else if( time <= 0.2f){
			transform.localScale -= new Vector3(0.1f,0.1f,0);
		}
		else{
			transform.localScale += new Vector3(0.1f,0.1f,0);
		}
	}
}
