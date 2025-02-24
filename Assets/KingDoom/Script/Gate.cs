using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gate : MonoBehaviour
{
    [SerializeField] private GameObject _loandingCanvas;
    [SerializeField] private Slider _slider;
    [SerializeField] TextMeshProUGUI _progressText;
    private float _progress = 0;
    public int scene;
    private void Start()
    {
        _loandingCanvas.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // hiển thị màng hình loanding
            _loandingCanvas.SetActive(true);
            _slider.value = _progress;
            _progressText.text = _progress + "%";
            StartCoroutine(LoandScens());
        }

        IEnumerator LoandScens()
        {
            while (_progress < 100)
            {
                _progress += 1;
                _slider.value = _progress;
                _progressText.text = _progress + "%";
                yield return new WaitForSeconds(0.01f);
            }
            // chuyển màng chơi
            SceneManager.LoadScene(scene);

        }
    }

}
