﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vuforia;

public class DragonsController : MonoBehaviour {
    public static DragonsController Instance { get; private set; }

	public bool gameOver = false;
    public bool win = false;
	public Text textGame;
	public Text textHint;
	public GameObject canvasBtn;
	public int level;
	private GameObject player;

    void Awake() {
        textGame.text = "";
		textHint.text = "";

        if (null == Instance) Instance = this;
        else {
            Debug.LogError("DragonsController::Awake - Instance already set. Is there more than one in scene?");
        }
    }

    void Start (){
        player = GameObject.FindWithTag("Player");
		canvasBtn.SetActive (false);
		StartCoroutine ("StartHint");
    }         

    // Update is called once per frame
    void Update () {
		if (gameOver) return;

		if (ErrorManager.error > 2) StartCoroutine ("GameOver");
		else if (win) StartCoroutine ("FinishLevel");
    }

	IEnumerator StartHint() {
		switch (level) {
		case 1:
			textHint.text = "NIVEL 1:\nNecesito deshacerme de estos árboles,\nsino no podré entrar al castillo.";
			break;
		case 2:
			textHint.text = "NIVEL 2:\nDebo matar a estos monstruos pero no\npuedo alcanzarlos desde el suelo.";
			break;
		default:
			break;
		}
		yield return new WaitForSeconds(7);
		textHint.text = "";
	}

	IEnumerator FinishLevel() {
		textGame.text = "Muy Bien!!!";
		player.GetComponent<Animator>().Play("AttackWin");
		yield return new WaitForSeconds(3);
		SceneManager.LoadScene ("Level"+(level+1));
	}

	IEnumerator GameOver() {
		gameOver = true;
		textGame.text = "Perdiste!";
		player.GetComponent<Animator> ().Play ("died");
		if (null != StageController.Instance) 
			StageController.Instance.setAudioLose();
		yield return new WaitForSeconds(3);
		canvasBtn.SetActive (true);
	}
}
