using System;
using UnityEngine;

namespace KronosTech.Levels
{
    public class DataHolderLevelData : MonoBehaviour
    {
        private LevelDataScriptableObject m_data;

        public event Action<LevelDataScriptableObject> OnLevelDataChanged;

        private void Start()
        {
            m_data = SelectedLevelData.GetData();

            if(m_data == null)
            {
                Debug.LogError($"{nameof(DataHolderLevelData)}.cs: " +
                    $"Data is null");
            }
            else
            {
                OnLevelDataChanged?.Invoke(m_data);
            }
        }
    }
}