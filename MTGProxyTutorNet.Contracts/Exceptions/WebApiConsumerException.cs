using System;
using System.Net;

namespace MTGProxyTutorNet.Contracts.Exceptions
{
	public class WebApiConsumerException : Exception
	{
		public HttpStatusCode? StatusCode { get; private set; }

		public WebApiConsumerException(string message, HttpStatusCode? statusCode)
			: base(message)
		{
			StatusCode = statusCode;
		}

		public WebApiConsumerException(string message, Exception innerException, HttpStatusCode? statusCode)
			: base(message, innerException)
		{
			StatusCode = statusCode;
		}
	}
}
