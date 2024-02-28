using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Application.Model
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class ResultItem
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class ItemBase
    {
        public int count { get; set; }
        public string next { get; set; }
        public string previous { get; set; }
        public List<ResultItem> results { get; set; }
    }
}
