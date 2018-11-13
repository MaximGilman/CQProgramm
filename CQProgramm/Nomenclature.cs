using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQProgramm
{
  public  class Nomenclature
    {
        public string Title { get; set; }
        public int [] Subclasses { get; set; }
        public Phase [] Phases { get; set; }
        public Nomenclature(string title, int[] subclasses, Phase [] phases)
        {
            this.Title = title;
            this.Subclasses = subclasses;
            this.Phases = phases;
        }
    }
}
