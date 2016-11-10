using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using System.Text.RegularExpressions;

public class gameManager : MonoBehaviour {

	public StoryPoint currentStoryPoint;
	public Text gameText;
	public Cradle.Story importedStory;
	Dictionary<string, Cradle.StoryLink> decisions;

	private Regex rxChoicePuller;
	private Regex rxLinkTextPuller;


		
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

		//Init variables
		decisions = new Dictionary<string, Cradle.StoryLink>();
		rxChoicePuller = new Regex("(?<=[(]{2}).*(?=[)]{2}$)");
		rxLinkTextPuller = new Regex(".*(?=[(]{2})");


		Debug.Log ("Just started up gameManager");

		importedStory.Begin ();
		readyCurrentPassage ();
	}

	void displayCurrentPassage () {
		string displayText = "";

		displayText += importedStory.CurrentPassageName + "\n";

		foreach (Cradle.StoryText text in importedStory.GetCurrentText ())
			displayText += text.Text + "\n\n";

		foreach (Cradle.StoryLink link in importedStory.GetCurrentLinks()) {
			displayText += pullLinkText(link) + "\n";
		}

		gameText.text = displayText;
	}

	// Gets the current passage all set up (displays on screen and listens for events)
	void readyCurrentPassage() {
		displayCurrentPassage ();
		decisions.Clear ();
		foreach (Cradle.StoryLink link in importedStory.GetCurrentLinks()) {
			string choice = pullChoiceFromLink(link);
			decisions.Add (choice, link);
		}
	}

	void printCurrentPassage() {
		print ("Current passage name: " + importedStory.CurrentPassageName);

		foreach (Cradle.StoryText text in importedStory.GetCurrentText ())
			print (text.Text);

		foreach (Cradle.StoryLink link in importedStory.GetCurrentLinks()) {
			print (pullLinkText(link));
			print (pullChoiceFromLink (link));
		}
	}

	string pullLinkText(Cradle.StoryLink link) {
		Match m = rxLinkTextPuller.Match(link.Text);
		if (m.Success)
			return m.Value;
		return link.Text + " - No valid decision recognized";
	}

	string pullChoiceFromLink(Cradle.StoryLink link) {
		Match m = rxChoicePuller.Match(link.Text);
		if (m.Success)
			return m.Value;
		return "";
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Y)) {
			print ("In game manager y pressed");
			try
			{
				Cradle.StoryLink next = decisions["keypress-y"];
				importedStory.DoLink(next);
				readyCurrentPassage();
			}
			catch (KeyNotFoundException)
			{
				print ("Key not in decision dictionary");
			}
		} else if (Input.GetKeyDown (KeyCode.N)) {
			print ("In game manager n pressed");
			try
			{
				Cradle.StoryLink next = decisions["keypress-n"];
				importedStory.DoLink(next);
				readyCurrentPassage();
			}
			catch (KeyNotFoundException)
			{
				print ("Key not in decision dictionary");
			}
		}
	}

	public void recieveCollisions(string name) {
		try
		{
			Cradle.StoryLink next = decisions[name];
			importedStory.DoLink(next);
			readyCurrentPassage();
		}
		catch (KeyNotFoundException)
		{
			print (name + " not in decision dictionary");
		}
	}

	void updateDecision(StoryPoint nextStoryPoint) {
		currentStoryPoint = nextStoryPoint;
		//print (currentStoryPoint.text);
	}
}
