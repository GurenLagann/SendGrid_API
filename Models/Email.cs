using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace APISendgrid.Models
{  
  public class Content
  {
     [JsonProperty("Content")]
     public List<Email> content {get; set;}
  }
  public class Email
  {
   [Key] 
   
    public int Id {get; set;}
   
    [JsonProperty("email")]
    public string Emails {get; set;}
    [JsonProperty("template")]
    public ExampleTemplateData Template {get; set;}
    
    [JsonProperty("templateId")] 
    public string TemplateId {get; set;}  
  }
}

