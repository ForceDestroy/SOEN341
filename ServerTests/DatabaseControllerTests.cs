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

                var user = JsonConvert.DeserializeObject<List<User>>(response);

                Assert.IsNotNull(user);
            } catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void Get_CorrectId()
        {
            // Arrange
            WebClient webClient = new WebClient();
            
            webClient.QueryString.Add("id", "1");

            try
            {
                // Act
                string response = webClient.DownloadString(API_DATABASE + "get");

                // Assert
                Assert.IsNotNull(response);

                var user = JsonConvert.DeserializeObject<User>(response);

                Assert.IsNotNull(user);

                Assert.AreEqual(1, user.userId);
                Assert.AreEqual("Sango", user.username);
                
            } catch (Exception)
            {
                Assert.Fail("An error was encountered while getting user by id.");
            }

        }

        [TestMethod]
        public void Get_IncorrectId()
        {
            // Arrange
            WebClient webClient = new WebClient();

            webClient.QueryString.Add("id", "-1");

            try
            {
                // Act
                string response = webClient.DownloadString(API_DATABASE + "get");

                // Assert
                Assert.Fail("Request succeeded when it should have failed.");
            }
            catch (WebException e)
            {
                Assert.AreEqual(WebExceptionStatus.ProtocolError, e.Status, $"Unexpected Exception: Looking for {WebExceptionStatus.ProtocolError} but found {e.Status}");
            }
        }

    }
}
