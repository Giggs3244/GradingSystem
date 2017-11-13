using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Escuela
{
    public class AuthContextMigrationConfiguration : DbMigrationsConfiguration<AuthContext>
    {
        public AuthContextMigrationConfiguration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }
    }
}