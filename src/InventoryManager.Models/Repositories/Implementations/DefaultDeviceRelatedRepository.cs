using Microsoft.EntityFrameworkCore;
using InventoryManager.Data;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManager.Models
{
	public class DefaultDeviceRelatedRepository : IDeviceRelatedRepository
	{
		BaseDbContext DataContext { get; } = new DefaultDbContext();

		public void AddDevice(Device newDevice) =>
			DataContext.Devices.Add(newDevice);

		public void RemoveDevice(Device deviceToDelete) =>
			DataContext.Devices.Remove(deviceToDelete);

		public void UpdateDevice(Device deviceToUpdate) =>
			DataContext.Devices.Update(deviceToUpdate);

		public void FindDevice(params object[] keys) =>
			DataContext.Devices.Find(keys);

		public IEnumerable<Device> AllDevices =>
			DataContext.
			Devices.
			Include(d => d.Cabinet).
				ThenInclude(c => c.Housing).
			Include(d => d.DeviceType).
			ToList();

		public void FixDeviceMovement(DeviceMovementHistoryNote newNote) =>
			DataContext.DeviceMovementHistoryNotes.Add(newNote);

		public void RemoveDeviceMovementNote(DeviceMovementHistoryNote noteToRemove) =>
			DataContext.DeviceMovementHistoryNotes.Remove(noteToRemove);

		public void UpdateMovementNote(DeviceMovementHistoryNote noteToUpdate) =>
			DataContext.DeviceMovementHistoryNotes.Update(noteToUpdate);

		public void AddDeviceAccount(DeviceAccount newAcc) =>
			DataContext.DeviceAccounts.Add(newAcc);

		public void RemoveDeviceAccount(DeviceAccount accToRemove) =>
			DataContext.DeviceAccounts.Remove(accToRemove);

		public void UpdateDeviceAccount(DeviceAccount accToUpdate) =>
			DataContext.DeviceAccounts.Update(accToUpdate);

		public void AddDeviceType(DeviceType newType) =>
			DataContext.DeviceTypes.Add(newType);

		public void RemoveDeviceType(DeviceType typeToRemove) =>
			DataContext.DeviceTypes.Remove(typeToRemove);

		public void UpdateDeviceType(DeviceType typeToUpdate) =>
			DataContext.DeviceTypes.Update(typeToUpdate);

		public IEnumerable<DeviceType> AllDeviceTypes =>
			DataContext.DeviceTypes.ToList();

		public void AddIPAddress(IPAddress newIP) =>
			DataContext.IPAddresses.Add(newIP);

		public void RemoveIPAddress(IPAddress IPtoRemove) =>
			DataContext.IPAddresses.Remove(IPtoRemove);

		public void UpdateIPAddress(IPAddress IPtoUpdate) =>
			DataContext.IPAddresses.Update(IPtoUpdate);

		public IEnumerable<IPAddress> AllIPAddresses =>
			DataContext.IPAddresses.ToList();

		public void AddHousing(Housing newHousing) =>
			DataContext.Housings.Add(newHousing);

		public void RemoveHousing(Housing housingToRemove) =>
			DataContext.Housings.Remove(housingToRemove);

		public void UpdateHousing(Housing housingToUpdate) =>
			DataContext.Housings.Update(housingToUpdate);

		public IEnumerable<Housing> AllHousings =>
			DataContext.Housings.ToList();

		public IQueryable<DeviceAccount> GetAllDeviceAccounts(Device device) =>
			DataContext.DeviceAccounts.Where(a => a.DeviceID == device.ID);

		public IQueryable<DeviceMovementHistoryNote> GetAllDeviceHistoryNotes(Device device) =>
			DataContext.
			DeviceMovementHistoryNotes.
			Include(h => h.TargetCabinet).
			Where(dmn => dmn.DeviceID == device.ID).
			OrderByDescending(dmn => dmn.ID);

		public IQueryable<IPAddress> GetAllDeviceIPAddresses(Device device) =>
			DataContext.IPAddresses.Where(ip => ip.DeviceID == device.ID);

		public void DeleteAllDeviceMovementHistory(Device device)
		{
			DataContext.
			DeviceMovementHistoryNotes.
			RemoveRange(
				DataContext.
				DeviceMovementHistoryNotes.
				Where(n => n.DeviceID == device.ID)
			);
		}

		public void AddCabinet(Cabinet newCabinet) =>
			DataContext.Cabinets.Add(newCabinet);

		public void RemoveCabinet(Cabinet cabinetToRemove) =>
			DataContext.Cabinets.Remove(cabinetToRemove);

		public void UpdateCabinet(Cabinet cabinetToUpdate) =>
			DataContext.Cabinets.Update(cabinetToUpdate);

		public Cabinet FindCabinet(params object[] keys) =>
			DataContext.Cabinets.Find(keys);

		public IEnumerable<Cabinet> AllCabinets =>
			DataContext.
			Cabinets.
			Include(c => c.Housing).
			ToList();

		public void SaveChanges() => DataContext.SaveChanges();
	}
}
