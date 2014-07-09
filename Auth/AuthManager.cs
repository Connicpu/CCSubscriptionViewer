using System.Configuration;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Tasks.v1;
using Google.Apis.Tasks.v1.Data;
using Google.Apis.Util.Store;

namespace CCSubscriptionViewer.Auth
{
    public class AuthManager
    {
        private static AuthResult authResult;
        private readonly MemoryStream clientSecrets;

        public AuthManager()
        {
            clientSecrets = new MemoryStream(AuthResources.client_secrets);
        }

        public async Task<AuthResult> Authorize()
        {
            if (authResult != null)
                return authResult;

            authResult = new AuthResult
            {
                Service = new TasksService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(clientSecrets).Secrets,
                        new[] {TasksService.Scope.Tasks},
                        "user", CancellationToken.None,
                        new FileDataStore("Tasks.Auth.Store")),
                    ApplicationName = "CC Subscription Viewer"
                })
            };

            var results = authResult.Service.Tasklists.List().Execute();

            return authResult;
        }
    }
}
