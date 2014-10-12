using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class WorldMeshGenerator: MonoBehaviour {
	public float CubeSize;

	//public Material ground;
	//public Material grass;
	public bool update = false;

	private List<Vector3> verticies = new List<Vector3>();
	private List<Vector3> colVertices = new List<Vector3>();
	private List<int> triangles = new List<int> ();
	private List<int> colTriangles = new List<int> ();
	private List<Vector2> UV = new List<Vector2>();
	private Mesh cubeMesh;
	private MeshCollider col;

	public int[,]voxel;
	private int squareCount = 0;
	private int colCount = 0;

	public static WorldMeshGenerator It;

	void Awake(){
		It = this;
	}
	// Use this for initialization
	void Start () {
		CubeSize = 1.0f;
		gameObject.tag = "Voxel";
		cubeMesh = GetComponent<MeshFilter> ().mesh;
		col = GetComponent<MeshCollider> ();
		GenTerrain ();

		update = true;
		It = this;
	}
	
	// Update is called once per frame
	void Update () {
		if(update){
			BuildMesh ();
			UpdateMesh ();
			update = false;
		}
	}

	void UpdateMesh(){
		cubeMesh.Clear ();
		cubeMesh.vertices = verticies.ToArray ();
		cubeMesh.triangles = triangles.ToArray ();
		cubeMesh.uv = UV.ToArray ();
		cubeMesh.Optimize ();
		cubeMesh.RecalculateNormals ();

		verticies.Clear ();
		UV.Clear ();
		triangles.Clear ();
		squareCount = 0;

		Mesh newMesh = new Mesh();
		newMesh.vertices = colVertices.ToArray();
		newMesh.triangles = colTriangles.ToArray();
		col.sharedMesh= newMesh;
	

		colVertices.Clear();
		colTriangles.Clear();
		colCount=0;
	}

	void GenTerrain(){
		int width = 30;
		int height = 30;
		voxel=new int[width,height];
		
		for(int px=0;px<voxel.GetLength(0);px++){
			for(int py=0;py<voxel.GetLength(1);py++){
				//if(py==3){
				//	voxel[px,py]=0;
				//} else{
				voxel[px,py]=1;
				//}
			}
		}

		GetComponent<WorldValueMgr> ().InitWithSize (width, height,CubeSize);
	}

	void BuildMesh(){
		for(int px=0;px<voxel.GetLength(0);px++){
			for(int py=0;py<voxel.GetLength(1);py++){	
				if(voxel[px,py]!=0){
					GenCollider(px,py);
					if(voxel[px,py]==1){
						CreateBlock(px,py);
					}
				}
			}
		}
	}

	void GenCollider(int x, int y){

		colVertices.Add( new Vector3 (x - CubeSize/2f , y +CubeSize/2f  , 0));
		colVertices.Add( new Vector3 (x + CubeSize/2f , y +CubeSize/2f  , 0));
		colVertices.Add( new Vector3 (x + CubeSize/2f , y -CubeSize/2f  , 0 ));
		colVertices.Add( new Vector3 (x - CubeSize/2f , y -CubeSize/2f  , 0 ));
		
		ColliderTriangles();
		
		colCount++;

		if(Block(x,y+1)==0){
			colVertices.Add( new Vector3 (x - CubeSize/2f , y +CubeSize/2f  , 1));
			colVertices.Add( new Vector3 (x + CubeSize/2f , y +CubeSize/2f  , 1));
			colVertices.Add( new Vector3 (x + CubeSize/2f , y +CubeSize/2f  , 0 ));
			colVertices.Add( new Vector3 (x - CubeSize/2f , y +CubeSize/2f  , 0 ));
			
			ColliderTriangles();
			
			colCount++;
		}
		
		//bot
		if(Block(x,y-1)==0){
			colVertices.Add( new Vector3 (x - CubeSize/2f  , y -CubeSize/2f , 0));
			colVertices.Add( new Vector3 (x + CubeSize/2f , y - CubeSize/2f , 0));
			colVertices.Add( new Vector3 (x + CubeSize/2f , y -CubeSize/2f , 1 ));
			colVertices.Add( new Vector3 (x -CubeSize/2f , y -CubeSize/2f , 1 ));
			
			ColliderTriangles();
			colCount++;
		}
		
		//left
		if(Block(x-1,y)==0){
			colVertices.Add( new Vector3 (x -CubeSize/2f  , y -CubeSize/2f , 1));
			colVertices.Add( new Vector3 (x -CubeSize/2f  , y +CubeSize/2f  , 1));
			colVertices.Add( new Vector3 (x-CubeSize/2f  , y+CubeSize/2f  , 0 ));
			colVertices.Add( new Vector3 (x-CubeSize/2f  , y -CubeSize/2f , 0 ));
			
			ColliderTriangles();
			
			colCount++;
		}
		
		//right
		if(Block(x+1,y)==0){
			colVertices.Add( new Vector3 (x +CubeSize/2f , y+CubeSize/2f  , 1));
			colVertices.Add( new Vector3 (x +CubeSize/2f , y -CubeSize/2f , 1));
			colVertices.Add( new Vector3 (x +CubeSize/2f , y -CubeSize/2f , 0 ));
			colVertices.Add( new Vector3 (x +CubeSize/2f , y+CubeSize/2f  , 0 ));
			
			ColliderTriangles();
			
			colCount++;
		}
		
	}

	int Block (int x, int y){
		
		if(x==-1 || x==voxel.GetLength(0) ||   y==-1 || y==voxel.GetLength(1)){
			return 0;
		}
		
		return voxel[x,y];
	}

	void ColliderTriangles(){
		colTriangles.Add(colCount*4);
		colTriangles.Add((colCount*4)+1);
		colTriangles.Add((colCount*4)+3);
		colTriangles.Add((colCount*4)+1);
		colTriangles.Add((colCount*4)+2);
		colTriangles.Add((colCount*4)+3);
	}

	void CreateBlock(float x, float y){
		verticies.Add (new Vector3 (x - CubeSize / 2f, y - CubeSize / 2f, 0f)); //bot left 0
		verticies.Add (new Vector3 (x - CubeSize / 2f, y + CubeSize / 2f, 0f)); //top left 1
		verticies.Add (new Vector3 (x + CubeSize / 2f, y - CubeSize / 2f, 0f)); //bot right 2
		verticies.Add (new Vector3 (x + CubeSize / 2f, y + CubeSize / 2f, 0f)); //top right 3

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

		squareCount++;
	}

	void CreateCube(float x , float y, float z){
		verticies.Add (new Vector3 (x - CubeSize / 2f, y - CubeSize / 2f, z - CubeSize / 2f)); //bot left out 0
		verticies.Add (new Vector3 (x - CubeSize / 2f, y - CubeSize / 2f, z + CubeSize / 2f)); //bot left in 1
		verticies.Add (new Vector3 (x + CubeSize / 2f, y - CubeSize / 2f, z - CubeSize / 2f)); //bot right out 2
		verticies.Add (new Vector3 (x + CubeSize / 2f, y - CubeSize / 2f, z + CubeSize / 2f)); //bot right in 3
		verticies.Add (new Vector3 (x - CubeSize / 2f, y + CubeSize / 2f, z - CubeSize / 2f)); //top left out 4
		verticies.Add (new Vector3 (x - CubeSize / 2f, y + CubeSize / 2f, z + CubeSize / 2f)); //top left in 5
		verticies.Add (new Vector3 (x + CubeSize / 2f, y + CubeSize / 2f, z - CubeSize / 2f)); //top right out 6
		verticies.Add (new Vector3 (x + CubeSize / 2f, y + CubeSize / 2f, z + CubeSize / 2f)); //top right in 7

		triangles.Add (0);
		triangles.Add (3);
		triangles.Add (1);

		triangles.Add (0);
		triangles.Add (2);
		triangles.Add (3);

		triangles.Add (0);
		triangles.Add (4);
		triangles.Add (6);

		triangles.Add (0);
		triangles.Add (6);
		triangles.Add (2);

		triangles.Add (0);
		triangles.Add (1);
		triangles.Add (5);

		triangles.Add (0);
		triangles.Add (5);
		triangles.Add (4);

		triangles.Add (1);
		triangles.Add (7);
		triangles.Add (5);

		triangles.Add (1);
		triangles.Add (3);
		triangles.Add (7);

		triangles.Add (4);
		triangles.Add (5);
		triangles.Add (7);

		triangles.Add (4);
		triangles.Add (7);
		triangles.Add (6);

		triangles.Add (6);
		triangles.Add (7);
		triangles.Add (3);

		triangles.Add (6);
		triangles.Add (3);
		triangles.Add (2);
	}

}
