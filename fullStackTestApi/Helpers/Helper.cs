using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fullStackTestApi.Helpers;

public class Helper : IHelper
{
    public string GenerateRandomNumber(int length)
    {
        Random random = new Random();
        StringBuilder sb = new StringBuilder();

        for(int i =0; i < length; i++)
        {
            sb.Append(random.Next(10));
        }

        return sb.ToString();
    }    
}
