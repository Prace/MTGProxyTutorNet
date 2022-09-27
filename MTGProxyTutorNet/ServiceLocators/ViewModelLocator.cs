using MTGProxyTutorNet.Contracts.Exceptions;
using MTGProxyTutorNet.DependencyInjection;
using MTGProxyTutorNet.ViewModels;

namespace MTGProxyTutorNet
{
    internal static class ViewModelLocator
    {
        private static readonly DIManager _DIManager = new DIManager();

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
