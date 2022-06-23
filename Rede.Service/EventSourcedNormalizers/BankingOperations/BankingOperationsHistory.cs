using System.Text.Json;
using Rede.Domain.Core.Events;

namespace Rede.Service.EventSourcedNormalizers.BankingOperations;

public class BankingOperationsHistory
    {
        public static IList<BankingOperationsHistoryData> HistoryData { get; set; }

        public static IList<BankingOperationsHistoryData> ToJavaScriptCustomerHistory(IList<StoredEvent> storedEvents)
        {
            HistoryData = new List<BankingOperationsHistoryData>();
            BankingOperationsHistoryDeserializer(storedEvents);

            var sorted = HistoryData.OrderBy(c => c.When);
            var list = new List<BankingOperationsHistoryData>();
            var last = new BankingOperationsHistoryData();

            foreach (var change in sorted)
            {
                var jsSlot = new BankingOperationsHistoryData
                {
                    Id = change.Id == Guid.Empty.ToString() || change.Id == last.Id
                        ? ""
                        : change.Id,
                    OriginAccount = string.IsNullOrWhiteSpace(change.OriginAccount) || change.OriginAccount == last.OriginAccount
                        ? ""
                        : change.OriginAccount,
                    DestinationAccount = string.IsNullOrWhiteSpace(change.DestinationAccount) || change.DestinationAccount == last.DestinationAccount
                        ? ""
                        : change.DestinationAccount,
                    Amount = string.IsNullOrWhiteSpace(change.Amount) || change.Amount == last.Amount
                        ? ""
                        : change.Amount,
                    Operation =  string.IsNullOrWhiteSpace(change.Operation) || change.Operation == last.Operation
                        ? ""
                        : change.Operation,
                    Action = string.IsNullOrWhiteSpace(change.Action) ? "" : change.Action,
                    When = change.When,
                    Who = change.Who
                };

                list.Add(jsSlot);
                last = change;
            }
            return list;
        }

        private static void BankingOperationsHistoryDeserializer(IEnumerable<StoredEvent> storedEvents)
        {
            foreach (var e in storedEvents)
            {
                var slot = new BankingOperationsHistoryData();
                dynamic values;

                switch (e.MessageType)
                {
                    case "CustomerRegisteredEvent":
                        values = JsonSerializer.Deserialize<Dictionary<string, string>>(e.Data);
                        slot.OriginAccount = values["OriginAccount"];
                        slot.DestinationAccount = values["DestinationAccount"];
                        slot.Amount = values["Amount"];
                        slot.Operation = values["Operation"];
                        slot.Action = "Registered";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;
                    case "CustomerUpdatedEvent":
                        values = JsonSerializer.Deserialize<Dictionary<string, string>>(e.Data);
                        slot.OriginAccount = values["OriginAccount"];
                        slot.DestinationAccount = values["DestinationAccount"];
                        slot.Amount = values["Amount"];
                        slot.Operation = values["Operation"];
                        slot.Action = "Updated";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;
                    case "CustomerRemovedEvent":
                        values = JsonSerializer.Deserialize<Dictionary<string, string>>(e.Data);
                        slot.Action = "Removed";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;
                }
                HistoryData.Add(slot);
            }
        }
    }
