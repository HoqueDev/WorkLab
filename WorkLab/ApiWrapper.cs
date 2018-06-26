using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using WorkLab.Error;
using WorkLab.Model;

namespace WorkLab 
{
    static class ApiWrapper { 
        //private const string BASE_URL = "http://www.worklab.maahi-dev.com/api/";
        private const string BASE_URL = "https://www.malh.pw/worklab/api/";
        //private const string BASE_URL = "http://localhost:8080/api/";
        private const string USER_AGENT = "Worklab/1.0";
        private static JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
        private static string token = "";


        private static async Task<ApiResponse<TResponse>> ApiRequest<TResponse>(WebRequest request)
        {
            Console.WriteLine("ApiRequest()");
            
            //request.Headers.Add("UserAgent", USER_AGENT);
            request.Headers.Set(HttpRequestHeader.Authorization, "Bearer: " + token);

            WebResponse res = await request.GetResponseAsync();
            
            using (var reader = new StreamReader(res.GetResponseStream()))
            {
                                        
                string result = await reader.ReadToEndAsync();
                Console.WriteLine(result);
                try
                {
                    ApiResponse<TResponse> obj = JsonConvert.DeserializeObject<ApiResponse<TResponse>>(result);

                    if (obj.Error != null)
                    {
                        throw new ApiException(obj.Error);
                    }

                    if (obj.Response != null)
                    {
                        return obj;
                    }
                        
                }
                catch(Exception ex)
                {

                    Console.WriteLine("exception during request: "+ex.Message);
                    if(ex.GetType() == typeof(ApiException))
                    {
                        Console.WriteLine("is apiexception");
                        throw ex;
                    }
                }

                //if an exception is thrown or obj.Response == null
                throw new ApiException(new ErrorModel());

            }
                
        }
        
        private static async Task<ApiResponse<TResponse>> ApiGet<TResponse>(string endpoint, params Object[] parameters)
        {
            //build url
            string url = BASE_URL + endpoint;
           
            foreach(Object p in parameters) 
            {
                url += "/" + p;
            }

            WebRequest request = WebRequest.Create(url);
            request.Method = "GET";

            return await ApiRequest<TResponse>(request);
        }

        private static async Task<ApiResponse<TResponse>> ApiPost<TRequest, TResponse>(string endpoint, TRequest data)
        {
            string url = BASE_URL + endpoint;

            string serialized = JsonConvert.SerializeObject(data);
            Console.WriteLine("serialized: " + serialized);
            
            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers.Set(HttpRequestHeader.Authorization, "Bearer: " + token);
            request.ContentLength = serialized.Length;
            using (Stream dataStream = request.GetRequestStream())
            {
                using (var sw = new StreamWriter(dataStream))
                {
                    await sw.WriteAsync(serialized);
                }
            }
            return await ApiRequest<TResponse>(request);
        }

        private static async Task<ApiResponse<TResponse>> ApiPut<TResponse>(string endpoint, byte[] buf)
        {
            string url = BASE_URL + endpoint;

            
            WebRequest request = WebRequest.Create(url);
            request.Method = "PUT";
            request.ContentType = "application/binary";
            request.ContentLength = buf.Length;
            request.Headers.Set(HttpRequestHeader.Authorization, "Bearer: " + token);
            using (Stream dataStream = request.GetRequestStream())
            {
                await dataStream.WriteAsync(buf, 0, buf.Length);
                
            }
            return await ApiRequest<TResponse>(request);
        }

        private static async Task<ApiResponse<TResponse>> ApiPut<TRequest, TResponse>(string endpoint, TRequest data)
        {
            string url = BASE_URL + endpoint;
            string serialized = JsonConvert.SerializeObject(data);

            WebRequest request = WebRequest.Create(url);
            request.Method = "PUT";
            request.ContentType = "application/json";
            
            request.ContentLength = serialized.Length;
            request.Headers.Set(HttpRequestHeader.Authorization, "Bearer: " + token);
            using (Stream dataStream = request.GetRequestStream())
            {
                using (var sw = new StreamWriter(dataStream))
                {
                    await sw.WriteAsync(serialized);
                }
            }
            return await ApiRequest<TResponse>(request);
        }

        public static async Task<bool> Authenticate(string email, string password)
        {
            try
            {
                ApiResponse<AuthenticateResponse> response = await ApiPost<AuthRequest, AuthenticateResponse>("auth", new AuthRequest(email, password));
                token = response.Response.Token;
                Console.WriteLine("token: " + token);
                return true;
            }
            catch(ApiException ex)
            {
                return false;
            }
        }

        public static async Task<List<Class>> GetClasses()
        {
            ApiResponse<List<Class>> response = await ApiGet<List<Class>>("class");
            return response.Response;
        }

        public static async Task<Class> GetClass(int id)
        {
            ApiResponse<Class> response = await ApiGet<Class>("class", id);
            return response.Response;
        }

        public static async Task<List<Homework>> GetHomeworks()
        {
            var response = await ApiGet<List<Homework>>("homework");
            return response.Response;
        }

        public static async Task<List<Homework>> GetHomeworks(int class_id)
        {
            var response = await ApiGet<List<Homework>>("homework", class_id);
            return response.Response;
        }


        public static async Task<string> UploadFile(string path)
        {
            string name = Path.GetFileName(path);
            
            //concatenate filename and content
            IEnumerable<byte> rv = Encoding.ASCII.GetBytes(name+"----").Concat(File.ReadAllBytes(path));
            var response = await ApiPut<UploadFileResponse>("file", rv.ToArray());
            return response.Response.Url;
        }

        public static async Task<bool> CreateHomework(int class_id, string name, string url)
        {
            ApiResponse<SuccessResponse> response = await ApiPost<CreateHomework, SuccessResponse>("homework", new CreateHomework(class_id, name, url));
           
            return response.Response.Success;
        }

        public static async Task<bool> SubmitHomework(int homework_id, string url)
        {
            ApiResponse<SuccessResponse> response = await ApiPut<SubmitHomework, SuccessResponse>("homework/"+homework_id, new SubmitHomework(url));

            return response.Response.Success;
        }

        public static async Task<List<StudentHomework>> GetStudentHomeworks(int homework_id)
        {
            ApiResponse<List<StudentHomework>> response = await ApiGet<List<StudentHomework>>("homework", homework_id);

            return response.Response;
        }

        public static async Task<bool> CreateClass(string name)
        {
            ApiResponse<SuccessResponse> response = await ApiPost<CreateClassRequest, SuccessResponse>("class", new CreateClassRequest(name));
            return response.Response.Success;
        }

        public static async Task<List<User>> GetUsers()
        {
            ApiResponse<List<User>> response = await ApiGet<List<User>>("user");
            return response.Response;
        }

        public static async Task<bool> AddStudentToClass(int class_id, int student_id)
        {
            var ucr = new UpdateClassRequest();
            ucr.AddStudent = student_id;

            ApiResponse<SuccessResponse> response = await ApiPut<UpdateClassRequest, SuccessResponse>("class/" + class_id, ucr);

            return response.Response.Success;
        }

        public static async Task<bool> RemoveStudentFromClass(int class_id, int student_id)
        {
            var ucr = new UpdateClassRequest();
            ucr.RemoveStudent = student_id;

            ApiResponse<SuccessResponse> response = await ApiPut<UpdateClassRequest, SuccessResponse>("class/" + class_id, ucr);

            return response.Response.Success;
        }

        public static async Task<bool> UpdateClass(int class_id, string name)
        {
            var ucr = new UpdateClassRequest();
            ucr.Name = name;

            ApiResponse<SuccessResponse> response = await ApiPut<UpdateClassRequest, SuccessResponse>("class/" + class_id, ucr);
            
            return response.Response.Success;
        }

        public static async Task<bool> CreateUser(string name, string email, string password, bool teacher)
        {
            ApiResponse<SuccessResponse> response = await ApiPost<CreateUserRequest, SuccessResponse>("user", new CreateUserRequest(name, email, password, teacher));
            return response.Response.Success;
        }
    }
}
