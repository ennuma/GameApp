using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class WorldGenerator : MonoBehaviour {
	//public int minX;
	public int maxX;
	//public int minY;
	public int maxY;

	//id 1
	public GameObject ground;
	//id 2
	public GameObject grass;

	public int[,] voxel;
	private GameObject[,] visible;
	private List<GameObject> groundPool;
	private List<GameObject> grassPool;
	public static WorldGenerator wg;
	// Use this for initialization
	void Start () {
		voxel = new int[maxX, maxY];
		visible = new GameObject[maxX,maxY];
		groundPool = new List<GameObject>();
		grassPool = new List<GameObject>();
		CreatePool ();
		GenerateWorld ();
		UpdateWorld ();
		wg = this;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		UpdateWorld ();
//		Debug.Log (groundPool.Count);
	}

	void CreatePool(){
		for (int i = 0; i < 200; i++) {
			GameObject goGround = Instantiate(ground) as GameObject;
			goGround.SetActive(false);
			groundPool.Add(goGround);

			//GameObject goGrass = Instantiate(grass) as GameObject;
			//goGrass.SetActive(false);
			//grassPool.Add(goGrass);
		}
	
	}

	void GenerateWorld(){
		for (int i = 0; i < maxX; i++) {
			for(int j = 0; j < maxY; j++){
				voxel[i,j] = 1;
			}		
		}
	}

	void UpdateWorld(){
		//RemoveAllVoxelInWorld ();
		for (int i = 0; i < maxX; i++) {
			for(int j = 0; j < maxY; j++){
				Vector3 point = new Vector3(i, j , transform.position.z);
				Vector3 converted = Camera.main.WorldToViewportPoint(point);
				GameObject vis = visible[i,j]; 
				int id = voxel[i,j];
				if(converted.x >=0 && converted.x <=1 && converted.y >=0 && converted.y <=1){
					if(!vis){
						//Debug.Log("not vis");
						if(id == 1){
							PlaceBlockAtPoint(ground,i,j);
						}else if(id == 2){
							PlaceBlockAtPoint(grass,i, j);
						}
					}else{
						if(id == 0){
							Debug.Log("not vis");
							Recycle(vis);
						}
					}
				}else{
					if(vis){
						Recycle(vis);
					}
				}
			}		
		}
	}

	void RemoveAllVoxelInWorld(){
		foreach (Transform child in transform) {
			Recycle(child.gameObject);
		}
	}

	void Recycle(GameObject go){
		//int id = voxel [(int)go.transform.position.x, (int)go.transform.position.y];
		//if (id == 1) {
			go.SetActive(false);	
			groundPool.Add(go);
		//}else if(id == 2){
		//	go.SetActive(false);
		//	grassPool.Add(go);
		//}
		visible [(int)go.transform.position.x, (int)go.transform.position.y] = null;
	}

	void PlaceBlockAtPoint(GameObject block, float x, float y){
		GameObject go = groundPool[0];
		if(go){
			groundPool.RemoveAt(0);
			go.tag = "Voxel";
			go.transform.position = new Vector3 (x, y, transform.position.z);
			go.transform.parent = transform;
			go.layer = LayerMask.NameToLayer("Voxel");
			go.SetActive(true);
			visible[(int)x,(int)y] = go;
		}else{
			throw new UnityException("There is nothing in Pool");
		}
	}
}
