using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class User : Person
    {
        #region Properties
        public string PassWord { get; set; }
        public string Login { get; set; }
        public string Sector { get; set; }
        public Role Role { get; set; }
        #endregion
    }
}
