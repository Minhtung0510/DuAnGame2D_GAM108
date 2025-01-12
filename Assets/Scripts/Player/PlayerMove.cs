using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float direction;
    [SerializeField] private float tocDo;
    [SerializeField] private float upJump;
    [SerializeField] private bool flip = true;

    public bool canJump;
    public Animator anmt;

    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        // animation
        anmt.SetFloat("isRun",Mathf.Abs(direction));
        Jump();

        
    }
    private void Move()
    {
         // di chuyen nhan vat
        direction=Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(tocDo*direction,rb.linearVelocity.y);
        Flip();
    }
    private void Flip()
    {
        if(flip == true && direction == -1)
        {
            transform.localScale = new Vector3(-1,1,1);
            flip=false;
        }
        if(flip == false && direction == 1)
        {
            transform.localScale = new Vector3(1,1,1);
            flip=true;

        }
    }
    private void Jump()
    {
        
        if(Input.GetButtonDown("Jump") && canJump)
        {
            rb.AddForce(Vector2.up*upJump, ForceMode2D.Impulse);
            anmt.SetBool("isJump", true);
        }
        else anmt.SetBool("isJump", false);
    }
    private void OnTriggerEnter2D(Collider2D map)
    {
        if(map.gameObject.tag == "Ground")
        {
            canJump=true;
        }
        
    }
    private void OnTriggerExit2D(Collider2D map)
    {
        if(map.gameObject.tag == "Ground")
        {
            canJump=false;
        }
    }
}