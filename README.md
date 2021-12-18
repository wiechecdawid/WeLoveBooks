# WeLoveBooks

## Description
WeLoveBooks is a web application for books enthusiasts, which allows you to find a book you might like. Do you have any opinion on a particular book? Write a review and help others to discover new books!

## Run a local instance
In Order to run the program locally, **you need to have your Super Admin username, email and password set up**. Preffered way to do so is to set up your user secrets. In VS you can right-click on the project in Solution Explorer and select option Manage User Secrets. You can also [follow .NET documentation](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=linux). The JSON format of the secret file should be as follows:
```
{
  "SuperAdmin": {
    "UserName": <your secret username>,
    "Email": <your secret email>,
    "Password": <your secret password>
  }
}
```
You also need to have your instance of the database (please find the *script.sql* file stored in the DataAccess project).
