using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class tblModuleAccessRight
    {
        [Key]

        public int Id { get; set; }
        [Required]
        public int ModuleId { get; set; }
        public int RoleId { get; set; }
        public bool ViewRight { get; set; }
        public bool AddRight { get; set; }
        public bool EditRight { get; set; }
        public bool DeleteRight { get; set; }
        [ForeignKey("ModuleId")]
        public virtual tblModule tblModule { get; set; }
        [ForeignKey("RoleId")]
        public virtual tblRole tblRole { get; set; }
    }
}
