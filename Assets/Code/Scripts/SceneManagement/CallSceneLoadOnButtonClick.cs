using UnityEngine;
using UnityEngine.UI;

namespace KronosTech.SceneManagement
{
    [RequireComponent(typeof(Button))]
    public class CallSceneLoadOnButtonClick : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private GameScene m_sceneToLoad;

        private Button m_button;

        private void OnEnable()
        {
            m_button.onClick.AddListener(OnClickCallbackLoadScene);
        }
        private void OnDisable()
        {
            m_button.onClick.RemoveListener(OnClickCallbackLoadScene);
        }
        private void Awake()
        {
            m_button = GetComponent<Button>();
        }

        private void OnClickCallbackLoadScene()
        {
            SceneLoader.LoadScene(m_sceneToLoad);
        }
    }
}
