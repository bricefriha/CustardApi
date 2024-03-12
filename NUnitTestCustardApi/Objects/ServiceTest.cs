
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
using NUnit.Framework;
using System.Threading;

namespace NUnitTestCustardApi
{
    class ServiceTest
    {
        private static Service _gamhubService;
        private Service _serviceRick;
        private Service _serviceWord;
        private Service _serviceReqres;

        [SetUp]
        public void Setup()
        {
            _gamhubService = new Service("api.gamhub.io", sslCertificate: true);
            _serviceRick = new Service("rickandmortyapi.com", sslCertificate: true);
            _serviceWord = new Service("random-word-api.herokuapp.com", sslCertificate: true);
            _serviceReqres = new Service("reqres.in", sslCertificate: true);
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
            constructorNonSSl = new Service("localhost", 2020);

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
            User actualResult = await _gamhubService.Post<User>("users", jsonBody: body, "authenticate");

            _gamhubService.Dispose();

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
            Collection<Article> actualResult = await _gamhubService.Get<Collection<Article>>("feeds");

            _gamhubService.Dispose();

            // Assert
            Assert.Greater(actualResult?.Count, 0);
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

            _gamhubService.RequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6Ikt3M1hJWC1hcVZsNmdtcE03bWRIbCJ9.eyJpc3MiOiJodHRwczovL2lzYW1vYmlsZWFwcC51cy5hdXRoMC5jb20vIiwic3ViIjoiZFhxeUNvQjB1VndIZWNaeU9CUjdKdzAxU3haZGxHWjlAY2xpZW50cyIsImF1ZCI6Imh0dHBzOi8vaXNhbW9iaWxlYXBwLnVzLmF1dGgwLmNvbS9hcGkvdjIvIiwiaWF0IjoxNjYwNzYwNTcxLCJleHAiOjE2NjA4NDY5NzEsImF6cCI6ImRYcXlDb0IwdVZ3SGVjWnlPQlI3SncwMVN4WmRsR1o5Iiwic2NvcGUiOiJyZWFkOmNsaWVudF9ncmFudHMgY3JlYXRlOmNsaWVudF9ncmFudHMgcmVhZDp1c2VycyIsImd0eSI6ImNsaWVudC1jcmVkZW50aWFscyJ9.wAdWudh-iVAdOhaMHpC5w2cysYZD7eJzNlT8zlXDzTx90JTKTiNqTOSRI0NUnQBh1eKOXz6gTjVTOJjjsgdoBVynwoY9wo7wynXpiZx3OxlkZDvG99VK2-UwjLKQZ-4BiBcuOOCxavFRgTROq3ea4PPHiTjS6rKlGEKczBaIZTmBPQ_OkE6PN2u4ccZBNLj0lLh1BLZbHvxpSazqBMRyEc5dT4OK4ZbFvJjDUrYNndTyFphooeIAPscfhaA39dQK9DBLr6ulTUKnasUHbF4nMsxR8J4E30IRLM1WGroQY4PyOI_MUZfaAhLEh3ZagqIPKKdZfd03dHTLzK2IS9X_Xg");


            // Act
            Collection<Todolist> actualResult = await _gamhubService.Get<Collection<Todolist>>("todolists");

            _gamhubService.Dispose();

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

            _gamhubService.RequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI1ZWUyNGVlMzc5NmQ5NTE5ZmNjMWIyNWQiLCJpYXQiOjE1OTE4ODk2MzV9.tpUBOo3D0JvS0XOQzGdnag4olb8HFOZEFmVAoEINYUU");


            // Act
            Todolist actualResult = await _gamhubService.Put<Todolist>(controller: "todolists", 
                                                                 action: "rename", 
                                                                 parameters: parameters, 
                                                                 jsonBody: body );

            _gamhubService.Dispose();

            // Assert
            Assert.AreEqual(Expectation.User, actualResult?.User);

            _gamhubService.Dispose();
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
            _gamhubService.RequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI1ZWUyNGVlMzc5NmQ5NTE5ZmNjMWIyNWQiLCJpYXQiOjE1OTE4ODk2MzV9.tpUBOo3D0JvS0XOQzGdnag4olb8HFOZEFmVAoEINYUU");
            ///
            /// Create the item that we gonna delete later on
            string body = "{ \"title\": \"Workout\" }";
            Todolist itemToDelete = await _gamhubService.Post<Todolist>("todolists", body, "create");
            /// 
            /// Put the id as parameters of the delete method
            string[] parameters = { itemToDelete.Id };

            // Act
            DeleteCode actualResult = await _gamhubService.Delete<DeleteCode>(controller:"todolists", parameters: parameters);

            _gamhubService.Dispose();

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
            string actualResult = await _gamhubService.Post("users", body, "authenticate",(e) => { });

            _gamhubService.Dispose();

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
            _gamhubService.RequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI1ZWUwZTI1NTU2Mjk0YzJjNzBlZTEyOGIiLCJpYXQiOjE1OTE3OTgwMDh9.dPiJu9zBRWEAOs-9DrPo9MtJrNt3HgNAlqtEt8QclMQ");


            // Act
            string actualResult = await _gamhubService.Get(controller:"todolists");

            _gamhubService.Dispose();
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

            _gamhubService.RequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI1ZWUyNGVlMzc5NmQ5NTE5ZmNjMWIyNWQiLCJpYXQiOjE1OTE4ODk2MzV9.tpUBOo3D0JvS0XOQzGdnag4olb8HFOZEFmVAoEINYUU");


            // Act
            string actualResult = await _gamhubService.Put(controller: "todolists", 
                                                     action: "rename", 
                                                     parameters: parameters, 
                                                     jsonBody: body);

            _gamhubService.Dispose();
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
            _gamhubService.RequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI1ZWUyNGVlMzc5NmQ5NTE5ZmNjMWIyNWQiLCJpYXQiOjE1OTE4ODk2MzV9.tpUBOo3D0JvS0XOQzGdnag4olb8HFOZEFmVAoEINYUU");
            ///
            /// Create the item that we gonna delete later on
            string body = "{ \"title\": \"Workout\" }";

            Todolist itemToDelete = await _gamhubService.Post<Todolist>("todolists", body, "create");

            /// 
            /// Put the id as parameters of the delete method
            string[] parameters = { itemToDelete.Id };

            // Act
            string actualResult = await _gamhubService.Delete("todolists", 
                                                        parameters: parameters);

            _gamhubService.Dispose();

            // Assert
            Console.WriteLine(actualResult);
            Assert.Pass();
        }
        [Test]
        public async Task GetMethodWithQueryParameters()
        {
            // Arrange
            string language = "en";
            int wordLength = 5;
            string controller = "word";

            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                { "length", $"{wordLength}" },
                { "lang", language }
            };

            // Act
            string result = await _serviceWord.Get(controller, jsonBody: null, parameters: parameters);


            // Assert
            Console.WriteLine(_serviceWord.LastCall);
            Console.WriteLine(result);
            Assert.IsTrue(!string.IsNullOrEmpty(result));

        }
        [Test]
        public async Task GetMethodWithPathParameters()
        {
            // Arrage
            string action = "users";
            string controller = "api";
            string[] param = { "2" };
           

            // Act
            var resultStr = await _serviceReqres.Get(controller: controller, 
                                                     action: action, 
                                                     parameters: param);
            // Assert
            Console.WriteLine(_serviceReqres.LastCall);
            Assert.IsNotNull(resultStr);
        }
        [Test]
        public async Task PostMethod()
        {
            // Arrange
            var userToCreate = new ReqresUser
            {
                Name = "morpheus",
                Job = "leader"
            };
            string action = "users";
            string controller = "api";

            // Act
            var result = await _serviceReqres.Post<ReqresUser>(controller: controller,
                                                               action: action,
                                                               payload: userToCreate);

            // Assert
            Console.WriteLine(JsonConvert.SerializeObject(result));
            Assert.IsNotNull(result);
        }
        [Test]
        public async Task PostMethodJsonPayload()
        {
            // Arrange
            var userToCreate = new ReqresUser
            {
                Name = "morpheus",
                Job = "leader"
            };
            string action = "users";
            string controller = "api";

            // Act
            var result = await _serviceReqres.Post<ReqresUser>(controller: controller, 
                                                               action: action, 
                                                               jsonBody: JsonConvert.SerializeObject(userToCreate) );
            
            // Assert
            Console.WriteLine(JsonConvert.SerializeObject(result));
            Assert.IsNotNull(result);
        }
        [Test]
        public async Task PutMethodWithPathParameters()
        {
            // Arrange
            var userToCreate = new ReqresUser
            {
                Name = "morpheus",
                Job = "leader"
            };
            string action = "users";
            string controller = "api";
            string[] param = { "2" };

            // Act
            var result = await _serviceReqres.Put<ReqresUser>(controller: controller, action: action, jsonBody: JsonConvert.SerializeObject(userToCreate), parameters: param );
            
            // Assert
            Console.WriteLine(JsonConvert.SerializeObject(result));
            Assert.IsNotNull(result);
        }
        [Test]
        public async Task DeleteMethodWithPathParameters()
        {
            // Arrage
            string action = "users";
            string controller = "api";
            string[] param = { "2" };

            // Act
            var result = await _serviceReqres.Delete(controller: controller, 
                                                     action: action, 
                                                     parameters: param, 
                                                     unSuccessCallback: (err) =>
            {
                Assert.Fail(err.ReasonPhrase);
            });

            // Assert
            Console.WriteLine(JsonConvert.SerializeObject(result));
            Assert.Pass();
        }
        [Test]
        [Timeout(5000)] // 5 seconds
        public async Task CancellationTokenRequest()
        {
            // Arrange
            const string controller = "feeds";
            using CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            var cancelationToken = cancellationTokenSource.Token;
            //

            // Act
            Task task =  _gamhubService.Get(controller: controller,
                                               cancellationToken: cancelationToken);
            // Simulate cancellation after 3 seconds
            await Task.Delay(TimeSpan.FromSeconds(3));

            // Request cancellation
            cancellationTokenSource.Cancel();

            // Assert
            Console.WriteLine(_gamhubService.LastCall);
            //Assert.Pass();
            Assert.Pass();


        }
    }
}