using UnityEngine;
using UnityEngine.SceneManagement;

namespace KronosTech.SceneManagement
{
    public static class SceneLoader
    {
        private static SceneRepository s_repo;

        internal static void Init(SceneRepository repository)
        {
            s_repo = repository;
        }

        public static void LoadScene(GameScene scene, LoadSceneMode mode = LoadSceneMode.Single)
        {
            if (s_repo == null)
            {
                Debug.LogError($"{nameof(SceneLoader)}.cs: " +
                    $"{nameof(SceneRepository)} not initialized!");

                return;
            }

            string path = s_repo.GetScenePath(scene);

            if (!string.IsNullOrEmpty(path))
            {
                SceneManager.LoadScene(path, mode);
            }
            else
            {
                Debug.LogError($"{nameof(SceneLoader)}.cs: " +
                    $"Couldn't find scene [{scene}] at path [{path}]");
            }
        }
    }
}