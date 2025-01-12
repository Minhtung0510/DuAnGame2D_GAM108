using System.Collections;
using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class NPCDialogue : MonoBehaviour
{
    public GameObject NPCPanel; // Panel hiển thị hội thoại
    public TextMeshProUGUI NPCName; // Tên của NPC
    public TextMeshProUGUI NPCContent; // Nội dung hội thoại
    public Button skipButton; // Nút "Bỏ qua"

    public string[] names; // Danh sách tên (ai đang nói)
    public string[] content; // Nội dung hội thoại

    private Coroutine coroutine;

    void Start()
    {
       
        NPCPanel.SetActive(false);
        NPCName.text = "";
        NPCContent.text = "";

     
        if (skipButton != null)
        {
            skipButton.onClick.AddListener(SkipDialogue);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            NPCPanel.SetActive(true); 
            coroutine = StartCoroutine(ReadContent()); 
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            NPCPanel.SetActive(false); 
            if (coroutine != null)
            {
                StopCoroutine(coroutine); 
            }
        }
    }

    private IEnumerator ReadContent()
    {
       
        for (int i = 0; i < content.Length; i++)
        {
            NPCContent.text = ""; 
            NPCName.text = names.Length > i ? names[i] : "Unknown"; 

            
            foreach (var item in content[i])
            {
                NPCContent.text += item;
                yield return new WaitForSeconds(0.05f); 
            }
            yield return new WaitForSeconds(1f); 
        }

       
        NPCPanel.SetActive(false);

        LoadScence();
    }

    public void SkipDialogue()
    {
        
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        NPCPanel.SetActive(false);

        LoadScence();


    }
    private void LoadScence()
    {
        SceneManager.LoadScene(2);
    }

  
}
