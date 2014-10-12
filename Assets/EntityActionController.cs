using UnityEngine;
using System.Collections;

public class EntityActionController : MonoBehaviour {

	//private WorldMeshGenerator world = WorldMeshGenerator.It;
	private Animator animCtrl;
	private bool m_actionDone;
	public bool actionDone {get{return m_actionDone;}}
	Vector3 targetPos = new Vector3();
	enum State{
		IDLE,
		MOVE,
		ATTACK,
	}
	State state;
	// Use this for initialization
	void Start () {
		state = State.IDLE;
		animCtrl = GetComponent<Animator> ();
		targetPos = transform.position;
		m_actionDone = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		switch (state) {
		case State.IDLE:{
			if(animCtrl){
				PlayIdleAnimation();
			}
		}
			break;
		case State.MOVE:{
			if(animCtrl){
				PlayWalkAnimation();
			}
			OnMoveHandler();
		}
			break;
		}
	}

	void OnMoveHandler(){
		//if (targetPos) {
		//	return;		
		//}
		if (Vector3.Distance (targetPos, transform.position) < 0.01) {
			m_actionDone = true;
		}
		Vector3 dir = (targetPos - transform.position).normalized;
		transform.position = transform.position + dir * Time.deltaTime*1;
	}

	public bool MoveTo(int x, int y){
		if (WorldMeshGenerator.It.voxel [x, y] != 0) {
			Debug.Log("try to move to the position that has block!");
			return false;	
		}else{
			targetPos = new Vector3(x,y,transform.position.z);
			state = State.MOVE;
			m_actionDone = false;
			return true;
		}

	}

	void PlayIdleAnimation(){
		animCtrl.Play("G2Idle");
	}

	void PlayWalkAnimation(){
		animCtrl.Play("G2Walk");
	}
}
