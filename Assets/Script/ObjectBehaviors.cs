using UnityEngine;
using Vuforia;
using System.Collections;

public class ObjectBehaviors : MonoBehaviour,ITrackableEventHandler {

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
                                        TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED){
            knightAttr.SetActive(true);
            
            if (null == DragonsController.Instance)  return;

            if(isSol) DragonsController.Instance.win = true;
            else StartCoroutine("DesactiveAttr"); 
        }
        else{
            knightAttr.SetActive(false);
        }
    }   

    IEnumerator DesactiveAttr() {
        yield return new WaitForSeconds(1);
        knightAttr.SetActive(false);
        ErrorManager.error++;
    }
}


