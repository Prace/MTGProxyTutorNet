using System.Threading.Tasks;

namespace MTGProxyTutorNet.Contracts.Interfaces
{
	public interface IWebApiConsumer
	{
		T Get<T>(string url, bool useCache = true) where T : class;
		Task<T> GetAsync<T>(string url, bool useCache = true) where T : class;
		byte[] GetBinary(string url, bool useCache = true);
		Task<byte[]> GetBinaryAsync(string url, bool useCache = true);
	}
}