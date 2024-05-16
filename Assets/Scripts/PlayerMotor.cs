using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
  private CharacterController controller;
  public Transform groundCheck;
  public float groundDistance = 0.4f;
  private bool isGrounded;
  private bool jumping;
  public LayerMask groundMask;

  private Vector3 playerVelocity;
  private Vector3 moveDirection = Vector3.zero;
  private Vector3 lastMoveDirection = Vector3.zero;

  public float speed = 5f;
  public float gravity = 20f;
  public float jumpHeight = 8f;
  public float airControlFactor = 0.5f;

  void Start()
  {
    controller = GetComponent<CharacterController>();
  }

  public void ProcessMove(Vector2 input)
  {
    isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

    if (isGrounded)
    {
      // Handle grounded movement
      moveDirection = new Vector3(input.x, 0, input.y);
      moveDirection = transform.TransformDirection(moveDirection) * speed;

      if (jumping)
      {
        moveDirection.y = jumpHeight;
      }

      // Update the last move direction for inertia
      lastMoveDirection = new Vector3(moveDirection.x, 0, moveDirection.z);
    }
    else
    {
      // Handle air movement
      Vector3 airMoveDirection = new Vector3(input.x, 0, input.y);
      airMoveDirection = transform.TransformDirection(airMoveDirection) * speed * airControlFactor;

      // Combine input movement and inertia
      moveDirection.x = airMoveDirection.x + lastMoveDirection.x / 2;
      moveDirection.z = airMoveDirection.z + lastMoveDirection.z / 2;
      // Gravity
      moveDirection.y -= gravity * Time.deltaTime;
    }

    jumping = false;
    // Move the character controller
    controller.Move(moveDirection * Time.deltaTime);
  }

  public void Jump()
  {
    jumping = true;
  }
}
