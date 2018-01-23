using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingDressedBusiness.Validation
{
    public interface IRules
    {
        bool EvaluatePreProcessingRule();
        bool EvaluateBusinessRule(int lastCmdIndex, int currentCmdIndex);
    }
}
