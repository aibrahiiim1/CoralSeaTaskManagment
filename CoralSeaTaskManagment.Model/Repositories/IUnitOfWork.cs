
using CoralSeaTaskManagment.Model.Repositories;

namespace CoralSeaTaskManagment.Repositories
{
    public interface IUnitOfWork:IDisposable
    {
        IHotelRepository Hotel { get; }
        IAssignRepository Assign { get; }
        IDepartmentRepository Department { get; }
        IEcategoryRepository Ecategory { get; }
        IEclassRepository Eclass { get; }
        IEfamilyRepository Efamily { get; }
        IEstatusRepository Estatus { get; }
        ILocationRepository Location { get; }
        IOtypeRepository Otype { get; }
        IPeriorityRepository Periority { get; }
        //IPickupRepository Pickup { get; }
        //IApplicationUserRepository ApplicationUser { get; }
        IOrderRepository Order { get; }
        //IOstatusRepository Ostatus { get; }
        //IEspareRepository Espare { get; }
        IAssignationRepository Assignation { get; }
        IItemRepository Item { get; }
        IGitemRepository Gitem { get; }
        IGorderRepository Gorder { get; }
        IGlocationRepository Glocation { get; }
        IGroomsRepository Grooms { get; }
        IOutgoingRepository Outgoing { get; }
        ISchedualeRepository Scheduale { get; }
        ISpartRepository Spart { get; }
        //IOclassRepository Oclass { get; }
        //IGpickupRepository Gpickup { get; }
        //IGassignationRepository Gassignation { get; }
        //IResolveRepository Resolve { get; }
        //IReopenRepository Reopen { get; }
        //ICancelRepository Cancel { get; }
        //ICloseRepository Close { get; }
        //ISyslogRepository Syslog { get; }
        int Complete();
        Task CompleteAsync();
    }
}
