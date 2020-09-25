using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

    private AudioSource sound;

    public Vector3 positionChange;
    private Vector3 startPosition;
    private Vector3 endPosition;
    
    [Header("In Seconds")]
    public float duration = 1.0f;

    private float totalTime = 0.0f;

    private bool doorOpened = false;
    private bool doorOpening = false;

	// Use this for initialization
	void Start () {
		sound = GetComponent<AudioSource>();

        startPosition = transform.position;
        endPosition = startPosition + positionChange;
    }
	
	// Update is called once per frame
	void Update () {
        totalTime += Time.deltaTime;

        if (doorOpened && doorOpening)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, totalTime / duration);

            if (totalTime > duration)
            {
                doorOpening = false;
            }
        }
    }

    public void OpenDoor()
    {
        if (!sound.isPlaying)
        {
            sound.Play(0);
        }

        if (!doorOpening)
        {
            totalTime = 0.0f;
            doorOpened = true;
            doorOpening = true;
        }

        Debug.Log("Door Opening");
    }
}
