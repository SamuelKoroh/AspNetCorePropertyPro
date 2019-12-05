using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCorePropertyPro.Data.Migrations
{
    public partial class SeedTenants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                    INSERT INTO Tenants(Id, Name, HostName, ConnectionString)
                    VALUES('43ce6f06-a472-461f-b990-3a25c7f44b7a', 'Tenant1', 'Tenant1', 'Server=(localDB)\\MSSqlLocalDB; database=PPTenantOne; Integrated Security=true');
                    INSERT INTO Tenants(Id, Name, HostName, ConnectionString)
VALUES('199b625e-6ac6-4757-a38f-9a0391866469', 'Tenant2', 'Tenant2', 'Server=(localDB)\\MSSqlLocalDB; database=PPTenantTwo; Integrated Security=true');
                    INSERT INTO Tenants(Id, Name, HostName, ConnectionString)
VALUES('93ce6f06-a472-461f-b990-3a25c7f44b7a', 'Tenant3', 'Tenant3', 'Server=(localDB)\\MSSqlLocalDB; database=PPTenantThree; Integrated Security=true');
                    
                ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("TRUNCATE TABLE  Tenants");
        }
    }
}
