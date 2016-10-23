using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UAI.Case.Dto
{
    public class ChangePasswordDTO
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ValidatePassword { get; set; }
    }
}
