using UnityEngine;
using System.Collections;

public class scroll : MonoBehaviour {

	private int screenBoxX = Screen.width;
	private int screenBoxY = Screen.height;

	private int scrollX = GameObject.Find(scroll).GetComponentsInChildren(Widget).width * 5;
	private int scrollY = GameObject.Find(character1).height;

	public Vector2 scrollPosition = Vector2.zero;


	void Awake(){
		Debug.Log ("box size is " + screenBoxX + " x " + screenBoxY);
	}

	// Use this for initialization
	void Start () {

	}

	void OnGUI(){

		scrollPosition = GUI.BeginScrollView(new Rect(0, 0, screenBoxX, screenBoxY),
		                                     scrollPosition, new Rect(0, 0, scrollX, scrollY));
		
		GUI.EndScrollView();
	}

	// Update is called once per frame
	void Update () {
	
	}
}
