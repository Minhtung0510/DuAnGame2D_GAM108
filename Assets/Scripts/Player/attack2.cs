using UnityEngine;

public class attcak2 : MonoBehaviour
{
    [Header("Object Settings")]
    public GameObject Prefab; 
    public Transform spawnPoint;    

    [Header("Motion Settings")]
    public float cooldownTime = 0.1f; 
    public float throwForce = 10f; 

    public Animator anmt;  

    private float cooldownTimer = 0f; 
    private float direction;         
    // Update is called once per frame
    void Update()
    {
        cooldownTimer -= Time.deltaTime;


        direction = Input.GetAxisRaw("Horizontal");


        if (Input.GetKeyDown(KeyCode.R) && cooldownTimer <= 0f)
        {
            anmt.SetBool("attack2", true);

            Throw();
        }
        else anmt.SetBool("attack2", false);
        
    }
    void Throw()
{

    cooldownTimer = cooldownTime;

    float characterDirection = transform.localScale.x;
    Quaternion rotation = (characterDirection > 0) ? Quaternion.identity : Quaternion.Euler(0, 180, 0);


    GameObject liemInstance = Instantiate(Prefab, spawnPoint.position, rotation);


    Rigidbody2D rb = liemInstance.GetComponent<Rigidbody2D>();
    Vector2 force = (characterDirection > 0) ? transform.right : -transform.right;
    rb.AddForce(force * throwForce, ForceMode2D.Impulse);
     Destroy(liemInstance, 0.2f);
}
}
