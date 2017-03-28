﻿using UnityEngine;
using Vuforia;
using System.Collections;

public class FinalLevelObjBehaviors : MonoBehaviour,ITrackableEventHandler {

	private TrackableBehaviour mTrackableBehaviour;
	public GameObject knightAttr;
	public bool isSol = false;

	void Start(){
		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour){
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		}
	}

	public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus,
										TrackableBehaviour.Status newStatus){
		if (null == FinalLevelController.Instance)  return;
		if (FinalLevelController.Instance.gameOver)  return;

		if (newStatus == TrackableBehaviour.Status.DETECTED ||
			newStatus == TrackableBehaviour.Status.TRACKED ||
			newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED){
			knightAttr.SetActive(true);
			if(!isSol) StartCoroutine("DesactiveAttr");
		}
		else knightAttr.SetActive(false);
	}   

	IEnumerator DesactiveAttr() {
		ErrorManager.error++;
		yield return new WaitForSeconds(1);
		knightAttr.SetActive(false);
	}
}



