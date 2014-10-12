using UnityEngine;
using System.Collections;

public class MainGameCamera : MonoBehaviour {

	public int moveVelocity = 0; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float vert = 0;//Input.GetAxis ("Vertical");
		float hori = 0;//Input.GetAxis("Horizontal");
		float scroll = 0;//Input.GetAxis("Mouse ScrollWheel")*15;
		/**if (hori > 0) {
			hori = 1;		
		}
		if (hori < 0) {
			hori = -1;		
		}
		if (vert > 0) {
			vert = 1;		
		}
		if (vert < 0) {
			vert = -1;		
		}**/
		if (Input.GetKeyDown (KeyCode.W)) {
			vert = 1;
		}
		if (Input.GetKeyDown (KeyCode.S)) {
			vert = -1;
		}
		if (Input.GetKeyDown (KeyCode.A)) {
			hori = -1;
		}
		if (Input.GetKeyDown (KeyCode.D)) {
			hori = 1;
		}
		MoveCamera(new Vector3(hori,vert,scroll).normalized,Time.deltaTime);
	}

	void MoveCamera(Vector3 direction, float deltaTime){
		Vector3 newpos = transform.position;
		newpos.x += direction.x;
		newpos.y += direction.y;
		newpos.z += -direction.z * moveVelocity * deltaTime*10;
		if (newpos.z >= -0.5f) {
			newpos.z = -0.5f;		
		}
		transform.position = newpos;
	}
	
}
