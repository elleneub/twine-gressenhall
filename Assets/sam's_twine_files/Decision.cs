using UnityEngine;
using System.Collections;

public class Decision {

	public StoryPoint consequence;
	public string condition;


	public Decision(StoryPoint consequence, string cond)
	{
		this.consequence = consequence;
		this.condition = cond;
		Debug.Log ("Making new decision with condition: " + cond);
	}

	// Use this for initialization
	void Start () {
		Debug.Log("Hello");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
