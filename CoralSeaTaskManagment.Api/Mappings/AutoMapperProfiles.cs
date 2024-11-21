using AutoMapper;
using CoralSeaTaskManagment.Api.Models.DTO;
using CoralSeaTaskManagment.Model.Models.Domain;

namespace CoralSeaTaskManagment.Api.Mappings
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            ///// this is the example of mapping between dto and domain " source , destenation "
           // CreateMap<DTOModel,DomainModel>()
           //// if the property for example Name is deferent of the another property you can add the below line for mapping
           ///.ForMember(x=>x.Name,Option=>option.MapFrom(x=>x.FullName))
           //.ReverseMap();

            // Hotel
            CreateMap<Hotel,HotelDto>().ReverseMap();
            CreateMap<HotelAddDto,Hotel>().ReverseMap();
            CreateMap<HotelUpdateDto,Hotel>().ReverseMap();

            // Department
            CreateMap<Department,DepartmentDto>().ReverseMap();
            CreateMap<DepartmentAddDto,Department>().ReverseMap();
            CreateMap<DepartmentUpdateDto, Department>().ReverseMap();  
            
            // Location
            CreateMap<Location,LocationDto>().ReverseMap();
            CreateMap<LocationAddDto, Location>().ReverseMap();
            CreateMap<LocationUpdateDto, Location>().ReverseMap();            
            
            // Assign
            CreateMap<Assign, AssignDto>().ReverseMap();
            CreateMap<AssignAddDto, Assign>().ReverseMap();
            CreateMap<AssignUpdateDto, Assign>().ReverseMap();            
            
            // Ecategory
            CreateMap<Ecategory, EcategoryDto>().ReverseMap();
            CreateMap<EcategoryAddDto, Ecategory>().ReverseMap();
            CreateMap<EcategoryUpdateDto, Ecategory>().ReverseMap();            
            
            // Efamily
            CreateMap<Efamily, EfamilyDto>().ReverseMap();
            CreateMap<EfamilyAddDto, Efamily>().ReverseMap();
            CreateMap<EfamilyUpdateDto, Efamily>().ReverseMap();            
            
            // Eclass
            CreateMap<Eclass, EclassDto>().ReverseMap();
            CreateMap<EclassAddDto, Eclass>().ReverseMap();
            CreateMap<EclassUpdateDto, Eclass>().ReverseMap();
            
            // Estatus
            CreateMap<Estatus, EstatusDto>().ReverseMap();
            CreateMap<EstatusAddDto, Estatus>().ReverseMap();
            CreateMap<EstatusUpdateDto, Estatus>().ReverseMap();            
            
            // Otype
            CreateMap<Otype, OtypeDto>().ReverseMap();
            CreateMap<OtypeAddDto, Otype>().ReverseMap();
            CreateMap<OtypeUpdateDto, Otype>().ReverseMap();          
            
            
            // Periority
            CreateMap<Periority, PeriorityDto>().ReverseMap();
            CreateMap<PeriorityAddDto, Periority>().ReverseMap();
            CreateMap<PeriorityUpdateDto, Periority>().ReverseMap();            
            
            // Item
            CreateMap<Item, ItemDto>().ReverseMap();
            CreateMap<ItemAddDto, Item>().ReverseMap();
            CreateMap<ItemUpdateDto, Item>().ReverseMap();

            // Grooms
            CreateMap<Grooms, GroomDto>().ReverseMap();
            CreateMap<GroomAddDto, Grooms>().ReverseMap();
            CreateMap<GroomUpdateDto, Grooms>().ReverseMap();           
            
            // Glocation
            CreateMap<Glocation, GlocationDto>().ReverseMap();
            CreateMap<GlocationAddDto, Glocation>().ReverseMap();
            CreateMap<GlocationUpdateDto, Glocation>().ReverseMap();

            // Gitem
            CreateMap<Gitem, GitemDto>().ReverseMap();
            CreateMap<GitemAddDto, Gitem>().ReverseMap();
            CreateMap<GitemUpdateDto, Gitem>().ReverseMap();         
            
            
            // Order
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<OrderAddDto, Order>().ReverseMap();
            CreateMap<OrderUpdateDto, Order>().ReverseMap();            
            
            // Gorder
            CreateMap<Gorder, GorderDto>().ReverseMap();
            CreateMap<GorderAddDto, Gorder>().ReverseMap();
            CreateMap<GorderUpdateDto, Gorder>().ReverseMap();            
            
            // Assignation
            CreateMap<Assignation, AssignationDto>().ReverseMap();
            CreateMap<AssignationAddDto, Assignation>().ReverseMap();
            CreateMap<AssignationUpdateDto, Assignation>().ReverseMap();

            // Outgoung
            CreateMap<Outgoing, OutgoingDto>().ReverseMap();
            CreateMap<OutgoingAddDto, Outgoing>().ReverseMap();
            CreateMap<OutgoingUpdateDto, Outgoing>().ReverseMap();            
            
            // Scheduale
            CreateMap<Scheduale, SchedualeDto>().ReverseMap();
            CreateMap<SchedualeAddDto, Scheduale>().ReverseMap();
            CreateMap<SchedualeUpdateDto, Scheduale>().ReverseMap();

            // Spare
            CreateMap<Spart, SpartDto>().ReverseMap();
            CreateMap<SpartAddDto, Spart>().ReverseMap();
            CreateMap<SpartUpdateDto, Spart>().ReverseMap();



        }

        //public class DTOModel
        //{
        //    public String Name { get; set; }
        //    //public String FullName { get; set; }
        //}        
        //public class DomainModel
        //{
        //    public String Name { get; set; }
        //}

    }
}
