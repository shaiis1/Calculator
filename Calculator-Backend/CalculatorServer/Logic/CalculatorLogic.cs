using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalculatorServer.Models;

namespace CalculatorServer.Logic
{
    public class CalculatorLogic
    {
        public static CalcResponse Calculate(string calcString, string filePath)
        {
            CalcResponse response = new CalcResponse();
            Stack<string> operatorStack = new Stack<string>();
            Stack<string> numbersStack = new Stack<string>();
            string firstOperand = "";
            string secondOperand = "";
            string op = "";
            for(int i = 0; i < calcString.Length; i++)
            {
                if(!isDigitOrDot(calcString[i]))
                {
                    op = calcString[i].ToString();
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(op))
                    {
                        firstOperand += calcString[i].ToString();
                    }
                    else
                    {
                        secondOperand += calcString[i].ToString();
                    }
                }

                if(!string.IsNullOrWhiteSpace(op) && (op == "x" || op == "/"))
                {
                    if (string.IsNullOrWhiteSpace(firstOperand))
                    {
                        firstOperand = numbersStack.Pop();
                    }
                    secondOperand = getSecondOperand(calcString, i + 1);
                    i = i + secondOperand.Length;
                    var result = calc(float.Parse(firstOperand), float.Parse(secondOperand), op);
                    numbersStack.Push(result.ToString());
                    firstOperand = "";
                    secondOperand = "";
                    op = "";
                }
                else if(!string.IsNullOrWhiteSpace(op) && (op == "-" || op == "+"))
                {
                    if (!string.IsNullOrWhiteSpace(firstOperand))
                    {
                        numbersStack.Push(firstOperand);
                    }
                    operatorStack.Push(op);
                    firstOperand = "";
                    op = "";
                }
            }

            if (!string.IsNullOrWhiteSpace(firstOperand))
            {
                numbersStack.Push(firstOperand);
            }
            if (numbersStack.Count != 0)
            {
                numbersStack = reverseStack(numbersStack);
            }

            if (operatorStack.Count != 0)
            {
                operatorStack = reverseStack(operatorStack);
            }

            while (operatorStack.Count > 0)
            {
                firstOperand = numbersStack.Pop();
                secondOperand = numbersStack.Pop();
                op = operatorStack.Pop();
                var res = calc(float.Parse(firstOperand), float.Parse(secondOperand), op);
                numbersStack.Push(res.ToString());
            }

            response.Result = numbersStack.Pop();
            writeResultToFile(calcString ,response.Result, filePath);
            response.AllResults = getAllResults(filePath);
            return response;
        }

        #region Read And Write Results To File

        public static List<string> getAllResults(string filePath)
        {
            List<string> response = new List<string>();
            if (File.Exists(filePath))
            {
                var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        response.Add(line);
                    }
                }
            }
                return response;
        }

        private static void writeResultToFile(string calcString, string result, string filePath)
        {
            using (StreamWriter sw = File.AppendText(filePath)){
                sw.WriteLine(calcString + " = " + result);
            }
        }
        #endregion

        private static Stack<string> reverseStack(Stack<string> stack)
        {
            Stack<string> response = new Stack<string>();

            while(stack.Count != 0)
            {
                response.Push(stack.Pop());
            }
            return response;
        }

        private static string getSecondOperand(string calcString, int i)
        {
            string response = "";
            while(i < calcString.Length && isDigitOrDot(calcString[i]))
            {
               response += calcString[i].ToString();
               i++;
            }
            return response;
        }

        private static float calc(float firstOperand, float secondOperand, string op)
        {
            float result = 0;
            switch (op)
            {
                case "x":
                    result = firstOperand * secondOperand;
                    break;
                case "/":
                    result = firstOperand / secondOperand;
                    break;
                case "+":
                    result = firstOperand + secondOperand;
                    break;
                case "-":
                    result = firstOperand - secondOperand;
                    break;
            }
            return result;
        }

        private static bool isDigitOrDot(char toCheck)
        {
            bool isDigitOrDot = false;
            if(Char.IsDigit(toCheck) || toCheck == '.')
            {
                isDigitOrDot = true;
            }

            return isDigitOrDot;
        }
    }
}
