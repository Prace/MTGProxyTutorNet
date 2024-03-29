﻿using MTGProxyTutorNet.Contracts.Exceptions;
using MTGProxyTutorNet.ViewModels;

namespace MTGProxyTutorNet.ServiceLocators
{
    internal static class ViewModelLocator
    {
        private static readonly DependencyInjectionManager _DIManager = new DependencyInjectionManager();

        public static T GetViewModel<T>() where T : BaseViewModel
        {
            try
            {
                var vmInstance = _DIManager.ServiceProvider.GetService(typeof(T)) as T;
                if (vmInstance == null)
                    throw new DependencyResolveFailedException($"Could not resolve deependency for ViewModel type {typeof(T).Name}");
                return vmInstance;
            }
            catch
            {
                throw;
            }
        }
    }
}
