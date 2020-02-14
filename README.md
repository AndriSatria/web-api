# Web API

Developed using :
- Visual Studio 2019 Community Edition
- ASP.NET Core 3.1
- JWT Authentication
- EF Core
- Auto Mapper

# Requirement
- Visual Studio 2019(v16.4.0 or later) to run .NET Core 3

# How to run


Open the solution with Visual Studio 2019. Press Ctrl+F5 to run the app. Visual Studio launches a browser and navigates to https://localhost:(port), where (port) is a randomly chosen port number.

If you get a dialog box that asks if you should trust the IIS Express certificate, select Yes. In the Security Warning dialog that appears next, select Yes.

Change db connection in <code>ConfigureServices</code> method in Startup.cs and settings in appsettings.json

# How to use
Users
- GET /users : Get all users
- GET /users/(id) : Get specific user by id
- POST /users : Create new user <b>(Anonymous allowed)</b>
- POST /users/authenticate : Get token with registered user <b>(Get JWT for authorization & anonymous allowed)</b>
- PUT /users/(id) : Update specific user by id
- DELETE /users/(id) : Delete specific user by id

Transactions
- GET /purchasetransactions : Get all transactions
- GET /purchasetransactions/(id) : Get transaction by transaction id
- POST /purchasetransactions : Create transaction