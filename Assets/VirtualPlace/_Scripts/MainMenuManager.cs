using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour 
{

	// Use this for initialization
	void Start ()
    {
		
	}
	


    public void OnClickPlayButton()
    {
        SceneManager.LoadScene("MainGame");
    }
}
