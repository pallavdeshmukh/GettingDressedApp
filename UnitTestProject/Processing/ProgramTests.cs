using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GettingDressedBusiness.Model;
using GettingDressedBusiness.Helper;
using GettingDressedBusiness.Processing;
using GettingDressedBusiness.Validation;

namespace GettingDressedBusiness.Processing.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        public void HOTSuccessTest()
        {
            // arrange  
            string[] inputArgs = new string[] { "HOT", "8", "6", "4", "2", "1", "7" };
            string expected = "Removing PJs, shorts, shirt, sunglasses, sandals, leaving house";

            IValidateInput inputValidator = new ValidateInput();
            CommandHelper commandHelper = new CommandHelper();
            IList<Command> commandList = commandHelper.GetCommandList();
            IRules rules = new Rules(inputArgs, commandList);

            // act 
            IGettingDressed getDressedObj = new GetDressed(inputArgs, inputValidator, commandList, rules);
            getDressedObj.ProcessRequest();
            string output = getDressedObj.OutputString.ToString();
            //Assert
            Assert.AreEqual(expected, output, true);            
        }

        [TestMethod()]
        public void ColdSuccessTest()
        {
            // arrange  
            string[] inputArgs = new string[] { "COLD", "8", "6", "3", "4", "2", "5", "1", "7" };
            string expected = "Removing PJs, pants, socks, shirt, hat, jacket, boots, leaving house";

            IValidateInput inputValidator = new ValidateInput();
            CommandHelper commandHelper = new CommandHelper();
            IList<Command> commandList = commandHelper.GetCommandList();
            IRules rules = new Rules(inputArgs, commandList);

            // act 
            IGettingDressed getDressedObj = new GetDressed(inputArgs, inputValidator, commandList, rules);
            getDressedObj.ProcessRequest();
            string output = getDressedObj.OutputString.ToString();
            
            //Assert
            Assert.AreEqual(expected, output, true);
        }         

        [TestMethod()]
        public void DuplicateClothFailTest()
        {
            // arrange  
            string[] inputArgs = new string[] { "HOT", "8", "6", "6" };
            string expected = "Removing PJs, shorts, fail";

            IValidateInput inputValidator = new ValidateInput();
            CommandHelper commandHelper = new CommandHelper();
            IList<Command> commandList = commandHelper.GetCommandList();
            IRules rules = new Rules(inputArgs, commandList);

            // act 
            IGettingDressed getDressedObj = new GetDressed(inputArgs, inputValidator, commandList, rules);
            getDressedObj.ProcessRequest();
            string output = getDressedObj.OutputString.ToString();

            //Assert
            Assert.AreEqual(expected, output, true);
        }
        [TestMethod()]
        public void SocksWhenItIsHotTest()
        {
            // arrange  
            string[] inputArgs = new string[] { "HOT", "8", "6", "3" };
            string expected = "Removing PJs, shorts, fail";

            IValidateInput inputValidator = new ValidateInput();
            CommandHelper commandHelper = new CommandHelper();
            IList<Command> commandList = commandHelper.GetCommandList();
            IRules rules = new Rules(inputArgs, commandList);

            // act 
            IGettingDressed getDressedObj = new GetDressed(inputArgs, inputValidator, commandList, rules);
            getDressedObj.ProcessRequest();
            string output = getDressedObj.OutputString.ToString();

            //Assert
            Assert.AreEqual(expected, output, true);
        }            
         
        [TestMethod()]
        public void ColdButNotPutFootwearTest()
        {
            // arrange  
            string[] inputArgs = new string[] { "COLD", "8", "6", "3", "4", "2", "5", "7" };
            string expected = "Removing PJs, pants, socks, shirt, hat, jacket, fail";

            IValidateInput inputValidator = new ValidateInput();
            CommandHelper commandHelper = new CommandHelper();
            IList<Command> commandList = commandHelper.GetCommandList();
            IRules rules = new Rules(inputArgs, commandList);

            // act 
            IGettingDressed getDressedObj = new GetDressed(inputArgs, inputValidator, commandList, rules);
            getDressedObj.ProcessRequest();
            string output = getDressedObj.OutputString.ToString();

            //Assert
            Assert.AreEqual(expected, output, true);
        }

        [TestMethod()]
        public void PajamasNotOffTest()
        {
            // arrange  
            string[] inputArgs = new string[] { "COLD", "6" };
            string expected = "fail";

            IValidateInput inputValidator = new ValidateInput();
            CommandHelper commandHelper = new CommandHelper();
            IList<Command> commandList = commandHelper.GetCommandList();
            IRules rules = new Rules(inputArgs, commandList);

            // act 
            IGettingDressed getDressedObj = new GetDressed(inputArgs, inputValidator, commandList, rules);
            getDressedObj.ProcessRequest();
            string output = getDressedObj.OutputString.ToString();

            //Assert
            Assert.AreEqual(expected, output, true);
        }
        [TestMethod()]
        public void HeadwearBeforeShirtTest()
        {
            // arrange  
            string[] inputArgs = new string[] { "COLD", "8", "6", "2", "4", "1", "7" };
            string expected = "Removing PJs, pants, fail";

            IValidateInput inputValidator = new ValidateInput();
            CommandHelper commandHelper = new CommandHelper();
            IList<Command> commandList = commandHelper.GetCommandList();
            IRules rules = new Rules(inputArgs, commandList);

            // act 
            IGettingDressed getDressedObj = new GetDressed(inputArgs, inputValidator, commandList, rules);
            getDressedObj.ProcessRequest();
            string output = getDressedObj.OutputString.ToString();

            //Assert
            Assert.AreEqual(expected, output, true);
        }

        [TestMethod()]
        public void JacketBeforeShirtTest()
        {
            // arrange  
            string[] inputArgs = new string[] { "COLD", "8", "6", "5", "4", "1", "7" };
            string expected = "Removing PJs, pants, fail";

            IValidateInput inputValidator = new ValidateInput();
            CommandHelper commandHelper = new CommandHelper();
            IList<Command> commandList = commandHelper.GetCommandList();
            IRules rules = new Rules(inputArgs, commandList);

            // act 
            IGettingDressed getDressedObj = new GetDressed(inputArgs, inputValidator, commandList, rules);
            getDressedObj.ProcessRequest();
            string output = getDressedObj.OutputString.ToString();

            //Assert
            Assert.AreEqual(expected, output, true);
        }
        [TestMethod()]
        public void JacketInHotWeatherTest()
        {
            // arrange  
            string[] inputArgs = new string[] { "HOT", "8", "6", "4", "5", "1", "7" };
            string expected = "Removing PJs, shorts, shirt, fail";

            IValidateInput inputValidator = new ValidateInput();
            CommandHelper commandHelper = new CommandHelper();
            IList<Command> commandList = commandHelper.GetCommandList();
            IRules rules = new Rules(inputArgs, commandList);

            // act 
            IGettingDressed getDressedObj = new GetDressed(inputArgs, inputValidator, commandList, rules);
            getDressedObj.ProcessRequest();
            string output = getDressedObj.OutputString.ToString();

            //Assert
            Assert.AreEqual(expected, output, true);
        }
    }
}