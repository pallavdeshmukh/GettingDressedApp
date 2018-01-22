using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GettingDressedBusiness.Model;
namespace GettingDressedBusiness.Helper
{   
    public class CommandHelper
    {
        public List<Command> CommandList { get; private set; }

        public CommandHelper()
        {
            CommandList = new List<Command>();
        }

        public List<Command> GetCommandList()
        {
            Command Command1 = new Command() { CommandId = 1, Description = "Put on footwear", HotResponse = "sandals", ColdResponse = "boots" };
            Command Command2 = new Command() { CommandId = 2, Description = "Put on headwear", HotResponse = "sunglasses", ColdResponse = "hat" };
            Command Command3 = new Command() { CommandId = 3, Description = "Put on socks", HotResponse = "fail", ColdResponse = "socks" };
            Command Command4 = new Command() { CommandId = 4, Description = "Put on shirt", HotResponse = "shirt", ColdResponse = "shirt" };
            Command Command5 = new Command() { CommandId = 5, Description = "Put on jacket", HotResponse = "fail", ColdResponse = "jacket" };
            Command Command6 = new Command() { CommandId = 6, Description = "Put on pants", HotResponse = "shorts", ColdResponse = "pants" };
            Command Command7 = new Command() { CommandId = 7, Description = "Leave house", HotResponse = "leaving house", ColdResponse = "leaving house" };
            Command Command8 = new Command() { CommandId = 8, Description = "Take off pajamas", HotResponse = "Removing PJs", ColdResponse = "Removing PJs" };
            CommandList.Add(Command1);
            CommandList.Add(Command2);
            CommandList.Add(Command3);
            CommandList.Add(Command4);
            CommandList.Add(Command5);
            CommandList.Add(Command6);
            CommandList.Add(Command7);
            CommandList.Add(Command8);
            return CommandList;
        }
    }
}
