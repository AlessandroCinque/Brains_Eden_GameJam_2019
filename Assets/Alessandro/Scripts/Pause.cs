using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

    public static bool paused = false;
    public GameObject PauseMenuUI;
    void Awake()
    {
        paused = false;
        Time.timeScale = 1f;
    }
    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //Debug.Log("I'M WORKING");
        if (paused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
	}
     public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        paused = false;

    }
   public void Paused()
    {
        paused = true;
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }
}
