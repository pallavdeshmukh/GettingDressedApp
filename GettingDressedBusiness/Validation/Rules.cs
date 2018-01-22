using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GettingDressedBusiness.Common;
using GettingDressedBusiness.Model;
namespace GettingDressedBusiness.Validation
{
    public class Rules
    {
        private string[] _inputArgs { get; set; }
        private List<Command> _commandList { get; set; }
        private TemperatureType _temperatureType { get; set; }

        public Rules(string[] inputArgs, List<Command> commandList)
        {
            _inputArgs = inputArgs;
            _commandList = commandList;
            _temperatureType = _inputArgs[0] == Enum.GetName(typeof(TemperatureType), 0) ? TemperatureType.HOT : TemperatureType.COLD;
        }

        #region [PreProcessingRule]        
        public bool EvaluatePreProcessingRule()
        {
            return IsPajamasNotOffFirst();
        }
        private bool IsPajamasNotOffFirst()
        {
            //Pajamas must be taken off before anything else can be put on
            int command = Convert.ToInt32(_inputArgs[1]);
            Command commandToRun = _commandList.Where(c => c.Description == "Take off pajamas").First();
            return command != commandToRun.CommandId;
        }

        #endregion

        #region [InCommandProcessingRule] 
        public bool EvaluateBusinessRule(int lastCmdIndex, int currentCmdIndex)
        {
            if (IsDuplicateCommand(_inputArgs[lastCmdIndex], _inputArgs[currentCmdIndex])
                || IsSocksPutWhenHot(_inputArgs[currentCmdIndex])
                || IsJacketPutWhenHot(_inputArgs[currentCmdIndex])
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
            Command commandToPutSocks = _commandList.Where(c => c.Description == "Put on socks").First();
            if (_temperatureType == TemperatureType.HOT)
            {
                return command == commandToPutSocks.CommandId;
            }
            return false;
        }
        private bool IsJacketPutWhenHot(string currentCmd)
        {
            int command = Convert.ToInt32(currentCmd);
            Command commandToPutJacket = _commandList.Where(c => c.Description == "Put on jacket").First();
            if (_temperatureType == TemperatureType.HOT)
            {
                return command == commandToPutJacket.CommandId;
            }
            return false;
        }
        private bool IsSocksNotPutBeforeFootwear(int currentCmdIndex)
        {
            if (_temperatureType == TemperatureType.COLD)
            {
                Command y = _commandList.Where(c => c.Description == "Put on footwear").First();
                Command x = _commandList.Where(c => c.Description == "Put on socks").First();
                return IsXNotPutBeforeY(currentCmdIndex, x, y);
            }           
            return false;
        }
        private bool IsPentsNotPutBeforeFootwear(int currentCmdIndex)
        {
            Command y = _commandList.Where(c => c.Description == "Put on footwear").First();
            Command x = _commandList.Where(c => c.Description == "Put on pants").First();
            return IsXNotPutBeforeY(currentCmdIndex, x, y);
        }       
        private bool IsShirtNotPutBeforeHeadwear(int currentCmdIndex)
        {
            Command y = _commandList.Where(c => c.Description == "Put on headwear").First();
            Command x = _commandList.Where(c => c.Description == "Put on shirt").First();
            return IsXNotPutBeforeY(currentCmdIndex,x, y);
        }
        private bool IsShirtNotPutBeforeJacket(int currentCmdIndex)
        {
            if (_temperatureType == TemperatureType.COLD)
            {
                Command y = _commandList.Where(c => c.Description == "Put on jacket").First();
                Command x = _commandList.Where(c => c.Description == "Put on shirt").First();
                return IsXNotPutBeforeY(currentCmdIndex, x, y);
            }
            return false;
        }
        private bool IsXNotPutBeforeY(int currentCmdIndex, Command x, Command y)
        {
            bool IsXPutBeforeY = false;
            int currentcommand = Convert.ToInt32(_inputArgs[currentCmdIndex]);            
            if (currentcommand == y.CommandId)
            {                
                for (int i = 1; i < currentCmdIndex; i++)
                {
                    if (Convert.ToInt32(_inputArgs[i]) == x.CommandId)
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
            int currentcommand = Convert.ToInt32(_inputArgs[currentCmdIndex]);
            Command commandToLeaveHouse = _commandList.Where(c => c.Description == "Leave house").First();
            if (currentcommand == commandToLeaveHouse.CommandId)
            {
                if (_temperatureType == TemperatureType.COLD)
                {
                    isMissing = _inputArgs.Count() < 9;
                }
                if (_temperatureType == TemperatureType.HOT)
                {
                    isMissing = _inputArgs.Count() < 7;
                }
            }
            return isMissing;
        }

        #endregion
                
    }
}
