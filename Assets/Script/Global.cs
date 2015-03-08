#define USE_LOCAL
#define DEBUG
using UnityEngine;
using System.Collections;
public class Global : MonoBehaviour {

	public string urlResources;
	public GameObject sceneAnchor;

	public static Global It;
	private bool m_bUseLocalResources = false;
	private bool m_bDebugMode = false;
	private string m_sBundleName;

	public bool bUseLocalResources { get { return m_bUseLocalResources; } }
	public string sBundleName { get { return m_sBundleName; } } 

	private NetMgr m_NetMgr;
	private ProtocalMgr m_ProtocalMgr;
	//private SceneMgr m_SceneMgr;
	private BundleMgr m_BundleMgr;
	private EventMgr m_EventMgr;

	public NetMgr NetMgr{get{return m_NetMgr;}}
	public ProtocalMgr ProtocalMgr{get{return m_ProtocalMgr;}}
	//public SceneMgr SceneMgr{get{return m_SceneMgr;}}
	public EventMgr SceneMgr{get{return m_EventMgr;}}
	public BundleMgr BundleMgr{get{return m_BundleMgr;}}
	public bool debug = false;
	void Awake(){
		//It = this;
#if USE_LOCAL
		m_bUseLocalResources = true;
#endif

#if DEBUG
		m_bDebugMode = true;
#endif

		//init eventmgr
		m_EventMgr = __CreateMgr<EventMgr> ();
	}

	void Start () {

		m_sBundleName = "Prefab";

		StartCoroutine ("_Start");
		It = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator _Start(){
		yield return 0;
		m_NetMgr = __CreateMgr<NetMgr> ();
		m_BundleMgr = __CreateMgr<BundleMgr> ();
		m_ProtocalMgr = __CreateMgr<ProtocalMgr> ();
		__CreateMgr<SceneDirector> ();

		if (!debug) {
			CreateGameStartView ();
		}
		CreateGameStartView ();
	}

	T __CreateMgr<T>() where T:Component{
		T link = this.GetComponentInChildren<T> ();
		if (link == null) {
			GameObject mgr = new GameObject (typeof(T).Name);
			link = mgr.AddComponent<T>();
			mgr.transform.parent = transform;
		}
		return link;
	}

	public void CreateGameStartView(){
		//Debug.Log(m_BundleMgr);
		ShowWaiting ();
		System.Action<Object> handler = (asset) => {
			//Debug.Log(asset);
			SceneDirector.It.SwitchScene (asset, sceneAnchor.transform);
			HideWaiting();
		};
		StartCoroutine(m_BundleMgr.CreateGameObject (sBundleName, "BattleView", handler));
	}

	public void CreateMainGameView(){
		ShowWaiting ();
		System.Action<Object> handler = (asset) => {
			SceneDirector.It.SwitchScene (asset, sceneAnchor.transform);
			HideWaiting();
		};
		StartCoroutine(m_BundleMgr.CreateGameObject (sBundleName, "HeroView", handler));
	}

	public void SendMsg(string data){
		NetMgr.postData (data);
	}

	public void ProcessMsg(string msg){

	}

	void ShowWaiting(){
		Debug.Log("waiting");
	}

	void HideWaiting(){
		Debug.Log("hide waiting");
	}
}
