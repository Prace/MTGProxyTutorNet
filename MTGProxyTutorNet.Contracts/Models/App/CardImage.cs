using System;
using System.IO;

namespace MTGProxyTutorNet.Contracts.Models.App
{
	public class CardImage
	{
		private readonly byte[] _binary;

		public CardImage(byte[] binary)
		{
			_binary = binary;
		}

		public CardImage(string base64str)
		{
			_binary = Convert.FromBase64String(base64str);
		}

		public MemoryStream GetStream()
		{
			return new MemoryStream(_binary);
		}

		public byte[] GetBinary()
		{
			return _binary;
		}

		public string GetBase64()
		{
			return Convert.ToBase64String(_binary);
		}
	}
}
