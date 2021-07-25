<p align="center" class="container" >
  <img width="200px" src="https://user-images.githubusercontent.com/37577669/85275198-47b3ca00-b480-11ea-8273-d990295416a7.png" />
  
</p>

# What's Custard? 
[![NuGet](https://img.shields.io/nuget/v/Custard.svg?style=flat)](https://www.nuget.org/packages/Custard/)

Custard is a .NET standard plugin to intuitively call web APIs. 😁



# Documentation 📄
## Installation
- Package manager
  ```Bash
  Install-Package Custard -Version 0.1.4
  ```
- .NET CLI
  ```Bash
  dotnet add package Custard --version 0.1.4
  ```
## Custard.Service
- ### Instantiate a service object:

```C#
Service yourService = new Service(string host, int port = 80, bool sslCertificate = false); 
```
- ### Create headers
```C#
IDictionary<string, string> headers = new Dictionary<string, string>();
headers.Add("Hearder", "Value "); // Do this for every headers
```
- ### Call a POST method

  **Parameters**:

  | Name      | Type     | Required     |
  | :------------- | :----------: | -----------: |
  |  *controller* | string   | ✔    |
  |  *action* | string   |  ❌   |
  |  *headers* | IDictionary<string, string>   |  ❌  |
  |  *jsonBody* | string   |   ❌  |
  |  *parameters* | string[]   |   ❌  |


  **Usage**:
  - To return a string:
    ```C#
    yourService.ExecutePost (controller, action, headers, jsonBody, parameters);
    ```
  - To return a model (T is the model):
    ```C#
    yourService.ExecutePost<T> (controller, action, headers, jsonBody, parameters);
    ```
- ### Call a PUT method

  **Parameters**:

  | Name      | Type     | Required     |
  | :------------- | :----------: | -----------: |
  |  *controller* | string   | ✔    |
  |  *action* | string   |  ❌   |
  |  *headers* | IDictionary<string, string>   |  ❌  |
  |  *jsonBody* | string   |   ❌  |
  |  *parameters* | string[]   |   ❌  |


  **Usage**:
  - To return a string:
    ```C#
    yourService.ExecutePut (controller, action, headers, jsonBody, parameters);
    ```
  - To return a model (T is the model):
    ```C#
    yourService.ExecutePut<T> (controller, action, headers, jsonBody, parameters);
    ```

- ### Call a GET method

  **Parameters**:

  | Name      | Type     | Required     |
  | :------------- | :----------: | -----------: |
  |  *controller* | string   | ✔    |
  |  *action* | string   |  ❌   |
  |  *headers* | IDictionary<string, string>   |  ❌  |
  |  *jsonBody* | string   |   ❌  |
  |  *parameters* | string[]   |   ❌  |


  **Usage**:
  - To return a string:
  ```C#
  yourService.ExecuteGet (controller, action, headers, jsonBody, parameters);
  ```
  - To return a model (T is the model):
  ```C#
  yourService.ExecuteGet<T> (controller, action, headers, jsonBody, parameters);
  ```


> ⚠ If you want to return a model the Http response body has to be in JSON format


  **I didn't finish the documentation, that why it's so ugly. Sorry about that 😁**

- ### Callback Error
  If needed you can even add a callback in case the request face an HTTP error. This will work with any method mentioned above. This will allow you to do an handle the error       more easily.
  Here's how it works:
  ``` Csharp
  var actualResult = await _service.ExecuteGet("todolists", headers: headers, callbackError: (code) => 
            {
                switch (code):
                          case HttpStatusCode.NotFound: 
                                    // do something
                                break;
                           case HttpStatusCode.BadRequest: 
                                    // do something else
                                break;
                          // .. etc
            });
  ```
  - **code**: the error status code (HttpStatusCode).

