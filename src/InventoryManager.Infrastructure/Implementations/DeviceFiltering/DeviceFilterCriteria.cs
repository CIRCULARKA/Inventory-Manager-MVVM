namespace InventoryManager.Infrastructure.Filtering
{
	public class DeviceFilteringCriteria
	{
		public DeviceFilteringCriteria(string criteriaName)
		{
			CriteriaName = criteriaName;
		}

		public string CriteriaName { get; }

		/// <summary>
		/// Default it true
		/// </summary>
		public bool State { get; set; } = true;
	}
}
