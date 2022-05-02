using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLP_WebApi.Models.ControllerModels
{
    public class ConvertorModel
    {
        public List<string> numberList { get; set; }
        public List<string> changedTextList { get; set; }
        public ConvertorModel()
        {
            changedTextList = new List<string>();
        }
    }
}
