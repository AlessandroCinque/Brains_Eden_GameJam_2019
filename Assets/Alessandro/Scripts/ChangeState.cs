using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeState : MonoBehaviour
{
   

    private IState CurrentRunning;
    private IState PreaviusState;
    public GameObject player;
    public Pl_Controller ContollerComponent;

    void Start()
    {
        player = GameObject.Find("Player");
        if (player != null)
        {

            ContollerComponent = player.GetComponent<Pl_Controller>();
        }
        else
        {
           // Debug.Assert(false);
        }

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        //player.GetComponent<GameObject>();
    }
    void Update()
    {
        if (ContollerComponent != null)
        {
            if (ContollerComponent.SwitchState == true)
            {
                SceneManager.LoadScene("MMenu");
                //Debug.Log("I'M WORKING");
            }
        }

    }
  
    public void GoToMainM()
    {
        SceneManager.LoadScene("MMenu");
    }
    public void GoToCredits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void GoToGame()
    {
        SceneManager.LoadScene("GameTest");
    }
}
//public void SwitchState(IState newState)
// {
//     if (this.CurrentRunning != null)
//     {
//         this.CurrentRunning.Exit();
//     }

//     this.PreaviusState = this.CurrentRunning;
//     this.CurrentRunning = newState;
//     this.CurrentRunning.Enter();
// }
// public void ExecuteStateUpdate()
// {
//     var runningState = this.CurrentRunning;
//     if (runningState != null)
//     {
//         this.CurrentRunning.Execute();

//     }

// }
//public void SwitchToPreavius()
//{
//    this.CurrentRunning.Exit();
//    this.CurrentRunning = this.PreaviusState;
//    this.CurrentRunning.Enter();
//}