using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RythmBehavior : MonoBehaviour {
	
	public GameObject HitPoint;
	public GameObject BornPoint;
	public GameObject StartPoint;
	public GameObject[] myArray = new GameObject[9];  // declaration
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
	public void createDragon(float num){
		int index = 0;
		for (float i = 0; i < num; i++) {
			GameObject tmp = NGUITools.AddChild (gameObject, Resources.Load ("Dragon") as GameObject);
			float x_val = StartPoint.transform.position.x + i/5;
			tmp.transform.position = new Vector3(x_val, StartPoint.transform.position.y, StartPoint.transform.position.z);
			myArray[index] = tmp;
			index++;
		}
	}
	public void destroyDragon(){
		for (int i = 0; i < 9; i++) {
			Destroy (myArray [i]);
		}
	}
	public void destoryRythmObj(){
		foreach (GameObject node in movingNodes) {
			Destroy(node);
			//Debug.Log (movingNodes.Count);
		}
	}
}
