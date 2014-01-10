using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MiscellenaousRnD.Properties;

namespace MiscellenaousRnD.InterFaceTesting
{
    class TestClass:I2
    {
        public void M1()
        {
            MessageBox.Show(Resources.TestClass_M1_M1_called);
        }

        public void M2()
        {
            MessageBox.Show("M2 called");
        }

        //public class List<T> : IList<T>, ICollection<T>, IList, ICollection, IReadOnlyList<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
        //public class Collection<T> : IList<T>, ICollection<T>, IList, ICollection, IReadOnlyList<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
    }

    class TestClassImpl : TestClass
    {

    }
}
