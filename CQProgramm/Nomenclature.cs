﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQProgramm
{
  public  class Nomenclature
    {
        string Title { get; set; }
        int [] Subclasses { get; set; }

        public Nomenclature(string title, int[] subclasses)
        {
            this.Title = title;
            this.Subclasses = subclasses;
        }
    }
}
