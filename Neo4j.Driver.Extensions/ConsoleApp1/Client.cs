namespace ConsoleApp1
{
    public class Client
    {
        public long? Id { get; set; }
        
        public string ExternalSystemName { get; set; }

        public string ExternalSystemId { get; set; }

        public string ImportSystemName { get; set; }

        public string StateJurisdiction { get; set; }

        public Person Person { get; set; }
    }
}
