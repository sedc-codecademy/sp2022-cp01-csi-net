using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoSimulator.Services.Interfaces
{
    public interface ICoinService
    {
        double GetPriceByCoinId(string coinId);

    }
}
