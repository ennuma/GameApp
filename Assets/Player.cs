using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Player : MonoBehaviour {

	private List<GameObject> onCollideList = new List<GameObject>();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKey) {
			OnKeyDown();		
		}
	}


	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Voxel") {
			onCollideList.Add(other.gameObject);	
		}
	}

	void OnCollisionExit2D(Collision2D other){
		if (other.gameObject.tag == "Voxel") {
			onCollideList.Remove(other.gameObject);	
		}
	}

	void OnKeyDown(){
		Vector2 currentPos = transform.position;
		int x = Mathf.RoundToInt(currentPos.x);
		int y = Mathf.RoundToInt(currentPos.y);
		float vert = Input.GetAxis ("Vertical");
		float hori = Input.GetAxis("Horizontal");
		if (vert<0 && hori == 0) {
			DestroyBlock(x,y-1);		
		}else if(hori<0 && vert == 0){
			DestroyBlock(x-1,y);	
		}else if(hori>0 & vert ==0){
			DestroyBlock(x+1,y);	
		}else if(vert >0 && hori == 0){
			DestroyBlock(x,y+1);	
		}
	}

	bool DestroyBlock(int x, int y){
		foreach (GameObject block in onCollideList) {
			if(new Vector2(block.transform.position.x,block.transform.position.y) == new Vector2(x,y)){
			//	onCollideList.Remove(block);
				//Destroy(block);
				//WorldGenerator.wg.voxel[x,y] = 0;
				//block.SetActive(false);
				//WorldGenerator.wg
				return true;
			}
		}
		return false;
	}
	/**
	void OnMouseDown(){
		Debug.Log("click!");
		foreach (GameObject block in onCollideList) {
			Destroy(block);		
		}
	}**/
}
