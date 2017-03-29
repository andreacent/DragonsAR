using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using System.Linq;

public class FinalLevelController: MonoBehaviour {
	public static FinalLevelController Instance { get; private set; }

	//TEXT
	public Text textGame;
	public Text textHint;
	//OTROS
	public string[] badTargetNames;
	public bool gameOver = false;
	public GameObject canvasBtn;

	private GameObject player;
	private GameObject dragon;

	void Awake() {
		textGame.text = "";
		textHint.text = "";

		if (null == Instance) Instance = this;
		else {
			Debug.LogError("FinalLevelController::Awake - Instance already set. Is there more than one in scene?");
		}
	}

	void Start (){
		player = GameObject.FindWithTag("Player");
		dragon = GameObject.FindWithTag("Dragon");
		canvasBtn.SetActive (false);
		StartCoroutine ("StartHint");
	}         

	// Update is called once per frame
	void Update () {
		if (gameOver) return;

		if (ErrorManager.error > 2) StartCoroutine ("LoseSecene");
		else {
			// Get the Vuforia StateManager
			StateManager sm = TrackerManager.Instance.GetStateManager ();
			// Query the StateManager to retrieve the list of currently 'active' trackables 
			IEnumerable<TrackableBehaviour> activeTrackables = sm.GetActiveTrackableBehaviours ();

			// Iterate through the list of active trackables
			bool win = true;
			int numActiveTrackables = 0;
			foreach (TrackableBehaviour tb in activeTrackables) {
				Debug.Log ("Trackable: " + tb.TrackableName);
				if (badTargetNames.Contains (tb.TrackableName)) {
					win = false;
					break;
				}
				numActiveTrackables++;
			}

			if (numActiveTrackables > 3 && win) StartCoroutine ("WinScene"); 
		}
	}

	IEnumerator WinScene() {
		gameOver = true;
		textGame.text = "Ganaste!!";
		//player attack
		player.GetComponent<Animator>().Play("Taunt");

		if (null != StageController.Instance) 
			StageController.Instance.setAudioWin();
		
		yield return new WaitForSeconds(2);
		//dragon die
		dragon.GetComponent<Animator>().Play("dead");
		yield return new WaitForSeconds(1);
		player.GetComponent<Animator>().Play("win");
		yield return new WaitForSeconds(1);
		canvasBtn.SetActive (true);
	}

	IEnumerator LoseSecene() {
		gameOver = true;
		textGame.text = "Perdiste!";
		dragon.GetComponent<Animator>().Play("attack");

		if (null != StageController.Instance) 
			StageController.Instance.setAudioLose();
		
		yield return new WaitForSeconds(1);
		player.GetComponent<Animator>().Play("died");
		yield return new WaitForSeconds(1);
		canvasBtn.SetActive (true);
	}

	IEnumerator StartHint() {
		textHint.text = "NIVEL 3:\nDebo estar preparado para esta pelea.";
		yield return new WaitForSeconds(5);
		textHint.text = "";
	}
}
