using System.Data.Entity;
using Microsoft.Owin;
using Owin;
using QLTN2018.DAL;
using QLTN2018.Migrations;

[assembly: OwinStartupAttribute(typeof(QLTN2018.Startup))]
namespace QLTN2018
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<QLTNContext, Configuration>());
            ConfigureAuth(app);
        }
    }
}
