namespace CoralSeaTaskManagment.Ui.Models
{
    public class ApiRequest
    {


        public int statusCode { get; set; }
        public string message { get; set; }
        public Datum[] data { get; set; }


        public class Datum
        {
            public int id { get; set; }
            public string name { get; set; }
            public string nameAr { get; set; }
            public Hotel hotel { get; set; }
            public DateTime createdTime { get; set; }
        }

        public class Hotel
        {
            public int id { get; set; }
            public string name { get; set; }
        }

    }
}
