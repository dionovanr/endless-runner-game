using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveController : MonoBehaviour
{
    [Header("Movement")]
    public float moveAccel;
    public float maxSpeed;

    [Header("Jump")]
    public float jumpAccel;

    [Header("Ground Raycast")]
    public float groundRaycastDistance;
    public LayerMask groundLayerMask;

    private bool isJumping;
    private bool isOnGround;

    private Animator anim;
    private CharacterSoundController sound;
    private Rigidbody2D rb;

  
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sound = GetComponent<CharacterSoundController>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isOnGround)
            {
                isJumping = true;
                sound.PlayJump();
            }
            
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundRaycastDistance, groundLayerMask);

        if (hit)
        {
            if (!isOnGround && rb.velocity.y <= 0)
            {
                isOnGround = true;
            }
        }
        else
        {
            isOnGround = false;
        }
        anim.SetBool("isOnGround", isOnGround);
    }

    private void FixedUpdate()
    {
        Vector2 velocityVector = rb.velocity;
        if (isJumping)
        {

            velocityVector.y += jumpAccel;
            isJumping = false;
        }


        velocityVector.x = Mathf.Clamp(velocityVector.x + moveAccel * Time.deltaTime, 0.0f, maxSpeed);

        rb.velocity = velocityVector;
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, transform.position + (Vector3.down * groundRaycastDistance), Color.white);
    }
}
