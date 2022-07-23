using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private InputController input = null;
    [SerializeField, Range(0f, 100f)] private float maxSpeed = 4f;
    [SerializeField, Range(0f, 100f)] private float maxAcceleration = 35f;
    [SerializeField, Range(0f, 100f)] private float maxAirAcceleration = 20f;

    private Vector2 direction;
    private Vector2 desiredVelocity;
    private Vector2 velocity;
    private Rigidbody2D body;
    private Ground ground;

    private float maxSpeedChange;
    private float acceleration;
    private bool onGround;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();
        
    }

    // Update is called once per frame
    void Update()
    {
        direction.x = input.RetrieveMoveInput();
        desiredVelocity = new Vector2(direction.x, 0f) * Mathf.Max(maxSpeed - ground.GetFriction(), 0f);

    }

    private void FixedUpdate()
    {
        onGround = ground.GetOnGround();
        velocity = body.velocity;

        acceleration = onGround ? maxAcceleration : maxAcceleration;
        maxSpeedChange = acceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);

        body.velocity = velocity;

        if (body.velocity.x < 0)
        {
            gameObject.transform.eulerAngles = new Vector3 (gameObject.transform.rotation.x, 180f, gameObject.transform.rotation.z);
        }
        if (velocity.x > 0)
        {
            gameObject.transform.eulerAngles = new Vector3(gameObject.transform.rotation.x, 0f, gameObject.transform.rotation.z);
        }

        if (onGround && body.velocity.x != 0)
        {
            
            GetComponent<Animator>().SetBool("isRunning", true);
        }
        else if (onGround && body.velocity.x == 0)
        {
            GetComponent<Animator>().SetBool("isRunning", false);
        }

        if (!onGround && body.velocity.y > 0 && !GetComponent<Ghost>().ReturnGhost())
        {
            GetComponent<Animator>().SetBool("isJumping", true);
            GetComponent<Animator>().SetBool("isRunning", false);
            GetComponent<Animator>().SetBool("isFalling", false);
        }
        else if (!onGround && body.velocity.y < 0 && !GetComponent<Ghost>().ReturnGhost())
        {
            GetComponent<Animator>().SetBool("isFalling", true);
            GetComponent<Animator>().SetBool("isJumping", false);
            GetComponent<Animator>().SetBool("isRunning", false);
        }

        if (onGround)
        {
            GetComponent<Animator>().SetBool("isFalling", false);
            GetComponent<Animator>().SetBool("isJumping", false);
        }

        if (GetComponent<Ghost>().ReturnGhost())
        {
            GetComponent<Animator>().SetBool("isFalling", false);
            GetComponent<Animator>().SetBool("isJumping", false);
            GetComponent<Animator>().SetBool("isRunning", false);
        }
    }
}
