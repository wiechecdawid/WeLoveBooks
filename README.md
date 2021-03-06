# WeLoveBooks

## Description
WeLoveBooks is a web application for books enthusiasts, which allows you to find a book you might like. Do you have any opinion on a particular book? Write a review and help others to discover new books!

## Run WeLoveBooks.PhotoBroker API
You can do that for example via console (while in the project folder, type `dotnet run -p .\WeLoveBooks.PhotoBroker`. Some test Cloudinary credentials have been added to appsettings.json for brevity. Preferably should be moved outside of the project.

## Run a local instance of WeLoveBooks.Mvc
In Order to run the program locally, **you need to have your Super Admin username, email and password set up**. Preffered way to do so is to set up your user secrets. In VS you can right-click on the project in Solution Explorer and select option Manage User Secrets. You can also [follow .NET documentation](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=linux). The JSON format of the secret file should be as follows:
```
{
  "SuperAdmin": {
    "UserName": <your secret username>,
    "Email": <your secret email>,
    "Password": <your secret password>
  },
  "Token": {
    "SecretKey": <long key>
  }
}
```
You also need to have your instance of the database (please find the *script.sql* file stored in the DataAccess project or update database with EF Core Tool and Migrations history).
