using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
	class RecommendationModel
	{
	}

	public enum RecommendationType { One, Two, Three }

	public class ReviewViewModel
	{
		public IEnumerable<RecommendationViewModel> Recommendations { get; set; }
	}

	public class RecommendationViewModel
	{
		public RecommendationType RecommendationType { get; set; }
		public bool IsChecked { get; set; }
	}
}
