using UnityEngine;

public class Defend : MonoBehaviour
{
    [Header("Motion Settings")]
    public float cooldownTime = 0.1f; 

    private float cooldownTimer = 0f;
    [SerializeField] private GameObject atk;

    public Animator anmt;

    void Start()
    {
        atk.SetActive(false);
    }
    void Update()
    {
        cooldownTimer -= Time.deltaTime;


        if (Input.GetKeyDown(KeyCode.F) && cooldownTimer <= 0f)
        {
            anmt.SetBool("isDefend", true);
            atk.SetActive(true);
        }
        else
        {
            anmt.SetBool("isDefend", false);
            atk.SetActive(false);
        }
        
    }
}
