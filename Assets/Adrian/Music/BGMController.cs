using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour {

    //public Transform cameraPos;

    private AudioSource sound;

    // Use this for initialization
    void Start () {
        sound = GetComponent<AudioSource>();

        DontDestroyOnLoad(transform.gameObject);

        sound.Play(0);
	}
	
	// Update is called once per frame
	void Update () {
        //transform.position = cameraPos.position;
	}
}
