using UnityEngine;

public class attack2 : MonoBehaviour
{ 
    [Header("Motion Settings")]
    public float cooldownTime = 0.1f; 
    private float cooldownTimer = 0f;
    [SerializeField] private GameObject atk;

    public Animator anmt;     

    public AudioSource audioSource; // Thêm AudioSource vào GameObject
    public AudioClip shootSound; // Kéo file âm thanh vào đây

    // Update is called once per frame
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        atk.SetActive(false);
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;


        if (Input.GetKeyDown(KeyCode.R) && cooldownTimer <= 0f)
        {
            Shoot();
            anmt.SetBool("attack2", true);
            atk.SetActive(true);
            
            cooldownTimer = cooldownTime;
            Invoke("ResetAnimation", 0.7f);
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
        anmt.SetBool("attack2", false);
        atk.SetActive(false);
    }
}
