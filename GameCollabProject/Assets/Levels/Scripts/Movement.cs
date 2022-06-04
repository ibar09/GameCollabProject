
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    
    private string facingSide;//hethi bech na3ref el collision men ana jiha tsir (there is probably an easier way but i couldnt find it improve if u can)
    //public bool grounded; na7it grounded w badtlha b methode bech najm nsepari el wall jumps from normal jumps ma8ir man3ml variable lel wall 
    private BoxCollider2D boxCollider;
    private void Awake()
    {
            body = GetComponent<Rigidbody2D>();
            boxCollider = GetComponent<BoxCollider2D>();
    }
   
   private void Update() {
          
           if(Input.GetKey(KeyCode.Space) && (isGrounded()||onWall()))
            {
              jump();
            }
            
            //hethi bech el player mayti7ech ki yolseg fel 7it (lel wall jumps)
            if(onWall())
            {
                    body.gravityScale=0;
            }
            else
            {
                    body.gravityScale=2;
            }
   }
   private void FixedUpdate() {
           //dhrabt f delta time bsh twali smooth l movement
           
           float direction=Input.GetAxisRaw("Horizontal");
            body.velocity=new Vector2(direction*speed*Time.deltaTime,body.velocity.y);
   }
   private void jump()
   {
        float wallJumpDirection;
        if(isGrounded())
        {
        body.velocity=Vector2.up*jumpSpeed;
        }

        else if (onWall())
        {
        if(facingSide=="right")
        {
        wallJumpDirection=1;
        }
        else
        {
          wallJumpDirection=-1;
        }
        body.velocity=new Vector2(wallJumpDirection*3,jumpSpeed);// if u can put a negative value in the first parametere when player collides on his right side and vise
        //versa delete the whole facing side and walljumpdirection bullshit.
            
        }
   }
   
private void OnCollisionEnter2D(Collision2D coll)//hethi lkol lel facing side i moved the other code to isGrounded and onWall 
   {
           if (coll.gameObject.tag == "Wall") {
            if ((this.transform.position.x - coll.collider.transform.position.x) < 0) {
                facingSide="left";
            } else if ((this.transform.position.x - coll.collider.transform.position.x) > 0) {
                facingSide="right";
            }
           }
          
   }
private bool isGrounded()//hethi fi blaset grounded 
{
RaycastHit2D raycastHit= Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size,0,Vector2.down,0.1f,groundLayer);
return raycastHit.collider !=null;
}

private bool onWall()//hethi tverifi fama wall wala 
{
RaycastHit2D raycastHit= Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size,0,new Vector2(transform.localScale.x,0),0.1f,wallLayer);
return raycastHit.collider !=null;

}
}
   