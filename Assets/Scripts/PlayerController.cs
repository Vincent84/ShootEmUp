using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 3f;                    // The speed that the player will move at (used without root motion)
    public float smoothRotation = 15f;          // The smoothness of the player rotation

    Vector3 movement;                           // The vector to store the direction of the player's movement.
    Animator playerAnimator;                    // Reference to the animator component.
    Rigidbody playerRigidbody;                  // Reference to the player's rigidbody.

    bool isMoving;
    bool isShooting;

    // Start is called before the first frame update
    void Awake()
    {

        playerAnimator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

        isMoving = Input.GetButton("Horizontal") || Input.GetButton("Vertical");
        isShooting = Input.GetButton("Fire1");

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Move the player in the scene (decomment if no root motion is present)
        //Move(horizontal, vertical);

        // Rotate the player
        Rotating(horizontal, vertical);

        //Animate the player
        Animating();

    }

    void Move(float horizontal, float vertical)
    {

        // Set the movement vector based on the axis input.
        movement.Set(horizontal, 0f, vertical);

        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * speed * Time.deltaTime;

        // Move the player to it's current position plus the movement.
        playerRigidbody.MovePosition(transform.position + movement);

    }

    void Rotating(float horizontal, float vertical)
    {

        if(isMoving)
        {
            //work out angle using atan 2
            float angle = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg;

            //transform.rotation = Quaternion.Euler(0f, angle, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, angle, 0f), smoothRotation * Time.deltaTime);
        }
        

    }

    void Animating()
    {

        // Tell the animator whether or not the player is walking.
        playerAnimator.SetBool("isWalking", isMoving);
        playerAnimator.SetBool("isShooting", isShooting);

    }
}
