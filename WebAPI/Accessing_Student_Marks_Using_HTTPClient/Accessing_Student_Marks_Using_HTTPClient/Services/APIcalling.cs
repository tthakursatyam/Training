using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using Accessing_Student_Marks_Using_HTTPClient.Model;
namespace Accessing_Student_Marks_Using_HTTPClient.Services
{
    public class APIcalling
    {
        public async Task<List<Student>> GetStudents()
        {
            List<Student> studentList = new List<Student>();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5068/");

                HttpResponseMessage response = await client.GetAsync("api/student");

                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    studentList = JsonConvert.DeserializeObject<List<Student>>(jsonData);
                }
            }

            return studentList;
        }
    }
}
