using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heurystyka
{
    public interface IFunkcje
    {
        double Min { get; set; }
        double Max { get; set; }
        double ObliczFunkcje(double[] przedzial);
    }
}
