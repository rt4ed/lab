using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Calculation.Implementation;
using Calculation.Interfaces;
using Moq;
using NUnit.Framework;

namespace Calculation.Tests
{
	public class CalculatorTests
	{
		private ICalculator _calculator;
		private Mock<ILogger> _loggerMock;

		[SetUp]
		public void Setup()
		{
			_loggerMock = new Mock<ILogger>();

			_loggerMock
				.Setup(logger => logger.Trace(It.IsAny<string>()))
				.Callback((string message) => Console.WriteLine($"Trace\t{message}"));

			_loggerMock
				.Setup(logger => logger.Info(It.IsAny<string>()))
				.Callback((string message) => Console.WriteLine($"Info \t{message}"));

			_loggerMock
				.Setup(logger => logger.Error(It.IsAny<Exception>()))
				.Callback((Exception exception) => Console.WriteLine($"Error\t{exception}"));

			_calculator = new Calculator(_loggerMock.Object);
		}

		#region Exception tests

		[Test]
		public void Sum_TooBigNumbers_ThrowsOriginalOverflowException()
		{
			var exception = Assert.Throws<OverflowException>(() => _calculator.Sum(int.MaxValue, int.MaxValue));

			var expectedTargetSite = typeof(BaseCalculator).GetMethod("SafeSum", BindingFlags.NonPublic | BindingFlags.Instance);
			Assert.AreEqual(expectedTargetSite, exception.TargetSite);
		}

		[Test]
		public void Sub_TooLittleNumbers_ThrowsOverflowExceptionWithModifiedStackTrace()
		{
			var exception = Assert.Throws<OverflowException>(() => _calculator.Sub(int.MinValue, int.MaxValue));
			var expectedTargetSite = typeof(Calculator).GetMethod(nameof(Calculator.Sub));
			Assert.AreEqual(expectedTargetSite, exception.TargetSite);
		}

		[Test]
		public void Multiply_TooBigNumbers_ThrowsInvalidOperationExceptionWithOriginalInnerException()
		{
			var exception = Assert.Throws<InvalidOperationException>(() => _calculator.Multiply(int.MaxValue, int.MaxValue));

			var expectedTargetSite = typeof(BaseCalculator).GetMethod("SafeMultiply", BindingFlags.NonPublic | BindingFlags.Instance);

			Assert.AreEqual(expectedTargetSite, exception.InnerException?.TargetSite);
		}

		[Test]
		public void Div_DivideByZero_ThrowsInvalidOperationExceptionWithoutOriginalInnerException()
		{
			var exception = Assert.Throws<InvalidOperationException>(() => _calculator.Div(int.MaxValue, 0));

			Assert.IsNull(exception.InnerException);
		}

		#endregion

		#region Logging tests

		[Test]
		public void Sum_SomeNaturalNumbers_LogsMethodBeginningMessage()
		{
			var numbers = new[] { 1, 2, 3 };
			_calculator.Sum(numbers);

			var messageCheck = new Func<string, bool>(
				message => message.Contains(nameof(Calculator.Sum)) &&
				           numbers.All(n => message.Contains(n.ToString())));

			Expression<Func<string, bool>> messageCheckExpression = message => messageCheck(message);

			_loggerMock.Verify(logger => logger.Trace(It.Is(messageCheckExpression)), Times.Exactly(1));
		}

		[Test]
		public void Sum_SomeNaturalNumbers_LogsMethodEndingMessage()
		{
			var numbers = new[] { 1, 2, 3 };
			var sum = _calculator.Sum(numbers);

			var messageCheck = new Func<string, bool>(
				message => message.Contains(nameof(Calculator.Sum)) &&
				           numbers.All(n => !message.Contains(n.ToString())) &&
				           !message.Contains(sum.ToString()));

			Expression<Func<string, bool>> messageCheckExpression = message => messageCheck(message);

			_loggerMock.Verify(logger => logger.Trace(It.Is(messageCheckExpression)), Times.Exactly(1));
		}

		[Test]
		public void Sum_SomeNaturalNumbers_LogsResultMessage()
		{
			var numbers = new[] {1, 2, 3};
			var sum = _calculator.Sum(numbers);

			var messageCheck = new Func<string, bool>(
				message => message.Contains(nameof(Calculator.Sum)) &&
				           numbers.All(n => message.Contains(n.ToString())) &&
				           message.Contains(sum.ToString()));

			Expression<Func<string, bool>> messageCheckExpression = message => messageCheck(message);

			_loggerMock.Verify(logger => logger.Info(It.Is(messageCheckExpression)), Times.Exactly(1));
		}

		[Test]
		public void Sum_SomeNaturalNumbers_ThereAreNotAnyErrorsInLog()
		{
			var numbers = new[] {1, 2, 3};
			_calculator.Sum(numbers);

			_loggerMock.Verify(logger => logger.Error(It.IsAny<Exception>()), Times.Never);
		}

		[Test]
		public void Sum_TooBigNumbers_LogsException()
		{
			var numbers = new[] {int.MaxValue, int.MaxValue};
			var exception = Assert.Throws<OverflowException>(() => _calculator.Sum(numbers));

			var exceptionCheck = new Func<Exception, bool>(
				loggedException => loggedException == exception);

			Expression<Func<Exception, bool>> exceptionCheckExpression = ex => exceptionCheck(ex);

			_loggerMock.Verify(logger => logger.Error(It.Is(exceptionCheckExpression)), Times.Exactly(1));
		}

		[Test]
		public void Sum_TooBigNumbers_LogsMethodBeginningMessage()
		{
			var numbers = new[] {int.MaxValue, int.MaxValue};
			Assert.Throws<OverflowException>(() => _calculator.Sum(numbers));

			var messageCheck = new Func<string, bool>(
				message => message.Contains(nameof(Calculator.Sum)) &&
				           numbers.All(n => message.Contains(n.ToString())));

			Expression<Func<string, bool>> messageCheckExpression = message => messageCheck(message);

			_loggerMock.Verify(logger => logger.Trace(It.Is(messageCheckExpression)), Times.Exactly(1));
		}

		[Test]
		public void Sum_TooBigNumbers_LogsMethodEndingMessage()
		{
			var numbers = new[] {int.MaxValue, int.MaxValue};
			Assert.Throws<OverflowException>(() => _calculator.Sum(numbers));

			var messageCheck = new Func<string, bool>(
				message => message.Contains(nameof(Calculator.Sum)) &&
				           numbers.All(n => !message.Contains(n.ToString())));

			Expression<Func<string, bool>> messageCheckExpression = message => messageCheck(message);

			_loggerMock.Verify(logger => logger.Trace(It.Is(messageCheckExpression)), Times.Exactly(1));
		}

		[Test]
		public void Sum_TooBigNumbers_ThereAreNotAnyLogsWithResultMessage()
		{
			var numbers = new[] {int.MaxValue, int.MaxValue};
			Assert.Throws<OverflowException>(() => _calculator.Sum(numbers));

			_loggerMock.Verify(logger => logger.Info(It.IsAny<string>()), Times.Never);
		}

		#endregion
	}
}