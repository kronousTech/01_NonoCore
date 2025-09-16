using KronosTech.Authentication;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace KronosTech.Services
{
    public static class BootstrapperServices
    {
        private static readonly Dictionary<Type, ServiceBase> s_services = new();
        private static readonly Dictionary<Type, List<Action<object>>> s_waiting = new();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void InitializeBootstrap()
        {
            Register("Unity Services", new UnityServicesInitializeService());
            Register("Authentication", new AuthenticationService());
        }

        private static void Register<T>(string name, T service) where T : ServiceBase
        {
            Debug.Log($"{nameof(BootstrapperServices)}: " +
                $"Initializing {name}...");

            service.Initialize(() =>
            {
                s_services[typeof(T)] = service;

                // Notify waiters
                if (s_waiting.TryGetValue(typeof(T), out var callbacks))
                {
                    foreach (var cb in callbacks)
                    {
                        cb(service);
                    }

                    s_waiting.Remove(typeof(T));
                }

                Debug.Log($"{nameof(BootstrapperServices)}: " +
                    $"{name} is ready.");
            });
        }
        public static void WhenReady<T>(Action<T> callback) where T : ServiceBase
        {
            if (s_services.TryGetValue(typeof(T), out var service))
            {
                callback(service as T);
            }
            else
            {
                if (!s_waiting.ContainsKey(typeof(T)))
                {
                    s_waiting[typeof(T)] = new();
                }

                s_waiting[typeof(T)].Add(s => callback(s as T));
            }
        }
    }
}