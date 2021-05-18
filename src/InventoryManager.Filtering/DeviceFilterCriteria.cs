namespace InventoryManager.Filtering
{
	public class DeviceFilteringCriteria
	{
		public DeviceFilteringCriteria(string deviceTypeName)
		{
			DeviceTypeName = deviceTypeName;
		}

		public string DeviceTypeName { get; }

		/// <summary>
		/// Default it true
		/// </summary>
		public bool State { get; set; } = true;
	}
}
