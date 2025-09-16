using System;
using UnityEngine;

namespace KronosTech.Services
{
    public abstract class ServiceBase
    {
        private bool m_isInitialized = false;
        private bool m_isInitializing = false;

        protected abstract void InitializeBehavior(Action callback);

        public void Initialize(Action serviceCallback)
        {
            if(m_isInitialized)
            {
                Debug.LogError($"{nameof(ServiceBase)}.cs: " +
                    $"You are trying to initialize the same service twice.");

                return;
            }

            if(m_isInitializing)
            {
                Debug.LogError($"{nameof(ServiceBase)}.cs: " +
                    $"You are trying to initialize a service that is initializing already.");

                return;
            }

            m_isInitializing = true;

            InitializeBehavior(() => RaiseServiceCallback(serviceCallback));
        }

        public void RaiseServiceCallback(Action serviceCallback)
        {
            m_isInitialized = true;
            m_isInitializing = false;

            serviceCallback?.Invoke();
        }
    }
}