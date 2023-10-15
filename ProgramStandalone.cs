using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

//this class runs on the client side to make calls to the API
public class ProgramStandalone
{
    //stores the file path 
    private static readonly string LocalDiaryPath = "C:\Users\mubir\OneDrive\Documents\GitHub\Online-Diary-API-Demo";
    //stores the API url
    private static readonly string ApiBaseURL = "https://cryptic-opus-400717.uk.r.appspot.com";

    //the main function
    static async Task Main(string[] args) {
        //what if there are no arguments
        if (args.Length == 0) {
            Console.WriteLine("Please input a command; get or update");
        }
        //switch statements
        switch (args[0].ToLower()) {
            //if the command is get
            case "get":
                //method to get
                GetDiaryFromAPI;
                break;
            case "update":
                //method to update
                UpdateDiaryToAPI();
                break;
            default:
                //non of the commands was called
                Conole.WriteLine("Invalid command: Please input 'get' or 'update'.");
        }
    }

    //method to get the diary from the API
    static async Task GetDiaryFromAPI() {
        //create a new client 
        using var httpClient = new HttpClient();
        //use the client to make a call to the API and store the response
        var response = await httpClient.GetAsync(ApiBaseURL + "\Diary");
        //checking if the request was successful
        if (response.IsSuccessStatusCode) {
            //create a stream
            using var stream = new FileStream(LocalDiaryPath, FileMode.Create());
            //load the content into the stream
            await response.Content.CopyToAsync(stream);
            //offer confirmation
            Console.WriteLine("Diary retrived and saved locally.")
        }
        else {
            //offer error confirmation
            Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
        }

    }

    //method to update the diary to the API
    static async Task UpdateDiaryToAPI() {
        //we want to read from a file and send it to the API
        //what if we do not have the file
        if (!File.Exists(LocalDiaryPath)) {
            //file does not exist at the path
            Console.WriteLine("The diary file does not exist at path! You need to create one.");
        }
        //create a client to make the call
        using var httpClient = new HttpClient();
        //create a stream from a file to upload with the HTTP request
        using var stream = new FileStream(LocalDiaryPath, FileMode.Open());
        //send the file to the API with the stream content
        var response = await httpClient.PostAsync(ApiBaseURL + "/Diary", new StreamContent(stream));
        //if the response is successful, update the user
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("Diary successfully uploaded to cloud!");
        }
        else {
            Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
        }
    }
}
