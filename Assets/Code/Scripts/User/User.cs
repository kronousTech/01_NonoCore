using KronosTech.CloudSave;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace KronosTech
{
    public static class User
    {
        private static int Coins = 0;
        // Each space in the array contains the progress for a specific difficulty
        private static int[] Progress = new int[4] {0,0,0,0};

        public static readonly UnityEvent<bool> OnAllDataLoaded = new();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            //Authentication.OnLogin.AddListener(LoadUserCloudData);
        }

        private static void LoadUserCloudData()
        {
            CloudSaveRequests.RetrieveAllData((result, data) =>
            {
                if (result)
                {
                    HelperCloudSave.SetValue(data, nameof(Progress), ref Progress);
                }

                OnAllDataLoaded?.Invoke(result);
            });
        }

        public static void SaveProgress(Action<bool> callback)
        {
            CloudSaveRequests.SaveSpecificData(nameof(Progress), Progress, (result) =>
            {
                if (result)
                {
                    Debug.Log("<color=#12a182>User.cs: Progress saved successfully</color>");
                }
                else
                {
                    Debug.LogError("User.cs: Error saving Progress");
                }

                callback?.Invoke(result);
            });
        }
    }
}