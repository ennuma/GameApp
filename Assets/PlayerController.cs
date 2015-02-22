using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public GameObject player_stop;
	protected Animator animator;
	private Vector3 old_position;
	private float direction;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		old_position = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (animator.GetFloat("Speed") != 0.0f) {
			transform.position = transform.position + new Vector3 (direction * animator.GetFloat ("Speed")*Time.deltaTime, 0);
		}
		//While it's moving forward, it can't pass the stop point
		if (direction > 0.0f && transform.position.x > player_stop.transform.position.x) {
			//Stop and switch to attack mode
			setSpeed(0);
			transform.position = player_stop.transform.position;
			animator.SetBool("Attacking",true);
		}
		else if(direction < 0.0f && transform.position.x < old_position.x){
			//Stop and switch to attack mode
			setSpeed(0);
			transform.position = old_position;
		}

		if (animator.GetCurrentAnimatorStateInfo (0).normalizedTime > 1 && !animator.IsInTransition (0) && animator.GetBool("Attacking") == true) {
			animator.SetBool("Attacking",false);
			transform.position = player_stop.transform.position;
			setSpeed(-1);
		}
	}
	public void setSpeed(float speed){
		if (speed > 0.0f) {
			direction = 1.0f;
		} else if (speed < 0.0f) {
			direction = -1.0f;
			speed = -1.0f * speed;
		}
		if (animator) {

			animator.SetFloat("Speed",speed);		
		}
	}
}
