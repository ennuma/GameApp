
using System;
/**
 * RythmeResult to Recall 
 **/
public class RythmeResult
{
	// record L/R button input
	public	enum leftOrRight{left, right, miss};
	private leftOrRight _myCommand;
	// record 
	private float _quality;


	public RythmeResult (leftOrRight lr, float qua)
	{
		_myCommand = lr;
		_quality = qua;
		//type = "battle turn start event";
	}

	public leftOrRight myCommand
	{
		get { return _myCommand; }
		set { _myCommand = value; }
	}

	public float quality
	{
		get { return _quality; }
		set { _quality = value; }
	}
}


