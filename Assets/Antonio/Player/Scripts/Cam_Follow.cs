//Antonio

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_Follow : MonoBehaviour
{
    //Reference
    [Header("Reference")]
    [SerializeField] Transform target;
    [SerializeField] Vector3 Offset;
    [SerializeField] Transform Cam;


    [Header("Vars Move")]
    [SerializeField] float IdealDelay = 0.1f;
    [SerializeField] float IdealStop = 0.01f;
    bool IsMoving;
    Vector3 NewPosition;
    float startTime;
    float journeyLength;
    [SerializeField] float camspeed;
    

    void Start ()
    {
        Cam.localPosition = Offset;
        transform.position = target.position;
        IsMoving = false;
    }

    void Update()
    {
        float CamDistance = Vector3.Distance(target.position, transform.position);
        if (!IsMoving)
        {
            if (CamDistance > IdealDelay)
            {
                IsMoving = true;
                startTime = Time.time;
                journeyLength = CamDistance;
            }
        }else{
            if (CamDistance < IdealStop)
            {
                IsMoving = false;
            }else{
                //Moving the camera
                float distCovered = (Time.time - startTime) * (camspeed * Time.deltaTime);
                float fracJourney = distCovered / journeyLength;
                NewPosition = Vector3.Lerp(transform.position, target.position, fracJourney);
                //NewPosition.y = target.position.y;
                //Cam.LookAt(target);
                transform.position = NewPosition;
            }
        }
        
    }
}
