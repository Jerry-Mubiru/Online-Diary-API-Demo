using Google.Apis.Storage.v1;
using Microsoft.AspNetCore.Mvc;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;

namespace jerry_first_online_notepad.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiaryController : ControllerBase
    {
        //stores the name of the Storage Bucket
        private const string bucket = "jerrys-hello-world-notepad";
        //stores the client to access the Storage Bucket
        private StorageClient client;

        //constructor to initalize the client
        public DiaryController(StorageClient storageClient) {
            this.client = storageClient;
        }

    }
}
