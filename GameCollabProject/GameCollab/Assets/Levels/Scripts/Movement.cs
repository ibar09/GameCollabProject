
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private float speed;
    private bool grounded =true;
    private void Awake()
    {
            body = GetComponent<Rigidbody2D>();
    }
   private void Update()
   {
            body.velocity=new Vector2(Input.GetAxis("Horizontal")*speed,body.velocity.y);

            if(Input.GetKey(KeyCode.Space) &&grounded)
            {
              jump();
            }
   }
   private void jump()
   {
        body.velocity=new Vector2(body.velocity.x,speed);
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
