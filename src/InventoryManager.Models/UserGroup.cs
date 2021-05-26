namespace InventoryManager.Models
{
	public class UserGroup
	{
		public int ID { get; set; }

		public string Name { get; set; }

		public override string ToString() => Name;
	}
}
