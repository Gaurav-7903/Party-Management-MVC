
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Party_Management.Models
{
    public class ApplicationRole : IdentityRole<Guid>
    {
    }
}
