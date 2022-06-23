using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Rede.Service.ViewModels;

public class CurrencyAccountViewModel
{
    [Key]
    [JsonIgnore]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "The Number Account is Required")]
    [DisplayName("Number Account")]
    public int NumberAccount { get; set; }

    [Required(ErrorMessage = "The Digit Account is Required")]
    [DisplayName("Digit Account")]
    public int Digit { get; set; }
        
    [Required(ErrorMessage = "The Balance is Required")]
    [DisplayName("Balance")]
    public double Balance { get; set; }

    [JsonIgnore]
    [DisplayName("CustomerId")]
    public Guid CustomerId { get; set; }
}