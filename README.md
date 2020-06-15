# What's Custard?
Custard is a .NET core library allowing to make API method calls easier.

# Documentation

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



**I didn't finish the documentation, that why it's so uggly. Sorry about that 😁**
