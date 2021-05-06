using Microsoft.EntityFrameworkCore;
using InventoryManager.Data;
using System.Collections.Generic;
using System.Linq;
using System;

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

		public IEnumerable<IPAddress> AllIPAddresses =>
			DataContext.IPAddresses.ToList();

		public IQueryable<IPAddress> AllAvailableIPAddresses =>
			DataContext.IPAddresses.Where(ip => ip.DeviceID == null);

		public void SetNewRangeOfIPAddresses(IEnumerable<IPAddress> range)
		{
			DataContext.IPAddresses.RemoveRange(
				DataContext.IPAddresses
			);

			DataContext.IPAddresses.AddRange(range);
		}

		public void AddIPToDevice(IPAddress ip, Device device)
		{
			var ipToAssign = DataContext.IPAddresses.Find(ip.ID);
			if (ipToAssign.DeviceID == null)
			{
				ipToAssign.DeviceID = device.ID;
				DataContext.IPAddresses.Update(ipToAssign);
			}
			else
				throw new Exception("Этот IP-адрес занят другим устройством");
		}

		public void RemoveIPFromDevice(IPAddress ip, Device device)
		{
			ip.DeviceID = null;
			DataContext.IPAddresses.Update(ip);
		}

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

		public void AddSoftware(Software softwareToAdd) =>
			DataContext.Software.Add(softwareToAdd);

		public void RemoveSoftware(Software softwareToRemove)
		{
			DataContext.SoftwareConfigurations.RemoveRange(
				DataContext.
					SoftwareConfigurations.
						Where(sc => sc.SoftwareID == softwareToRemove.ID)
			);
			DataContext.Software.Remove(softwareToRemove);
		}

		public void UpdateSoftware(Software softwareToUpdate) =>
			DataContext.Software.Update(softwareToUpdate);

		public Software FindSoftware(params object[] keys) =>
			DataContext.Software.Find(keys);

		public IEnumerable<Software> AllSoftware =>
			DataContext.Software.Include(s => s.Type).ToList();

		public void AddSoftwareType(SoftwareType newType) =>
			DataContext.SoftwareTypes.Add(newType);

		public void RemoveSoftwareType(SoftwareType typeToRemove) =>
			DataContext.SoftwareTypes.Remove(typeToRemove);

		public void UpdateSoftwareType(SoftwareType typeToUpdate) =>
			DataContext.SoftwareTypes.Update(typeToUpdate);

		public SoftwareType FindSoftwareType(params object[] keys) =>
			DataContext.SoftwareTypes.Find(keys);

		public IEnumerable<SoftwareType> AllSoftwareTypes =>
			DataContext.SoftwareTypes.ToList();

		public void AddSoftwareConfiguration(SoftwareConfiguration newConfiguration) =>
			DataContext.SoftwareConfigurations.Add(newConfiguration);

		public void RemoveSoftwareConfiguration(SoftwareConfiguration configToRemove) =>
			DataContext.SoftwareConfigurations.Remove(configToRemove);

		public void UpdateSoftwareConfiguration(SoftwareConfiguration configToUpdate) =>
			DataContext.SoftwareConfigurations.Update(configToUpdate);

		public SoftwareConfiguration FindSoftwareConfiguration(params object[] keys) =>
			DataContext.SoftwareConfigurations.Find(keys);

		public IEnumerable<SoftwareConfiguration> AllSoftwareConfiguration =>
			DataContext.
				SoftwareConfigurations.
					Include(sc => sc.Software).
						ToList();

		public IQueryable<Software> GetAllDeviceSoftware(Device device) =>
			DataContext.
				Software.
					Include(s => s.Type).
						Where(s => s.DeviceID == device.ID);

		public IQueryable<SoftwareConfiguration> GetAllSoftwareConfiguration(Software target) =>
			DataContext.SoftwareConfigurations.Where(sc => sc.SoftwareID == target.ID);

		public SoftwareConfiguration GetSoftwareConfiguration(Software target)
		{
			try
			{
				return DataContext.SoftwareConfigurations.First(sc => sc.SoftwareID == target.ID);
			}
			catch (InvalidOperationException)
			{ throw new Exception("Software has no configuration"); }
		}

		public void SaveChanges() => DataContext.SaveChanges();
	}
}
