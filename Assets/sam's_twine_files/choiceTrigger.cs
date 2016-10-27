using UnityEngine;
using System.Collections;

public class choiceTrigger : MonoBehaviour {

	MeshRenderer my_rend;


	Canvas canvas;
	gameManager manager;



	void Awake() {
		canvas = GameObject.Find ("Canvas").GetComponent<Canvas> ();
		manager = canvas.GetComponent<gameManager> ();
		//testText.enabled = false;
		//testText.text = "You walk into the local pub, a man in a cloak approaches and asks if you would like to go on an adventure. Yes or no?" +
//			"\n Press y for yes or n for no";
		//done = false;

	}

	// Use this for initialization
	void Start () {
		my_rend = GetComponent<MeshRenderer> ();
	}

	public void OnTriggerEnter(Collider other) {
		
		if (other.gameObject.name == "Player") {
			print ("In object: " + gameObject.name);
			manager.recieveCollisions (gameObject.name);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
