using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using CustardApi.Objects;
using NUnitTestCustardApi.ModelsTest;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using System.Net;
using System.Reflection.PortableExecutable;
using Newtonsoft.Json;

namespace NUnitTestCustardApi
{
    class ServiceTest
    {
        private static Service _service;

        [SetUp]
        public void Setup()
        {
            _service = new Service("api.gamhub.io", sslCertificate: true) ;
        }
        [Test]
        public async Task LotusAiLogin()
        {
            //string body = $"{{ \"email\": \"{ _email}\", \"password\": \"{ _password }\" }}";
            await _service.Post(action: "authenticate", controller: "authenticateuserbyemail", jsonBody: "{ \"email\": \"jason+mobiletest1@lotusai.co\", \"password\": \"u1oX9es6IOIf\" }");
            Assert.Pass();
        }
        // Construtor
        //
        // Without SSL certificate
        [Test]
        public void ConstructorNonSSl()
        {
            // Arrange
            Service constructorNonSSl;

            // Act
            constructorNonSSl = new Service("localhost", 2520);

            // Assert
            Assert.AreEqual("http://localhost:2520/", constructorNonSSl.BaseUrl);
        }
        //
        // With a SSL certificate
        [Test]
        public void ConstructorSSl()
        {
            // Arrange
            Service constructorNonSSl;

            // Act
            constructorNonSSl = new Service("localhost", 2520, true);

            // Assert
            Assert.AreEqual("https://localhost:2520/", constructorNonSSl.BaseUrl);
        }

        //
        // Without any port field
        [Test]
        public void ConstructorNoPort()
        {
            // Arrange
            Service constructorNonSSl;

            // Act
            constructorNonSSl = new Service("localhost");

            // Assert
            Assert.AreEqual("http://localhost/", constructorNonSSl.BaseUrl);
        }
        //
        // Without a port field
        [Test]
        public void ConstructorPort()
        {
            // Arrange
            Service constructorNonSSl;

            // Act
            constructorNonSSl = new Service("localhost",2020);

            // Assert
            Assert.AreEqual("http://localhost:2020/", constructorNonSSl.BaseUrl);
        }

        // Post Method
        //
        // With a body no params no token
        [Test]
        public async Task PostMethodWithBody()
        {
            // Arrange
            User Expectation = new User
            {
                Username = "BriceFriha",
                FirstName = "Brice",
                LastName = "Friha",
            };

            string body = "{ \"email\": \"brice.friha@outlook.com\", \"password\": \"pwd\" }";

            // Act
            User actualResult = await _service.Post<User>( "users", "authenticate", jsonBody: body);

            _service.Dispose();

            // Assert
            Assert.AreEqual(Expectation.ToString(), actualResult?.ToString());
        }
        // Post Method
        // With a body a token but no params 
        [Test]
        public async Task GetMethod()
        {
            // Arrange
            //Collection<Todolist> Expectation = new Collection<Todolist>
            //{
            //    new Todolist
            //    {
            //        Title = "Shopping list",
            //        User = "5ee0e25556294c2c70ee128b"
            //    },
            //    new Todolist
            //    {
            //        Title = "Shopping list",
            //        User = "5ee0e25556294c2c70ee128b"
            //    },
            //    new Todolist
            //    {
            //        Title = "Shopping list",
            //        User = "5ee0e25556294c2c70ee128b"
            //    },
            //    new Todolist
            //    {
            //        Title = "Shopping list",
            //        User = "5ee0e25556294c2c70ee128b"
            //    },
            //    new Todolist
            //    {
            //        Title = "Shopping list",
            //        User = "5ee0e25556294c2c70ee128b"
            //    },

            //};

            //_service.RequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI1ZWUwZTI1NTU2Mjk0YzJjNzBlZTEyOGIiLCJpYXQiOjE1OTE3OTgwMDh9.dPiJu9zBRWEAOs-9DrPo9MtJrNt3HgNAlqtEt8QclMQ");


            // Act
            Collection<Article> actualResult = await _service.Get<Collection<Article>>("feeds");

            _service.Dispose();

            // Assert
            Assert.Greater(actualResult?.Count,0 );
        }
        // Post Method
        // With a body a token but no params 
        [Test]
        public async Task GetMethodWithToken()
        {
            // Arrange
            Collection<Todolist> Expectation = new Collection<Todolist>
            {
                new Todolist
                {
                    Title = "Shopping list",
                    User = "5ee0e25556294c2c70ee128b"
                },
                new Todolist
                {
                    Title = "Shopping list",
                    User = "5ee0e25556294c2c70ee128b"
                },
                new Todolist
                {
                    Title = "Shopping list",
                    User = "5ee0e25556294c2c70ee128b"
                },
                new Todolist
                {
                    Title = "Shopping list",
                    User = "5ee0e25556294c2c70ee128b"
                },
                new Todolist
                {
                    Title = "Shopping list",
                    User = "5ee0e25556294c2c70ee128b"
                },

            };

            _service.RequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6Ikt3M1hJWC1hcVZsNmdtcE03bWRIbCJ9.eyJpc3MiOiJodHRwczovL2lzYW1vYmlsZWFwcC51cy5hdXRoMC5jb20vIiwic3ViIjoiZFhxeUNvQjB1VndIZWNaeU9CUjdKdzAxU3haZGxHWjlAY2xpZW50cyIsImF1ZCI6Imh0dHBzOi8vaXNhbW9iaWxlYXBwLnVzLmF1dGgwLmNvbS9hcGkvdjIvIiwiaWF0IjoxNjYwNzYwNTcxLCJleHAiOjE2NjA4NDY5NzEsImF6cCI6ImRYcXlDb0IwdVZ3SGVjWnlPQlI3SncwMVN4WmRsR1o5Iiwic2NvcGUiOiJyZWFkOmNsaWVudF9ncmFudHMgY3JlYXRlOmNsaWVudF9ncmFudHMgcmVhZDp1c2VycyIsImd0eSI6ImNsaWVudC1jcmVkZW50aWFscyJ9.wAdWudh-iVAdOhaMHpC5w2cysYZD7eJzNlT8zlXDzTx90JTKTiNqTOSRI0NUnQBh1eKOXz6gTjVTOJjjsgdoBVynwoY9wo7wynXpiZx3OxlkZDvG99VK2-UwjLKQZ-4BiBcuOOCxavFRgTROq3ea4PPHiTjS6rKlGEKczBaIZTmBPQ_OkE6PN2u4ccZBNLj0lLh1BLZbHvxpSazqBMRyEc5dT4OK4ZbFvJjDUrYNndTyFphooeIAPscfhaA39dQK9DBLr6ulTUKnasUHbF4nMsxR8J4E30IRLM1WGroQY4PyOI_MUZfaAhLEh3ZagqIPKKdZfd03dHTLzK2IS9X_Xg");


            // Act
            Collection<Todolist> actualResult = await _service.Get<Collection<Todolist>>("todolists");

            _service.Dispose();

            // Assert
            Assert.AreEqual(Expectation.ToString(), actualResult?.ToString());
        }
        // Put Method
        [Test]
        public async Task PutMethodWithToken()
        {
            // Arrange
            Todolist Expectation = new Todolist
            {
                    Title = "Unit test",
                    User = "5ee24ee3796d9519fcc1b25d"
            };

            string body = "{ \"title\": \"Workout\" }";

            string[] parameters = { "5ee24eff796d9519fcc1b25e" };

            _service.RequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI1ZWUyNGVlMzc5NmQ5NTE5ZmNjMWIyNWQiLCJpYXQiOjE1OTE4ODk2MzV9.tpUBOo3D0JvS0XOQzGdnag4olb8HFOZEFmVAoEINYUU");


            // Act
            Todolist actualResult = await _service.Put<Todolist>("todolists", "rename", body, parameters);

            _service.Dispose();

            // Assert
            Assert.AreEqual(Expectation.User, actualResult?.User);

            _service.Dispose();
        }

        // Delete Method
        [Test]
        public async Task DeleteMethodWithToken()
        {
            // Arrange
            DeleteCode Expectation = new DeleteCode
            {
                Status = "OK",
            };
            ///
            /// Prepare the header
            _service.RequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI1ZWUyNGVlMzc5NmQ5NTE5ZmNjMWIyNWQiLCJpYXQiOjE1OTE4ODk2MzV9.tpUBOo3D0JvS0XOQzGdnag4olb8HFOZEFmVAoEINYUU");
            ///
            /// Create the item that we gonna delete later on
            string body = "{ \"title\": \"Workout\" }";
            Todolist itemToDelete = await _service.Post<Todolist>("todolists", "create", body);
            /// 
            /// Put the id as parameters of the delete method
            string[] parameters = { itemToDelete.Id };

            // Act
            DeleteCode actualResult = await _service.Delete<DeleteCode>("todolists", parameters: parameters);

            _service.Dispose();

            // Assert
            Assert.AreEqual(Expectation.Status, actualResult?.Status);
        }
        // Post Method
        //
        // With a body no params no token
        [Test]
        public async Task PostMethodWithBodyString()
        {
            // Arrange

            string body = "{ \"email\": \"brice.friha@outlook.com\", \"password\": \"pwd\" }";

            // Act
            string actualResult = await _service.Post( "users", "authenticate", jsonBody: body);

            _service.Dispose();

            // Assert
            Console.WriteLine(actualResult);
            Assert.Pass();
        }
        // Post Method
        // With a body a token but no params 
        [Test]
        public async Task GetMethodWithTokenString()
        {
            // Arrange
            _service.RequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI1ZWUwZTI1NTU2Mjk0YzJjNzBlZTEyOGIiLCJpYXQiOjE1OTE3OTgwMDh9.dPiJu9zBRWEAOs-9DrPo9MtJrNt3HgNAlqtEt8QclMQ");


            // Act
            string actualResult = await _service.Get ( "todolists");

            _service.Dispose();
            // Assert
            Console.WriteLine(actualResult);
            Assert.Pass();
        }
        // Put Method
        [Test]
        public async Task PutMethodWithTokenString()
        {
            // Arrange
            string body = "{ \"title\": \"Workout\" }";

            string[] parameters = { "5ee24eff796d9519fcc1b25e" };

            _service.RequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI1ZWUyNGVlMzc5NmQ5NTE5ZmNjMWIyNWQiLCJpYXQiOjE1OTE4ODk2MzV9.tpUBOo3D0JvS0XOQzGdnag4olb8HFOZEFmVAoEINYUU");


            // Act
            string actualResult = await _service.Put ("todolists", "rename", jsonBody: body, parameters: parameters);

            _service.Dispose();
            // Assert
            Console.WriteLine(actualResult);
            Assert.Pass();
        }

        // Delete Method
        [Test]
        public async Task DeleteMethodWithTokenString()
        {
            // Arrange
            ///
            /// Prepare the header
            _service.RequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI1ZWUyNGVlMzc5NmQ5NTE5ZmNjMWIyNWQiLCJpYXQiOjE1OTE4ODk2MzV9.tpUBOo3D0JvS0XOQzGdnag4olb8HFOZEFmVAoEINYUU");
            ///
            /// Create the item that we gonna delete later on
            string body = "{ \"title\": \"Workout\" }";

            Todolist itemToDelete = await _service.Post<Todolist>("todolists", "create", body);

            /// 
            /// Put the id as parameters of the delete method
            string[] parameters = { itemToDelete.Id };

            // Act
            string actualResult = await _service.Delete ("todolists", parameters: parameters);

            _service.Dispose();

            // Assert
            Console.WriteLine(actualResult);
            Assert.Pass();
        }
        // Test Callback with  http code status parameter
        [Test]
        public async Task MethodWithCallback()
        {
            // Arrange
            HttpStatusCode? expectationCode = HttpStatusCode.NotFound;
            HttpStatusCode? actualCode = HttpStatusCode.OK;

            IDictionary<string, string> headers = new Dictionary<string, string>();

            _service.RequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI1ZWUwZTI1NTU2Mjk0YzJjNzBlZTEyOGIiLCJpYXQiOjE1OTE3OTgwMDh9.dPiJu9zBRWEAOs-9DrPo9MtJrNt3HgNAlqtEt8QclMQ");


            // Act
            await _service.Get<Collection<Conference>>("errorTest", callbackError: (code) => {
                //actualCode = (code as HttpListenerException).ErrorCode;
            });

            _service.Dispose();

            // Assert
            Assert.AreEqual(expectationCode, actualCode);
        }
        // Test Callback with  http code status parameter
        [Test]
        public async Task MethodTest()
        {
        }
    }
}