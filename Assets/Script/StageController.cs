using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class StageController: MonoBehaviour,ITrackableEventHandler {
	public static StageController Instance { get; private set; }

	public bool gamePause = true;

	private AudioSource audio = null;
	private TrackableBehaviour mTrackableBehaviour;

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
	}

	public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus,
										TrackableBehaviour.Status newStatus){

		if (newStatus == TrackableBehaviour.Status.DETECTED ||
		    newStatus == TrackableBehaviour.Status.TRACKED ||
		    newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED) {
			gamePause = false;
			audio.Play ();
		} else if (gamePause) {
			gamePause = true;
			audio.Pause();
		}
	}   

}