using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class Results : MonoBehaviour
{
    private string sceneName;
    public float time = 0.0f;

    //===============================

    //GameObject textComponent;
    //Text ourComponent;

    GameObject MeshComponent;
    TextMeshProUGUI textMesh;
    //TMP_Text tExt;

    // Use this for initialization
    void Start ()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
     
        
        //textComponent = GameObject.Find("MyText");
        //ourComponent = textComponent.GetComponent<Text>();

        MeshComponent = GameObject.Find("MyText1");
        textMesh = MeshComponent.GetComponent<TextMeshProUGUI>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "GameTest")
        {
            time += Time.deltaTime;
            //ourComponent.text = time.ToString();
            textMesh.text = time.ToString("0");
        }
    }
}
