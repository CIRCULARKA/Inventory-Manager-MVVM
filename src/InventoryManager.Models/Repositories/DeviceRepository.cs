using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace InventoryManager.Models
{
	public class DeviceRepository : RepositoryBase<Device>
	{
		public override IEnumerable<Device> All =>
			DataContext.Devices.
				Include(d => d.Cabinet).
					ThenInclude(c => c.Housing).
				Include(d => d.DeviceType).ToList();

		public IEnumerable<DeviceType> AllDeviceTypes =>
			DataContext.DeviceTypes.ToList();

		public void AddDeviceAccount(DeviceAccount account) =>
			DataContext.DeviceAccounts.Add(account);

		public IEnumerable<DeviceAccount> AllDeviceAccounts =>
			DataContext.DeviceAccounts.ToList();

		public void RemoveDeviceAccount(DeviceAccount account) =>
			DataContext.DeviceAccounts.Remove(account);

		public void AddIPAddress(IPAddress ip) =>
			DataContext.IPAddresses.Add(ip);

		public void RemoveIPAddress(IPAddress ip) =>
			DataContext.IPAddresses.Remove(ip);

		public IEnumerable<IPAddress> GetAllDeviceIPAddresses(Device device) =>
			DataContext.IPAddresses.Where(ip => ip.DeviceID == device.ID);

		public void NoteDeviceMovemet(DeviceMovementHistory note) =>
			DataContext.DeviceMovementHistory.Add(note);

		public void RemoveDeviceMovementNote(DeviceMovementHistory note) =>
			DataContext.DeviceMovementHistory.Remove(note);

		public IEnumerable<DeviceMovementHistory> GetAllDeviceHistoryNotes(Device target) =>
			DataContext.DeviceMovementHistory.ToList();
	}
}
