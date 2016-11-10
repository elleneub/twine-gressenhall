using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoryPoint {

	public string text;
	public string decisionType;
	IList<Decision> decisionList;

	public StoryPoint(string storyText, string storyDecisionType)
	{
		this.text = storyText;
		this.decisionType = storyDecisionType;
		initialize ();
	}

	public StoryPoint(string text)
	{
		text = this.text;
		decisionType = "none";
		initialize ();
	}

	// Use this for initialization
	void initialize () {
		//Debug.Log ("Making new storypoint: " + text);
		decisionList = new List<Decision> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void addDecision (Decision newDecision) {
		decisionList.Add (newDecision);
	}

	public void addChoice (StoryPoint nextStoryPoint, string condition) {
		Decision newDecision = new Decision (nextStoryPoint, condition);
		decisionList.Add (newDecision);
	}

	public StoryPoint checkDecisions(string condition) {
		foreach (Decision element in decisionList)
		{
			if (element.condition == condition) {
				return element.consequence;
			}
		}
		return null;
	}
}
