using Newtonsoft.Json;

namespace PaaspopService.Common.Middleware
{
    public class ExceptionObject
    {
        public string Message { get; set; }
        public string InnerExceptionMessage { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}