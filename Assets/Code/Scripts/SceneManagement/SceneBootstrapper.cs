using UnityEngine;

namespace KronosTech.SceneManagement
{
    public class SceneBootstrapper : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private SceneRepository m_repository;

        private void Awake()
        {
            SceneLoader.Init(m_repository);
        }
    }
}