using System.Linq;
using System.Collections.Generic;

namespace InventoryManager.Models
{
	public class Account : ModelBase<Account>
	{
		public int ID { get; set; }

		public string Login { get; set; }

		public string Password { get; set; }

		public override List<Account> All() =>
			DataContext.Accounts.ToList();
	}
}
