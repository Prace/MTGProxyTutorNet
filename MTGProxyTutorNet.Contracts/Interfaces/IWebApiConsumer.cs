using System.Threading.Tasks;

namespace MTGProxyTutorNet.Contracts.Interfaces
{
	public interface IWebApiConsumer
	{
		T Get<T>(string url, int msDelay = 0, bool useCache = true) where T : class;
		Task<T> GetAsync<T>(string url, int msDelay = 0, bool useCache = true) where T : class;
		byte[] GetBinary(string url, int msDelay = 0, bool useCache = true);
		Task<byte[]> GetBinaryAsync(string url, int msDelay = 0, bool useCache = true);
	}
}