using CoralSeaTaskManagment.Data.Data;
using CoralSeaTaskManagment.Data.Migrations;
using CoralSeaTaskManagment.Implementation;
using CoralSeaTaskManagment.Model.Models.Domain;
using CoralSeaTaskManagment.Model.Repositories;
using CoralSeaTaskManagment.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoralSeaTaskManagment.Data.Implementation
{
    public class OutgoingRepository : GenericRepository<Outgoing>, IOutgoingRepository
    {
        private readonly ApplicationDbContext _context;
        public OutgoingRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Outgoing outgoing)
        {
            var outgoingionindb = _context.Outgoings.FirstOrDefault(x => x.Id == outgoing.Id);
            if (outgoingionindb != null)
            {  

                outgoingionindb.CreatedTime = DateTime.Now;
                outgoingionindb.OrderId = outgoing.OrderId;
                outgoingionindb.HotelId = outgoing.HotelId;
                outgoingionindb.ItemId = outgoing.ItemId;
                outgoingionindb.ApplicationUserId = outgoing.ApplicationUserId;
                outgoingionindb.Price = outgoing.Price;
                outgoingionindb.No = outgoing.No;
                outgoingionindb.ReturnDate = outgoing.ReturnDate;
                outgoingionindb.Remark = outgoing.Remark;
                outgoingionindb.CompanyName = outgoing.CompanyName;
            }
        }
    }
}
