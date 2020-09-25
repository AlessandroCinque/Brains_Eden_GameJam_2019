using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{

    public enum ColorEnum { RED, ORANGE, YELLOW, GREEN, BLUE, PURPLE, WHITE };

    //public ColorEnum Color = ColorEnum.WHITE;

    public GameObject door;
    public float triggerRange = 1.0f;

    public Transform playerPosition;

    private AudioSource sound;

    private bool switchPressed = false;

    // Use this for initialization
    void Start()
    {
        //GetComponent<Renderer>().material.color = QueryColor(Color);

        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 diff = transform.position - playerPosition.position;

        //Debug.Log(diff.x);

        if (Mathf.Abs(diff.x) <= triggerRange && !switchPressed)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                door.GetComponent<DoorController>().OpenDoor();
               // Debug.Log(diff.x);
                switchPressed = true;

                if (!sound.isPlaying)
                {
                    sound.Play(0);
                }

                door.GetComponent<DoorController>().OpenDoor();

                switchPressed = false;
            }
        }
    }
}

//    void OnTriggerEnter(Collider other)
//    {
//        if (other.gameObject.tag == "Player")
//        {
//            Debug.Log("Hit switch");
//        }

//        if (!switchPressed)
//        {
            
//        }
//    }
//}
