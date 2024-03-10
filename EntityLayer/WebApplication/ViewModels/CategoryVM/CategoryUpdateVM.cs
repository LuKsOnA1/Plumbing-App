namespace EntityLayer.WebApplication.ViewModels.CategoryVM
{
	public class CategoryUpdateVM
	{
		public int Id { get; set; }
		public string CreatedDate { get; set; } = DateTime.Now.ToString("d");
		public string? UpdatedDate { get; set; }
		public byte[] RowVersion { get; set; } = null!;

        public string Name { get; set; } = null!;
	}
}
