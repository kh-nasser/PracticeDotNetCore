install Microsoft.EntityFrameworkCore.Design

generate dao from database: Scaffold-DbContext "Server=.;Database=TestDb;User ID=sa;Password=1;" -Provider Microsoft.EntityFrameworkCore.SqlServer -DataAnnotations -OutputDir DAO -ContextDir DataContext -Context MyTestContext -Force -Verbose

 Add-Migration init -Context MyTestContext 
 Update-Database -Context MyTestContext 