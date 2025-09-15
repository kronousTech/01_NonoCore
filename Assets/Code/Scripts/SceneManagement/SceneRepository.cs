using System.Collections.Generic;
using UnityEngine;

namespace KronosTech.SceneManagement
{
    [CreateAssetMenu(fileName = "SceneRepository", menuName = "KronosTech/SceneManagement/SceneRepository")]
    public class SceneRepository : ScriptableObject
    {
        [SerializeField] private List<SceneEntry> m_scenes = new();
        
        private readonly Dictionary<GameScene, string> m_lookup = new();

        private void OnEnable()
        {
            m_lookup.Clear();
            
            foreach (var sceneEntry in m_scenes)
            {
                if (!m_lookup.ContainsKey(sceneEntry.Key))
                {
                    m_lookup.Add(sceneEntry.Key, sceneEntry.Path);
                }
            }
        }

        public string GetScenePath(GameScene scene)
        {
            if (m_lookup.TryGetValue(scene, out string path))
            {
                return path;
            }
            else
            {
                Debug.LogError($"{nameof(SceneRepository)}.cs: " +
                    $"Scene {scene} not found in repository!");

                return string.Empty;
            }
        }
    }
}