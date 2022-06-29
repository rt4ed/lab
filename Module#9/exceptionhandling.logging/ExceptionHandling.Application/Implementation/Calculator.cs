using System.Linq;
using Calculation.Interfaces;
using System;
using System.Text;

namespace Calculation.Implementation
{
	public class Calculator : BaseCalculator, ICalculator
	{
		private readonly ILogger _logger;

		public Calculator(ILogger logger)
		{
			_logger = logger;
		}


        public int Sum(params int[] numbers)
        {
            try
            {
                StringBuilder StrBuild = new StringBuilder();
                for (int i = 0; i < numbers.Length; i++)
                {
                    StrBuild.Append($"{numbers[i]} ");
                }

                string args = StrBuild.ToString();
                _logger.Trace($"Method {nameof(Sum)} starts with args: {args}");

                var sum = SafeSum(numbers);
                _logger.Info($"Method {nameof(Sum)} ended with result: {sum}, args: {args}");

                return sum;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
            finally
            {
                _logger.Trace($"Method ended: {nameof(Sum)}");
            }
        }

        public int Sub(int a, int b)
		{
            try
            {
				return SafeSub(a, b);
			}
            catch
            {
				throw new OverflowException();
			}
		}

		public int Multiply(params int[] numbers)
		{
            try
            {
				if (!numbers.Any())
					return 0;

				return SafeMultiply(numbers);
            }
            catch(OverflowException a)
            {
				throw new InvalidOperationException("Something went wrong", a);
			}
				
			
		}

		public int Div(int a, int b)
		{
            try
            {
				return a / b;
            }
            catch
            {
				throw new InvalidOperationException();
			}
			
		}
	}
}