namespace InventoryManager.Models
{
	public class Model
	{
		public static Device Device { get; } = new Device();

		public static Account Account { get; } = new Account();

		public static Cabinet Cabinet { get; } = new Cabinet();

		public static Certificate Certificate { get; } = new Certificate();

		public static DeviceType DeviceType { get; } = new DeviceType();

		public static Group Group { get; } = new Group();

		public static Housing Housing { get; } = new Housing();

		public static IPAddress IPAddress { get; } = new IPAddress();

		public static User User { get; } = new User();
	}
}
