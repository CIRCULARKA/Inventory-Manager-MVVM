namespace InventoryManager.Models
{
	public interface IDeviceMovementHistoryNoteRepository : IRepository
	{
		void FixDeviceMovement(DeviceMovementHistoryNote newNote) =>
			DataContext.DeviceMovementHistoryNotes.Add(newNote);

		void RemoveDeviceMovementNote(DeviceMovementHistoryNote noteToRemove) =>
			DataContext.DeviceMovementHistoryNotes.Remove(noteToRemove);

		void UpdateMovementNote(DeviceMovementHistoryNote noteToUpdate) =>
			DataContext.DeviceMovementHistoryNotes.Update(noteToUpdate);
	}
}
