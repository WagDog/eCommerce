General Notes:-

Migrations:-
The Project must reference the Entity Framework
Migrations must be enabled through Tools->NuGet Package Manager->Package Manager Console. PM>Enable-Migrations
Add migrations from Code First workflow from the Package Manager Console. PM>Add-Migration <Name of Migration>
	Use the -Force switch to overwrite an existing migration if you have updated anything, prior to updating the database.
Update the database with any oustanding migrations from the Package Manager Console. PM>Update-Database

To view the new database, in the Solution Explorer panel, click the 'Show All Files' button
and in the project's App_Data folder will be the *.mdf file if using LocalDB. Double Click this to reveal the
Tables and schema in the database etc..

Note - The initial migration will only create tables relating to the IdentityModel which is in the Project's Models folder
To create migrations for the Models in the seperate Model project, we need to create some properties to those Models so that the
Package Manager knows about them , and can include them in the Migrations. This is done by creating a property for each model in 
the ApplicationDbContext class in the IdentityModel Model.

You can rollback to any migration by using:
Update-Database -TargetMigration:"MigrationName"
If you want to rollback all migrations you can use:
Update-Database -TargetMigration:0
or equivalent:
Update-Database -TargetMigration:$InitialDatabase

To find out which migrations have been applied to a database, do as follows from the package manager console:-
get-migrations

If a migration has been added, that has not been applied to the database, then the migration files can be deleted if the migration
is not required, or other parameters set in the model before re-running the add-migration with the same name, to override the 
original migration.
If a migration has been run on the database, and you wish to remove it, first run the update-database <Last Good Migration to Keep> command,
before removing the unwanted migration files. 

The Package Manager Console is used initially so much that it is worth creating a keyboard short cut to it, as follows:-
Tools->Options->Environment->Keyboard. Search for PackageManagerConsole. In the 'Press shortcut keys' box, enter Alt+/ and Alt+.
This will show the package manager console when the Alt key is held down while slash and dot are pressed.
This key combination doesn't conflict with other key combinations

## Using GIT ##
To push changes from your local machine to the online remote repository, use the Windows app. Click the Changes button and give a
name to the collection of changes. Press the Commit button and then Sync with the remote repository
To pull changes from the online repository and overwrite the local files, do as follows:-

git fetch --all, followed by:-
git reset --hard origin/master

Git fetch downloads the latest from remote without trying to merge or rebase anything.
Then the git reset resets the master branch to what you just fetched. 
The --hard option changes all the files in your working tree to match the files in origin/master

## Web API ##
In order to use the ASP.Net API framework, you must make some additions to your application as follows:-
Add GlobalConfiguration.Configure(WebApiConfig.Register); to the start of the Application_Start() function
in file Global.asax. 
if 'WebApiConfig' is not recognised, add the following code to the RouteConfig file in App_Start folder:-
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }

Create a folder in the application->Controllers folder.
Add a Web API Controller Class to this folder and name it after a model such as CustomersController. 
Note the plural model name in the API controller class which is convention. Refer to the CustomerControllers
class to view the RESTful convention.
Use the framework structure to code your API, and use the CustomersController API as an example.
Use the Postman REST Client extension in Chrome to test the Web API.
Domain models should not be used as parameters or return values in the API as that is considered bad practice,
as they are considered to be implimentation detail that is subject to change. These changes in future, may break 
other websites dependant on the structure of inputor returned types. This is a public contract with end user and must 
remain consistent from the outset. To solve this, we use a DTO or Data Transfer Object, which in essence is similar
to our view model, but uses very primitive data types. DTOs should not change through the life of the project, thus
ensuring applications reliant on the API do not break, even though the domain model may be refactored.
Also, when using a domain model as input into the API, a hacker can inject properties, that we really do not
want them to change. By using a DTO, we can restrict changes to just the fields we want to expose.
AutoMapper was installed into the WebUI and Model projects to remove the tediousness of mapping the domain model
fields to those in the corresponding DTO.
In the WebUI project, we added a class derived from the AutoMapper namespace 'Profile', called MappingProfile 
to the App_Start folder. This is where we create our mappings.
In order for our Mapper to work, we need to add the following to the Application_Start() function in file Global.asax:-
The standard JSON output from our API uses Camel Notation where the first letter is lowercase, but the start of
each word is capitalised. e.g Db field MembershipTypeId (Uses Pascal notation) comes out as membershipTypeId
Javascript prefers Camel notation when consumin an API object.
To make sure we output the API JSON data with Camel Notation, we must include the following lines in the 
WebApiConfig function :-
	var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
    jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

The REST convention states that we return a success code of 201 and not 200, when successfully creating a new Model
entity, such as a new customer. In order to do that, we need more control on our API return values, not just
returning a DTO or Model. We need to use the Http helper function Created() which returns a code 201 and also 
returns the URI of the newly created object, and the DTO or Model.
We now have two ways of creating a record. This is not the best practice. Better would be to modify our Create form
and POST to our API end point.

## JQuery ##
BootBox.js - We use this to create BootStrap type dialog boxes etc.. Rather than use Bootstrap directly, this is a 
simple library which makes it easier.
In Tools->Package Manager, browse for the BootBox package and install against the WebUI project.

