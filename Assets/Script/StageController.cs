using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class StageController: MonoBehaviour,ITrackableEventHandler {
	public static StageController Instance { get; private set; }

	public bool gamePause = true;
	public AudioClip audioWin;
	public AudioClip audioLose;

	private AudioSource audio = null;
	private TrackableBehaviour mTrackableBehaviour;
	private bool gameOver = false;

	void Awake(){
		audio = GetComponent<AudioSource>();
		if (null == Instance) Instance = this;
		else {
			Debug.LogError("StageController::Awake - Instance already set. Is there more than one in scene?");
		}
	}

	void Start(){
		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour){
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		}
		audio.volume = 0.4f;
	}

	public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus,
										TrackableBehaviour.Status newStatus){
		if (gameOver) return;
		
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
		    newStatus == TrackableBehaviour.Status.TRACKED ||
		    newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED) {
			gamePause = false;
			audio.Play ();
		} else if (!gamePause) {
			gamePause = true;
			audio.Pause();
		}
	}   

	public void setAudioWin(){
		gameOver = true;
		audio.clip = audioWin;
		StartCoroutine ("waitSeconds");
	}

	public void setAudioLose(){
		gameOver = true;
		audio.clip = audioLose;
		StartCoroutine ("waitSeconds");
	}

	IEnumerator waitSeconds(){
		audio.volume = 1f;
		audio.Play ();
		yield return new WaitForSeconds(6);
		audio.Stop ();
	}

}