using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ProjektPiotrMVC.Models
{
    // Możesz dodać dane profilu dla użytkownika, dodając więcej właściwości do klasy ApplicationUser. Odwiedź stronę https://go.microsoft.com/fwlink/?LinkID=317594, aby dowiedzieć się więcej.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Element authenticationType musi pasować do elementu zdefiniowanego w elemencie CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Dodaj tutaj niestandardowe oświadczenia użytkownika
            return userIdentity;
        }

        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string PESEL { get; set; }

		public int KlasaID { get; set; }
		public virtual Klasa Klasa { get; set; }

		public int PrzedmiotID { get; set; }

		public virtual ICollection<UczenRodzic> UczniowieRodzice { get; set; }
        public virtual ICollection<Ocena> Oceny { get; set; }
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

        public DbSet<KlasaPrzedmiot> KlasaPrzedmiots { get; set; }
		public DbSet<UczenRodzic> UczenRodzics { get; set; }
        public DbSet<Klasa> Klasas { get; set; }
        public DbSet<Przedmiot> Przedmiots { get; set; }
        public DbSet<Ocena> Ocenas { get; set; }
        public DbSet<Ogloszenie> Ogloszenies { get; set; }
        public DbSet<Plik> Pliks { get; set; }
		public DbSet<Wiadomosc> Wiadomoscs { get; set; }

		public class IdentityManager
		{
			public RoleManager<IdentityRole> LocalRoleManager
			{
				get
				{
					return new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
				}
			}

			public UserManager<ApplicationUser> LocalUserManager
			{
				get
				{
					return new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
				}
			}

			public ApplicationUser GetUserByID(string userID)
			{
				ApplicationUser user = null;
				UserManager<ApplicationUser> um = this.LocalUserManager;

				user = um.FindById(userID);

				return user;
			}

			public ApplicationUser GetUserByName(string email)
			{
				ApplicationUser user = null;
				UserManager<ApplicationUser> um = this.LocalUserManager;

				user = um.FindByEmail(email);

				return user;
			}

			public bool RoleExists(string name)
			{
				var rm = LocalRoleManager;

				return rm.RoleExists(name);
			}

			public bool CreateRole(string name)
			{
				var rm = LocalRoleManager;
				var idResult = rm.Create(new IdentityRole(name));

				return idResult.Succeeded;
			}

			public bool CreateUser(ApplicationUser user, string password)
			{
				var um = LocalUserManager;
				var idResult = um.Create(user, password);

				return idResult.Succeeded;
			}

			public bool AddUserToRole(string userId, string roleName)
			{
				var um = LocalUserManager;
				var idResult = um.AddToRole(userId, roleName);

				return idResult.Succeeded;
			}

			public bool AddUserToRoleByUsername(string username, string roleName)
			{
				var um = LocalUserManager;

				string userID = um.FindByName(username).Id;
				var idResult = um.AddToRole(userID, roleName);

				return idResult.Succeeded;
			}

			public void ClearUserRoles(string userId)
			{
				var um = LocalUserManager;
				var user = um.FindById(userId);
				var currentRoles = new List<IdentityUserRole>();

				currentRoles.AddRange(user.Roles);

				foreach (var role in currentRoles)
				{
					um.RemoveFromRole(userId, role.RoleId);
				}
			}
		}
    }
}