using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Cinemachine;

public class player_movement : MonoBehaviour
{
    public Transform feetTransform;
    public float health = 100f;

    // Start is called before the first frame update
    public Animator animator;
    float velocityX = 0.0f;
    float velocityZ = 0.0f;
    public float acceleration = 2.0f;
    public float deceleration = 2.0f;
    public float maxWalk = 0.5f;
    public float maxRun = 2.0f;


    //increase the performance
    int velozityZHash;
    int velozityXHash;

    // camera
    public CinemachineVirtualCamera defaultCamera;
    public CinemachineVirtualCamera alternateCamera;

    void Start()
    {
        //increase the performance
        velozityZHash = Animator.StringToHash("z-axis");
        velozityXHash = Animator.StringToHash("x-axis");
    }

    // Update is called once per frame
    void Update()
    {
        bool forward_pressed = Input.GetKey(KeyCode.W);
        bool backward_pressed = Input.GetKey(KeyCode.S);
        bool right_pressed = Input.GetKey(KeyCode.D);
        bool left_pressed = Input.GetKey(KeyCode.A);
        bool run_pressed = Input.GetKey(KeyCode.LeftShift);
        bool jump = Input.GetKey(KeyCode.Space);


        //set current maxVelocity...... if run_pressed currentmaxVelocity set to maxRun and viseversa
        float currentmaxVelocity = run_pressed ? maxRun : maxWalk;

        //Playing sounds
        if(forward_pressed | backward_pressed | right_pressed | left_pressed)
        {
            FindObjectOfType<AudioManager>().PlayWalk("Walking", forward_pressed | backward_pressed | right_pressed | left_pressed, run_pressed);
        }
        else
        {
            FindObjectOfType<AudioManager>().PlayWalk("Walking", forward_pressed | backward_pressed | right_pressed | left_pressed, run_pressed);
        }
        //accelaration
        if (forward_pressed && velocityZ < currentmaxVelocity)
        {
            velocityZ += Time.deltaTime * acceleration;
        }
        if (backward_pressed && velocityZ > -currentmaxVelocity)
        {
            velocityZ -= Time.deltaTime * acceleration;
        }
        if (right_pressed && velocityX < currentmaxVelocity)
        {
            velocityX += Time.deltaTime * acceleration;
        }
        if (left_pressed && velocityX > -currentmaxVelocity)
        {
            velocityX -= Time.deltaTime * acceleration;
        }

        //decelaration
        if(!forward_pressed && velocityZ > 0.0f)
        {
            velocityZ -= Time.deltaTime * deceleration; 
        }
        if (!backward_pressed && velocityZ < 0.0f)
        {
            velocityZ += Time.deltaTime * deceleration;
        }
        if(!left_pressed && velocityX < 0.0f)
        {
            velocityX += Time.deltaTime * deceleration;
        }
        if(!right_pressed && velocityX > 0.0f)
        {
            velocityX -= Time.deltaTime * deceleration;
        }


        //reset
        if(!right_pressed && !left_pressed && velocityX != 0.0f && (velocityX > -0.5f && velocityX < 0.5f))
        {
            velocityX = 0.0f;
        }
        if(!forward_pressed && !backward_pressed && velocityZ != 0.0f && (velocityZ > -0.5f && velocityZ < 0.5f))
        {
            velocityZ = 0.0f;
        }


        //lock forward
        if(forward_pressed && run_pressed && velocityZ > currentmaxVelocity)
        {
            velocityZ = currentmaxVelocity;
        }
        //decelarate to the forward maximum walk
        else if(forward_pressed && velocityZ > currentmaxVelocity)
        {
            velocityZ -= Time.deltaTime * deceleration;
            //round to the currentmxVelocity within the offset
            if(velocityZ > currentmaxVelocity && velocityZ < (currentmaxVelocity + 0.05f))
            {
                velocityZ = currentmaxVelocity;
            }
        }
        //round to the currentmaxVelocity within the offset
        else if(forward_pressed && velocityZ < currentmaxVelocity && velocityZ > (currentmaxVelocity - 0.05f))
        {
            velocityZ = currentmaxVelocity;
        }


        //lock_backward
        if (backward_pressed && run_pressed && velocityZ < -currentmaxVelocity)
        {
            velocityZ = -currentmaxVelocity;
        }
        //decelarate to the backward maximum walk
        else if (backward_pressed && velocityZ < -currentmaxVelocity)
        {
            velocityZ += Time.deltaTime * deceleration;
            //round to the currentmxVelocity within the offset
            if (velocityZ < -currentmaxVelocity && velocityZ > (-currentmaxVelocity - 0.05f))
            {
                velocityZ = -currentmaxVelocity;
            }
        }
        //round to the currentmaxVelocity within the offset
        else if (backward_pressed && velocityZ > -currentmaxVelocity && velocityZ < (currentmaxVelocity + 0.05f))
        {
            velocityZ = -currentmaxVelocity;
        }


        //lock right
        if (right_pressed && run_pressed && velocityX > currentmaxVelocity)
        {
            velocityX = currentmaxVelocity;
        }
        //decelarate to the right maximum walk
        else if (right_pressed && velocityX > currentmaxVelocity)
        {
            velocityX -= Time.deltaTime * deceleration;
            //round to the currentmxVelocity within the offset
            if (velocityX > currentmaxVelocity && velocityX < (currentmaxVelocity + 0.05f))
            {
                velocityX = currentmaxVelocity;
            }
        }
        //round to the currentmaxVelocity within the offset
        else if (right_pressed && velocityX < currentmaxVelocity && velocityX > (currentmaxVelocity - 0.05f))
        {
            velocityX = currentmaxVelocity;
        }


        //left_backward
        if (left_pressed && run_pressed && velocityX < -currentmaxVelocity)
        {
            velocityX = -currentmaxVelocity;
        }
        //decelarate to the left maximum walk
        else if (left_pressed && velocityX < -currentmaxVelocity)
        {
            velocityX += Time.deltaTime * deceleration;
            //round to the currentmxVelocity within the offset
            if (velocityX < -currentmaxVelocity && velocityX > (-currentmaxVelocity - 0.05f))
            {
                velocityX = -currentmaxVelocity;
            }
        }
        //round to the currentmaxVelocity within the offset
        else if (left_pressed && velocityX > -currentmaxVelocity && velocityX < (currentmaxVelocity + 0.05f))
        {
            velocityX = -currentmaxVelocity;
        }

        // if player Jumps
        if (jump)
        {
            animator.SetBool("Jump", true);
            FindObjectOfType<AudioManager>().PlayJump("Jump", true);
        }
        else
        {
            FindObjectOfType<AudioManager>().PlayJump("Jump", false);
            animator.SetBool("Jump", false);
        }

        animator.SetFloat(velozityZHash, velocityZ);
        animator.SetFloat(velozityXHash, velocityX);

        //if rightclick pressed zoom in
            if (Input.GetMouseButtonDown(1))
            {
                defaultCamera.Priority = 0;
                alternateCamera.Priority = 100;
            }
            if (Input.GetMouseButtonUp(1))
            {
                defaultCamera.Priority = 100;
                alternateCamera.Priority = 0;
            }

            //checking the health
            if(health <= 0)
        {
            FindObjectOfType<GameManager>().EndGame();
        }
    }
}
