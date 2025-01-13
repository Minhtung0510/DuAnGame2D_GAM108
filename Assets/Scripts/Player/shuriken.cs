using UnityEngine;

public class shuriken : MonoBehaviour
{
    [Header("Object Settings")]
    public GameObject Prefab; 
    public Transform spawnPoint;    

    [Header("Motion Settings")]
    public float cooldownTime = 0.1f; 
    public float throwForce = 10f;    

    private float cooldownTimer = 0f; 
    private float direction;    

    public Animator anmt;

    // Update is called once per frame
    void Update()
    {
        cooldownTimer -= Time.deltaTime;


        direction = Input.GetAxisRaw("Horizontal");


        if (Input.GetKeyDown(KeyCode.G) && cooldownTimer <= 0f)
        {
            anmt.SetBool("shuriken", true);
            Throw();
        }
        else
        {
            anmt.SetBool("shuriken", false);
        }
        
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
     Destroy(liemInstance, 2f);
}
}
