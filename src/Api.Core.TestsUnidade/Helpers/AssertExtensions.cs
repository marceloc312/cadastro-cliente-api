using Microsoft.Extensions.Logging;
using Moq;
using System;

namespace Api.Core.TestsUnidade
{
    public static class AssertExtensions
    {
        public static void _AssertLog<TRefer>(this Mock<ILogger<TRefer>> mock, LogLevel logLevel, string mensagem, int totalTime) where TRefer : class
        {
            mock.Verify(
                m => m.Log(
                    logLevel,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((object v, Type _) => v.ToString().Contains(mensagem)),
                    null,
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
                Times.Exactly(totalTime));
        }
    }
}
