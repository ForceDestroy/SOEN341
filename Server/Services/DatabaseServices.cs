using MongoDB.Driver;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using RestSharp; //add pkg: dotnet add package RestSharp --version 106.11.7

namespace Server.Services
{
    public class DatabaseServices
    {
        private readonly IMongoCollection<User> _users;

        public DatabaseServices(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _users = database.GetCollection<User>(settings.CollectionName);
        }

        //takes a base64 represntation of an image
        //returns imgur link
        public static string UploadToImgur(string imgBase64)
        {
            string key = "6af588629190cb3";
            
            var client = new RestClient("https://api.imgur.com/3/image");
            client.Timeout = -1;
            
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Client-ID " + key);
            request.AlwaysMultipartFormData = true;
            request.AddParameter("image", imgBase64);
            IRestResponse response = client.Execute(request);

            var content = response.Content;
            int start = content.IndexOf("\"link\":\"https:");

            string imageLink = content.Substring(start + 8);
            imageLink = imageLink.Substring(0, (imageLink.IndexOf("success\"")) - 4);

            imageLink = imageLink.Replace("\\", "");

            return imageLink;
        }

        public List<User> Get() => _users.Find(user => true).ToList();

        public User Get(int id) => _users.Find(user => user.userId == id).FirstOrDefault();

        public User Get(string username) => _users.Find(user => user.username == username).FirstOrDefault();

        public User GetHighest() => _users.Find(user => true).SortByDescending(user => user.userId).Limit(1).FirstOrDefault();

        public List<User> Search(string username) => _users.Find(user => user.username.Contains(username)).ToList();

        public User Create(User user)
        {
            _users.InsertOne(user);

            return user;
        }

        public void Update(int id, User userIn) => _users.ReplaceOne(user => user.userId == id, userIn);

        public void Delete(int id) => _users.DeleteOne(user => user.userId == id);

        public void Delete(User userIn) => _users.DeleteOne(user => user.userId == userIn.userId);
    }
}
