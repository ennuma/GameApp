using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {
	AudioSource audioSource;
	float timeSinceStart = 0;
	// Use this for initialization
	void Start () {
		audioSource = GetComponentInChildren<AudioSource>();
		if (audio) {
			audio.Play();		
		}
		timeSinceStart = 0;
	}
	
	// Update is called once per frame
	void Update () {
		float deltaTime = Time.deltaTime;
		timeSinceStart += deltaTime;
	}

	public float GetStartTime()
	{

	}
	public float GetIntervelTime()
	{

	}
}
