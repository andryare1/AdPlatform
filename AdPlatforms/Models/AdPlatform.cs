using System;
namespace AdPlatforms.Models
{
	public class AdPlatform
	{
		public string Name { get; set; } 
		public List<string> Location { get; set; }

		public AdPlatform( string name, List<string> location)
		{
			Name = name;
			Location = location;
		}
	}
}

