using UnityEngine;
using Vuforia;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class DragonsController : MonoBehaviour {
    public static DragonsController Instance { get; private set; }

    public bool win = false;
    public Text textGame;
    private Animator player_animator;

    void Awake() {
        textGame.text = "";

        if (null == Instance) {
            Instance = this;
        } else {
            Debug.LogError("DragonsController::Awake - Instance already set. Is there more than one in scene?");
        }
    }

    void Start (){
        player_animator = GameObject.FindWithTag("Player").GetComponent<Animator>();
    }         

    // Update is called once per frame
    void Update () {
        if(ErrorManager.error > 2) {
            textGame.text = "Perdedor";
            player_animator.Play("died");
        }
        else if(win) {
            textGame.text = "Muy Bien!!!";
            player_animator.Play("win");
        }
    }
}
