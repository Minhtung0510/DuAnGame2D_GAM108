using UnityEngine;

public class attack1 : MonoBehaviour
{ 
    [Header("Motion Settings")]
    public float cooldownTime = 0.1f; 
    private float cooldownTimer = 0f;
    [SerializeField] private GameObject atk;

    public Animator anmt;     

    public AudioSource audioSource; // Thêm AudioSource vào GameObject
    public AudioClip shootSound;

    // Update is called once per frame
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        atk.SetActive(false);
        
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;


        if (Input.GetKeyDown(KeyCode.E) && cooldownTimer <= 0f)
        {
            anmt.SetBool("attack1", true);
            atk.SetActive(true);
            Shoot();
            cooldownTimer = cooldownTime;
            Invoke("ResetAnimation", 0.28f);
        }
        
        
    }

    void Shoot()
    {
        if (shootSound != null)
        {
            audioSource.PlayOneShot(shootSound); // Phát âm thanh một lần
        }
    }

    void ResetAnimation()
    {
        anmt.SetBool("attack1", false);
        atk.SetActive(false);
    }
}
