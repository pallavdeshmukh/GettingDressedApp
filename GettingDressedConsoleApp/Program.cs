using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GettingDressedBusiness.Model;
using GettingDressedBusiness.Common;
using GettingDressedBusiness.Helper;
using GettingDressedBusiness.Processing;
using GettingDressedBusiness.Validation;

namespace GettingDressedConsoleApp
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            if (args.Count() == 0)
            {
                Console.WriteLine("Input: ");
                string args1 = Console.ReadLine();
                args = args1.Replace(",",string.Empty).Split(' ');
            }
            ValidateInput inputValidator = new ValidateInput();
            CommandHelper commandHelper = new CommandHelper();
            List<Command> commandList = commandHelper.GetCommandList();
            Rules rules = new Rules(args, commandList);

            GetDressed gd = new GetDressed(args, inputValidator, commandList, rules);
            gd.ProcessRequest();

            Console.WriteLine("Output: " + gd._outputString.ToString());
            Console.ReadLine();
        }
    }
}
