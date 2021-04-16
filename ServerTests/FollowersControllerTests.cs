using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Threading.Tasks;


namespace ServerTests
{
    [TestClass]
    public class FollowersControllerTests
    {
        public const string API_DATABASE = "https://localhost:44318/follower/";
        public const string API_DATABASE_DB = "https://localhost:44318/db/";

        [TestMethod]
        public void Create_Delete_Followers()
        {
            User _user1 = new User("", "TestUser", "TestPassword1!", 420, "Test User", "I am a robot.", "img.img", new List<MiniUser>(), new List<MiniUser>(), new List<Post>(), 0);
            User _user2 = new User("", "TestUser2", "TestPassword2!", 4200, "Test User2", "I am a robot.", "img.img", new List<MiniUser>(), new List<MiniUser>(), new List<Post>(), 0);

            string jsonDataUser1 = JsonConvert.SerializeObject(_user1);
            string jsonDataUser2 = JsonConvert.SerializeObject(_user2);

            bool isDeleted = false;
            try
            {
                WebClient webClient = new WebClient();
                // User Creation
                // Act
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                string response = webClient.UploadString(API_DATABASE_DB + "create", jsonDataUser1);

                // Assert
                webClient = new WebClient();
                webClient.QueryString.Add("id", "420");
                string assertUserResponse = webClient.DownloadString(API_DATABASE_DB + "get");
                var user = JsonConvert.DeserializeObject<User>(assertUserResponse);

                Assert.IsNotNull(user);
                Assert.AreEqual(_user1.userId, user.userId, $"User Id does not match");
                Assert.AreEqual(_user1.username, user.username, $"User Name does not match");

                webClient = new WebClient();
                // User Creation
                // Act
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                response = webClient.UploadString(API_DATABASE_DB + "create", jsonDataUser2);

                // Assert
                webClient = new WebClient();
                webClient.QueryString.Add("id", "4200");
                assertUserResponse = webClient.DownloadString(API_DATABASE_DB + "get");
                var user2 = JsonConvert.DeserializeObject<User>(assertUserResponse);

                Assert.IsNotNull(user2);
                Assert.AreEqual(_user2.userId, user2.userId, $"User Id does not match");
                Assert.AreEqual(_user2.username, user2.username, $"User Name does not match");


                // Adding Follower
                // Act
                webClient = new WebClient();
                var parameters = new NameValueCollection();
                parameters.Add("userId", "420");
                parameters.Add("followerId", "4200");

                webClient.QueryString.Add(parameters);
                var reactionResponse = webClient.UploadValues(API_DATABASE + "addfollower", "POST", webClient.QueryString);

                // Assert
                webClient = new WebClient();
                webClient.QueryString.Add("id", "420");
                var assertResponse = webClient.DownloadString(API_DATABASE + "getfollowers");
                var followers = JsonConvert.DeserializeObject<List<MiniUser>>(assertResponse);

                webClient = new WebClient();
                webClient.QueryString.Add("id", "420");
                assertUserResponse = webClient.DownloadString(API_DATABASE_DB + "get");
                user = JsonConvert.DeserializeObject<User>(assertUserResponse);

                webClient = new WebClient();
                webClient.QueryString.Add("id", "4200");
                assertUserResponse = webClient.DownloadString(API_DATABASE_DB + "get");
                user2 = JsonConvert.DeserializeObject<User>(assertUserResponse);

                Assert.IsNotNull(followers);
                Assert.IsTrue(followers.Find(follower => follower.userId.Equals(user2.userId)) != null, $"Follower is not in list");
                Assert.IsTrue(user2.following.Find(following => following.userId.Equals(user.userId)) != null, $"Following is not in list");


                // Removing Follower
                // Act
                webClient = new WebClient();
                parameters = new NameValueCollection();
                parameters.Add("userId", "420");
                parameters.Add("followerId", "4200");

                webClient.QueryString.Add(parameters);
                reactionResponse = webClient.UploadValues(API_DATABASE + "removefollower", "DELETE", webClient.QueryString);

                // Assert
                webClient = new WebClient();
                webClient.QueryString.Add("id", "420");
                assertResponse = webClient.DownloadString(API_DATABASE + "getfollowers");
                followers = JsonConvert.DeserializeObject<List<MiniUser>>(assertResponse);

                webClient = new WebClient();
                webClient.QueryString.Add("id", "420");
                assertUserResponse = webClient.DownloadString(API_DATABASE_DB + "get");
                user = JsonConvert.DeserializeObject<User>(assertUserResponse);

                webClient = new WebClient();
                webClient.QueryString.Add("id", "4200");
                assertUserResponse = webClient.DownloadString(API_DATABASE_DB + "get");
                user2 = JsonConvert.DeserializeObject<User>(assertUserResponse);

                Assert.IsNotNull(followers);
                Assert.IsTrue(followers.Find(follower => follower.userId.Equals(user2.userId)) == null, $"Follower is not in list");
                Assert.IsTrue(user2.following.Find(following => following.userId.Equals(user.userId)) == null, $"Following is not in list");
                isDeleted = true;


                //Assert.Fail("Request succeeded when it should have failed.");


            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                if (!isDeleted)
                    Assert.Fail("Unexpected error occured");
            }
            finally
            {
                // User Removal
                // Dispose
                WebClient webClient = new WebClient();
                webClient.QueryString.Add("id", "420");
                var deleteResponse = webClient.UploadString(API_DATABASE_DB + "delete", "DELETE", "");
                webClient = new WebClient();
                webClient.QueryString.Add("id", "4200");
                deleteResponse = webClient.UploadString(API_DATABASE_DB + "delete", "DELETE", "");
            }

        }

        [TestMethod]
        public void Create_Delete_Following()
        {
            User _user1 = new User("", "TestUser", "TestPassword1!", 420, "Test User", "I am a robot.", "img.img", new List<MiniUser>(), new List<MiniUser>(), new List<Post>(), 0);
            User _user2 = new User("", "TestUser2", "TestPassword2!", 4200, "Test User2", "I am a robot.", "img.img", new List<MiniUser>(), new List<MiniUser>(), new List<Post>(), 0);

            string jsonDataUser1 = JsonConvert.SerializeObject(_user1);
            string jsonDataUser2 = JsonConvert.SerializeObject(_user2);

            bool isDeleted = false;
            try
            {
                WebClient webClient = new WebClient();
                // User Creation
                // Act
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                string response = webClient.UploadString(API_DATABASE_DB + "create", jsonDataUser1);

                // Assert
                webClient = new WebClient();
                webClient.QueryString.Add("id", "420");
                string assertUserResponse = webClient.DownloadString(API_DATABASE_DB + "get");
                var user = JsonConvert.DeserializeObject<User>(assertUserResponse);

                Assert.IsNotNull(user);
                Assert.AreEqual(_user1.userId, user.userId, $"User Id does not match");
                Assert.AreEqual(_user1.username, user.username, $"User Name does not match");

                webClient = new WebClient();
                // User Creation
                // Act
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                response = webClient.UploadString(API_DATABASE_DB + "create", jsonDataUser2);

                // Assert
                webClient = new WebClient();
                webClient.QueryString.Add("id", "4200");
                assertUserResponse = webClient.DownloadString(API_DATABASE_DB + "get");
                var user2 = JsonConvert.DeserializeObject<User>(assertUserResponse);

                Assert.IsNotNull(user2);
                Assert.AreEqual(_user2.userId, user2.userId, $"User Id does not match");
                Assert.AreEqual(_user2.username, user2.username, $"User Name does not match");


                // Adding Following
                // Act
                webClient = new WebClient();
                var parameters = new NameValueCollection();
                parameters.Add("userId", "420");
                parameters.Add("followingId", "4200");

                webClient.QueryString.Add(parameters);
                var reactionResponse = webClient.UploadValues(API_DATABASE + "addfollowing", "POST", webClient.QueryString);

                // Assert
                webClient = new WebClient();
                webClient.QueryString.Add("id", "420");
                var assertResponse = webClient.DownloadString(API_DATABASE + "getfollowings");
                var followings = JsonConvert.DeserializeObject<List<MiniUser>>(assertResponse);

                webClient = new WebClient();
                webClient.QueryString.Add("id", "420");
                assertUserResponse = webClient.DownloadString(API_DATABASE_DB + "get");
                user = JsonConvert.DeserializeObject<User>(assertUserResponse);

                webClient = new WebClient();
                webClient.QueryString.Add("id", "4200");
                assertUserResponse = webClient.DownloadString(API_DATABASE_DB + "get");
                user2 = JsonConvert.DeserializeObject<User>(assertUserResponse);

                Assert.IsNotNull(followings);
                Assert.IsTrue(followings.Find(following => following.userId.Equals(user2.userId)) != null, $"Following is not in list");
                Assert.IsTrue(user2.followers.Find(follower => follower.userId.Equals(user.userId)) != null, $"Follower is not in list");


                // Removing Following
                // Act
                webClient = new WebClient();
                parameters = new NameValueCollection();
                parameters.Add("userId", "420");
                parameters.Add("followingId", "4200");

                webClient.QueryString.Add(parameters);
                reactionResponse = webClient.UploadValues(API_DATABASE + "removefollowing", "DELETE", webClient.QueryString);

                // Assert
                webClient = new WebClient();
                webClient.QueryString.Add("id", "420");
                assertResponse = webClient.DownloadString(API_DATABASE + "getfollowings");
                followings = JsonConvert.DeserializeObject<List<MiniUser>>(assertResponse);

                webClient = new WebClient();
                webClient.QueryString.Add("id", "420");
                assertUserResponse = webClient.DownloadString(API_DATABASE_DB + "get");
                user = JsonConvert.DeserializeObject<User>(assertUserResponse);

                webClient = new WebClient();
                webClient.QueryString.Add("id", "4200");
                assertUserResponse = webClient.DownloadString(API_DATABASE_DB + "get");
                user2 = JsonConvert.DeserializeObject<User>(assertUserResponse);

                Assert.IsNotNull(followings);
                Assert.IsTrue(followings.Find(following => following.userId.Equals(user2.userId)) == null, $"Following is not in list");
                Assert.IsTrue(user2.followers.Find(follower => follower.userId.Equals(user.userId)) == null, $"Follower is not in list");
                isDeleted = true;


                //Assert.Fail("Request succeeded when it should have failed.");


            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                if (!isDeleted)
                    Assert.Fail("Unexpected error occured");
            }
            finally
            {
                // User Removal
                // Dispose
                WebClient webClient = new WebClient();
                webClient.QueryString.Add("id", "420");
                var deleteResponse = webClient.UploadString(API_DATABASE_DB + "delete", "DELETE", "");
                webClient = new WebClient();
                webClient.QueryString.Add("id", "4200");
                deleteResponse = webClient.UploadString(API_DATABASE_DB + "delete", "DELETE", "");
            }

        }

        [TestMethod]
        public void Get_Followers_Followings()
        {
            User _user1 = new User("", "TestUser", "TestPassword1!", 420, "Test User", "I am a robot.", "img.img", new List<MiniUser>(), new List<MiniUser>(), new List<Post>(), 0);
            User _user2 = new User("", "TestUser2", "TestPassword2!", 4200, "Test User2", "I am a robot.", "img.img", new List<MiniUser>(), new List<MiniUser>(), new List<Post>(), 0);

            string jsonDataUser = JsonConvert.SerializeObject(_user1);


            bool success = false;

            try
            {

               
                // Arrange
                WebClient webClient = new WebClient();
                webClient.QueryString.Add("id", "4");

                // Act
                string response = webClient.DownloadString(API_DATABASE + "getfollowers");

                // Assert
                Assert.IsNotNull(response);

                // Arrange
                webClient = new WebClient();
                webClient.QueryString.Add("id", "4");

                // Act
                response = webClient.DownloadString(API_DATABASE + "getfollowings");

                // Assert
                Assert.IsNotNull(response);

                success = true;

            }
            catch (Exception)
            {
                if (!success)
                    Assert.Fail("Unexpected error occured");
            }
        }
    }
}
