
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    public bool grounded =true;
    private void Awake()
    {
            body = GetComponent<Rigidbody2D>();
    }
   
   private void Update() {
          
           if(Input.GetKey(KeyCode.Space) && grounded)
            {
              jump();
            }
   }
   private void FixedUpdate() {
           //dhrabt f delta time bsh twali smooth l movement
           
           float direction=Input.GetAxisRaw("Horizontal");
            body.velocity=new Vector2(direction*speed*Time.deltaTime,body.velocity.y);
   }
   private void jump()
   {
        body.velocity=Vector2.up*jumpSpeed;
        grounded=false;
   }
   
private void OnCollisionEnter2D(Collision2D collision)
   {
           if(collision.gameObject.tag=="Ground")
           {
                   grounded=true;
           }
   }
}
