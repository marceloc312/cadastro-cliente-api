﻿using Api.Core.Contracts.Services;
using Api.Core.Contracts.Services.RestServices;
using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Core.TestsUnidade
{
    public static class TestHelper
    {
        public const string JSON_CEP = @"{
  ""cep"": ""01001-000"",
  ""logradouro"": ""Praça da Sé"",
  ""complemento"": ""lado ímpar"",
  ""bairro"": ""Sé"",
  ""localidade"": ""São Paulo"",
  ""uf"": ""SP"",
  ""ibge"": ""3550308"",
  ""gia"": ""1004"",
  ""ddd"": ""11"",
  ""siafi"": ""7107""
}";

        public static HttpClient CreateHttpClientFake(HttpStatusCode statusCode, string content, Exception exception = null)
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = string.IsNullOrEmpty(content) ? null : new StringContent(content),
            };

            if (exception == null)
                handlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()).ReturnsAsync(response);
            else
                handlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()).ThrowsAsync(new Exception("Time out"));

            var httpClient = new HttpClient(handlerMock.Object) { BaseAddress = new Uri("http://localhost:8989/") };

            return httpClient;
        }

        public static HttpResponseMessage CreateHttpClientHttpResponseMessageFake(HttpStatusCode statusCode, string content, Exception exception = null)
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = string.IsNullOrEmpty(content) ? null : new StringContent(content),
            };

            return response;
        }

        public static Mock<IRestClientCEPService> CreateRestClientCEPServiceMock(HttpStatusCode statusCode, string content, Exception exception = null)
        {

            var restClientCEPService = new Mock<IRestClientCEPService>();

            if (exception == null)
                restClientCEPService.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(TestHelper.CreateHttpClientHttpResponseMessageFake(statusCode,content));
            else
                restClientCEPService.Setup(x => x.GetAsync(It.IsAny<string>())).ThrowsAsync(new Exception("Time out"));

            return restClientCEPService;
        }
    }
}
