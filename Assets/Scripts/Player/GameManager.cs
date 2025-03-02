using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int fragmentCount = 0;
    public string bossSceneName = "BossScene";
    public GameObject _player;

    void Start()
    {

    }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CollectFragment()
    {
        fragmentCount++;

        if (fragmentCount >= 3)
        {
            SceneManager.LoadScene(bossSceneName);
             _player.SetActive(false);
        }
    }
}
