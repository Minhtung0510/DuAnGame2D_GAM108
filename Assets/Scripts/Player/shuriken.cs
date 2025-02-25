using UnityEngine;
using System.Collections;

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

    void Start()
    {
        if (anmt == null)
            anmt = GetComponent<Animator>(); // Tự động tìm Animator nếu quên gán
    }

    void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        direction = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.G) && cooldownTimer <= 0f)
        {
            anmt.SetBool("shuriken", true);
            StartCoroutine(ThrowWithDelay(0.2f)); // Gọi hàm ném sau 0.3 giây
        }
    }

    IEnumerator ThrowWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Throw();
        Invoke("ResetAnimation", 0.25f); // Tắt animation sau 0.2 giây
    }

    void Throw()
    {
        cooldownTimer = cooldownTime;

        float characterDirection = transform.localScale.x;
        Quaternion rotation = (characterDirection > 0) ? Quaternion.identity : Quaternion.Euler(0, 180, 0);

        GameObject shurikenInstance = Instantiate(Prefab, spawnPoint.position, rotation);

        Rigidbody2D rb = shurikenInstance.GetComponent<Rigidbody2D>();
        Vector2 force = characterDirection * Vector2.right * throwForce;
        rb.AddForce(force, ForceMode2D.Impulse);

        Destroy(shurikenInstance, 2f);
    }

    void ResetAnimation()
    {
        anmt.SetBool("shuriken", false);
    }
}
