using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Rotorsoft.Forms
{
    internal class SortFieldComparer : IComparer
    {
        public SortFieldComparer(IList<SortDescription> sortDescriptions)
        {

        }

        public int Compare(object x, object y)
        {
            throw new NotImplementedException();
        }
    }
}
