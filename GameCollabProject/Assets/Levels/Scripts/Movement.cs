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

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    //badlt barsha fil wall jumping system
    private void Update()
    {

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

        float direction = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(direction * speed * Time.deltaTime, 0, 0);

    }
    private void jump()
    {

        if (onWall())
        {
            Debug.Log("WALL JUMP");
            body.AddForce(new Vector2(-7, 8), ForceMode2D.Impulse);
            body.gravityScale = 2;

        }
        else if (isGrounded())
        {
            body.velocity = Vector2.up * jumpSpeed;
        }
    }
    private IEnumerator WallHoldingTimer() //hehdi bsh l player yalsg just 2 seconds aal hit mayalsgsh ala toul
    {
        body.velocity = new Vector2(body.velocity.x, 0);
        body.gravityScale = 0.1f;
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

}
