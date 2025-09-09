using UnityEngine;

namespace KronosTech.Levels
{
    public static class SelectedLevelData
    {
        private static LevelData Data;

        public static LevelData GetData()
        {
            return Data;
        }
        public static void SetData(LevelData data)
        {
            Data = data;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            var data = new LevelData(new sbyte[,]
            {
                { +0, +0, -1, +0 },
                { +1, +2, +0, +0 },
                { -1, +1, +4, +3 },
                { +0, +1, +2, +0 }
            });

            SetData(data);
        }
    }
}

