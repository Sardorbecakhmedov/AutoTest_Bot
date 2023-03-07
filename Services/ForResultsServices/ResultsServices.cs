using AutoTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTest.Services.ForResultsServices
{
    partial class ResultsServices
    {
        public List<Result> Results { get; set; }
        private string FileName { get; set; } = "Results.json";
        public ResultsServices()
        {
            Results = new List<Result>();
        }

    }
}
