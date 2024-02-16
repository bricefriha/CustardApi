<p align="center" class="container" >
  <img width="200px" src="https://user-images.githubusercontent.com/37577669/85275198-47b3ca00-b480-11ea-8273-d990295416a7.png" />
  
</p>

# What's Custard? 
[![NuGet](https://img.shields.io/nuget/v/Custard.svg?style=flat)](https://www.nuget.org/packages/Custard/)

Custard is a .NET standard plugin to call web APIs intuitively. 😁

Fully compatible with:
- **.NET MAUI**
- **Xamarin Forms**

# Documentation 📄
## Installation
- Package manager
  ```Bash
  Install-Package Custard -Version 0.3.3
  ```
- .NET CLI
  ```Bash
  dotnet add package Custard --version 0.3.3
  ```
## Custard.Service
- ### Instantiate a service object:

```C#
Service yourService = new Service(string host, int port = 80, bool sslCertificate = false); 
```
- ### Create headers
```C#
yourService.RequestHeaders.Add("Hearder", "Value "); // Do this for every headers
```

- ### Call a POST method

  **Parameters**:

  | Name      | Type     | Required     |
  | :------------- | :----------: | -----------: |
  |  *controller* | `string`   | ✔    |
  |  *action* | `string`   |  ❌   |
  |  *headers* | `IDictionary<string, string>`   |  ❌  |
  |  *jsonBody* | `string`   |   ❌  |
  |  *parameters* | `string[] / IDictonary<string,string>`   |   ❌  |


  **Usage**:
  - To return a string:
    ```C#
    yourService.Post (controller, action, headers, jsonBody, parameters);
    ```
  - To return a model (T is the model):
    ```C#
    yourService.Post<T> (controller, action, headers, jsonBody, parameters);
    ```
- ### Call a PUT method

  **Parameters**:

  | Name      | Type     | Required     |
  | :------------- | :----------: | -----------: |
  |  *controller* | `string`   | ✔    |
  |  *action* | `string`   |  ❌   |
  |  *headers* | `IDictionary<string, string>`   |  ❌  |
  |  *jsonBody* | `string`   |   ❌  |
  |  *parameters* | `string[] / IDictonary<string,string>`   |   ❌  |


  **Usage**:
  - To return a string:
    ```C#
    yourService.Put (controller, action, headers, jsonBody, parameters);
    ```
  - To return a model (T is the model):
    ```C#
    yourService.Put<T> (controller, action, headers, jsonBody, parameters);
    ```

- ### Call a GET method

  **Parameters**:

  | Name      | Type     | Required     |
  | :------------- | :----------: | -----------: |
  |  *controller* | `string`   | ✔    |
  |  *action* | `string`   |  ❌   |
  |  *headers* | `IDictionary<string, string>`   |  ❌  |
  |  *jsonBody* | `string`   |   ❌  |
  |  *parameters* | `string[] / IDictonary<string,string>`   |   ❌  |


  **Usage**:
  - To return a string:
  ```C#
  yourService.Get (controller, action, headers, jsonBody, parameters);
  ```
  - To return a model (T is the model):
  ```C#
  yourService.Get<T> (controller, action, headers, jsonBody, parameters);
  ```

## Passing Parameters to your requests
Custard now supports two types of parameters:
- Path parameters
- Query parameters

### Path parameters
To pass path parameters to your requests, you have to pass them as `string[]`:

**E.g**: for `/users/api/2/3/4` we would use:
``` C#
string action = "users";
string controller = "api";
string[] param = { "2", "3", "4" };
           
var resultStr = await yourService.Get(controller: controller, action: action, parameters: param);
```
### Path parameters
To pass query parameters to your requests, you have to pass them as `Dictionary<string, string>`:

**E.g**: for `/users/api?two=2&three=3&four=4` we would use:
``` C#
string action = "users";
string controller = "api";

Dictionary<string, string> param = new Dictionary<string, string>
{
    { "two", "2" },
    { "three", "3" },
    { "four", "4" }
};
           
var resultStr = await yourService.Get(controller: controller, action: action, parameters: param);
```

> ⚠ If you want to return a model, the HTTP response body has to be in JSON format


  **I didn't finish the documentation. That's why it's so ugly. Sorry about that 😁**

- ### Callback Error
  If needed, you can add a callback if the request faces an HTTP error. This will work with any method mentioned above. This will allow you to do an handle the error       more easily.
  Here's how it works:
  ``` Csharp
  var actualResult = await yourService.Get("todolists", headers: headers, callbackError: (err) => 
            {
                
            });
  ```
  - **code**: the error status code (HttpStatusCode).

