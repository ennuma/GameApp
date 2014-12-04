using UnityEngine;
using System.Collections;

public class RythmBehavior : MonoBehaviour {
	
	public GameObject HitPoint;
	public GameObject BornPoint;


	// Use this for initialization
	void Start () {
		//Invoke("createNewRythmObj",5);
	}
	
	// Update is called once per frame
	void Update () {

	}
	

	public void createNewRythmObj(float time){

		float length =  HitPoint.transform.position.x - BornPoint.transform.position.x;
		float speed = length / time;
		GameObject node = NGUITools.AddChild (gameObject,Resources.Load ("Node") as GameObject);
		node.transform.position = BornPoint.transform.position;
		node.rigidbody.velocity = new Vector3 (speed, 0, 0);

		//Invoke("createNewRythmObj",5); // 五秒建造一个rythmobj
	}
}
