using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQProgramm
{
    public class Phase
    {
        public string Title { get; set; }
        public string[] Types { get; set; }
        public string[] Codes { get; set; }

        public Phase(string title, string[] types, string[] codes)
        {
            this.Title = title;
            this.Types = types;
            this.Codes = codes;
        }

    }
}
