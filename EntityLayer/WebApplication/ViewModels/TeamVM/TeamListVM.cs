﻿namespace EntityLayer.WebApplication.ViewModels.TeamVM
{
	public class TeamListVM
	{
		public int Id { get; set; }
		public string CreatedDate { get; set; } = null!;
		public string? UpdatedDate { get; set; }


		public string FullName { get; set; } = null!;
		public string Title { get; set; } = null!;
		public string FileName { get; set; } = null!;
		public string FileType { get; set; } = null!;
		public string? Twitter { get; set; }
		public string? LinkedIn { get; set; }
		public string? Facebook { get; set; }
		public string? Instagram { get; set; }
	}
}
