using Services.Core.Accounts.Interface.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Core.Accounts.Data.Entities
{
    public class UserAccountEntity : AccountBaseEntity
    {
        public string Username { get; set; } = default!;
        
        public string Email { get; set; } = default!;
        
        public string FirstName { get; set; } = default!;
        
        public string LastName { get; set; } = default!;        
    }
}
