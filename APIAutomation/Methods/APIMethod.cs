using APIAutomation.Model;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using APIAutomation.Utilities;
using APIAutomation.Config;


namespace APIAutomation.Methods
{
    public class APIMethod
    {
        private readonly string _projectDir = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
        private static readonly HttpClient _restClient = new HttpClient();
        private static readonly AppConfig appConfig = new AppConfig();
        private const string _PostsEndpoint = "/Posts";
        private const string _CommentsEndpoint = "/Comments";

        //Method to Call GET request
        public async Task<string> GetData(string methodName, DataCategory dataCategory)
        {

            try
            {
                HttpResponseMessage response;

                if (dataCategory == DataCategory.Comments)
                {
                    ExtentReport.LogInfo("Retrieve Data from JSON File based on TestCase#");
                    //Retrieve Data from JSON File based on TestCase#
                    var testCase = ReadDataFromJson_Comments().Testcases.FirstOrDefault(item => item.TestcaseNo == methodName);
                    if (testCase == null)
                        return "Test case not found";

                    ExtentReport.LogInfo("Retrieve the ID# and GET request to API endpoint with parameters");
                    //Retrieve the ID# and GET request to API endpoint with parameters
                    int postId = testCase.Data.PostId;
                    response = await _restClient.GetAsync($"{appConfig.BaseURI}{_CommentsEndpoint}?postId={postId}");

                }
                else
                {
                    ExtentReport.LogInfo("GET request to API endpoint to retrieve all the data");
                    //GET request to API endpoint to retrieve all the data
                    response = await _restClient.GetAsync($"{appConfig.BaseURI}{_PostsEndpoint}");
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine(jsonResponse);

                return response.ReasonPhrase;
            
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        //Method to Call POST request
        public async Task<string> CreateDataByTestcase(string methodName, DataCategory dataCategory)
        {
            try { 
                HttpResponseMessage response;

                if (dataCategory == DataCategory.Posts)
                {
                    ExtentReport.LogInfo("Retrieve Data from Json File based on TestCase#");
                    var testCase = ReadDataFromJson_Posts().TestCases.FirstOrDefault(item => item.TestcaseNo == methodName);
                    if (testCase == null)
                        return "Test case not found";

                    ExtentReport.LogInfo("Serialize the Data to JSON");
                    string jsonData = JsonConvert.SerializeObject(testCase);
                    HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");


                    ExtentReport.LogInfo("POST request to API endpoint");
                    response = await _restClient.PostAsync($"{appConfig.BaseURI}{_PostsEndpoint}", content);

                }
                else

                {
                    ExtentReport.LogInfo("Retrieve Data from Json File based on TestCase#");
                    var testCase = ReadDataFromJson_Comments().Testcases.FirstOrDefault(item => item.TestcaseNo == methodName);
                    if (testCase == null)
                        return "Test case not found";

                    ExtentReport.LogInfo("Serilize the Data to JSON");
                    string jsonData = JsonConvert.SerializeObject(testCase);
                    HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");


                    ExtentReport.LogInfo("POST request to API endpoint with parameter");
                    int postId = testCase.Data.PostId;
                    response = await _restClient.PostAsync($"{appConfig.BaseURI}{_PostsEndpoint}/{postId}{_CommentsEndpoint}", content);
                }

                //Convert JSON to string format
                string jsonResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine(jsonResponse);

                return response.ReasonPhrase;


            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        //Method to Call PUT request
        public async Task<string> UpdateDataByTestcase(string methodName)
        {
            try 
            {
                ExtentReport.LogInfo("Retrieve Data from Json File based on TestCase#");
                var testCase = ReadDataFromJson_Posts().TestCases.FirstOrDefault(item => item.TestcaseNo == methodName);
                if (testCase == null)
                    return "Test case not found";


                ExtentReport.LogInfo("Serialize the Data to JSON");
                string jsonData = JsonConvert.SerializeObject(testCase);
                HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                ExtentReport.LogInfo("PUT request to API endpoint with parameter");
                int userId = testCase.Data.UserId;
                HttpResponseMessage response = await _restClient.PutAsync($"{appConfig.BaseURI}{_PostsEndpoint}/{userId}", content);


                //Convert JSON to string format
                string jsonResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine(jsonResponse);
                return response.ReasonPhrase;

            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }

        }

        //Method to Call DELETE request
        public async Task<string> DeleteData(string methodName)
        {
            try
            { 
                ExtentReport.LogInfo("Retrieve Data from Json File based on TestCase#");
                var testCase = ReadDataFromJson_Posts().TestCases.FirstOrDefault(item => item.TestcaseNo == methodName);
                if (testCase == null)
                    return "Test case not found";

                ExtentReport.LogInfo("DELETE request to API endpoint with parameter");
                int userId = testCase.Data.UserId;
                HttpResponseMessage response = await _restClient.DeleteAsync($"{appConfig.BaseURI}{_PostsEndpoint}/{userId}");

                return response.ReasonPhrase;

            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public PostsTestCasesContainer ReadDataFromJson_Posts()
        {
            try
            {
                string fileLocation = $"{_projectDir}{appConfig.FileLocationPosts}";

                //Read the data from file and properly dispose
                using (var reader = new StreamReader(fileLocation))
                {
                    string json = reader.ReadToEnd();
                    
                    //Deserialize the data to JSON structure
                    var testCasesContainer = JsonConvert.DeserializeObject<PostsTestCasesContainer>(json);

                    return testCasesContainer;
                }
            }
            catch (JsonException ex)
            {
                //Log the JSON deserialization error
                ExtentReport.LogFail($"JSON deserialization error: {ex.Message}");
                Console.WriteLine($"JSON deserialization error: {ex.Message}");
                return new PostsTestCasesContainer();
            }
            catch (Exception ex)
            {
                //Log any other errors
                ExtentReport.LogFail($"An error occurred: {ex.Message}");
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new PostsTestCasesContainer();
            }
        }

        public CommentsTestCasesContainer ReadDataFromJson_Comments()
        {
            try
            {

                string fileLocation = $"{_projectDir}{appConfig.FileLocationComments}"; 

                //Read the data from file and properly dispose
                using (var reader = new StreamReader(fileLocation))
                {
                    string json = reader.ReadToEnd();

                    //Deserialize the data to JSON structure
                    var testCasesContainer = JsonConvert.DeserializeObject<CommentsTestCasesContainer>(json);

                    return testCasesContainer;
                }
            }
            catch (JsonException ex)
            {
                //Log the JSON deserialization error
                ExtentReport.LogFail($"JSON deserialization error: {ex.Message}");
                Console.WriteLine($"JSON deserialization error: {ex.Message}");
                return new CommentsTestCasesContainer();
            }
            catch (Exception ex)
            {
                //Log any other errors
                ExtentReport.LogFail($"An error occurred: {ex.Message}");
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new CommentsTestCasesContainer();
            }
        }



    }
}
