using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryFormatterApp
{
    [Serializable]
    public class UserPrefs
    {
        public string WindowColor { get; set; }
        public int FontSize { get; set; }
    }
}
