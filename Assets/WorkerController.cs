using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class WorkerController : MonoBehaviour {
	EntityActionController actionCtrl;
	enum State{
		Idle,
		Action,
	}
	State state;
	private List<System.Action> actionArr = new List<System.Action>();
	private int valueCarry = 0;
	// Use this for initialization
	void Start () {
		actionCtrl = GetComponent<EntityActionController> ();
		//Invoke ("_start", 2.0f);
		state = State.Idle;
	}
	
	// Update is called once per frame
	void Update () {
		switch (state){
		case State.Idle:{
			OnIdle();
		}
			break;
		case State.Action:{
			OnAction();
		}
			break;
		}
	}

	void OnIdle(){
		//make actionArr
		state = State.Action;
		int[,] world = WorldMeshGenerator.It.voxel;
		int[,] value = WorldValueMgr.It.valueMatrix;
		int x = Mathf.RoundToInt( transform.position.x);
		int y = Mathf.RoundToInt( transform.position.y);
		if (ValueAround (x, y)) {
			absorbAt (x, y);
			return;
		} 
		if(Walkable(x,y)){
			if(Random.Range(0,100)<30){
				productAt(x,y);
			}else{
				randomWalk(x,y);
			}
			return;
		}
		productAt (x, y);
	}
	void randomWalk(int x,int y){
		Debug.Log ("from :" + x + "," + y);
		int[,] world = WorldMeshGenerator.It.voxel;
		if (World(x-1,y)&& world [x - 1, y] == 0) {
			if(Random.Range(0,100)<50){
				buildActionMoveTo(x-1,y);
				return;		
			}
		}
		if (World(x+1,y)&& world [x + 1, y] == 0) {
			if(Random.Range(0,100)<50){
				buildActionMoveTo(x+1,y);
				return;		
			}		}
		if (World(x,y-1)&& world [x, y-1] == 0) {
			if(Random.Range(0,100)<50){
				buildActionMoveTo(x,y-1);
				return;		
			}		}
		if (World(x,y+1)&& world [x, y+1] == 0) {
			if(Random.Range(0,100)<50){
				buildActionMoveTo(x,y+1);
				return;		
			}
		}

	}
	void productAt(int x, int y){
		int[,] world = WorldValueMgr.It.valueMatrix;
		int rnd = Random.Range (1, 4);
		if (rnd==1) {
			buildActionProduct(x-1,y);		
		}
		if (rnd==2) {
			buildActionProduct(x+1,y);				
		}
		if (rnd==3) {
			buildActionProduct(x,y-1);			
		}
		if (rnd==4) {
			buildActionProduct(x,y+1);		
		}
	}

	bool Walkable(int x, int y){
		int[,] world = WorldMeshGenerator.It.voxel;
		if (World(x-1,y)&& (world [x - 1, y] == 0)) {
			return true;		
		}
		if (World(x+1,y)&& (world [x + 1, y] == 0)) {
			return true;		
		}
		if (World(x,y-1)&& (world [x, y-1] == 0)) {
			return true;		
		}
		if (World(x,y+1)&& (world [x, y+1] == 0)) {
			return true;		
		}

		return false;
	}

	void absorbAt(int x, int y){
		int[,] world = WorldValueMgr.It.valueMatrix;
		if (Value (x - 1, y) && world[x-1,y]> 0) {
			buildActionAbsorb(x-1,y);		
		}
		if (Value (x + 1, y) && world[x+1,y]> 0) {
			buildActionAbsorb(x+1,y);				
		}
		if (Value (x, y-1)&& world[x,y-1] > 0) {
			buildActionAbsorb(x,y-1);			
		}
		if (Value (x, y+1) && world[x,y+1]> 0) {
			buildActionAbsorb(x,y+1);		
		}
	}

	bool ValueAround(int x, int y){
		int[,] world = WorldValueMgr.It.valueMatrix;
		if (Value (x - 1, y)&& world[x-1,y] > 0) {
			return true;		
		}
		if (Value (x + 1, y)&& world[x+1,y] > 0) {
			return true;		
		}
		if (Value (x, y-1)&& world[x,y-1] > 0) {
			return true;		
		}
		if (Value (x, y+1)&& world[x,y+1]> 0) {
			return true;		
		}
		return false;
	}

	bool Value(int x, int y){
		int[,] world = WorldValueMgr.It.valueMatrix;
		int maxX = world.GetLength (0);
		int maxY = world.GetLength (1);
		if (x < 0 || x >= maxX) {
			return false;
		}
		if (y < 0 || y >= maxY) {
			return false;
		}
		return true;
	}

	bool World(int x, int y){
		int[,] world = WorldMeshGenerator.It.voxel;
		int maxX = world.GetLength (0);
		int maxY = world.GetLength (1);
		if (x < 0 || x >= maxX) {
			return false;
		}
		if (y < 0 || y >= maxY) {
			return false;
		}
		return true;
	}

	void OnAction(){
		if (actionArr.Count == 0&&actionCtrl.actionDone) {
			state = State.Idle;
			return;		
		}
		if (actionCtrl.actionDone) {
			System.Action nextAction = actionArr[0];
			actionArr.Remove(nextAction);
			nextAction();
		}
	}
	//void _start(){
	//	actionCtrl.MoveTo (15, 25);
	//}
	void buildActionMoveTo(int x, int y){
		System.Action act = () => {
			Debug.Log("move to "+x+","+y);
			actionCtrl.MoveTo (x, y);
		};
		actionArr.Add (act);
	}

	void buildActionAbsorb(int x, int y){
		System.Action act = () => {
			bool b =WorldValueMgr.It.DecreaseVlaueSprite(x,y);
			if(b){
				valueCarry += 2;
			}
			Debug.Log("absorb at "+x+","+y);
		};
		actionArr.Add (act);
	}

	void buildActionProduct(int x, int y){
		System.Action act = () => {
			while(valueCarry>0&&WorldValueMgr.It.IncreaseVlaueSprite(x,y)){
				valueCarry-=1;
			}
			Debug.Log("product at "+x+","+y);
		};
		actionArr.Add (act);
	}
}
