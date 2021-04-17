using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveController : MonoBehaviour
{

    [Header("Movement")]
    public float moveAccel;
    public float maxSpeed;
    Rigidbody2D rig;

    [Header("Jump")]
    public float jumpAccel;

    private bool isJumping;
    private bool isGround;




    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isGround)
            {
                isJumping = true;
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 velocityVector = rig.velocity;
        velocityVector.x = Mathf.Clamp(velocityVector.x + moveAccel * Time.deltaTime, 0.0f, maxSpeed);
        rig.velocity = velocityVector;
    }
}
