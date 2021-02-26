namespace InventoryManager.Models
{
	public interface IDeviceMovementHistoryNoteRepository : IRepository
	{
		void FixDeviceMovement(DeviceMovementHistoryNote newNote);

		void RemoveDeviceMovementNote(DeviceMovementHistoryNote noteToRemove);

		void UpdateMovementNote(DeviceMovementHistoryNote noteToUpdate);
	}
}
