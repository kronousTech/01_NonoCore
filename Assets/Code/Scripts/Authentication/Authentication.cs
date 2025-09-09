using System;
using System.Collections;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;
using UnityEngine.Events;

public static class Authentication
{
    public static readonly UnityEvent OnLogin = new();
    public static readonly UnityEvent OnNameChange = new();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Initialize()
    {
        TryLogin();
    }

    private static async void TryLogin()
    {
        var options = new InitializationOptions()
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            .SetEnvironmentName("development");
#else
             .SetEnvironmentName("production");
#endif

        await UnityServices.InitializeAsync(options);

        Debug.Log("<color=green>Authentication.cs: UnityServices Initialized: </color>");

        Login();
    }

    public static async void Login()
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();

            Debug.Log("<color=green>Authentication.cs: Login Successful: </color>"
                + AuthenticationService.Instance.PlayerId);

            OnLogin?.Invoke();
        }
        catch (AuthenticationException ex)
        {
            Debug.LogWarning("Authentication.cs: Login failed AuthenticationException, retrying in 5: " + ex.Message);

            await WaitForTimeout(Login);
        }
        catch (RequestFailedException ex)
        {
            Debug.LogWarning("Authentication.cs: Login failed RequestFailedException, retrying in 5: " + ex.Message);

            await WaitForTimeout(Login);
        }
        catch (Exception ex)
        {
            // The session token is invalid and has been cleared.
            // The associated account is no longer accessible through this login method.
            if (ex.Message == "The session token is not valid.")
            {
                Login();
            }
            else
            {
                Debug.LogError("Authentication.cs: Login failed: " + ex.Message);
            }
        }
    }

    public static async Task WaitForTimeout(Action action)
    {
        Console.WriteLine("Waiting for 5 seconds...");
        await Task.Delay(5000); // Wait for 5000 milliseconds (5 seconds)
        Console.WriteLine("Done waiting.");

        action.Invoke();
    }
    //public static void Logout()
    //{
    //    try
    //    {
    //        AuthenticationService.Instance.ClearSessionToken();
    //
    //        Debug.Log("<color=green>AuthenticationUnityPlay.cs: Unlink is successful.</color>");
    //    }
    //    catch (AuthenticationException ex)
    //    {
    //        Debug.LogError("AuthenticationUnityPlay.cs: Logout failed AuthenticationException: " + ex.Message);
    //    }
    //    catch (RequestFailedException ex)
    //    {
    //        Debug.LogError("AuthenticationUnityPlay.cs: Logout failed RequestFailedException: " + ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        Debug.LogError("AuthenticationUnityPlay.cs: Logout failed: " + ex.Message);
    //    }
    //}

    //public async static void ChangePlayerName(string name, Action callback)
    //{
    //    try
    //    {
    //        await AuthenticationService.Instance.UpdatePlayerNameAsync(name);
    //
    //        OnNameChange?.Invoke();
    //
    //        callback?.Invoke();
    //    }
    //    catch (AuthenticationException ex)
    //    {
    //        Debug.LogWarning("AuthenticationUnityPlay.cs: AuthenticationException error changing player name. name: "
    //            + name + " . " + ex.Message);
    //
    //        Timer.AddMethod("AuthenticationUnityPlay_ChangePlayerName", 3, false, () => ChangePlayerName(name, callback));
    //    }
    //    catch (RequestFailedException ex)
    //    {
    //        Debug.LogWarning("AuthenticationUnityPlay.cs: RequestFailedException error changing player name, retrying in 4. name: "
    //            + name + " . " + ex.Message);
    //
    //        Timer.AddMethod("AuthenticationUnityPlay_ChangePlayerName", 3, false, () => ChangePlayerName(name, callback));
    //    }
    //    catch (Exception ex)
    //    {
    //        Debug.LogError("AuthenticationUnityPlay.cs: Error changing player name. name: "
    //           + name + " . " + ex.Message);
    //
    //        OnNameChange?.Invoke();
    //
    //        callback?.Invoke();
    //    }
    //}
}
