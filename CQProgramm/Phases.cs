using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQProgramm
{
    public class Phase
    {
        public static Countable[] CountableList = Countable.Init();

        public string Title { get; set; }
        public string[] Types { get; set; }
        public string[] Codes { get; set; }

        public Phase(string title, string[] types, string[] codes)
        {
            this.Title = title;
            this.Types = types;
            this.Codes = codes;
        }
        public Countable GetElem(string code)
        {
            return CountableList.Where(x => x.Title == code).FirstOrDefault();

        }
        public bool Contains(string element)
        {
           return CountableList.getTitles().Contains(element);
         }

    }
}
