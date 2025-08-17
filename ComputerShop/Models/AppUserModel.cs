using Microsoft.AspNetCore.Identity;

namespace ComputerShop.Models
{
	public class AppUserModel: IdentityUser
	{
		public string Ocupation { get; set; }
		public string RoleId { get; set; }
	}
	
}
