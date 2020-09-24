using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.ComponentModel.DataAnnotations;

namespace APISendgrid.Models
{
  public class ExampleTemplateData
  {
    [Key]
    public int Id {get; set;}
    [JsonProperty("subject")]
    public string Subject { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("valor")]
    public string Valor { get; set; }
  }  
}

