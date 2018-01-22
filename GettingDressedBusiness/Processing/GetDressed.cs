using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GettingDressedBusiness.Model;
using GettingDressedBusiness.Common;
using GettingDressedBusiness.Validation;

namespace GettingDressedBusiness.Processing
{
    public class GetDressed
    {
        const string FAIL = "fail";
        const string SEPRATOR = ", ";
        public StringBuilder _outputString { get; set; }
        private List<Command> _commandList { get; set; }
        private ValidateInput _InputValidator { get; set; }
        private string[] _inputArgs { get; set; }
        private Rules _rules { get; set; }

        public GetDressed(string[] inputArgs, ValidateInput validateInput, List<Command> commandList, Rules rules)
        {
            _InputValidator = validateInput;
            _commandList = commandList;
            _inputArgs = inputArgs;
            _rules = rules;
            _outputString = new StringBuilder();
        }

        public void ProcessRequest()
        {            
            if (!_InputValidator.IsInputValid(_inputArgs))
            {
                PrintOutput(FAIL);
                return;
            }            
            if (_rules.EvaluatePreProcessingRule())
            {
                PrintOutput(FAIL);
                return;
            }
            for (int i = 1; i < _inputArgs.Count(); i++)
            {
                int currentCommand = Convert.ToInt32(_inputArgs[i]);

                if (_rules.EvaluateBusinessRule(i-1, i))
                {
                    PrintOutput(FAIL);
                    return;
                }                           
                Command commandToRun = _commandList.Where(c => c.CommandId == currentCommand).First();
                string response = _inputArgs[0] == Enum.GetName(typeof(TemperatureType), 0) ? commandToRun.HotResponse : commandToRun.ColdResponse;
                PrintOutput(response, i);
            }            
        }
        public void PrintOutput(string response, int stepNumber=100)
        {            
            _outputString.Append(response);
            if ((stepNumber + 1) < _inputArgs.Count())
                _outputString.Append(SEPRATOR);
        }

    }
}
