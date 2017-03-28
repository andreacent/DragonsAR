using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vuforia;

public class DragonsController : MonoBehaviour {
    public static DragonsController Instance { get; private set; }

    public bool win = false;
    public Text textGame;
    public string nextSceneName;
    private GameObject player;

    void Awake() {
        textGame.text = "";

        if (null == Instance) {
            Instance = this;
        } else {
            Debug.LogError("DragonsController::Awake - Instance already set. Is there more than one in scene?");
        }
    }

    void Start (){
        player = GameObject.FindWithTag("Player");
    }         

    // Update is called once per frame
    void Update () {
        if(ErrorManager.error > 2) {
            textGame.text = "Perdedor";
            player.GetComponent<Animator>().Play("died");
        }
		else if(win) StartCoroutine("FinishScene"); 
    }

	IEnumerator FinishScene() {
		textGame.text = "Muy Bien!!!";
		player.GetComponent<Animator>().Play("win");
		yield return new WaitForSeconds(3);
		SceneManager.LoadScene (nextSceneName);
	}
}
