using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SendGrid;
using SendGrid.Helpers.Mail;
using Newtonsoft.Json;
using System.Net.Http;

using APISendgrid.Models;
using APISendgrid.Data;

namespace APISendgrid.Controllers
{
  [ApiController]
  [Route("/envio")]

  public class TemplateData : Controller
  {
    [HttpGet]
    [Route("")]
    public void Get() => Console.WriteLine("Hello");

    [HttpPost]
    [Route("")]
    public async Task<ActionResult<dynamic>> Post(
      [FromBody]Email model
    )
    {
      Console.WriteLine(model);
      var apiKey = "KEY SENDGRID"; 
      var client = new SendGridClient(apiKey);
      var from = new EmailAddress("wallace.nascimento@lftm.com.br", "Wallace Brito");
      var to = new EmailAddress(model.Emails, model.Template.Name);
      var templateId = model.TemplateId;
      var dynamicTemplateData = new ExampleTemplateData
      {
        Subject = model.Template.Subject,
        Name = model.Template.Name,
        Valor = model.Template.Valor
      };

      var msg = MailHelper.CreateSingleTemplateEmail(
                                                      from,
                                                      to,
                                                      templateId,
                                                      dynamicTemplateData                                                                            
                                                    );

      var response = await client.SendEmailAsync(msg);
      Console.WriteLine(response.StatusCode);
      Console.WriteLine(response.Headers.ToString());

      return (model);
    }
  }
}