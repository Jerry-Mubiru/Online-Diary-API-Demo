using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;

namespace jerry_first_online_notepad.Services
{
    //this class will be responsible for creating a client in the API to authenticate with the GCP
    //the constructor creates the class and the method returns the client
    public class ClientCreator
    {
        //method that returns a client with credentials
        public StorageClient CreateClient() {
            //stores the credentials
            GoogleCredential credentials = GoogleCredential.GetApplicationDefault();
            StorageClient client = StorageClient.Create(credentials);
            return client;
        }
    }
}
