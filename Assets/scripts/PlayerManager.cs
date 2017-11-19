using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

    [HideInInspector] public bool facingRight = true;
    [HideInInspector]
    public GameObject mainCamera;
    public bool jump = false;
    public bool bounce = false;
    public bool button1 = false;
    public bool button2 = false;
    public GameObject move1;
    public GameObject move2;
    public GameObject puzzleStartBlock;
    public Text txt;
    Animator anim;
    private float height;
    private float startX;
    private float forward;
	public Rigidbody2D player;
    public Transform groundCheck;
	[SerializeField]
	private float moveSpeed = 10;
    
	[SerializeField]
	private float maxSpeed = 10;

    private bool isGrounded = false;
	// Use this for initialization
	void Awake () {
		player = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        height = player.transform.position.y;
        startX = player.transform.position.x;
        mainCamera = GameObject.Find("Main Camera");
        move1 = GameObject.Find("Cube (43)");
        move2 = GameObject.Find("Cube (45)");
        puzzleStartBlock = GameObject.Find("Cube (47)");
    }
	
	// Update is called once per frame
	void Update () {
        isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jump = true;
        }
        if (bounce)
        {
            player.velocity =  new Vector2(player.velocity.x,0); player.AddForce(new Vector2(0, 25), ForceMode2D.Impulse);
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
        mainCamera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void FixedUpdate() {

		//get horizontal input
		float xInput = Input.GetAxis ("Horizontal");

        //if pressing right
        if (xInput > 0)
        {
            player.AddForce(Vector2.right * moveSpeed);
            forward = player.transform.position.x;
            anim.SetBool("Running", true);
        }
			
		//if pressing left
		if (xInput < 0) {
			player.AddForce (Vector2.left * moveSpeed);
            forward = player.transform.position.x;
            anim.SetBool("Running", true);
        }

        if (xInput == 0)
        {
            anim.SetBool("Running", false);
        }
       
		//set max horizontal speed
		if (Mathf.Abs (player.velocity.x) > 10) {
			player.velocity = new Vector2 (Mathf.Sign (player.velocity.x) * maxSpeed, player.velocity.y);
		}

		//if pressing jump

        if (xInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (xInput < 0 && facingRight)
        {
            Flip();
        }

        if (jump)
        {
            player.AddForce(new Vector2(0, 15), ForceMode2D.Impulse);
            jump = false;
        }

        if (player.transform.position.y < (height - 30) && player.transform.position.x > (startX+20))
        {
            player.transform.position = new Vector2((forward-20), height + 20);
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

        if (platform.transform.tag == "MovingPlatform"){
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
