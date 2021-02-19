using System.Collections.Generic;

namespace InventoryManager.Models
{
	public interface IDeviceMovementHistoryNoteRepository
	{
		void FixDeviceMovement(DeviceMovementHistoryNote newNote);

		void RemoveMovementNote(DeviceMovementHistoryNote noteToRemove);

		void UpdateMovementNote(DeviceMovementHistoryNote noteToUpdate);
	}
}
