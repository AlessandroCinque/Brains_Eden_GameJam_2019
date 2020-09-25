using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSoundController : MonoBehaviour {

    public GameObject explosion;

    private float totalTime = 0.0f;
    private float audioTimeCount = 0.0f;

    //public float speed = 1.0f;
    public float spawnInterval = 4.0f; // in seconds

    private AudioSource sound;

    public Vector3 offset = new Vector3(1.5f, 0.0f, 0.0f);

    //private bool spawnedChild = false;

    [Header("Time Between Playing Sound")]
    public float audioBuffer = 2.0f;
    public bool useAudioBuffer = false;

    [Header("Prevent Spawning In The Air")]
    public float waitBeforeSpawning = 0.1f;
    private bool lookingForNewPosition = false;

    [Header("Waiting X Seconds To Start Spawning")]
    public float waitUntilToStart = 30.0f;
    private float spawnTime = 0.0f;

    private Rigidbody rb;

    // check if stop moving
    //private Vector3 oldPos;
    //private Vector3 newPos;

    private float waitTime = 0.0f;

    // Use this for initialization
    void Start () {
        sound = GetComponent<AudioSource>();

        //Instantiate(explosion, transform.position, Quaternion.identity);

        //sound.Play(0);

        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        spawnTime += Time.deltaTime;

        if (spawnTime >= waitUntilToStart)
        {
            totalTime += Time.deltaTime;
            audioTimeCount += Time.deltaTime;

            //newPos = transform.position;

            if (totalTime >= spawnInterval && !lookingForNewPosition)
            {

                //oldPos = newPos;

                //Debug.Log(transform.position);

                transform.position = transform.position + offset;
                Vector3 temp = transform.position;
                temp[1] = 7;
                transform.position = temp;

                rb.velocity = new Vector3(0, -0.5f, 0);

                lookingForNewPosition = true;

                //Debug.Log(transform.position);

                //newPos = transform.position;


            }

            //float distance = Vector3.Distance(newPos, oldPos);

            if (lookingForNewPosition)
            {
                waitTime += Time.deltaTime;

                if (waitTime >= waitBeforeSpawning)
                {
                    if (rb.velocity.y == 0)
                    {

                        //Debug.Log(lookingForNewPosition);
                        Instantiate(explosion, transform.position, Quaternion.identity);
                        lookingForNewPosition = false;
                        totalTime = 0.0f;

                        if (useAudioBuffer)
                        {
                            if (audioTimeCount >= audioBuffer)
                            {
                                sound.Stop();
                                sound.Play(0);

                                audioTimeCount = 0.0f;
                            }
                        }
                        else
                        {
                            if (!sound.isPlaying)
                            {
                                sound.Play(0);
                            }
                        }

                        Vector3 temp = new Vector3(transform.position.x, -5, transform.position.z);
                        transform.position = temp;
                    }
                    waitTime = 0.0f;
                }
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
    }
}


