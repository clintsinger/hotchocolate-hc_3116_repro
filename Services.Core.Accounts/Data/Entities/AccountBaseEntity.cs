using Services.Core.Accounts.Interface.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Core.Accounts.Data.Entities
{
    public abstract class AccountBaseEntity
    {
        public Guid Id { get; set; }

        public AccountStatus Status { get; set; }

        public Guid? OrganizationId { get; set; }

        public IEnumerable<PostalAddress>? Addresses { get; set; }

        public IEnumerable<PhoneNumber>? Phones { get; set; }

        public string? RefLink { get; set; }

        public string? Notes { get; set; }

        public DateTime? CreatedUtc { get; set; }

        public DateTime? UpdatedUtc { get; set; }

        public DateTime? DeletedUtc { get; set; }
    }
}
