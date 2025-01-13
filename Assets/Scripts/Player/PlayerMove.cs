using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float direction;
    [SerializeField] private float tocDo;
    [SerializeField] private float upJump;
    [SerializeField] private bool flip = true;

    public bool canJump;
    public Animator anmt;

    [SerializeField] private float maxHealth = 1000f;
    private float currentHealth;
    [SerializeField] private Image healthBarFill;
  

    // Start is called before the first frame update
    void Start()
    {
        
        currentHealth = maxHealth;
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Die")
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage; 
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); 

      
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = currentHealth / maxHealth;
        }

       
        if (currentHealth <= 0)
        {
            Debug.Log("Player đã chết!");
            
        }
    }
    

}