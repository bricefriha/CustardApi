<p align="center" class="container" >
  <img width="200px" src="https://user-images.githubusercontent.com/37577669/85275198-47b3ca00-b480-11ea-8273-d990295416a7.png" />
  
</p>

# What's Custard? 
Custard is a .NET core library allowing to make API method calls easily. 😁

[![NuGet](https://img.shields.io/nuget/v/Custard.svg?style=flat)](https://www.nuget.org/packages/Custard/)

# Documentation 📄

## Custard.Service
- ### Instanciate a service object:

```C#
Service yourService = new Service(string host, int port = 80, bool sslCertificate = false); 
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

  ```C#
  yourService.ExecuteGet<T> (controller, action, headers, jsonBody, parameters);
  ```



**I didn't finish the documentation, that why it's so ugly. Sorry about that 😁**
