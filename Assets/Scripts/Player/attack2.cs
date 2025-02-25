using UnityEngine;

public class attack2 : MonoBehaviour
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


        if (Input.GetKeyDown(KeyCode.R) && cooldownTimer <= 0f)
        {
            anmt.SetBool("attack2", true);
            atk.SetActive(true);
            cooldownTimer = cooldownTime;
            Invoke("ResetAnimation", 0.7f);
        }
        // if (atk.activeSelf && cooldownTimer <= cooldownTime - 0.1f)
        // {
        //     atk.SetActive(false);
        // }
        
    }
    void ResetAnimation()
    {
        anmt.SetBool("attack2", false);
        atk.SetActive(false);
    }
}
