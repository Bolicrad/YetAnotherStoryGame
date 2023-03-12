using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UIControllers
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Image blackPanel;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private float transitionTime;
        private bool _isTransiting;
        private float _timer;
        private AsyncOperation _asyncOperation;
        
        public void StartGame()
        {
            _asyncOperation = SceneManager.LoadSceneAsync("CyberCity");
            _asyncOperation.allowSceneActivation = false;
            _isTransiting = true;
        }
        public void QuitGame()
        {
            Debug.Log("quit");
            Application.Quit();
        }
   
        private void Start()
        {
            _timer = 0;
            _isTransiting = false;
        }

        private void Update()
        {
            if (!_isTransiting) return;
            _timer += Time.deltaTime;
            if (_timer >= transitionTime)
            {
                _asyncOperation.allowSceneActivation = true;
                _isTransiting = false;
            }
            blackPanel.color = new Color(0, 0, 0, _timer / transitionTime);
            audioSource.volume = 1 - _timer / transitionTime;
        }
    }
}
