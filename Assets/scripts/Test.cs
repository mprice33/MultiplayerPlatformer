using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Test : MonoBehaviour {

    [HideInInspector]
    public bool facingRight = true;
    [HideInInspector]
    public bool jump = false;
    public bool bounce = false;
    public bool button1 = false;
    public bool button2 = false;
    public GameObject move1;
    public GameObject move2;
    public GameObject puzzleStartBlock;
    Animator anim;
    private float height;
    private float startX;
    private float forward;
    public Rigidbody2D player;
    public SpriteRenderer sprite;
    public Transform groundCheck;
    [SerializeField]
    private float moveSpeed = 10;

    [SerializeField]
    private float maxSpeed = 10;

    private bool isGrounded = false;
    // Use this for initialization
    void Awake()
    {
        player = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponentsInChildren<SpriteRenderer>()[0];
        height = player.transform.position.y;
        startX = player.transform.position.x;

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (CrossPlatformInputManager.GetButtonDown("Jump") && isGrounded)
        {
            jump = true;
        }
        if (bounce)
        {
            player.velocity = new Vector2(player.velocity.x, 0); player.AddForce(new Vector2(0, 25), ForceMode2D.Impulse);
            bounce = false;
        }
        if (button1)
        {
            if (move1.transform.position.y > (move1.transform.position.y - 20))
            {
                move1.transform.Translate(Vector3.down * 5 * Time.deltaTime);
            }
        }
        if (button2)
        {
            if (move2.transform.position.y > (move2.transform.position.y - 20))
            {
                move2.transform.Translate(Vector3.down * 5 * Time.deltaTime);
            }
        }
    }

    //void Flip()
    //{
    //    facingRight = !facingRight;
    //    Vector3 theScale = transform.localScale;
    //    theScale.x *= -1;
    //    transform.localScale = theScale;
    //}

    void FixedUpdate()
    {

        //get horizontal input
        float xInput = CrossPlatformInputManager.GetAxis("Horizontal");

        //if pressing right
        if (xInput > 0)
        {
            player.AddForce(Vector2.right * moveSpeed);
            forward = player.transform.position.x;
            anim.SetBool("Running", true);
            sprite.flipX = false;

            
        }

        //if pressing left
        if (xInput < 0)
        {
            player.AddForce(Vector2.left * moveSpeed);
            forward = player.transform.position.x;
            anim.SetBool("Running", true);
            sprite.flipX = true;
            
        }

        if (xInput == 0)
        {
            anim.SetBool("Running", false);
        }

        //set max horizontal speed
        if (Mathf.Abs(player.velocity.x) > 10)
        {
            player.velocity = new Vector2(Mathf.Sign(player.velocity.x) * maxSpeed, player.velocity.y);
        }

        //if pressing jump

        if (xInput > 0 && !facingRight)
        {
            //Flip();
        }
        else if (xInput < 0 && facingRight)
        {
            //Flip();
        }

        if (jump)
        {
            player.AddForce(new Vector2(0, 15), ForceMode2D.Impulse);
            jump = false;
        }

        if (player.transform.position.y < (height - 30) && player.transform.position.x > (startX + 20))
        {
            player.transform.position = new Vector2((forward - 20), height + 20);
            player.velocity = new Vector2(0, 0);
        }
        else if (player.transform.position.y < (height - 30) && player.transform.position.x <= (startX + 20))
        {
            player.transform.position = new Vector2(startX, height);
            player.velocity = new Vector2(0, 0);
        }

        if (player.transform.position.x >= puzzleStartBlock.transform.position.x)
        {

        }

    }
    void OnCollisionEnter2D(Collision2D platform)
    {

        if (platform.transform.tag == "MovingPlatform")
        {
            transform.parent = platform.transform;

        }
        else if (platform.transform.tag == "Bouncer")
        {
            bounce = true;
        }
        else if (platform.transform.tag == "Button")
        {
            button1 = true;
        }
        else if (platform.transform.tag == "Button2")
        {
            button2 = true;
        }
    }
    void OnCollisionExit2D(Collision2D platform)
    {
        if (platform.transform.tag == "MovingPlatform")
        {
            transform.parent = null;
            maxSpeed = 10;
            moveSpeed = 10;

        }
    }



}
