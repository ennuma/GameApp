using UnityEngine;
using System.Collections;

public class PlayerMoveCtrl : MonoBehaviour {
	public float velocity = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.anyKey){
			float vert = Input.GetAxis ("Vertical");
			float hori = Input.GetAxis("Horizontal");
			Vector2 newpos = new Vector2(transform.position.x,transform.position.y) + new Vector2 (hori, vert) * velocity*Time.deltaTime;
			rigidbody2D.MovePosition (newpos);
		}
	}
}
