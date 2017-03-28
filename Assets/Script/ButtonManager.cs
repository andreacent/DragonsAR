using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour{

	public void NewGameBtn(){
		SceneManager.LoadScene("Level1");
	}

	public void ExitGameBtn(){
		Application.Quit ();
	}
}

