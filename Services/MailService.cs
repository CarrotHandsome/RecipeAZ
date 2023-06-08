using Amazon;
using System;
using System.Collections.Generic;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;

namespace RecipeAZ.Services {
    public class MailService {
        

        // The configuration set to use for this email. If you do not want to use a
        // configuration set, comment out the following property and the
        // ConfigurationSetName = configSet argument below. 
        static readonly string configSet = "ConfigSet";

        

        public async static Task Send(string recipientAddress, string subject, string body) {
            // Replace USWest2 with the AWS Region you're using for Amazon SES.
            // Acceptable values are EUWest1, USEast1, and USWest2.
            
            using (var client = new AmazonSimpleEmailServiceClient(RegionEndpoint.USWest2)) {
                var sendRequest = new SendEmailRequest {
                    Source = "carrothands@gmail.com",
                    Destination = new Destination {
                        ToAddresses =
                        new List<string> { recipientAddress }
                    },
                    Message = new Message {
                        Subject = new Content(subject),
                        Body = new Body {
                            Html = new Content {
                                Charset = "UTF-8",
                                Data = body
                            },
                            Text = new Content {
                                Charset = "UTF-8",
                                Data = string.Empty
                            }
                        }
                    },
                    // If you are not using a configuration set, comment
                    // or remove the following line 
                    //ConfigurationSetName = configSet
                };
                try {
                    Console.WriteLine("Sending email using Amazon SES...");
                    var response = await client.SendEmailAsync(sendRequest);
                    Console.WriteLine("The email was sent successfully.");
                } catch (Exception ex) {
                    Console.WriteLine("The email was not sent.");
                    Console.WriteLine("Error message: " + ex.Message);

                }
            }

            
        }
    }
}