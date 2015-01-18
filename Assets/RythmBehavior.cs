using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RythmBehavior : MonoBehaviour {
	
	public GameObject HitPoint;
	public GameObject BornPoint;
	List<GameObject> movingNodes;

	// Use this for initialization
	void Start () {
		//Invoke("createNewRythmObj",5);
		movingNodes = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {

	}
	

	public void createNewRythmObj(float time){

		float length =  HitPoint.transform.position.x - BornPoint.transform.position.x;
		float speed = length / time;
		GameObject node = NGUITools.AddChild (gameObject,Resources.Load ("Node") as GameObject);
		movingNodes.Add(node);
		node.transform.position = BornPoint.transform.position;
		node.rigidbody.velocity = new Vector3 (speed, 0, 0);

		//Invoke("createNewRythmObj",5); // 五秒建造一个rythmobj
	}

	public void destoryRythmObj(){
		foreach (GameObject node in movingNodes) {
			Destroy(node);
			Debug.Log (movingNodes.Count);
		}
	}
}
