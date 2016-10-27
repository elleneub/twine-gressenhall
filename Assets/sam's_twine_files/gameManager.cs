using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class gameManager : MonoBehaviour {

	public StoryPoint currentStoryPoint;
	public Text gameText;
		
	/*
	 * For the "room" type choice point you can use these rooms:
	 * TopOfLeftStairs, TopOfRightStairs, Hallway, MainEntrance, OutsideFront
	 * 
	 * For the "stationary" type choice point you can use these rooms:
	 * keypress-y and keypress-n
	 * 
	 */

	// Use this for initialization
	void Start () {
		Debug.Log ("Just started up gameManager");

		StoryPoint zeroPoint = new StoryPoint ("Do you want to go to the workhouse? (Press y for yes, n for no.)", "stationary");

		StoryPoint firstPointA = new StoryPoint ("No? To bad. You are outside of a workhouse. Walk up to the workhouse entrance.", "room");
		StoryPoint firstPointB = new StoryPoint ("Great, cause you don't have a choice. You are outside of a workhouse. Walk up to the workhouse entrance.", "room");


		zeroPoint.addChoice (firstPointA, "keypress-n");
		zeroPoint.addChoice (firstPointB, "keypress-y");

		StoryPoint secondPoint = new StoryPoint ("You stand in front of the workhouse looking at it's imposing entrance." +
			"Go ahead on inside.", "room");

		firstPointA.addChoice (secondPoint, "OutsideFront");
		firstPointB.addChoice (secondPoint, "OutsideFront");

		StoryPoint thirdPoint = new StoryPoint ("Welcome to the workhouse. You aren't really sure you want to be here. You think you might be able to leave if you " +
			"just go back out the entrance, but the workhouse master is asking you to walk up the stairs on the right.", "room");

		secondPoint.addChoice (thirdPoint, "MainEntrance");

		StoryPoint fourthPoint = new StoryPoint ("You try to run away but that just isn't going to happen. There is no where to go..." +
			" Go back in the workhouse and up the stairs to the right.", "room");

		StoryPoint fifthPoint = new StoryPoint ("Your life in the workhouse begins today.", "none");

		thirdPoint.addChoice (fourthPoint, "OutsideFront");
		thirdPoint.addChoice (fifthPoint, "TopOfRightStairs");

		fourthPoint.addChoice (fifthPoint, "TopOfRightStairs");


		/* Initialize */
		currentStoryPoint = zeroPoint;
		gameText.text = currentStoryPoint.text;
	} 
	
	// Update is called once per frame
	void Update () {
		
		print (currentStoryPoint.decisionType);
		if (currentStoryPoint.decisionType == "stationary") {
			StoryPoint nextStoryPoint = null;
			if (Input.GetKeyDown (KeyCode.Y)) {
				print ("In game manager y pressed");
				nextStoryPoint = currentStoryPoint.checkDecisions ("keypress-y");
			} else if (Input.GetKeyDown (KeyCode.N)) {
				print ("In game manager n pressed");
				nextStoryPoint = currentStoryPoint.checkDecisions ("keypress-n");
			}

			if (nextStoryPoint != null) {
				currentStoryPoint = nextStoryPoint;
				print (nextStoryPoint.text);
				gameText.text = currentStoryPoint.text;
			}
		}

	}

	public void recieveCollisions(string name) {
		
		print ("In game manager recieved collision from: " + name);
		if (currentStoryPoint.decisionType == "room") {
			StoryPoint nextStoryPoint = currentStoryPoint.checkDecisions (name);
			if (nextStoryPoint != null) {
				currentStoryPoint = nextStoryPoint;
				print (nextStoryPoint.text);
				gameText.text = currentStoryPoint.text;
			}

		}
	}

	void updateDecision(StoryPoint nextStoryPoint) {
		currentStoryPoint = nextStoryPoint;
		print (currentStoryPoint.text);
	}
}
