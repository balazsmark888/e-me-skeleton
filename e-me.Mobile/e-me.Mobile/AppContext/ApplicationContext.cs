using System.Collections.Generic;

namespace e_me.Mobile.AppContext
{
    public class ApplicationContext
    {
        public IDictionary<string, object> ApplicationProperties { get; set; }

        public string BackendBaseAddress { get; set; }
    }
}
