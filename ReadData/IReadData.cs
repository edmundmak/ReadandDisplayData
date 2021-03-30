using Common.Infrastructure.Responses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ReadData
{
    public interface IReadData
    {
        public string ConvertToJsonFromTextFile(string fileName);
        public string ReadFromJsonFile(string fileName);

    }
}
