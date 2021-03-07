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

namespace NUnitTestCustardApi
{
    class ServiceTest
    {
        private static Service _service;

        [SetUp]
        public void Setup()
        {
            _service = new Service("localhost", 80);
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
            User actualResult = await _service.ExecutePost<User>( "users", "authenticate", null,  body);

            // Assert
            Assert.AreEqual(Expectation.ToString(), actualResult.ToString());
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
            IDictionary<string, string> headers = new Dictionary<string, string>() ;

            headers.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI1ZWUwZTI1NTU2Mjk0YzJjNzBlZTEyOGIiLCJpYXQiOjE1OTE3OTgwMDh9.dPiJu9zBRWEAOs-9DrPo9MtJrNt3HgNAlqtEt8QclMQ");


            // Act
            Collection<Todolist> actualResult = await _service.ExecuteGet<Collection<Todolist>>( "todolists", null, headers);

            // Assert
            Assert.AreEqual(Expectation.ToString(), actualResult.ToString());
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
            IDictionary<string, string> headers = new Dictionary<string, string>();

            string[] parameters = { "5ee24eff796d9519fcc1b25e" };

            headers.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI1ZWUyNGVlMzc5NmQ5NTE5ZmNjMWIyNWQiLCJpYXQiOjE1OTE4ODk2MzV9.tpUBOo3D0JvS0XOQzGdnag4olb8HFOZEFmVAoEINYUU");


            // Act
            Todolist actualResult = await _service.ExecutePut<Todolist>("todolists", "rename", headers, body, parameters);

            // Assert
            Assert.AreEqual(Expectation.User, actualResult.User);
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
            IDictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI1ZWUyNGVlMzc5NmQ5NTE5ZmNjMWIyNWQiLCJpYXQiOjE1OTE4ODk2MzV9.tpUBOo3D0JvS0XOQzGdnag4olb8HFOZEFmVAoEINYUU");
            ///
            /// Create the item that we gonna delete later on
            string body = "{ \"title\": \"Workout\" }";
            Todolist itemToDelete = await _service.ExecutePost<Todolist>("todolists", "create", headers, body);
            /// 
            /// Put the id as parameters of the delete method
            string[] parameters = { itemToDelete.Id };

            // Act
            DeleteCode actualResult = await _service.ExecuteDelete<DeleteCode>("todolists", null, headers, null, parameters); 

            // Assert
            Assert.AreEqual(Expectation.Status, actualResult.Status);
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
            string actualResult = await _service.ExecutePost ( "users", "authenticate", null,  body);

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
            IDictionary<string, string> headers = new Dictionary<string, string>() ;

            headers.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI1ZWUwZTI1NTU2Mjk0YzJjNzBlZTEyOGIiLCJpYXQiOjE1OTE3OTgwMDh9.dPiJu9zBRWEAOs-9DrPo9MtJrNt3HgNAlqtEt8QclMQ");


            // Act
            string actualResult = await _service.ExecuteGet ( "todolists", null, headers);

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
            IDictionary<string, string> headers = new Dictionary<string, string>();

            string[] parameters = { "5ee24eff796d9519fcc1b25e" };

            headers.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI1ZWUyNGVlMzc5NmQ5NTE5ZmNjMWIyNWQiLCJpYXQiOjE1OTE4ODk2MzV9.tpUBOo3D0JvS0XOQzGdnag4olb8HFOZEFmVAoEINYUU");


            // Act
            string actualResult = await _service.ExecutePut ("todolists", "rename", headers, body, parameters);

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
            IDictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI1ZWUyNGVlMzc5NmQ5NTE5ZmNjMWIyNWQiLCJpYXQiOjE1OTE4ODk2MzV9.tpUBOo3D0JvS0XOQzGdnag4olb8HFOZEFmVAoEINYUU");
            ///
            /// Create the item that we gonna delete later on
            string body = "{ \"title\": \"Workout\" }";
            Todolist itemToDelete = await _service.ExecutePost<Todolist>("todolists", "create", headers, body);
            /// 
            /// Put the id as parameters of the delete method
            string[] parameters = { itemToDelete.Id };

            // Act
            string actualResult = await _service.ExecuteDelete ("todolists", null, headers, null, parameters);

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

            headers.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI1ZWUwZTI1NTU2Mjk0YzJjNzBlZTEyOGIiLCJpYXQiOjE1OTE3OTgwMDh9.dPiJu9zBRWEAOs-9DrPo9MtJrNt3HgNAlqtEt8QclMQ");


            // Act
            await _service.ExecuteGet("errorTest", headers: headers, callbackError: (code) => {
                actualCode = code;
            });

            // Assert
            Assert.AreEqual(expectationCode, actualCode);
        }
    }
}