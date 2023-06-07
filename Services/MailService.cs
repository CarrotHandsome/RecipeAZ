using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Google.Apis.Auth.OAuth2;
using MimeKit;
using Google.Apis.Util.Store;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;

namespace RecipeAZ.Services {
    public class MailService {
        private static string[] Scopes = { GmailService.Scope.GmailSend };
        private static string ApplicationName = "Recipeaz";
        public IWebHostEnvironment _env { get; set; }

        public MailService(IWebHostEnvironment env) {
            _env = env;
        }
        public async Task Send(string to, string subject, string bodyText) {
            var initializer = new BaseClientService.Initializer {
                HttpClientInitializer = GetCredential(),
                ApplicationName = ApplicationName,
            };

            var service = new GmailService(initializer);
            
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Jacob Billings", "carrothands@gmail.com"));
            email.To.Add(new MailboxAddress(string.Empty, to));
            email.Subject = subject;
            email.Body = new TextPart("html") { Text = bodyText };

            var message = new Message {
                Raw = Base64UrlEncode(email.ToString())
            };

            var request = service.Users.Messages.Send(message, "me");
            await request.ExecuteAsync();
        }        
        private UserCredential GetCredential() {
            UserCredential credential;
            string credentialPath = "credentials.json";
            using (var stream = new FileStream(credentialPath, FileMode.Open, FileAccess.Read)) {
                string credPath = "token.json";

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }
            return credential;
        }

        private static string Base64UrlEncode(string input) {
            var inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(inputBytes).Replace('+', '-').Replace('/', '_').Replace("=", "");
        }
    }

}
