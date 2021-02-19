namespace InventoryManager.Models
{
	public interface IDeviceMovementHistoryNoteRepository : IRepository
	{
		void FixDeviceMovement(DeviceMovementHistoryNote newNote) =>
			DataContext.DeviceMovementHistory.Add(newNote);

		void RemoveDeviceMovementNote(DeviceMovementHistoryNote noteToRemove) =>
			DataContext.DeviceMovementHistory.Remove(noteToRemove);

		void UpdateMovementNote(DeviceMovementHistoryNote noteToUpdate) =>
			DataContext.DeviceMovementHistory.Update(noteToUpdate);
	}
}
