using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.AboutVM;

namespace EntityLayer.WebApplication.ViewModels.SocialMediaVM
{
	public class SocialMediaAddVM
	{
		public string? Twitter { get; set; }
		public string? LinkedIn { get; set; }
		public string? Facebook { get; set; }
		public string? Instagram { get; set; }

		public AboutAddVM About { get; set; } = null!;
	}
}
