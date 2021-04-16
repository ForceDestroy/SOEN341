using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ServerTests
{
    [TestClass]
    public class DatabaseControllerTests
    {
        public const string API_DATABASE = "https://localhost:44318/db/";

        [TestMethod]
        public void GetAll()
        {
            // Arrange
            WebClient webClient = new WebClient();

            try
            {
                // Act 
                string response = webClient.DownloadString(API_DATABASE + "getall");

                // Assert
                Assert.IsNotNull(response);

                var users = JsonConvert.DeserializeObject<List<User>>(response);

                Assert.IsNotNull(users);

                Assert.IsTrue(users.Count > 0);

            } catch (Exception)
            {
                Assert.Fail("Unexpected error occured");
            }
        }

        [TestMethod]
        public void Get()
        {
            bool success = false;
            try
            {
                // Arrange
                WebClient webClient = new WebClient();
                webClient.QueryString.Add("id", "1");

                // Act
                string response = webClient.DownloadString(API_DATABASE + "get");

                // Assert
                Assert.IsNotNull(response);

                var user = JsonConvert.DeserializeObject<User>(response);

                Assert.IsNotNull(user);

                Assert.AreEqual(1, user.userId);

                success = true;

                // Arrange
                webClient.QueryString.Add("id", "-1");

                // Act
                response = webClient.DownloadString(API_DATABASE + "get");

                // Assert
                Assert.Fail("Request succeeded when it should have failed.");

            }
            catch (Exception)
            {
                if(!success)
                    Assert.Fail("Unexpected error occured");
            }
        }

        [TestMethod]
        public void Create_Delete()
        {
            // Arrange
            User _user = new User("", "TestUser", "TestPassword1!", 300, "Test User", "I am a robot.","img.img", new List<MiniUser>(), new List<MiniUser>(), new List<Post>(), 0);
            string jsonData = JsonConvert.SerializeObject(_user);
            bool isDeleted = false;

            try
            {
                WebClient webClient = new WebClient();

                // Act
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                string response = webClient.UploadString(API_DATABASE + "create", jsonData);

                // Assert
                webClient = new WebClient();
                webClient.QueryString.Add("id", "300");
                string assertResponse = webClient.DownloadString(API_DATABASE + "get");
                var user = JsonConvert.DeserializeObject<User>(assertResponse);

                Assert.IsNotNull(user);
                Assert.AreEqual(_user.userId, user.userId, $"User Id does not match");
                Assert.AreEqual(_user.username, user.username, $"User Name does not match");

                // Act
                webClient = new WebClient();
                webClient.QueryString.Add("id", "300");
                string deleteResponse = webClient.UploadString(API_DATABASE + "delete", "DELETE", "");
                isDeleted = true;

                // Assert
                webClient = new WebClient();
                webClient.QueryString.Add("id", "300");
                assertResponse = webClient.DownloadString(API_DATABASE + "get");

                Assert.Fail("Request succeeded when it should have failed.");
            }
            catch(Exception)
            {
                if (!isDeleted)
                    Assert.Fail("Unexpected error occured");
            }
        }

        [TestMethod]
        public void Update()
        {
            // Arrange
            User _user = new User("", "TestUser", "TestPassword1!", 300, "Test User", "I am a robot.", "img.img", new List<MiniUser>(), new List<MiniUser>(), new List<Post>(), 0);
            User _user2 = new User("", "TestUser2", "TestPassword2!", 300, "Test User", "I am a robot.", "img.img", new List<MiniUser>(), new List<MiniUser>(), new List<Post>(), 0);
            string jsonData = JsonConvert.SerializeObject(_user);
            bool isUpdated = false;

            try
            {
                WebClient webClient = new WebClient();

                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                string response = webClient.UploadString(API_DATABASE + "create", jsonData);

                webClient = new WebClient();
                webClient.QueryString.Add("id", "300");
                string assertResponse = webClient.DownloadString(API_DATABASE + "get");
                var user = JsonConvert.DeserializeObject<User>(assertResponse);

                Assert.IsNotNull(user);
                Assert.AreEqual(_user.userId, user.userId, $"User Id does not match");
                Assert.AreEqual(_user.username, user.username, $"User Name does not match");

                // Act
                webClient = new WebClient();
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                jsonData = JsonConvert.SerializeObject(_user2);
                webClient.QueryString.Add("id", "300");
                string updateResponse = webClient.UploadString(API_DATABASE + "update", "PUT", jsonData);
                isUpdated = true;

                // Assert
                webClient = new WebClient();
                webClient.QueryString.Add("id", "300");
                assertResponse = webClient.DownloadString(API_DATABASE + "get");
                var user2 = JsonConvert.DeserializeObject<User>(assertResponse);

                Assert.IsNotNull(user);
                Assert.AreNotEqual(_user.username, user2.username, $"User Name should not match");
                Assert.AreEqual(_user2.username, user2.username, $"User Name does not match");
                Assert.AreNotEqual(_user.password, user2.password, $"Password should not match");
                Assert.AreEqual(_user2.password, user2.password, $"Password does not match");

                // Dispose
                webClient = new WebClient();
                webClient.QueryString.Add("id", "300");
                string deleteResponse = webClient.UploadString(API_DATABASE + "delete", "DELETE", "");
            }
            catch (Exception)
            {
                if (!isUpdated)
                    Assert.Fail("Unexpected error occured");
            }
        }

    }

}
