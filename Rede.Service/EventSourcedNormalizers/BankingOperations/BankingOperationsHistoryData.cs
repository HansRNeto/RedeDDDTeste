namespace Rede.Service.EventSourcedNormalizers.BankingOperations;

public class BankingOperationsHistoryData
    {
        public string Action { get; set; }
        public string Id { get; set; }
        public string OriginAccount { get; set; }
        public string DestinationAccount { get; set; }
        public string Amount { get; set; }
        public string Operation { get; set; }
        public string When { get; set; }
        public string Who { get; set; }
    }

