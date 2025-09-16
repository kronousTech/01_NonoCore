using KronosTech.Services;
using System;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

namespace KronosTech.Authentication
{
    /// <summary>
    /// Auto logins once the authentication is done.
    /// </summary>
    public class AuthenticationService : ServiceBase
    {
        public static async void Login(Action callback)
        {
            try
            {
                await Unity.Services.Authentication.AuthenticationService.Instance.SignInAnonymouslyAsync();

                Debug.Log($"{nameof(AuthenticationService)}: " +
                    $"Login Successful: "
                    + Unity.Services.Authentication.AuthenticationService.Instance.PlayerId);

                callback?.Invoke();
            }
            catch (AuthenticationException ex)
            {
                Debug.LogWarning($"{nameof(AuthenticationService)}: " +
                    $"Login failed {nameof(AuthenticationException)}, retrying in 5: " + ex.Message);

                await WaitForTimeout(() => Login(callback));
            }
            catch (RequestFailedException ex)
            {
                Debug.LogWarning($"{nameof(AuthenticationService)}: " +
                    $"Login failed {nameof(RequestFailedException)}, retrying in 5: " + ex.Message);

                await WaitForTimeout(() => Login(callback));
            }
            catch (Exception ex)
            {
                // The session token is invalid and has been cleared.
                // The associated account is no longer accessible through this login method.
                if (ex.Message == "The session token is not valid.")
                {
                    Login(callback);
                }
                else
                {
                    Debug.LogError($"{nameof(AuthenticationService)}: " +
                        $"Login failed: " + ex.Message);
                }
            }
        }
        private static async Task WaitForTimeout(Action action)
        {
            Console.WriteLine("Waiting for 5 seconds...");
            await Task.Delay(5000); // Wait for 5000 milliseconds (5 seconds)
            Console.WriteLine("Done waiting.");

            action.Invoke();
        }

        #region ServiceBase
        protected override void InitializeBehavior(Action callback)
        {
            BootstrapperServices.WhenReady<UnityServicesInitializeService>((result) =>
            {
                Login(callback);
            });
        }
        #endregion
    }
}