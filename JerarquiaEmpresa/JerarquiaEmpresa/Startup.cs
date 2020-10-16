using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JerarquiaEmpresa.Startup))]
namespace JerarquiaEmpresa
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
