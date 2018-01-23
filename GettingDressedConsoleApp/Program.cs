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
            IValidateInput inputValidator = new ValidateInput();
            CommandHelper commandHelper = new CommandHelper();
            IList<Command> commandList = commandHelper.GetCommandList();
            IRules rules = new Rules(args, commandList);

            IGettingDressed getDressedObj = new GetDressed(args, inputValidator, commandList, rules);
            getDressedObj.ProcessRequest();

            Console.WriteLine("Output: " + getDressedObj.OutputString.ToString());
            Console.ReadLine();
        }
    }
}
