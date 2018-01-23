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
    public class GetDressed : IGettingDressed
    {
        const string FAIL = "fail";
        const string SEPRATOR = ", ";
        public StringBuilder OutputString { get; set; }
        private IList<Command> CommandList { get; set; }
        private IValidateInput InputValidator { get; set; }
        private string[] InputArgs { get; set; }
        private IRules Rules { get; set; }

        public GetDressed(string[] inputArgs, IValidateInput validateInput, IList<Command> commandList, IRules rules)
        {
            InputValidator = validateInput;
            CommandList = commandList;
            InputArgs = inputArgs;
            Rules = rules;
            OutputString = new StringBuilder();
        }

        public void ProcessRequest()
        {            
            if (!InputValidator.IsInputValid(InputArgs))
            {
                FormatOutput(FAIL);
                return;
            }            
            if (Rules.EvaluatePreProcessingRule())
            {
                FormatOutput(FAIL);
                return;
            }
            for (int i = 1; i < InputArgs.Count(); i++)
            {
                int currentCommand = Convert.ToInt32(InputArgs[i]);

                if (Rules.EvaluateBusinessRule(i-1, i))
                {
                    FormatOutput(FAIL);
                    return;
                }                           
                Command commandToRun = CommandList.Where(c => c.CommandId == currentCommand).First();
                string response = InputArgs[0] == Enum.GetName(typeof(TemperatureType), 0) ? commandToRun.HotResponse : commandToRun.ColdResponse;
                FormatOutput(response, i);
            }            
        }
        public void FormatOutput(string response, int stepNumber=100)
        {            
            OutputString.Append(response);
            if ((stepNumber + 1) < InputArgs.Count())
                OutputString.Append(SEPRATOR);
        }

    }
}
