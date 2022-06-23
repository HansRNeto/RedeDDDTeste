using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Rede.Service.ViewModels;

public class BankingOperationsViewModel
{
    [Key]
    [JsonIgnore]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "The Origin Account is Required")]
    [DisplayName("Origin Account")]
    public string OriginAccount { get; set; }

    [Required(ErrorMessage = "The Destination Account is Required")]
    [DisplayName("Destination Account")]
    public string DestinationAccount { get; set; }
        
    [Required(ErrorMessage = "The Amount is Required")]
    [DisplayName("Amount")]
    public double Amount { get; set; }

    [Required(ErrorMessage = "The Operation is Required")]
    [DisplayName("Operation")]
    public string Operation { get; set; }
    
    // public enum Operation 
    // {
    //     [Display(Name = "Deposit")]
    //     Deposit,
    //     [Display(Name = "Withdraw")]
    //     Withdraw,
    //     [Display(Name = "Balance")]
    //     Balance
    // }
}