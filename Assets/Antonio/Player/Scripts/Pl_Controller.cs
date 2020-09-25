//Antonio

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Pl_Controller : MonoBehaviour
{
    //Reference
    Animator anim;
    CharacterController CC;
    [SerializeField] HealthBar HB;

    //Inputs
    [Header("Inputs")]
    //[SerializeField] is to can show the value in the Inspector to can monitor it while debugging, but it keeps the variable as private.
    [SerializeField] float h; //To move right or left
    [SerializeField] float v; //To jump or move up or down.
    [SerializeField] bool Action; //To interact with switches

    
    [Header("Sensors")]
    [SerializeField] bool IsMoving;
    [SerializeField] bool IsOnGround;
    [SerializeField] bool IsJumping;
    [SerializeField] bool IsFloating;
    [SerializeField] bool IsDead;
    [SerializeField] float TimeAfterDeath = 2.0f; //The quantity to add on the death animation.
    [SerializeField] float WaitingTimeDeath; //To store the goal time variable, so each frame will compare the time with this one when it were initialized.
    [SerializeField] bool IsActivatingSomething;
    bool ithasjustdie = false;

    [Header ("Vars Movement")] 
    [SerializeField] Vector3 NewDirection;  //This is a temporal one, but because I'm using it on many sequencial frames, I placed here for optimization issues.
    [SerializeField] float MovSpeed = 4.0f;
    [SerializeField] Vector3 DirTarget;
    [SerializeField] float RotSpeed = 4.0f;
    [SerializeField] float step;

    [Header("Vars Gravity")]
    [SerializeField] LayerMask GroundLayer;
    [SerializeField] float GroundDistance = 0.1f;
    [SerializeField] float GravityVariation = 0.2f;
    [SerializeField] float GravityFalling;
    [SerializeField] float GravityStrengh = 2.0f;
    [SerializeField] float JumpVariation = 0.2f;
    [SerializeField] float JumpHeightNormalized = 0.0f;
    [SerializeField] float JumpPower = 3.0f;
    [SerializeField] bool JumpRise = true;
    [SerializeField] float TimeBetweenJumps = 0.5f;
    [SerializeField] float TimeSinceLastJamp = -1.0f;

    public bool SwitchState;

    void Start ()
    {
        anim = GetComponent<Animator>();
        CC = GetComponent<CharacterController>();
        if (HB == null)
        {
            print("ERROR, Health Bar has not been applied correctly on the player obj");
        }
	}
	
	void Update ()
    {
        /*
         1` check up input
         2` Future Translation
         3` Future Rotation
         4` Transform Update
         5` Animations management
         6` Sounds management
        */

        if (HB.HP <= 0)
        {
            if (!ithasjustdie)
            {
                IsDead = true;
                WaitingTimeDeath = Time.time;
                ithasjustdie = true;
            }
            
        }else
        {
            IsDead = false;
        }
        if (!IsDead)
        {
            //1` check up input
            Action = Input.GetButtonDown("Jump");
            if (!Action)
            {
                h = Input.GetAxis("Horizontal");
                v = Input.GetAxis("Vertical");
            }else{
                h = 0;
                v = 0;
            }
            //if (Input.GetButton("Fire1"))
            //{
            //    SwitchState = true;
            //    Debug.Log("Fire 1 Pressed");
            //}
        }else{ //If it's actually dead
            h = 0.0f;
            v = 0.0f;
            Action = false;
            
            if (Time.time >= (WaitingTimeDeath + TimeAfterDeath))
            {
                //It has passed 2 secs
                //print("CHANGE");
                SceneManager.LoadScene("MMenu");
            }
        }
        
        Motion();
        AnimationManager();


    }

    void Motion()
    {
        NewDirection = Vector3.zero;
        //2` Future Translation
        if ((h > 0.1f)||(h < -0.1f))  //Just checking if any direction is pressed
        {
            IsMoving = true;
            if (h >= 0)
            {
                NewDirection.x = (MovSpeed * Time.deltaTime);
                DirTarget = new Vector3(0, 0, 0);
            }else{
                NewDirection.x = (MovSpeed * Time.deltaTime * (-1));
                DirTarget = new Vector3(0, 180, 0);
            }
        }else{
            IsMoving = false;
            //NewDirection.x = 0.0f;
        }

        if (Action)
        {
            DirTarget = new Vector3(0, -90, 0);
        }

        //2b, jumping and gravity.
        CheckGravity(); //This changes the NewDirection.y projection and .y

        // 3` Future Rotation
        if (Vector3.Angle(transform.forward, DirTarget) > 5) step = 0.0f;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(DirTarget), step + (RotSpeed * Time.deltaTime));

        //4` Transform Update
        CC.Move(NewDirection);
    }

    void CheckGravity()
    {
        //1st, check if it's on the ground or if it has move to a place where there is not floor.
        RaycastHit MyHit;
        if (Physics.Raycast(transform.position + (Vector3.up * (GroundDistance / 2)), -Vector3.up, out MyHit, GroundDistance, GroundLayer))
        {//If it's on the ground, get the floor normal and make the Direction Move, paralel to the floor.
            IsOnGround = true;
            NewDirection = Vector3.ProjectOnPlane(NewDirection, MyHit.normal);
        }else{
            IsOnGround = false;
            NewDirection = Vector3.ProjectOnPlane(NewDirection, Vector3.up);
        }

        float IdealY = 0.0f;

        if (!IsFloating)//Do the normal gravity physics
        {
            if (IsJumping)//It in jumping state
            {
                GravityFalling = 0;
                if (JumpRise)//The jump is growing
                {
                    JumpHeightNormalized += JumpVariation * Time.deltaTime; //Lineal jump at the moment, to change into parabolical, this function should change.
                    if (JumpHeightNormalized >= 1.0f) //Max point of the jump
                    {
                        JumpRise = false; //start going down
                    }
                }else{//The jump is decreasing
                    JumpHeightNormalized -= JumpVariation * Time.deltaTime;//Lineal fall, I think this is ok.
                    if (JumpHeightNormalized <= 0.0f)
                    {
                        IsJumping = false;
                    }
                }

                if ((IsOnGround) && (!JumpRise)) //If it has landed
                {
                    IsJumping = false;
                }//the normal is that this state won't be OnGround, so no more additions are needed in that aspect.

                IdealY = JumpPower * JumpHeightNormalized;

            }else{//Is not jumping

                if (!IsOnGround)//It's falling
                {
                    if (GravityFalling < 1) GravityFalling += GravityVariation * Time.deltaTime;
                    IdealY = GravityFalling * GravityStrengh * (-1);

                }else{ //It's on ground
                    GravityFalling = 0.0f;
                    JumpHeightNormalized = 0.0f;
                    
                    //Because he is in the floor, he can jump.
                    if ((v > 0.01f) && (Time.time >= TimeSinceLastJamp))
                    {
                        IsJumping = true;
                        JumpRise = true;
                        TimeSinceLastJamp = Time.time;
                    }

                    IdealY = 0.0f;
                }
            }

        }else{//Flying physics

            if (v > 0.1f)
            {
                IdealY = MovSpeed * Time.deltaTime;
            }else if (v < -0.1f) {
                IdealY = -MovSpeed * Time.deltaTime;
            }
        }

        NewDirection = NewDirection + (Vector3.up * IdealY);
    }

    void AnimationManager ()
    {
        //5` Animations management
        anim.SetBool("isMoving", IsMoving);
        anim.SetBool("isJumping", IsJumping);
        anim.SetBool("isFloating", IsFloating);
        anim.SetBool("isDead", IsDead);
        if (IsActivatingSomething)
        {
            anim.SetBool("isAction", Action);
        }
        
    }

    void SoundManager ()
    {
        //6` Sounds management
    }

    //void 
}


