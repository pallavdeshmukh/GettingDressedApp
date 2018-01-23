using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingDressedBusiness.Processing
{
    public interface IGettingDressed
    {
        StringBuilder OutputString { get; set; }
        void ProcessRequest();
    }
}
