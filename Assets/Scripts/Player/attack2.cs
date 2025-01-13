using UnityEngine;

public class attcak2 : MonoBehaviour
{ 
    [Header("Motion Settings")]
    public float cooldownTime = 0.1f; 
    public float throwForce = 10f;

    private float cooldownTimer = 0f;
    [SerializeField] private GameObject atk;

    public Animator anmt;     
    // Update is called once per frame
    void Start()
    {
        atk.SetActive(false);
    }
    void Update()
    {
        cooldownTimer -= Time.deltaTime;


        if (Input.GetKeyDown(KeyCode.R) && cooldownTimer <= 0f)
        {
            anmt.SetBool("attack2", true);
            atk.SetActive(true);
        }
        else
        {
            anmt.SetBool("attack2", false);
            atk.SetActive(false);
        }
        
    }
}
