using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ModuleDTO
    {
        public int Id { get; set; }
        public string ModuleName { get; set; }
        public string Description { get; set; }
        public bool HasViewRight { get; set; }
        public bool HasAddRight { get; set; }
        public bool HasDeleteRight { get; set; }
        public bool HasEditRight { get; set; }
        public bool IsEnabled { get; set; }
    }
}
