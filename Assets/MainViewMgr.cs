using UnityEngine;
using System.Collections;

public class MainViewMgr : MonoBehaviour {


	public GameObject heroView;
	public GameObject scrollView;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void SwitchToSelectHero(){
		Debug.Log("slect hero");
		heroView.SetActive (false);
		scrollView.SetActive (true);
	}

	public void SwitchToHeroView(){
		Debug.Log(" hero");
		heroView.SetActive (true);
		scrollView.SetActive (false);
	}
}
