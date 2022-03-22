using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rbody;
    private float horizInput;
    private float moveSpeed = 450.0f;
    private float jumpHeight = 3.0f;
    private float jumpTime = 0.75f;
    private float initialJumpVelocity;
    float gravity;
    private bool isGrounded = false;
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private LayerMask groundLayerMask;
    private float groundCheckRadius = 0.3f;
    private int jumpMax = 2;
    private int jumpsAvailable = 0;
    [SerializeField] private Animator anim;
    private bool isMoving;
    private bool facingRight = true;
    // Start is called before the first frame update
    void Start()
    {
        
        float timeToApex = jumpTime / 2.0f;
        gravity = (-2 * jumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = Mathf.Sqrt(jumpHeight * -2 * gravity);
    }
    void Jump()
    {
        rbody.velocity = new Vector2(rbody.velocity.x, initialJumpVelocity);
        jumpsAvailable--;
        anim.SetTrigger("Jump");
    }
    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(Vector3.up * 180);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float xVel = horizInput * moveSpeed * Time.deltaTime;
        rbody.velocity = new Vector2(xVel, rbody.velocity.y);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheckPoint.position, groundCheckRadius);
    }
    void Update()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayerMask)  && rbody.velocity.y < 0.01;
        anim.SetBool("isGrounded", isGrounded);
        if (isGrounded)
        {
           
            jumpsAvailable = jumpMax;
        }
       
        rbody.gravityScale = gravity / Physics2D.gravity.y;
        horizInput = Input.GetAxis("Horizontal");
        isMoving = (horizInput > 0.01) || (horizInput < -0.01);
        anim.SetBool("isRunning", isMoving);
        if ((!facingRight && horizInput > 0.01f) || (facingRight && horizInput < -0.01f))
        {
            Flip();
        }
        if (Input.GetButtonDown("Jump") && jumpsAvailable > 0)
        {
            Jump();
        }

    }
}
