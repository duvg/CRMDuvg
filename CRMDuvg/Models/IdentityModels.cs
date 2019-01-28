using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CRMDuvg.Models
{
    // Para agregar datos de perfil del usuario, agregue más propiedades a su clase ApplicationUser. Visite https://go.microsoft.com/fwlink/?LinkID=317594 para obtener más información.
    public class ApplicationUser : IdentityUser
    {

        public string Nombre { get; set; }
        public string Apellidos { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Tenga en cuenta que el valor de authenticationType debe coincidir con el definido en CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Agregar aquí notificaciones personalizadas de usuario
            userIdentity.AddClaim(new Claim("Nombre", this.Nombre));
            userIdentity.AddClaim(new Claim("Apellidos", this.Apellidos));
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<CRMDuvg.Models.TipoActividad> TipoActividades { get; set; }

        public System.Data.Entity.DbSet<CRMDuvg.Models.TipoCliente> TipoClientes { get; set; }

        public System.Data.Entity.DbSet<CRMDuvg.Models.Actividad> Actividades { get; set; }

        public System.Data.Entity.DbSet<CRMDuvg.Models.Contacto> Contactos { get; set; }

        public System.Data.Entity.DbSet<CRMDuvg.Models.Direccion> Direcciones  { get; set; }

        public System.Data.Entity.DbSet<CRMDuvg.Models.Email> Emails { get; set; }

        public System.Data.Entity.DbSet<CRMDuvg.Models.Telefono> Telefonos  { get; set; }

        public System.Data.Entity.DbSet<CRMDuvg.Models.Campania> Campanias { get; set; }

        public System.Data.Entity.DbSet<CRMDuvg.Models.Cliente> Clientes { get; set; }



    }
}