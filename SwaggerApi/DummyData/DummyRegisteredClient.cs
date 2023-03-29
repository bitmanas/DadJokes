namespace SwaggerApi.DummyData
{
    public class DummyRegisteredClient
    {
        public static List<DummyClient> Clients;
        static DummyRegisteredClient()
        {
            Clients = GetClients();
        }
        private static List<DummyClient> GetClients()
        {
            List<DummyClient> clientList = new List<DummyClient>();

            //Client 1 with Free Plan  (5 hits)
            clientList.Add(new DummyClient
            {
                ClientId = 1,
                ApiKey = "ClientKey1",
                KeyCreateDate = DateTime.Now,
                NoOfHitToday = 0,
                Plan = new ConsumePlan { PlanId = 1, PlanType = PlanType.Free, MaxHitPerDay = 5 }
            });

            //Client 2 with Basic Plan (10 hits)
            clientList.Add(new DummyClient
            {
                ClientId = 2,
                ApiKey = "ClientKey2",
                KeyCreateDate = DateTime.Now,
                NoOfHitToday = 0,
                Plan = new ConsumePlan { PlanId = 2, PlanType = PlanType.Basic, MaxHitPerDay = 10 }
            });

            //Client 3 with Premium Plan (20 hits)
            clientList.Add(new DummyClient
            {
                ClientId = 3,
                ApiKey = "ClientKey3",
                KeyCreateDate = DateTime.Now,
                NoOfHitToday = 0,
                Plan = new ConsumePlan { PlanId = 3, PlanType = PlanType.Premium, MaxHitPerDay = 20 }
            });

            return clientList;
        }
    }

    public class DummyClient
    {
        public int ClientId { get; set; }
        public string ApiKey { get; set; }
        public DateTime KeyCreateDate { get; set; }
        public int NoOfHitToday { get; set; }
        public ConsumePlan Plan { get; set; }
    }

    public class ConsumePlan
    {
        public int PlanId { get; set; }
        public PlanType PlanType { get; set; }
        public int MaxHitPerDay { get; set; }
    }

    public enum PlanType
    {
        Free = 0,
        Basic = 1,
        Premium = 2
    }
}
