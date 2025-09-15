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
            var data = new LevelData(2, new sbyte[,]
{
    { +0, -1, +0, +0, +1, +2, +3, +3, -1, +1 },
    { +0, +4, +1, +1, -1, +3, +3, +1, -1, +2 },
    { +3, +2, +2, +2, +4, +4, +1, +0, +1, +3 },
    { +3, +2, +4, -1, +1, +2, +2, +0, +3, +3 },
    { +4, +0, +4, +1, +3, +0, +1, -1, +4, -1 },
    { +1, +0, +0, +3, +0, +0, -1, +1, +3, +3 },
    { +0, +0, +3, +3, +1, +3, +2, +3, +2, +0 },
    { +2, +3, -1, +0, +0, +4, -1, +3, +2, +3 },
    { +0, -1, +1, +3, +0, +3, +4, +1, +0, +2 },
    { +1, +4, +0, -1, +1, +2, +0, +3, +0, +3 },
});

            SetData(data);
        }
    }
}

