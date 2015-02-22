using UnityEngine;
using System.Collections;

public class SceneDirector : MonoBehaviour {

	public static SceneDirector It;
	private GameObject currentScene;
	void Awake(){
		It = this;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SwitchScene(Object res, Transform root){
		GameObject scene = Instantiate (res) as GameObject;
		scene.transform.position = Vector3.zero;
		scene.transform.parent = root;

//		Camera cam = Camera.main;
//		
//		float pos = (cam.nearClipPlane + 0.01f);
//		
//		transform.position = cam.transform.position + cam.transform.forward * pos;
//		
//		float h = Mathf.Tan(cam.fov*Mathf.Deg2Rad*0.5f)*pos*2f;
//		
//		transform.localScale = new Vector3(h*cam.aspect,h,0f);

		Destroy (currentScene);
		currentScene = scene;
	}
}
