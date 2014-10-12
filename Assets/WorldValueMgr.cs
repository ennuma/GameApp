using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class WorldValueMgr : MonoBehaviour {

	public static WorldValueMgr It;
	public int[,] valueMatrix;
	public Material[] valueSprites;
	private GameObject[,] spriteMatrix;
	private float CubeSize;

	private List<Vector3> verticies = new List<Vector3>();
	private List<int> triangles = new List<int> ();
	private List<Vector2> UV = new List<Vector2>();
	// Use this for initialization

	void Awake(){
		valueSprites = Resources.LoadAll<Material>("Image/DestroyStage/Materials");
		Debug.Log (valueSprites.GetLength (0));
	}

	void Start () {
		It = this;
	}

	public void InitWithSize(int x, int y, float _cubeSize){
		valueMatrix = new int[x, y];
		spriteMatrix = new GameObject[x, y];
		CubeSize = _cubeSize;
		for(int px=0;px<valueMatrix.GetLength(0);px++){
			for(int py=0;py<valueMatrix.GetLength(1);py++){
				//if(py==3){
				//	voxel[px,py]=0;
				//} else{
				if(Random.Range(0,100)<20){
					valueMatrix[px,py]=1;
					CreateValueSprite(1,px,py);
				}else{
					valueMatrix[px,py]=0;
				}
				//}
			}
		}

	}


	public int DestroyVlaueSprite(int x, int y){
		int intense = valueMatrix [x, y];
		valueMatrix [x, y] = 0;
		Destroy(spriteMatrix[x,y]);	
		return intense;
	}

	public bool IncreaseVlaueSprite(int x, int y){
		if (WorldMeshGenerator.It.voxel [x, y] == 0) {
			return false;		
		}
		int intense = valueMatrix [x, y];
		intense += 1;
		if (intense > 10) {
			intense = 10;
			return false;
		}
		valueMatrix [x, y] = intense;
		CreateValueSprite(intense,x,y);	
		return true;
	}

	public bool DecreaseVlaueSprite(int x, int y){
		if (WorldMeshGenerator.It.voxel [x, y] == 0) {
			return false;		
		}
		int intense = valueMatrix [x, y];
		intense -= 1;
		if (intense < 0) {
			intense = 0;
			return false;
		}
		valueMatrix [x, y] = intense;
		CreateValueSprite(intense,x,y);	
		return true;
	}

	void CreateValueSprite(int intense, int x, int y){
		if (spriteMatrix [x, y] != null) {
			Destroy(spriteMatrix[x,y]);	
		}
		if (intense == 0) {
			return;		
		}
		// create the object
		GameObject fruit = new GameObject();
		fruit.tag = "BlockValue";
		fruit.layer = LayerMask.NameToLayer("BlockValue");
		// add a "SpriteRenderer" component to the newly created object
		fruit.AddComponent<MeshRenderer>();
		// assign "fruit_9" sprite to it
		fruit.GetComponent<MeshRenderer>().material = valueSprites[intense-1];
		// to assign the 5th frame
		fruit.AddComponent<MeshFilter>();

		Mesh cubeMesh = new Mesh ();
		CreateBlock (x, y);
		cubeMesh.Clear ();
		cubeMesh.vertices = verticies.ToArray ();
		cubeMesh.triangles = triangles.ToArray ();
		cubeMesh.uv = UV.ToArray ();
		cubeMesh.Optimize ();
		cubeMesh.RecalculateNormals ();
		fruit.GetComponent<MeshFilter> ().mesh = cubeMesh;

		spriteMatrix [x, y] = fruit;
	}

	void CreateBlock(float x, float y){
		int squareCount = 0;
		verticies.Clear ();
		triangles.Clear ();
		UV.Clear ();
		verticies.Add (new Vector3 (x - CubeSize / 2f, y - CubeSize / 2f, -0f)); //bot left 0
		verticies.Add (new Vector3 (x - CubeSize / 2f, y + CubeSize / 2f, -0f)); //top left 1
		verticies.Add (new Vector3 (x + CubeSize / 2f, y - CubeSize / 2f, -0f)); //bot right 2
		verticies.Add (new Vector3 (x + CubeSize / 2f, y + CubeSize / 2f, -0f)); //top right 3
		
		triangles.Add (squareCount*4+0);
		triangles.Add (squareCount*4+1);
		triangles.Add (squareCount*4+3);
		
		triangles.Add (squareCount*4+0);
		triangles.Add (squareCount*4+3);
		triangles.Add (squareCount*4+2);
		
		UV.Add (new Vector2 (0, 1));
		UV.Add (new Vector2 (0, 0));
		UV.Add (new Vector2 (1, 1));
		UV.Add (new Vector2 (1, 0));
	}

}
