using CoralSeaTaskManagment.Data.Data;
using CoralSeaTaskManagment.Data.Implementation;
using CoralSeaTaskManagment.Model.Repositories;
using CoralSeaTaskManagment.Repositories;

namespace CoralSeaTaskManagment.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IHotelRepository Hotel { get; private set; }
        public IAssignRepository Assign { get; private set; }
        public IDepartmentRepository Department { get; private set; }
        public IEcategoryRepository Ecategory { get; private set; }
        public IEclassRepository Eclass { get; private set; }
        public IEfamilyRepository Efamily { get; private set; }
        public IEstatusRepository Estatus { get; private set; }
        public ILocationRepository Location { get; private set; }
        public IOtypeRepository Otype { get; private set; }

        public IPeriorityRepository Periority { get; private set; }

        //public IPickupRepository Pickup { get; private set; }

        //public IApplicationUserRepository ApplicationUser { get; private set; }

        public IOrderRepository Order { get; private set; }

        //public IOstatusRepository Ostatus { get; private set; }

        //public IEspareRepository Espare { get; private set; }

        public IAssignationRepository Assignation { get; private set; }

        public IItemRepository Item { get; private set; }

        public IGitemRepository Gitem { get; private set; }

        public IGorderRepository Gorder { get; private set; }
        public IGlocationRepository Glocation { get; private set; }
        public IGroomsRepository Grooms { get; private set; }
        public IOutgoingRepository Outgoing { get; private set; }
        public ISchedualeRepository Scheduale { get; private set; }
        public ISpartRepository Spart { get; private set; }

        //public IOclassRepository Oclass { get; private set; }

        //public IGpickupRepository Gpickup { get; private set; }

        //public IGassignationRepository Gassignation { get; private set; }

        //public IResolveRepository Resolve { get; private set; }

        //public IReopenRepository Reopen { get; private set; }

        //public ICancelRepository Cancel { get; private set; }

        //public ICloseRepository Close { get; private set; }
        //public ISyslogRepository Syslog { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Hotel=new HotelRepository(context);
            Assign=new AssignRepository(context);
            Department = new DepartmentRepository(context);
            Ecategory = new EcategoryRepository(context);
            Eclass = new EclassRepository(context);
            Efamily = new EfamilyRepository(context);
            Estatus = new EstatusRepository(context);
            Location = new LocationRepository(context);
            Otype = new OtypeRepository(context);
            Periority=new PeriorityRepository(context);
            //Pickup=new PickupRepository(context);
            //ApplicationUser=new ApplicationUserRepository(context);
            Order=new OrderRepository(context);
            //Ostatus=new OstatusRepository(context);
            //Espare=new EspareRepository(context);
            Assignation=new AssignationRepository(context);
            Item=new ItemRepository(context);
            Gitem=new GitemRepository(context);
            Glocation=new GlocationRepository(context);
            Gorder=new GorderRepository(context);
            Grooms=new GroomsRepository(context);
            Outgoing=new OutgoingRepository(context);
            Spart=new SpartRepository(context);
            Scheduale=new SchedualeRepository(context);
            //Oclass=new OclassRepository(context);
            //Gpickup=new GpickupRepository(context);
            //Gassignation=new GassignationRepository(context);
            //Cancel=new CancelRepository(context);
            //Close=new CloseRepository(context);
            //Resolve=new ResolveRepository(context);
            //Reopen=new ReopenRepository(context);
            //Syslog=new SyslogRepository(context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public Task CompleteAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
