﻿using MongoDB.Driver;
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

        //Uploading image to imgur here

        public static string UploadToImgur(string imgPath, string apiKey)
        {

           byte [] imageArr = File.ReadAllBytes(imgPath);
           string imgBase64 = Convert.ToBase64String(imageArr);


            var client = new RestClient("https://api.imgur.com/3/image");
            
            client.Timeout = -1;
            
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Client-ID " + apiKey);
            request.AlwaysMultipartFormData = true;
            request.AddParameter("image", imgBase64);
            IRestResponse response = client.Execute(request);

            var content = response.Content;
            int start = content.IndexOf("\"link\":\"https:");

            string link = content.Substring(start + 8);
            link = link.Substring(0, (link.IndexOf("success\"")) - 4);

            link = link.Replace("\\", "");

            return link;
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
