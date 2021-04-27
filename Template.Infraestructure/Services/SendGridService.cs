using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;
using System.Threading.Tasks;
using Template.Core.Interfaces;

namespace Template.Infraestructure.Services
{
    public class SendGridService : ISendGridService
    {
        private readonly IConfiguration _configuration;

        public SendGridService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task SendMailPassword(string subject, string password, string email, string name)
        {
            var apiKey = _configuration.GetSection("SENDGRID_API_KEY").Value;
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage();

            msg.SetFrom(new EmailAddress("noreply@microsabiertosprimaafp.com", "Micros Abiertos Team"));

            var recipients = new List<EmailAddress>
            {
                new EmailAddress(email, name),
            };
            msg.AddTos(recipients);

            msg.SetSubject(subject);
            msg.SetTemplateId("d-daa71c5d795f4c72859097bfb84429c1");
            var dynamicTemplateData = new ExampleTemplateData
            {
                Name = name,
                Password = password,
                Email = email
            };

            msg.SetTemplateData(dynamicTemplateData);
            //msg.AddContent(MimeType.Text, content);
            //msg.AddContent(MimeType.Html, content);
            var response2 = await client.SendEmailAsync(msg);
        }

        public async Task SendMailConfirmation(string subject, string token, string email, string name)
        {
            var apiKey = _configuration.GetSection("SENDGRID_API_KEY").Value;
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage();

            msg.SetFrom(new EmailAddress("noreply@microsabiertosprimaafp.com", "Micros Abiertos Team"));

            var recipients = new List<EmailAddress>
            {
                new EmailAddress(email, name),
            };
            msg.AddTos(recipients);

            msg.SetSubject(subject);
            msg.SetTemplateId("d-5780bd022e0449f8800cf97ba8c0c775");
            var dynamicTemplateData = new ExampleTemplateData
            {
               
                Name = name,
                Token = token
            };

            msg.SetTemplateData(dynamicTemplateData);
            //msg.AddContent(MimeType.Text, content);
            //msg.AddContent(MimeType.Html, content);
            var response2 = await client.SendEmailAsync(msg);
        }
    }

    public class ExampleTemplateData
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
