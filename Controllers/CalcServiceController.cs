﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Backend1.Models;

namespace Backend1.Controllers
{
    public class CalcServiceController : Controller
    {
        private readonly ILogger<CalcServiceController> _logger;

        public CalcServiceController(ILogger<CalcServiceController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ManualWithSeparateHandlers()
        {
            ViewData["calcTitle"] = "ManualWithSeparateHandlers";
            return View("Manual");
        }

        [HttpPost]
        public IActionResult ManualWithSeparateHandlers(int firstInt, string action, int secondInt)
        {
            Dictionary<string, string> result = this.calculate(firstInt, action, secondInt);

            string data = $"{firstInt} {result["action"]} {secondInt} = {result["answer"]}";
            ViewData["result"] = data;
            return View("Result");
        }
        
        public IActionResult Manual(int? firstInt, string action, int? secondInt)
        {
            if (firstInt == null & secondInt == null) {
                ViewData["calcTitle"] = "Manual";
                return View("Manual");
            }

            Dictionary<string, string> result = this.calculate(firstInt, action, secondInt);

            string data = $"{firstInt} {result["action"]} {secondInt} = {result["answer"]}";
            ViewData["result"] = data;
            return View("Result");
        }

        [HttpGet]
        public IActionResult ModelBindingSeparateModel()
        {
            ViewData["calcTitle"] = "ModelBindingSeparateModel";
            return View("ModelBinding");
        }

        [HttpPost]
        public IActionResult ModelBindingSeparateModel(int _firstInt, string _action, int _secondInt)
        {
            CalcModel calcModel = new CalcModel();
            Dictionary<string, string> result = calcModel.calculate(_firstInt, _action, _secondInt);

            string data = $"{_firstInt} {result["action"]} {_secondInt} = {result["answer"]}";
            ViewData["result"] = data;
            return View("Result");
        }

        [HttpGet]
        public IActionResult ModelBindingInParameters()
        {
            ViewData["calcTitle"] = "ModelBindingInParameters";
            return View("ModelBinding");
        }

        [HttpPost]
        public IActionResult ModelBindingInParameters(CalcModel calcModel)
        {
            Dictionary<string, string> result = this.calculate(calcModel._firstInt, calcModel._action, calcModel._secondInt);

            string data = $"{calcModel._firstInt} {result["action"]} {calcModel._secondInt} = {result["answer"]}";
            ViewData["result"] = data;
            return View("Result");
        }

        protected Dictionary<string, string> calculate(int? firstInt, string action, int? secondInt)
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
