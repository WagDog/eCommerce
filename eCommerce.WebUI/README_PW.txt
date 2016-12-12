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
Datatables.js - This is an extension that we can use to replace our code at runtime for an HTML table with
a javascript one. Datatables has JSON objects as a possible source meaning we can use the API code to return data
from the server, and the Datatable component populates the table with the necessary data and renders it in the client.
This saves resources on the server, as it is only sending JSON data back to the client, and the client machine is 
doing the formatting and rendering to the client. This application model of the server sending only lightweight
JSON objects to the client and then the client doing all the work of formatting and rendering is why javascript
frameworks such Angular JS are becoming so popular.
Single Page Applications or SPAs, are becoming more and more the future, as it save the application providers 
money, as they are saving more processing resources.

## ASP.Net Identity ##
When a new MVC solution is created via the solution wizard, there are various options for authentication, ranging
from non, to Windows Authentication. The default system is to use Individual User Accounts.
Access can be restricted by default for the whole program by setting the following in the App_Start->FilterConfig file:-

	filters.Add(new AuthorizeAttribute());  // Set global authentication

This will only show the Login Pageas the main page, and require a valid login to proceed. Normally, a small portion of
site will be available, such as the main Index page, About page, Contact Us page etc.. These can be made available
by decorating the controller actions with the [AllowAnonymous] annotation. The controller declaration itself can be 
decorated with this annotation, which will affect all the controller actions contained in the controller, overiding the 
global authorize setting set in FilterConfig.
Controllers and their actions can be decorated with the [Authorize] attribute to require authorisation for that controller
or action. If an action is requested which is restricted by the [Authorize] attribute, the login page will be displayed, if
the user is not logged in.
Controllers and Actions can be restricted to users within certain Roles. Restrictions are placed on a controller or action
by decorating the controller/action with the [Authorize(Roles="canManageMovies, canManageCustomers")] annotation.
To check if a user is authenticated, or belonging to a certain role, the following code can be used:-
if( User.Identity.IsAuthenticated){...}
if( User.IsInRole("canManage"){...}

To populate the database with a default user and role, temporary code can be written in the AccoutController->Register() function:-
	var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());
	var roleManager = new RoleManager<IdentityRole>(roleStore);
	await roleManager.CreateAsync(new IdentityRole("RoleNameHere"));	// Insert the role name here.
	await UserManager.AddToRoleAsync(user.Id, "RoleNameHere");

When a new user is created with the above code in place, a new role is created with the supplied name in the AspNetRoles table.
The new user is created in the AspNetUsers table, and a record that ties up the new user with the new role is created in the 
AspNetUserRoles table.
Once the required initial Roles and Users have been created, we can create a Seed Migration to include these new users and roles
We create an empty migration called AddSeedUsers or similar. We then script the user and roles date to sql insert strings, by 
viewing the table data and then selecting the records we require and right clicking on them and selecting the 'script' option.
We add the script lines to the new migration up() function as sql commands. This is done for the AspNetUsers, AspNetRoles and 
AspNetUserRoles tables.
The records in the database are deleted and the migration run on the database with the update-database command. You can use
a specific migration with Update-Database -TargetMigration:"MigrationName" to redo an old migration.
Additional property fields can be added to the User by first of all adding the properties to the ApplicationUser model in IdentityModel
Once the new properties and annotations have been added, we then create a new migration and update the database. Views Register.cshtml
and ExternalLoginConfirmation.cshtml are then amended to include the form fields for the new properties. In the Account controller, 
the Register and ExternalLoginConfirmation actions which take view models, require editing to make sure the model being stored has the 
new fields set to the input view model.

If you wish to use social media logins, then refer to the tutorial on that.

## Performance Optimisation ##
Rules of thumb:-
Do not sacrifice the maintainability of your code to premature optimisation.
Be realistic and think like an engineer.
Be pragmatic and ensure your efforts have observable results and give value.
Premature optimisation is the root of all evil.

The 3 tiers of an application, i.e Database, Application and Client, should be tackled in that order, with most performance gains
coming from database optimisations. 
Database Tier:-
Schema - Every table must have a primary key
Tables should have relationships
Put indexes on columns that you filter on. Remember, too many indexes can have a detrimental impact on performance.
Avoid Entity-Attribute-Value (EAV Pattern) databases.

Queries - Use Glimpse extension to keep an eye on Entity Framework queries. If a query is slow, use a stored procedure.
Use Execution Plan in SQL server to find performance bottlenecks in your queries.
To use Glimpse, make sure NuGet packages Glimpse.Mvc5 and Glimpse.EF6 are installed. Then from the test browser, go to
http://localhost:56623/glimpse.axd and turn on the product. This will track your requests and show query times.

Application Tier:-
On pages where you have costly queries, on data that doesn't change frequently, use the [OutputCache] decoration on the relevant
controller or action.e.g [OutputCache(Duration= 50, Location = OutputCacheLocation.Server, VaryByParam = "*")]. Set the duration
to 0 to disable the cache.

Memory Caching can be used also for data that remains static, however, you should always make sure that this will actually make a real
improvement before using. Also make sure you are only doing this for views that are displaying the data and not editing it. Below is an example:-
	public ActionResult Index()
	{
		if(MemoryCache.Default["Genres"] == null)
		{
			MemoryCache.Default["Genres"] = _context.Genres.ToList();
		}

		var genres = MemoryCache.Default["Genres"] as IEnumerable<Genre>;

		return View();
	}

Using fuctions that are ....Async() does not improve performance, only scalability assuming you are not using a single SQL server backend.
You should be using SQL Azure.

Disable sessions in web.config. Sessions are a piece of memory in the web server allocated to each user. These impact the scalability of
an application. Applications should be stateless. To disable sessions edit the Web.config file as follows:-
  <system.web>
    <sessionState mode = "Off" />	// PW - Added to remove sessions
    <authentication mode="None" />
    <compilation debug="true" targetFramework="4.5.2" />
    <httpRuntime targetFramework="4.5.2" />
    <httpModules>
      <add name="ApplicationInsightsWebTracking" type="Microsoft.ApplicationInsights.Web.ApplicationInsightsHttpModule, Microsoft.AI.Web" />
    </httpModules>
  </system.web>

Use Release builds in production.

Client Tier:-
Put the JS and CSS files in bundles such as a LIB bundle. The compiler wil combine and minimise the files to be small and light.
Put the script bundles near the end of the <body> tag, usually in the _Layout.cshtml file, so that the rest of the html will be
rendered before the scripts are fully loaded, so the user can see something is happening. Exception is Modernizr which needs to be
in the head.
Return small lightweight DTOs from the API.
Render the html markup on the client. That is the case with single page applications. (SPA).
Compress images.
Use image sprites.
Reduce the amount of data stored in cookies, because they are sent back and forth with every request.
Use content delivery networks. 




