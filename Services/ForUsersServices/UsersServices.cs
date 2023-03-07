using Newtonsoft.Json;
using User = AutoTest.Models.User;

namespace AutoTest.Services.ForUsersServices
{
    partial class UsersServices
    {
        List<User> Users { get; set; }

        private const string FilePath = "Users.json";
       
        public UsersServices()
        {
            Users = new List<User>();
        }
  
        public void SaveUserData()
        {
            var json = JsonConvert.SerializeObject(Users);
            File.WriteAllText(FilePath, json);
        }

        public void ReadUserData()
        {
            if (File.Exists(FilePath))
            {
                var textJson = File.ReadAllText(FilePath);
                Users = JsonConvert.DeserializeObject<List<User>>(textJson) ?? new List<User>();
            }
            else
                Users = new List<User>();
        }
       

    }
   
}
