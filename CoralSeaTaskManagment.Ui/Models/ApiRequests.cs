namespace CoralSeaTaskManagment.Ui.Models
{
    public static class ApiRequests
    {
        //Departments
        public static string BasicUrl = "https://localhost:7097/api/";
        public static string DepartmentApi => BasicUrl + "department";
        public static string DepartmentCreate => BasicUrl + "department/Create";
        public static string DepartmentUpdate => BasicUrl + "department";
        public static string DepartmentDelete => BasicUrl + "department";

        // Hotels
        public static string HotelApi => BasicUrl + "hotel";
        public static string HotelCreate => BasicUrl + "hotel/Create";
        public static string HotelUpdate => BasicUrl + "hotel";
        public static string HotelDelete => BasicUrl + "hotel";        
        
        // Locations
        public static string LocationApi => BasicUrl + "Location";
        public static string LocationCreate => BasicUrl + "Location/Create";
        public static string LocationUpdate => BasicUrl + "Location";
        public static string LocationDelete => BasicUrl + "Location";        
        public static string LocationByDep => BasicUrl + "Location/GetByDep";        
        
        // Assign
        public static string AssignApi => BasicUrl + "Assign";
        public static string AssignCreate => BasicUrl + "Assign";
        public static string AssignUpdate => BasicUrl + "Assign";
        public static string AssignDelete => BasicUrl + "Assign";        
        
        // Assign
        public static string OtypeApi => BasicUrl + "Otype";
        public static string OtypeCreate => BasicUrl + "Otype";
        public static string OtypeUpdate => BasicUrl + "Otype";
        public static string OtypeDelete => BasicUrl + "Otype";
        public static string OtypeEnum => BasicUrl + "Otype/Enum";


        // Periority
        public static string PeriorityApi => BasicUrl + "Periority";
        public static string PeriorityCreate => BasicUrl + "Periority/Create";
        public static string PeriorityUpdate => BasicUrl + "Periority";
        public static string PeriorityDelete => BasicUrl + "Periority";      
        
        // Ecategory
        public static string EcategoryApi => BasicUrl + "Ecategory";
        public static string EcategoryCreate => BasicUrl + "Ecategory";
        public static string EcategoryUpdate => BasicUrl + "Ecategory";
        public static string EcategoryDelete => BasicUrl + "Ecategory";

        // Eclass
        public static string EclassApi => BasicUrl + "Eclass";
        public static string EclassCreate => BasicUrl + "Eclass";
        public static string EclassUpdate => BasicUrl + "Eclass";
        public static string EclassDelete => BasicUrl + "Eclass";

        // Efamily
        public static string EfamilyApi => BasicUrl + "Efamily";
        public static string EfamilyCreate => BasicUrl + "Efamily";
        public static string EfamilyUpdate => BasicUrl + "Efamily";
        public static string EfamilyDelete => BasicUrl + "Efamily";

        // Item
        public static string ItemApi => BasicUrl + "Item";
        public static string ItemCreate => BasicUrl + "Item/Create";
        public static string ItemUpdate => BasicUrl + "Item";
        public static string ItemDelete => BasicUrl + "Item";        
        
        // Gitem
        public static string GitemApi => BasicUrl + "Gitem";
        public static string GitemCreate => BasicUrl + "Gitem/Create";
        public static string GitemUpdate => BasicUrl + "Gitem";
        public static string GitemDelete => BasicUrl + "Gitem";
        public static string GitemByLoc => BasicUrl + "Gitem/GetByLoc";

        // Glocation
        public static string GlocationApi => BasicUrl + "Glocation";
        public static string GlocationCreate => BasicUrl + "Glocation/Create";
        public static string GlocationUpdate => BasicUrl + "Glocation";
        public static string GlocationDelete => BasicUrl + "IGlocationtem";

        // Grooms
        public static string GroomsApi => BasicUrl + "Grooms";
        public static string GroomsCreate => BasicUrl + "Grooms/Create";
        public static string GroomsUpdate => BasicUrl + "Grooms";
        public static string GroomsDelete => BasicUrl + "Grooms";       
        
        // Order
        public static string OrderApi => BasicUrl + "Order";
        public static string OrderCreate => BasicUrl + "Order/Create";
        public static string OrderUpdate => BasicUrl + "Order";
        public static string OrderDelete => BasicUrl + "Order";        
        public static string OrderUpdateAss => BasicUrl + "Order/Assign";        
        
        // Gorder
        public static string GorderApi => BasicUrl + "Gorder";
        public static string GorderCreate => BasicUrl + "Gorder/Create";
        public static string GorderUpdate => BasicUrl + "Gorder";
        public static string GorderDelete => BasicUrl + "Gorder";        
        
        // Assignation
        public static string AssignationApi => BasicUrl + "Assignation";
        public static string AssignationCreate => BasicUrl + "Assignation/Create";
        public static string AssignationUpdate => BasicUrl + "Assignation";
        public static string AssignationDelete => BasicUrl + "Assignation";


        // Outgoing
        public static string OutgoingApi => BasicUrl + "Outgoing";
        public static string OutgoingCreate => BasicUrl + "Outgoing";
        public static string OutgoingUpdate => BasicUrl + "Outgoing";
        public static string OutgoingDelete => BasicUrl + "Outgoing";        
        
        // Scheduale
        public static string SchedualeApi => BasicUrl + "Scheduale";
        public static string SchedualeCreate => BasicUrl + "Scheduale/Create";
        public static string SchedualeUpdate => BasicUrl + "Scheduale";
        public static string SchedualeDelete => BasicUrl + "Scheduale";       
        
        // Spart
        public static string SpartApi => BasicUrl + "Spart";
        public static string SpartCreate => BasicUrl + "Spart";
        public static string SpartUpdate => BasicUrl + "Spart";
        public static string SpartDelete => BasicUrl + "Spart";
    }
}
