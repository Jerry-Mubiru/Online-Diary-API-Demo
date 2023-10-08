using Google.Apis.Storage.v1;
using Microsoft.AspNetCore.Mvc;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Google;

namespace jerry_first_online_notepad.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiaryController : ControllerBase
    {
        //stores the name of the Storage Bucket
        private const string bucket = "jerrys-hello-world-notepad";
        //stores the name of the file in the storage bucket
        private string fileName = "diary.txt";
        //stores the client to access the Storage Bucket
        private StorageClient client;

        //constructor to initalize the client
        public DiaryController(StorageClient storageClient) {
            this.client = storageClient;
        }

        //this function will download the diary to the Http request in the API
        [HttpGet]
        public IActionResult GetDiary() {
            //potential exception
            try {
                //create a memory stream
                using var ms = new MemoryStream();
                //download diary.txt to said memory stream through an authorized client
                client.DownloadObject(bucket, fileName, ms);
                //reset the stream Position
                ms.Position = 0;
                //return said stream using File(Stream stream, string contentType)
                return File(ms, "text/plain");
            }
            //catch the not Found Exception
            catch (Google.GoogleApiException ex) when (ex.Error.Code == 404)
            {
                return NotFound("The diary file does not exist on the server.");
            }
            //catch any other errors
            catch (Exception ex){
                return StatusCode(500, "Internal server error:"+ex.Message);
            }
        }

        //this function will upload content from the api to the bucket
        [HttpPost]
        public async IActionResult UpdateDiary() {
            //create a memory stream 
            using var ms = new MemoryStream();
            //copy the content from the HTTP request to the stream
            await Request.Body.CopyToAsync(ms);
            //reset the position of the stream 
            ms.Position = 0;
            //upload from the stream to the storage bucket
            client.UploadObject(bucket,fileName,null,ms);
            //return type
            return Ok("Diary Updated Successfully");
        }

    }
}
