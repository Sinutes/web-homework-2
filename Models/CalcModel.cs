using System;
using System.Collections.Generic;

namespace Backend1.Models
{
    public class CalcModel
    {
        public int _firstInt { get; set; }
        public int _secondInt { get; set; }
        public string _action { get; set; }

        public Dictionary<string, string> calculate(int? firstInt, string action, int? secondInt)
        {
            string answer = "";
            int? temp = 0;
            switch (action)
            {
                case "add":
                    temp = firstInt + secondInt;
                    answer = temp.ToString();
                    action = "+";
                    break;
                case "sub":
                    temp = firstInt - secondInt;
                    answer = temp.ToString();
                    action = "-";
                    break;
                case "mult":
                    temp = firstInt * secondInt;
                    answer = temp.ToString();
                    action = "*";
                    break;
                case "div":
                    if (secondInt == 0) {
                        answer = "error";
                    } else {
                        temp = firstInt / secondInt;
                        answer = temp.ToString();
                    }
                    action = "/";
                    break;
            }
            Dictionary<string, string> answerArray = new Dictionary<string, string>();
            answerArray.Add("action", action);
            answerArray.Add("answer", answer);

            return answerArray;
        }
    }
}
