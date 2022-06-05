using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D body;
    // jumping and speed 
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;

    // layers
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    //wall detection and jump
    public Transform TopRight;
    public Transform BotttomLeft;
    private bool firstTime = true;// hedhi bsh tkhali l player yjumpi mara barka mn ala wall maghirha player can spam Space and get over the wall

    //dashing
    [SerializeField]private float dashDistance;
    private float doubleTapTimer;
    private KeyCode lastKeyCode;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    //badlt barsha fil wall jumping system
    private void Update()
    {
        //hethi partie taa dashing 
            //dashing right 
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {   //second time pressing
            if(doubleTapTimer>Time.time && lastKeyCode==KeyCode.RightArrow)
            {
                dash(1);// 1 is for the direction
            }
            //first time pressing 
            else
            {
                doubleTapTimer=Time.time+0.4f; // after u press a key 3andk el 7a9 fi 0.5 secs to verify the condition fel if li 9ablha
            }
            lastKeyCode=KeyCode.RightArrow;
        }
                //dashing left
            if(Input.GetKeyDown(KeyCode.LeftArrow))
        {   //second time pressing
            if(doubleTapTimer>Time.time && lastKeyCode==KeyCode.LeftArrow)
            {
                dash(-1);// -1 is for the direction
            }
            //first time pressing 
            else
            {
                doubleTapTimer=Time.time+0.4f; 
            }
            lastKeyCode=KeyCode.LeftArrow;


        }
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded() || onWall()))
        {
            if (onWall() && firstTime)
            {
                jump();
                firstTime = false;
            }
            else if (isGrounded())
            {
                jump();
                firstTime = true;
            }
        }
    
    }
    private void FixedUpdate()
    {
        //dhrabt f delta time bsh twali smooth l movement

        float direction = Input.GetAxisRaw("Horizontal");
        body.velocity = new Vector2(direction * speed * Time.deltaTime, body.velocity.y);
    }
    private void jump()
    {

        if (onWall())
        {
            body.velocity=new Vector2(-10,10);//im trying to make the player bounce of the wall up and in the opposite direction instead of climibing it ( didnt work u try it )
            body.gravityScale = 2;

        }
        else if (isGrounded())
        {
            body.velocity = Vector2.up * jumpSpeed;
        }
    }
    private IEnumerator WallHoldingTimer() //hehdi bsh l player yalsg just 2 seconds aal hit mayalsgsh ala toul
    {
        body.gravityScale = 0;
        yield return new WaitForSeconds(2);
        body.gravityScale = 2;

    }

    private void OnCollisionEnter2D(Collision2D coll)//hethi lkol lel facing side i moved the other code to isGrounded and onWall 
    {
        if (coll.gameObject.tag == "Wall")
        {
            StartCoroutine(WallHoldingTimer());
        }

    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            body.gravityScale = 2; //ki yokhrj ml wall l gravity tarja3
            firstTime = true; //ki yokhrj ml wall first time tarjaa true 
        }
    }
    // badlt el raycast bl overlap area khtr omri makhdmt b raycast xd khtr kn fama glitches w shakit li homa ml raycast donc badlt
    private bool isGrounded()
    {

        return Physics2D.OverlapArea(TopRight.position, BotttomLeft.position, groundLayer);
    }

    private bool onWall()
    {

        return Physics2D.OverlapArea(TopRight.position, BotttomLeft.position, wallLayer);

    }

    private void dash(int direction)
    {   
        
        body.gravityScale=0;
        body.velocity=new Vector2(dashDistance*direction,0);//i straight up cant understand how to add a force in a specific direction in unity if u do know tell me 
        body.gravityScale=2;
    }

}
