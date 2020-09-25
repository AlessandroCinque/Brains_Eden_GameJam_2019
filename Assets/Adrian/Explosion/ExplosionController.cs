using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour {

    //private float totalTime = 0.0f;

    public float radius = 1.0f;
    public float power = 10.0f;
    [Header("In Seconds")]
    public float lifetime = 30.0f;

    //private float particleDuration;

	// Use this for initialization
	void Start () {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);

        foreach (Collider hit in colliders)
        {
            if (hit.gameObject.tag != "IgnoreExplosion")
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(power, explosionPos, radius, 1.0f);
                }
            }  
        }

        //particleDuration = GetComponent<ParticleSystem>().main.duration;

        Destroy(gameObject, lifetime);
    }
	
	// Update is called once per frame
	void Update () {

    }
}
