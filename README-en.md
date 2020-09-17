# Misskey .NET

[日本語](README.md)・English

A new Misskey API library for .NET that is easy and straightforward to use.

## Without Authentication

```cs
var io = new Misskey("misskey.io");

try
{
    var user = await io.ApiAsync<Dictionary<string, object>>("users/show", new {
        userId = "7rkr2cvs0v",
    });
    Console.WriteLine(user["username"]); // Admin
}
catch (MisskeyApiException e)
{
    // When an error object is returned from the server
    Console.WriteLine("Failed to fetch the user information.");
}
catch (HttpRequestException e)
{
    // When something happens to the server
    Console.WriteLine("Something happens to the server. Wait a while and try again.");
}
```
## MiAuth Authorization (for Misskey v12)

```cs
var miAuth = new MiAuth("misskey.io", "MissDeck", "https://missdeck.example.com/icon.png", null, Permission.All);
if (!miAuth.TryOpenBrowser())
{
    Console.WriteLine("Open the following URL in your browser to finish the authorization.");
    Console.WriteLine(miAuth.Url);
}
Console.WriteLine("Press ENTER after authorization is finished.");
Console.ReadLine();

try
{
    Misskey io = await miAuth.CheckAsync();
    var i = io.ApiAsync<Dictionary<string, object>("i");
    Console.WriteLine(user["username"]); // Authorized user's name
}
catch (MisskeyApiException e)
{
    Console.WriteLine("Failed to authorize.");
}
catch (HttpRequestException e)
{
    Console.WriteLine("Something happens to the server. Wait a while and try again.");
}
```

## Legacy Authorization (for Misskey v10, v11)

Coming soon

## Serialize the auth information

```cs
// will be serialized as simple INI-formatted string
// You should encrypt it before saving because it contains confidential data.
string serialized = misskey.Serialize();

// Instantiate a Misskey client from serialized string
string m = Misskey.Deserialize(serialized);
```