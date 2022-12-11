
 
 # VintriBeers

<br/>

This is a solution in response to the challenge of implementing the following:
<br/>
* Add a REST API endpoint to allow a user to add a rating to a beer
* Add a REST API endpoint to retrieve a list of beers.
* Add a custom Web API Action Filter Attribute
* Add unit tests

## Technologies

* OWIN
* .NET Framework 4.8.1
* MediatR
* FluentValidation
* XUnit
* Visual Studio 2019 Community

## Getting Started

1. Clone the repository into a directory of choice 
2. Open the .sln file in VS and build to install NuGet packages
3. Open appsettings.json in the VintriBeers.Api project and alter each of the two settings to reflect the file path where the repo was cloned to
4. Similar settings are found in the InitializeConfiguration method in the ApplicationTestBase.cs file in the VintriBeers.Application.Tests project
5. Right-click the VintriBeers.Api project and select Properties. Click the Web tab and then click the radio button labeled "Don't open a page. Wait for a request from an external application." and then hit that save icon
6. Ensure that the VintriBeers.Api project is Set as Startup Project and then hit F5

## Making it Work - Add a Rating

1. Open up Postman
2. Open a new request and make it a POST and paste in http://localhost:63770/api/Ratings/?id=1
3. Click the Body tab and select the raw option then copy and paste the following JSON and hit Send
   ```yaml
   {
        "userName": "alias@email.info",
        "rating": 3,
        "comments": "It's a beer"
   }

### Data/Logging Configuration

The user beer ratings data is stored in the file named database.json in the Data folder of the VintriBeers.Domain project.

Logging will be written to the Logging folder within the same project.

## Overview

### Domain

This contains all entities and logic specific to the domain layer.

### Application

This layer contains all application logic. It is dependent on the domain and externalities layers. 

### Externalities

This layer contains classes for accessing external resources such web services. 

