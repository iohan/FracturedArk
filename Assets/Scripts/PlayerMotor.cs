using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
  private CharacterController controller;
  public Transform groundCheck;
  public float groundDistance = 0.4f;
  private bool isGrounded;
  public LayerMask groundMask;

  private Vector3 playerVelocity;
  private bool jump;

  public float speed = 12f;
  public float gravity = -9.81f;
  public float jumpHeight = 1.5f;

  // Start is called before the first frame update
  void Start()
  {
    controller = GetComponent<CharacterController>();
  }

  // Update is called once per frame
  /*void Update()
  {
    isGrounded = controller.isGrounded;
    Vector3 velocity = controller.velocity + (Physics.gravity * Time.deltaTime);
    if (Physics.SphereCast(transform.position, 0.5f, Vector3.down, out RaycastHit hit, 0.6f))
    {
      if (jump)
      {
        velocity.y += 10;
        jump = false;
      }
      else
      {
        velocity += ((transform.right * Input.GetAxis("Horizontal")) + (transform.forward * Input.GetAxis("Vertical"))).normalized * 150 * Time.deltaTime;
        velocity -= velocity * 14 * Time.deltaTime; // ground friction
        velocity.y = -6; // helps to keep the character pinned to the ground when going down slopes
      }
    }
    playerVelocity = velocity;
  }*/

  public void ProcessMove(Vector2 input)
  {
    //isGrounded = controller.isGrounded;
    isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

    Vector3 moveDirection = Vector3.zero;
    moveDirection.x = input.x;
    moveDirection.z = input.y;

    Vector3 move = (transform.right * moveDirection.x) + (transform.forward * moveDirection.z);
    controller.Move(move * speed * Time.deltaTime);

    // Gravity
    playerVelocity.y += gravity * Time.deltaTime;
    if (isGrounded && playerVelocity.y < 0)
      playerVelocity.y = -2f;
    controller.Move(playerVelocity * Time.deltaTime);
  }

  public void Jump()
  {
    //playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
    //playerVelocity.y += 15;
    jump = true;

  }
}
