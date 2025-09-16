using KronosTech.Services;
using System;
using Unity.Services.Core;
using Unity.Services.Core.Environments;

public class UnityServicesInitializeService : ServiceBase
{
    private const string k_developmentEnvironmentName = "development";
    private const string k_productionEnvironmentName = "production";

    private async void InitializeUnityServices(Action callback)
    {
        var options = new InitializationOptions()
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            .SetEnvironmentName(k_developmentEnvironmentName);
#else
            .SetEnvironmentName(k_productionEnvironmentName);
#endif

        await UnityServices.InitializeAsync(options);

        callback?.Invoke();
    }

    #region ServiceBase
    protected override void InitializeBehavior(Action callback)
    {
        InitializeUnityServices(callback);
    }
    #endregion
}
