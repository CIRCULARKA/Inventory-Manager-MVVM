using System;

namespace InventoryManager.Reports
{
	public class PropertyDisplayInfo
	{
		private string _displayName;

		public PropertyDisplayInfo(string propertyName, string displayName)
		{
			if (propertyName == null || displayName == null)
				throw new Exception("Property name or display name can't be null");

			PropertyName = propertyName;
			_displayName = displayName;
		}

		public PropertyDisplayInfo(string propertyName) =>
			PropertyName = propertyName;

		public string PropertyName { get; }

		/// <summary>
		/// Returns PropertyName if null
		/// </summary>
		public string DisplayName => _displayName ?? PropertyName;
	}
}
