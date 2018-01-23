using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GettingDressedBusiness.Common;

namespace GettingDressedBusiness.Validation
{
    public class ValidateInput : IValidateInput
    {              
        public bool IsInputValid(string[] args)
        {
            bool isInputValid = true;
            if (args.Count() == 1 && string.IsNullOrEmpty(args[0]))
            {
                Console.WriteLine("Please provide valid input");
                return false;
            }            
            if (args[0] != Enum.GetName(typeof(TemperatureType), 0) && 
                args[0] != Enum.GetName(typeof(TemperatureType), 1))
            {
                Console.WriteLine("Invalid temperature type, valid input are HOT or COLD");
                isInputValid = false;
            }
            for (int i = 1; i < args.Count(); i++)
            {
                int command;
                if (!int.TryParse(args[i].Replace(",", string.Empty), out command))
                {
                    Console.WriteLine("Except 1st parameter all other parameter should be integer value");
                    isInputValid = false;
                }                
            }
            return isInputValid;
        }
    }
}
