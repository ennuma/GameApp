using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RythmeManager : MonoBehaviour {
	
	public RythmBehavior rythmBehavior;
	private float intervalTime;
	private float dropTime;
	private float creatTime;
	private int rythmeCount;
	//Local game time 
	private float currentTime;
	private List<float> timeList = new List<float>();
	private List<RythmeResult> resultList = new List<RythmeResult>();
	private Dictionary<string, BattleEvent> battleDic = new Dictionary<string, BattleEvent>();

	//value for range
	public float bad;
	public float normal;
	public float good;
	public float perfect;

	//flag to indicate if turn ends
	private bool isEnd;

	// Use this for initialization
	void Start () {
		//get interval time (second) from Database
		intervalTime = 1;
		//get dropTime from DATABASE 
		dropTime = 2;
		System.Action<IEventType> callback = turn_Start;
		EventMgr.It.register(new BattleTurnStartEvent(),callback);
		BattleTurnStartEvent btse = new BattleTurnStartEvent ();
		EventMgr.It.queueEvent (btse);

		Debug.Log("in Start");

		//Timelist should get from database , here fake a timelist to test
		timeList.Add (1);
		timeList.Add (2);
		timeList.Add (3);
		timeList.Add (4);

		battleDic.Add ("2211", new BattleEventAttack());
		battleDic.Add ("1122", new BattleEventDefense());

	}
	
	// Update is called once per frame
	void Update () {
		if (isEnd) {
			return;
		}
		currentTime += Time.deltaTime;
		if (rythmeCount < 4) {
			creatTime += Time.deltaTime;
			if (creatTime>intervalTime) {
				rythmBehavior.createNewRythmObj (dropTime);
				creatTime = 0;
				rythmeCount ++;
			}
		}
		checkMiss ();
		checkResult ();
	}

	public void onRightButtonClick(){
		Debug.Log("RightButtonClick");
		int currentCount = resultList.Count;
		if (currentCount > 3) {
			return;		
		}
		float nextCheckingTime = timeList [currentCount]+dropTime;
		float timedelta = currentTime - nextCheckingTime;
		float absdelta = Mathf.Abs (timedelta);
		if (absdelta > bad) {
			//miss
			//call rythmeBehavior invalid
			return;		
		}else if(absdelta > normal)
		{
			//bad
			RythmeResult rs = new RythmeResult(RythmeResult.leftOrRight.right, 3);
			resultList.Add(rs);
			//call rythmeBehavior 
			return;
		}else if(absdelta > good)
		{
			//normal
			RythmeResult rs = new RythmeResult(RythmeResult.leftOrRight.right, 2);
			resultList.Add(rs);
			//call rythmeBehavior 
			return;
		}else if(absdelta > perfect)
		{
			//good
			RythmeResult rs = new RythmeResult(RythmeResult.leftOrRight.right, 1);
			resultList.Add(rs);
			//call rythmeBehavior 
			return;
		}else
		{
			//perfect
			RythmeResult rs = new RythmeResult(RythmeResult.leftOrRight.right, 0);
			resultList.Add(rs);
			//call rythmeBehavior 
			return;
		}
	}

	
	public void onLeftButtonClick(){
		Debug.Log("LeftButtonClick");
		int currentCount = resultList.Count;
		if (currentCount > 3) {
			return;		
		}
		float nextCheckingTime = timeList [currentCount]+dropTime;
		float timedelta = currentTime - nextCheckingTime;
		float absdelta = Mathf.Abs (timedelta);
		if (absdelta > bad) {
			//miss
			Debug.Log("Invalid");
			//call rythmeBehavior 
			return;		
		}else if(absdelta > normal)
		{
			//bad
			Debug.Log("BAD");
			RythmeResult rs = new RythmeResult(RythmeResult.leftOrRight.left, 3);
			resultList.Add(rs);
			//call rythmeBehavior 
			return;
		}else if(absdelta > good)
		{
			//normal
			Debug.Log("normal");
			RythmeResult rs = new RythmeResult(RythmeResult.leftOrRight.left, 2);
			resultList.Add(rs);
			//call rythmeBehavior 
			return;
		}else if(absdelta > perfect)
		{
			Debug.Log("good");
			//good
			RythmeResult rs = new RythmeResult(RythmeResult.leftOrRight.left, 1);
			resultList.Add(rs);
			//call rythmeBehavior 
			return;
		}else
		{
			Debug.Log("perfect");
			//perfect
			RythmeResult rs = new RythmeResult(RythmeResult.leftOrRight.left, 0);
			resultList.Add(rs);
			//call rythmeBehavior 
			return;
		}
	}


	void turn_Start(IEventType turnStartEvent){
		Debug.Log("in RythmeManager turn_Start: \n");
		creatTime = 0;
		currentTime = 0;
		rythmeCount = 0;

		//reset everything
		resultList.Clear ();
		isEnd = false;
	}


	void checkMiss(){
		int currentCount = resultList.Count;
		if (currentCount > 3) {
			return;		
		}
		float nextCheckingTime = timeList [currentCount]+dropTime;
		float upperbound = nextCheckingTime + bad;
		if (currentTime > upperbound) {
			//miss
			Debug.Log("MISS");
			RythmeResult rs = new RythmeResult(RythmeResult.leftOrRight.miss, 4);
			resultList.Add(rs);
		}
	}

	void checkResult(){

		string instResult = "";
		int qualityResult = 0;
		if (resultList.Count == 4) {
			//do everything we need to send msg to server	

			isEnd = true;
			foreach(RythmeResult rs in resultList)
			{
				if (rs.myCommand == RythmeResult.leftOrRight.miss){
					BattleEventMiss info = new BattleEventMiss();
					info.self_id = 0;
					info.rhythm_quality = 0;
					EventMgr.It.queueEvent(info);
					instResult = "miss";
					qualityResult = 4;
					Debug.Log(instResult + qualityResult);
					return;
				}
				else if(rs.myCommand == RythmeResult.leftOrRight.left){
					instResult += "1";
				}
				else { instResult += "2"; }
				qualityResult = qualityResult + rs.quality;
		
			}
			Debug.Log(instResult);
			if(battleDic.ContainsKey(instResult)){
				Dictionary<string, BattleEvent>  newDic = new Dictionary<string, BattleEvent>(battleDic); 
				BattleEvent returnEvent = newDic[instResult];
				returnEvent.self_id = 0;
				returnEvent.rhythm_quality = qualityResult/4;
				//Debug.Log(returnEvent.type);
				EventMgr.It.queueEvent(returnEvent);
			}
			else{
				BattleEventMiss info = new BattleEventMiss();
				info.self_id = 0;
				info.rhythm_quality = 0;
				instResult = "miss";
				qualityResult = 4;
				EventMgr.It.queueEvent(info);

			}


			BattleEventDefense enemy = new BattleEventDefense ();
			enemy.self_id = 1;
			enemy.rhythm_quality=1;
			EventMgr.It.queueEvent (enemy);
		}
	}

}
