using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp
{
    // allow class & memebers to be seialized need to add attribute
    [Serializable]  // attribute  -> this info is for CLR  // metadata
   public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
    }
}
