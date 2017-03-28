using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vuforia;

public class FinalLevelController: MonoBehaviour {
	public static FinalLevelController Instance { get; private set; }

	public bool win = false;
	public Text textGame;
	private GameObject player;
	private GameObject dragon;

	void Awake() {
		textGame.text = "";

		if (null == Instance) Instance = this;
		else {
			Debug.LogError("FinalLevelController::Awake - Instance already set. Is there more than one in scene?");
		}
	}

	void Start (){
		player = GameObject.FindWithTag("Player");
		dragon = GameObject.FindWithTag("Dragon");
	}         

	// Update is called once per frame
	void Update () {
		if(ErrorManager.error > 2) StartCoroutine("LoseSecene"); 
		else if(win) StartCoroutine("WinScene"); 
	}

	IEnumerator WinScene() {
		textGame.text = "Muy Bien!!!";
		player.GetComponent<Animator>().Play("Taunt");
		yield return new WaitForSeconds(2);
		dragon.GetComponent<Animator>().Play("dead");
		yield return new WaitForSeconds(1);
		player.GetComponent<Animator>().Play("win");
	}

	IEnumerator LoseSecene() {
		textGame.text = "Perdiste!";
		dragon.GetComponent<Animator>().Play("attack");
		yield return new WaitForSeconds(1);
		player.GetComponent<Animator>().Play("died");
	}
}
