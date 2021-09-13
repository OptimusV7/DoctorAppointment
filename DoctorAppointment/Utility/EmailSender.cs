using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity.UI.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorAppointment.Utility
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MailjetClient client = new MailjetClient(Environment.GetEnvironmentVariable("91271dd0a2697c86050f1019448c143f"), Environment.GetEnvironmentVariable("55d3001bc0e841d3e5a57de79b46943c"))
            {

            };
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
             .Property(Send.Messages, new JArray {
             new JObject {
              {
               "From",
               new JObject {
                {"Email", "gilbert.mule@kra.go.ke"},
                {"Name", "Appointment"}
               }
              }, {
               "To",
               new JArray {
                new JObject {
                 {"Email", email}
                }
               }
              }, {"Subject", subject},{
               "HTMLPart", htmlMessage
              }, {
               "CustomID",
               "AppGettingStartedTest"
              }
             }
                     });
            MailjetResponse response = await client.PostAsync(request);

        }
    }
}
