using System.Linq;
using System.Collections.Generic;

namespace InventoryManager.Models
{
	public class Account : ModelBase<Account>
	{
		private List<Account> _allAccounts;

		public Account() =>
			_allAccounts = DataContext.Accounts.ToList();

		public int ID { get; set; }

		public string Login { get; set; }

		public string Password { get; set; }

		public int DeviceID { get; set; }

		public Device Device { get; set; }

		public override List<Account> All() => _allAccounts;
	}
}
