using System.Text.Json;
using Rede.Domain.Core.Events;

namespace Rede.Service.EventSourcedNormalizers.CurrencyAccount;

public class CurrencyAccountHistory
    {
        public static IList<CurrencyAccountHistoryData> HistoryData { get; set; }

        public static IList<CurrencyAccountHistoryData> ToJavaScriptCustomerHistory(IList<StoredEvent> storedEvents)
        {
            HistoryData = new List<CurrencyAccountHistoryData>();
            CurrencyAccountHistoryDeserializer(storedEvents);

            var sorted = HistoryData.OrderBy(c => c.When);
            var list = new List<CurrencyAccountHistoryData>();
            var last = new CurrencyAccountHistoryData();

            foreach (var change in sorted)
            {
                var jsSlot = new CurrencyAccountHistoryData
                {
                    Id = change.Id == Guid.Empty.ToString() || change.Id == last.Id
                        ? ""
                        : change.Id,
                    NumberAccount = string.IsNullOrWhiteSpace(change.NumberAccount) || change.NumberAccount == last.NumberAccount
                        ? ""
                        : change.NumberAccount,
                    Digit = string.IsNullOrWhiteSpace(change.Digit) || change.Digit == last.Digit
                        ? ""
                        : change.Digit,
                    Balance = string.IsNullOrWhiteSpace(change.Balance) || change.Balance == last.Balance
                        ? ""
                        : change.Balance,
                    CustomerId =  string.IsNullOrWhiteSpace(change.CustomerId) || change.CustomerId == last.CustomerId
                        ? ""
                        : change.CustomerId,
                    Action = string.IsNullOrWhiteSpace(change.Action) ? "" : change.Action,
                    When = change.When,
                    Who = change.Who
                };

                list.Add(jsSlot);
                last = change;
            }
            return list;
        }

        private static void CurrencyAccountHistoryDeserializer(IEnumerable<StoredEvent> storedEvents)
        {
            foreach (var e in storedEvents)
            {
                var slot = new CurrencyAccountHistoryData();
                dynamic values;

                switch (e.MessageType)
                {
                    case "CustomerRegisteredEvent":
                        values = JsonSerializer.Deserialize<Dictionary<string, string>>(e.Data);
                        slot.NumberAccount = values["NumberAccount"];
                        slot.Digit = values["Digit"];
                        slot.Balance = values["Balance"];
                        slot.CustomerId = values["CustomerId"];
                        slot.Action = "Registered";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;
                    case "CustomerUpdatedEvent":
                        values = JsonSerializer.Deserialize<Dictionary<string, string>>(e.Data);
                        slot.NumberAccount = values["NumberAccount"];
                        slot.Digit = values["Digit"];
                        slot.Balance = values["Balance"];
                        slot.CustomerId = values["CustomerId"];
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
