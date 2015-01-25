using UnityEngine;
using System.Collections;

public class QiBehavior : MonoBehaviour {
	public GameObject StartPoint;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void createNewRythmObj(int num){
		for (int i = 0; i < num; i++) {
			Debug.Log ("dragon added");
			GameObject dragon = NGUITools.AddChild (gameObject, Resources.Load ("Dragon") as GameObject);
			dragon.transform.position = StartPoint.transform.position;
		}
		//Invoke("createNewRythmObj",5); // 五秒建造一个rythmobj
	}
}
