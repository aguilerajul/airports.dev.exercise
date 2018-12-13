# Developer Exercise

# Introduction
  In this exercise you will create a basic .NET solution from scratch. Create this exercise using C# and Visual Studio (2015 or higher). This document describes the components, how you structure your solution is up to you. We expect you to do this in about 8 hours.

# The web application
This application retrieves the content of a JSON feed that exposes airports. 
-	Download the content using the HttpClient class from the Microsoft.Net.Http NuGet package to download the JSON.
-	The program should get the airports from this JSON feed: https://raw.githubusercontent.com/jbrooksuk/JSON-Airports/master/airports.json
Show a list of all the European airports and offer the functionality to filter the list on country. Retrieving the list of the airports should only happen once every 5 minutes. A response header should be used to indicate whether the application got its data from the JSON feed.
-	The web application should be an MVC 5 application.
-	The name of the response header should be ‘from-feed’.
For extra points:
Create a view that shows the distance between two airports, airports are identified on IATA code. -	The application should accept two airports in the URL. 

# What we will look for
-	Using common frameworks
-	Cleverness & performance
-	Separation of concerns
-	The application should build and run on other computers

# Finished?
Clean out the bin folders and zip the solution. Share your solution via WeTransfer or an online git repository.
