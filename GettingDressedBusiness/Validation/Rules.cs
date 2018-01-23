using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GettingDressedBusiness.Common;
using GettingDressedBusiness.Model;
namespace GettingDressedBusiness.Validation
{
    public class Rules : IRules
    {
        private string[] InputArgs { get; set; }
        private IList<Command> CommandList { get; set; }
        private TemperatureType TemperatureType { get; set; }

        public Rules(string[] inputArgs, IList<Command> commandList)
        {
            InputArgs = inputArgs;
            CommandList = commandList;
            TemperatureType = InputArgs[0] == Enum.GetName(typeof(TemperatureType), 0) ? TemperatureType.HOT : TemperatureType.COLD;
        }

        #region [PreProcessingRule]        
        public bool EvaluatePreProcessingRule()
        {
            return IsPajamasNotOffFirst();
        }
        private bool IsPajamasNotOffFirst()
        {
            //Pajamas must be taken off before anything else can be put on
            int command = Convert.ToInt32(InputArgs[1]);
            Command commandToRun = CommandList.Where(c => c.Description == "Take off pajamas").First();
            return command != commandToRun.CommandId;
        }

        #endregion

        #region [InCommandProcessingRule] 
        public bool EvaluateBusinessRule(int lastCmdIndex, int currentCmdIndex)
        {
            if (IsDuplicateCommand(InputArgs[lastCmdIndex], InputArgs[currentCmdIndex])
                || IsSocksPutWhenHot(InputArgs[currentCmdIndex])
                || IsJacketPutWhenHot(InputArgs[currentCmdIndex])
                || IsSocksNotPutBeforeFootwear(currentCmdIndex)
                || IsPentsNotPutBeforeFootwear(currentCmdIndex)
                || IsShirtNotPutBeforeHeadwear(currentCmdIndex)
                || IsShirtNotPutBeforeJacket(currentCmdIndex)
                || isAnyClothingMissing(currentCmdIndex))
                return true;
            else
                return false;
        }
        private bool IsDuplicateCommand(string lastCmd, string currentCmd)
        {
            return lastCmd == currentCmd;
        }
        private bool IsSocksPutWhenHot(string currentCmd)
        {
            int command = Convert.ToInt32(currentCmd);
            Command commandToPutSocks = CommandList.Where(c => c.Description == "Put on socks").First();
            if (TemperatureType == TemperatureType.HOT)
            {
                return command == commandToPutSocks.CommandId;
            }
            return false;
        }
        private bool IsJacketPutWhenHot(string currentCmd)
        {
            int command = Convert.ToInt32(currentCmd);
            Command commandToPutJacket = CommandList.Where(c => c.Description == "Put on jacket").First();
            if (TemperatureType == TemperatureType.HOT)
            {
                return command == commandToPutJacket.CommandId;
            }
            return false;
        }
        private bool IsSocksNotPutBeforeFootwear(int currentCmdIndex)
        {
            if (TemperatureType == TemperatureType.COLD)
            {
                Command y = CommandList.Where(c => c.Description == "Put on footwear").First();
                Command x = CommandList.Where(c => c.Description == "Put on socks").First();
                return IsXNotPutBeforeY(currentCmdIndex, x, y);
            }           
            return false;
        }
        private bool IsPentsNotPutBeforeFootwear(int currentCmdIndex)
        {
            Command y = CommandList.Where(c => c.Description == "Put on footwear").First();
            Command x = CommandList.Where(c => c.Description == "Put on pants").First();
            return IsXNotPutBeforeY(currentCmdIndex, x, y);
        }       
        private bool IsShirtNotPutBeforeHeadwear(int currentCmdIndex)
        {
            Command y = CommandList.Where(c => c.Description == "Put on headwear").First();
            Command x = CommandList.Where(c => c.Description == "Put on shirt").First();
            return IsXNotPutBeforeY(currentCmdIndex,x, y);
        }
        private bool IsShirtNotPutBeforeJacket(int currentCmdIndex)
        {
            if (TemperatureType == TemperatureType.COLD)
            {
                Command y = CommandList.Where(c => c.Description == "Put on jacket").First();
                Command x = CommandList.Where(c => c.Description == "Put on shirt").First();
                return IsXNotPutBeforeY(currentCmdIndex, x, y);
            }
            return false;
        }
        private bool IsXNotPutBeforeY(int currentCmdIndex, Command x, Command y)
        {
            bool IsXPutBeforeY = false;
            int currentcommand = Convert.ToInt32(InputArgs[currentCmdIndex]);            
            if (currentcommand == y.CommandId)
            {                
                for (int i = 1; i < currentCmdIndex; i++)
                {
                    if (Convert.ToInt32(InputArgs[i]) == x.CommandId)
                    {
                        IsXPutBeforeY = true;
                        break;
                    }
                }
            }
            else
            {
                return IsXPutBeforeY;
            }
            return !IsXPutBeforeY;
        }
        private bool isAnyClothingMissing(int currentCmdIndex)
        {
            bool isMissing = false;
            //You cannot leave the house until all items of clothing are on (except socks and a jacket when it’s hot)
            int currentcommand = Convert.ToInt32(InputArgs[currentCmdIndex]);
            Command commandToLeaveHouse = CommandList.Where(c => c.Description == "Leave house").First();
            if (currentcommand == commandToLeaveHouse.CommandId)
            {
                if (TemperatureType == TemperatureType.COLD)
                {
                    isMissing = InputArgs.Count() < 9;
                }
                if (TemperatureType == TemperatureType.HOT)
                {
                    isMissing = InputArgs.Count() < 7;
                }
            }
            return isMissing;
        }

        #endregion
                
    }
}
