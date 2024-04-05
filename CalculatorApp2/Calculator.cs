using System;
using System.Collections.Generic;
using System.Linq;

namespace CalculatorApp2
{
    class Calculator
    {
        private readonly Logger logger;

        public Calculator(Logger logger)
        {
            this.logger = logger;
        }

        public double Calculate(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException("Expression cannot be empty.");
            }

            logger.LogInput(input, "Calculate");

            try
            {
                var postfixExpression = ConvertToPostfix(input);
                if (postfixExpression.Count == 0)
                {
                    throw new ArgumentException("Invalid expression.");
                }

                var result = EvaluatePostfix(postfixExpression);

                logger.LogOutput( result, "Calculate");

                return result;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error occurred: {ex.Message}", "Calculate");
                throw;
            }
        }

        private List<string> ConvertToPostfix(string input)
        //This method converts an infix mathematical expression into a postfix expression using the shunting-yard algorithm. It takes an infix expression as input and returns a list of strings representing the equivalent postfix expression.
        {
            var precedence = new Dictionary<char, int>
            {
                ['+'] = 1,
                ['-'] = 1,
                ['*'] = 2,
                ['/'] = 2,
                ['%'] = 2
            };
            //shunting-yard algorithm.

            var output = new List<string>();
            var operatorStack = new Stack<char>();

            string currentNumber = "";

            foreach (char token in input)
            {
                if (char.IsDigit(token))
                {
                    currentNumber += token;
                }
                else if (precedence.ContainsKey(token))
                {
                    if (!string.IsNullOrEmpty(currentNumber))
                    {
                        output.Add(currentNumber);
                        currentNumber = "";
                    }

                    while (operatorStack.Count > 0 && precedence[token] <= precedence[operatorStack.Peek()])
                    {
                        output.Add(operatorStack.Pop().ToString());
                    }

                    operatorStack.Push(token);
                }
            }

            if (!string.IsNullOrEmpty(currentNumber))
            {
                output.Add(currentNumber);
            }

            while (operatorStack.Count > 0)
            {
                output.Add(operatorStack.Pop().ToString());
            }

            return output;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="postfix"></param>
        /// <returns></returns>
        private double EvaluatePostfix(List<string> postfix)
        { 
            
                var stack = new Stack<double>();

                foreach (string token in postfix)
                {
                    if (double.TryParse(token, out double number))
                    {
                        stack.Push(number);
                    }
                    else
                    {
                        double operand2 = stack.Pop();
                        double operand1 = stack.Pop();

                        switch (token)
                        {
                            case "+":
                                stack.Push(operand1 + operand2);
                                break;
                            case "-":
                                stack.Push(operand1 - operand2);
                                break;
                            case "*":
                                stack.Push(operand1 * operand2);
                                break;
                            case "/":
                                if (operand2 == 0)
                                {
                                    throw new ArgumentException("Cannot divide by zero.");
                                }
                                stack.Push(operand1 / operand2);
                                break;
                            case "%":
                                if (operand2 == 0)
                                {
                                    throw new ArgumentException("Cannot modulo by zero.");
                                }
                                stack.Push(operand1 % operand2);
                                break;
                            default:
                                throw new ArgumentException("Invalid token.");
                        }
                    }
                }

                return stack.Pop();
            
        }
    }
}
