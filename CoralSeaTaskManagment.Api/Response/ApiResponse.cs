namespace CoralSeaTaskManagment.Api.Response
{
    public static class ApiResponse
    {
        public static int statusCode { get; set; }
        public static string Message { get; set; }
        public static object Data { get; set; }
        
        public static void SetResponse(int _statusCode,string _Message,object _Data) 
        {
            statusCode = _statusCode;
            Message = _Message;
            Data = _Data;
        }
    }
    
}
