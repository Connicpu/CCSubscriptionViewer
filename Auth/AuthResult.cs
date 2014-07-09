using Google.Apis.Auth.OAuth2;
using Google.Apis.Tasks.v1;

namespace CCSubscriptionViewer.Auth
{
    public class AuthResult
    {
        public static AuthResult AuthFailed = new AuthResult(true);

        private AuthResult(bool failed)
        {
            Failed = failed;
        }

        public AuthResult()
            : this(false)
        {
        }

        public bool Failed { get; private set; }

        public TasksService Service { get; internal set; }
        public UserCredential Credential { get; internal set; }

        public static implicit operator bool(AuthResult result)
        {
            return !result.Failed;
        }
    }
}
