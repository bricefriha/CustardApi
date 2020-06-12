# What's CustardApi?
Custard is a .NET core library allowing to make API method calls easier.

# Documentation

## Custard.Service
- ### Instanciate a service object:

```C#
Service yourService = new Service(string host, int port = 80, bool sslCertificate = false); 
```

- ### Call a POST method

```C#
yourService.ExecutePost<T>( string controller, string action = null, IDictionary<string, string> headers = null, string jsonBody = null, string[] parameters = null)
```




**I didn't finish the documentation, that why it's so uggly. Sorry about that 😁**
