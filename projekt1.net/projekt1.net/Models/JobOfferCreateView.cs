using System.Collections.Generic;

namespace projekt1.net.Models
{
	public class JobOfferCreateView : JobOffer
	{
		public IEnumerable<Company> Companies { get; set; }
	}
}
