using UnityEngine;
using UnityEngine.SceneManagement;

public class FragmentCollector : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Fragment"))
    {
        Destroy(other.gameObject);
        GameManager.Instance.CollectFragment();
    }
}

}
