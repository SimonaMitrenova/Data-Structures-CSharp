namespace CalculateArithmeticExpression
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class CalculateArithmeticExpression
    {
        public static readonly IDictionary<string, int> Operators = new Dictionary<string, int>()
        {
            { "-", 0 },
            { "+", 0 },
            { "*", 1 },
            { "/", 1 },
            { "%", 1 }
        }; 

        public static void Main(string[] args)
        {
            var expression = Console.ReadLine();
            expression =
                Regex.Replace(expression, @"((?<=[\-\+\/\*\%\(\)\= ])|(?<=^))\-?\d+\.?(?:\d+)?|[\(\)]", " $0 ").Trim();
            var reversedPolishNotation = GetReversedPolishNotation(expression).ToList();
            try
            {
                var result = ReversePolishNotationParser(reversedPolishNotation);
                Console.WriteLine(result);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            
        }

        public static double ReversePolishNotationParser(IList<string> reversedPolishNotation)
        {
            var operandStack = new Stack<double>();
            for (int i = 0; i < reversedPolishNotation.Count(); i++)
            {
                string currentTolken = reversedPolishNotation[i];
                if (!IsOperator(currentTolken))
                {
                    operandStack.Push(double.Parse(currentTolken));
                }
                else
                {
                    PerformOperation(operandStack, currentTolken);
                }
            }

            if (operandStack.Count > 1)
            {
                throw new ArgumentException("Error: Invalid operands count.");
            }

            double result = operandStack.Peek();

            return result;
        }

        private static void PerformOperation(Stack<double> operandStack, string currentTolken)
        {
            if (operandStack.Count < 2)
            {
                throw new ArgumentException("Error: Invalid operands count.");
            }
            
            var secondOperand = operandStack.Pop();
            var firstOperand = operandStack.Pop();

            switch (currentTolken)
            {
                case "-":
                    operandStack.Push(firstOperand - secondOperand);
                    break;

                case "+":
                    operandStack.Push(firstOperand + secondOperand);
                    break;

                case "*":
                    operandStack.Push(firstOperand * secondOperand);
                    break;

                case "/":
                    operandStack.Push(firstOperand / secondOperand);
                    break;

                case "%":
                    operandStack.Push(firstOperand % secondOperand);
                    break;
            }
        }

        public static IEnumerable<string> GetReversedPolishNotation(string expression)
        {
            var outputQueue = new Queue<string>();
            var operatorStack = new Stack<string>();

            string[] expressionTolkens = expression.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries).ToArray();
            for (int i = 0; i < expressionTolkens.Length; i++)
            {
                string tolken = expressionTolkens[i];
                if (!(IsOperator(tolken) || IsLeftParenthesis(tolken) || IsRightParenthesis(tolken)))
                {
                    outputQueue.Enqueue(tolken);
                }
                else if (IsOperator(tolken))
                {
                    while (operatorStack.Any() && IsOperator(operatorStack.Peek()) && Operators[operatorStack.Peek()] > Operators[tolken])
                    {
                        var currentOperator = operatorStack.Pop();
                        outputQueue.Enqueue(currentOperator);
                    }

                    operatorStack.Push(tolken);
                }
                else if (IsLeftParenthesis(tolken))
                {
                    operatorStack.Push(tolken);
                }
                else if (IsRightParenthesis(tolken))
                {
                    while (operatorStack.Peek() != "(")
                    {
                        var currentOperator = operatorStack.Pop();
                        outputQueue.Enqueue(currentOperator);
                    }

                    operatorStack.Pop();
                }
            }

            while (operatorStack.Count > 0)
            {
                var currentOperator = operatorStack.Pop();
                outputQueue.Enqueue(currentOperator);
            }

            return outputQueue;
        }

        private static bool IsOperator(string tolken)
        {
            return Operators.ContainsKey(tolken);
        }

        private static bool IsLeftParenthesis(string tolken)
        {
            return tolken == "(";
        }

        private static bool IsRightParenthesis(string tolken)
        {
            return tolken == ")";
        }
    }
}
