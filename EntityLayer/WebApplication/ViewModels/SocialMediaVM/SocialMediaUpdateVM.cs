using EntityLayer.WebApplication.Entities;
using EntityLayer.WebApplication.ViewModels.AboutVM;

namespace EntityLayer.WebApplication.ViewModels.SocialMediaVM
{
	public class SocialMediaUpdateVM
	{
		public int Id { get; set; }
		public string? UpdatedDate { get; set; }
		public byte[] RowVersion { get; set; } = null!;


		public string? Twitter { get; set; }
		public string? LinkedIn { get; set; }
		public string? Facebook { get; set; }
		public string? Instagram { get; set; }

		public AboutUpdateVM About { get; set; } = null!;
	}
}
