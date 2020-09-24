using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SendGrid;
using SendGrid.Helpers.Mail;
using Newtonsoft.Json;

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

    public async Task<ActionResult<Email>> Post(
      [FromServices]DataContext context,
      [FromBody]Email model
    )
    {
      if(ModelState.IsValid){
        context.Email.Add(model);
        await context.SaveChangesAsync();       
        return model;
      }
      else
      {
        return BadRequest(ModelState);
      }


    }

    static async Task Execute()
    {

      var send = new Email();

      var apiKey = "SENDGRID_APIKEY"; 
      var client = new SendGridClient(apiKey);

      var from = new EmailAddress("wallace.nascimento@lftm.com.br", "Wallace Brito");
      var to = new EmailAddress(send.Emails, send.Template.Name);
      var templateId = send.TemplateId;
      var dynamicTemplateData = new ExampleTemplateData
      {
        Subject = send.Template.Subject,
        Name = send.Template.Name,
        Valor = send.Template.Valor
      };

      // var from = new EmailAddress("wallace.nascimento@lftm.com.br", "Wallace Brito");
      // var tos = new List<EmailAddress>
      // {
      //     new EmailAddress("dragonwits@gmail.com", "Wallace"),
      //     new EmailAddress("samuel.conceicao@lifetimeinvest.com.br", "Samuel"),
      //     new EmailAddress("pedro.silva@lftm.com.br", "Pedro")
      // };
      
      // var templateId = "d-9d2b006a4d354f2ab7438ee69a61782f";
      // var dynamicTemplateData = new List<Object>
      // {
      //     new ExampleTemplateData  {
      //         Subject = "Hi!",
      //         Name = "Wallace",
      //         Valor = "500,00"
      //     },
      //     new ExampleTemplateData  {
      //         Subject = "Hi!",
      //         Name = "Samuel",
      //         Valor = "1500,00"                    
      //     },
      //     new ExampleTemplateData  {
      //         Subject = "Hi!",
      //         Name = "Pedro",
      //         Valor = "5000,00"
      //     }
      // };

      var msg = MailHelper.CreateSingleTemplateEmail(
                                                                      from,
                                                                      to,
                                                                      templateId,
                                                                      dynamicTemplateData                                                                            
                                                                      );

      var response = await client.SendEmailAsync(msg);
      Console.WriteLine(response.StatusCode);
      Console.WriteLine(response.Headers.ToString());
    }
  }

}