using UnityEngine;

public class Atk1 : MonoBehaviour
{ 
    [Header("Motion Settings")]
    public float cooldownTime = 0.1f; 
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


        if (cooldownTimer <= 0f)
        {
            anmt.SetBool("attack1", true);
            atk.SetActive(true);
            cooldownTimer = cooldownTime;
        }
        else 
        {
            anmt.SetBool("attack1", false);
        }
        if (atk.activeSelf && cooldownTimer <= cooldownTime - 0.1f)
        {
            atk.SetActive(false);
        }
        
    }
}
