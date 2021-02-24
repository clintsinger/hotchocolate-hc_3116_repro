using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Core.Accounts.Interface.Schema
{
    public record Provider(
        string? Name = null,
        string? SubjectId = null,
        string? ExternalIdToken = null
    );
}
