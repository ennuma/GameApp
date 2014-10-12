using UnityEngine;
using System.Collections;

public class MainPlayerController : MonoBehaviour {
	//public WorldMeshGenerator wg;
	// Use this for initialization
	void Start () {
	
	}
	


	void Update(){
		if (Input.GetKeyDown (KeyCode.E)) {
			//Debug.Log("space enter");
			//DestroyBlock(new Vector2(transform.position.x, transform.position.y));

			RaycastHit hit;
			Vector3 target = transform.position;
			target.z += 15;
			float distance=Vector3.Distance(transform.position,target);
			
			if( Physics.Raycast(transform.position, (target -
			                                         transform.position).normalized, out hit, distance)){
				
				//Debug.DrawLine(transform.position,hit.point,Color.red);
				if(hit.collider.gameObject.tag == "Voxel"){
					Vector2 point= new Vector2(hit.point.x, hit.point.y); 
					DestroyBlock(point);
				}
				//Debug.Log("hit");
				
			} else {
				//Debug.DrawLine(transform.position,target,Color.blue);
				//Debug.Log("miss");
			}		

		}
	}

	void DestroyBlock(Vector2 point){
		WorldMeshGenerator.It.voxel [(int)point.x, (int)point.y] = 0;
		//Debug.Log("destroy:"+point);
		WorldMeshGenerator.It.update = true;

		int value = WorldValueMgr.It.DestroyVlaueSprite ((int)point.x, (int)point.y);
		Debug.Log ("vlaue point: " + value);
		if (value > 0) {
			GameObject go = Instantiate(Resources.Load("Image/Worker/Worker")) as GameObject;
			go.transform.position = new Vector3(point.x,point.y,1);
		}
	}
}
