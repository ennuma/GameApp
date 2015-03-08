using UnityEngine;
using System.Collections;

public class BundleMgr : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public IEnumerator CreateGameObject(string bundleName, string resName, System.Action<Object> handler){
		yield return 0;
		Object asset = null;
		LoadResources (bundleName, resName, out asset);
		if (!asset) {
			while(true){
				if(CheckBundleExist()){
					LoadResources(bundleName,resName,out asset);
					break;
				}
			yield return new WaitForSeconds(0.1f);
			}	
		}
		if (handler != null) {
			handler(asset);
		}
	}

	private void LoadResources(string bundleName, string resName, out Object asset){
		if (Global.It.bUseLocalResources) {
			string path = "";
			path += bundleName;
			path += "/";
			path += resName;
			asset = Resources.Load (path);
			//Debug.Log(path);
			if (asset == null) {
				//throw new UnityException("Resources not found, Scene cannot be initialized!");		
			}
		}else{
			asset = null;
		}
	}

	private bool CheckBundleExist(){
		return false;
	}
}

