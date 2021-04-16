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
    public class PostsControllerTests
    {

        public const string API_DATABASE = "https://localhost:44318/post/";
        public const string API_DATABASE_DB = "https://localhost:44318/db/";

        [TestMethod]
        public void Create_Delete_Post()
        {
            // Arrange
            User _user = new User("", "TestUser", "TestPassword1!", 420, "Test User", "I am a robot.", "img.img", new List<MiniUser>(), new List<MiniUser>(), new List<Post>(), 0);
            Post _post = new Post(420, "test dummy", "test caption", new List<Comment>(), new DateTime(), new List<int>(), new List<int>());
            _post.postId = "420-1";
            string jsonDataUser = JsonConvert.SerializeObject(_user);
            string jsonDataPost = JsonConvert.SerializeObject(_post);
            bool isDeleted = false;

            try
            {
                WebClient webClient = new WebClient();
                // User Creation
                // Act
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                string response = webClient.UploadString(API_DATABASE_DB + "create", jsonDataUser);

                // Assert
                webClient = new WebClient();
                webClient.QueryString.Add("id", "420");
                string assertResponse = webClient.DownloadString(API_DATABASE_DB + "get");
                var user = JsonConvert.DeserializeObject<User>(assertResponse);

                Assert.IsNotNull(user);
                Assert.AreEqual(_user.userId, user.userId, $"User Id does not match");
                Assert.AreEqual(_user.username, user.username, $"User Name does not match");


                // Post Creation
                // Act
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                response = webClient.UploadString(API_DATABASE + "addnewpost", jsonDataPost);

                // Assert
                webClient = new WebClient();
                webClient.QueryString.Add("postId", "420-1");
                assertResponse = webClient.DownloadString(API_DATABASE + "getpost");
                var post = JsonConvert.DeserializeObject<Post>(assertResponse);

                Assert.IsNotNull(post);
                Assert.AreEqual(_post.userId, post.userId, $"User Id does not match");
                Assert.AreEqual(_post.postId, post.postId, $"Post Id does not match");

                // Post Removal
                // Act
                webClient = new WebClient();
                webClient.QueryString.Add("postId", "420-1");
                string deleteResponse = webClient.UploadString(API_DATABASE + "removepost", "DELETE", "");
                isDeleted = true;

                // Assert
                webClient = new WebClient();
                webClient.QueryString.Add("postId", "420-1");
                assertResponse = webClient.DownloadString(API_DATABASE + "getpost");

                Assert.Fail("Request succeeded when it should have failed.");

                
            }
            catch (Exception)
            {
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
            }
        }

        [TestMethod]
        public void GetPost()
        {
            bool success = false;
            try
            {
                // Arrange
                WebClient webClient = new WebClient();
                webClient.QueryString.Add("postId", "4-1");

                // Act
                string response = webClient.DownloadString(API_DATABASE + "getpost");

                // Assert
                Assert.IsNotNull(response);

                var post = JsonConvert.DeserializeObject<Post>(response);

                Assert.IsNotNull(post);

                Assert.AreEqual("4-1", post.postId);

                success = true;

                // Arrange
                webClient.QueryString.Add("postId", "4--1");

                // Act
                response = webClient.DownloadString(API_DATABASE + "getpost");

                // Assert
                Assert.Fail("Request succeeded when it should have failed.");

            }
            catch (Exception)
            {
                if (!success)
                    Assert.Fail("Unexpected error occured");
            }
        }

        [TestMethod]
        public void Add_Remove_Reaction()
        {
            // Arrange
            User _user1 = new User("", "TestUser", "TestPassword1!", 420, "Test User", "I am a robot.", "img.img", new List<MiniUser>(), new List<MiniUser>(), new List<Post>(), 0);
            User _user2 = new User("", "TestUser2", "TestPassword2!", 4200, "Test User2", "I am a robot.", "img.img", new List<MiniUser>(), new List<MiniUser>(), new List<Post>(), 0);

            Post _post = new Post(420, "test dummy", "test caption", new List<Comment>(), new DateTime(), new List<int>(), new List<int>());
            _post.postId = "420-1";
            string jsonDataUser = JsonConvert.SerializeObject(_user1);
            string jsonDataPost = JsonConvert.SerializeObject(_post);
            bool isDeleted = false;

            try
            {
                WebClient webClient = new WebClient();
                // User Creation
                // Act
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                string response = webClient.UploadString(API_DATABASE_DB + "create", jsonDataUser);

                // Assert
                webClient = new WebClient();
                webClient.QueryString.Add("id", "420");
                string assertResponse = webClient.DownloadString(API_DATABASE_DB + "get");
                var user = JsonConvert.DeserializeObject<User>(assertResponse);

                Assert.IsNotNull(user);
                Assert.AreEqual(_user1.userId, user.userId, $"User Id does not match");
                Assert.AreEqual(_user1.username, user.username, $"User Name does not match");


                // Post Creation
                // Act
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                response = webClient.UploadString(API_DATABASE + "addnewpost", jsonDataPost);

                // Assert
                webClient = new WebClient();
                webClient.QueryString.Add("postId", "420-1");
                assertResponse = webClient.DownloadString(API_DATABASE + "getpost");
                var post = JsonConvert.DeserializeObject<Post>(assertResponse);

                Assert.IsNotNull(post);
                Assert.AreEqual(_post.userId, post.userId, $"User Id does not match");
                Assert.AreEqual(_post.postId, post.postId, $"Post Id does not match");


                // Adding Reaction
                // Act
                webClient = new WebClient();
                var parameters = new NameValueCollection();
                parameters.Add("userId", "4200");
                parameters.Add("postId", "420-1");

                webClient.QueryString.Add(parameters);
                var reactionResponse = webClient.UploadValues(API_DATABASE + "addlikereaction", "POST", webClient.QueryString);
                
                // Assert
                webClient = new WebClient();
                webClient.QueryString.Add("postId", "420-1");
                assertResponse = webClient.DownloadString(API_DATABASE + "getpost");
                post = JsonConvert.DeserializeObject<Post>(assertResponse);

                Assert.IsNotNull(post);
                Assert.IsTrue(post.likes.Contains(4200), $"User Id is not in like list");


                // Removing Reaction
                // Act
                webClient = new WebClient();
                parameters = new NameValueCollection();
                parameters.Add("userId", "4200");
                parameters.Add("postId", "420-1");

                webClient.QueryString.Add(parameters);
                reactionResponse = webClient.UploadValues(API_DATABASE + "removelikereaction", "DELETE", webClient.QueryString);

                // Assert
                webClient = new WebClient();
                webClient.QueryString.Add("postId", "420-1");
                assertResponse = webClient.DownloadString(API_DATABASE + "getpost");
                post = JsonConvert.DeserializeObject<Post>(assertResponse);

                Assert.IsNotNull(post);
                Assert.IsTrue(!post.likes.Contains(4200), $"User Id is not in like list");
                isDeleted = true;


                Assert.Fail("Request succeeded when it should have failed.");


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
            }
        }

        [TestMethod]
        public void AddComment()
        {
            // Arrange
            User _user1 = new User("", "TestUser", "TestPassword1!", 420, "Test User", "I am a robot.", "img.img", new List<MiniUser>(), new List<MiniUser>(), new List<Post>(), 0);
            User _user2 = new User("", "TestUser2", "TestPassword2!", 4200, "Test User2", "I am a robot.", "img.img", new List<MiniUser>(), new List<MiniUser>(), new List<Post>(), 0);

            Post _post = new Post(420, "test dummy", "test caption", new List<Comment>(), new DateTime(), new List<int>(), new List<int>());
            _post.postId = "420-1";
            Comment _comment = new Comment(_user2.username, _post.postId, "Testing is fun! Bip! Bop!");
            _post.comments.Add(_comment);
            _user1.posts.Add(_post);

            string jsonDataUser = JsonConvert.SerializeObject(_user1);
            string jsonDataPost = JsonConvert.SerializeObject(_post);
            string jsonDataComment = JsonConvert.SerializeObject(_comment);
            bool isDeleted = false;

            try
            {
                WebClient webClient = new WebClient();
                // User Creation
                // Act
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                string response = webClient.UploadString(API_DATABASE_DB + "create", jsonDataUser);

                // Assert
                webClient = new WebClient();
                webClient.QueryString.Add("id", "420");
                string assertResponse = webClient.DownloadString(API_DATABASE_DB + "get");
                var user = JsonConvert.DeserializeObject<User>(assertResponse);

                Assert.IsNotNull(user);
                Assert.AreEqual(_user1.userId, user.userId, $"User Id does not match");
                Assert.AreEqual(_user1.username, user.username, $"User Name does not match");


                // Post Creation
                // Act
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                response = webClient.UploadString(API_DATABASE + "addnewpost", jsonDataPost);

                // Assert
                webClient = new WebClient();
                webClient.QueryString.Add("postId", "420-1");
                assertResponse = webClient.DownloadString(API_DATABASE + "getpost");
                var post = JsonConvert.DeserializeObject<Post>(assertResponse);

                Assert.IsNotNull(post);
                Assert.AreEqual(_post.userId, post.userId, $"User Id does not match");
                Assert.AreEqual(_post.postId, post.postId, $"Post Id does not match");


                // Adding Comment
                // Act
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                response = webClient.UploadString(API_DATABASE + "addnewComment", jsonDataComment);

                // Assert
                webClient = new WebClient();
                webClient.QueryString.Add("postId", "420-1");
                assertResponse = webClient.DownloadString(API_DATABASE + "getpost");
                post = JsonConvert.DeserializeObject<Post>(assertResponse);

                Assert.IsNotNull(post);
                Assert.IsTrue(_post.comments[0].username.Equals(post.comments[0].username) && _post.comments[0].content.Equals(post.comments[0].content), $"User Id is not in like list");



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
            }
        }
    }
}
