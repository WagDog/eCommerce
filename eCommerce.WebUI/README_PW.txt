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

The Package Manager Console is used initially so much that it is worth creating a keyboard short cut to it, as follows:-
Tools->Options->Environment->Keyboard. Search for PackageManagerConsole. In the 'Press shortcut keys' box, enter Alt+/ and Alt+.
This will show the package manager console when the Alt key is held down while slash and dot are pressed.
This key combination doesn't conflict with other key combinations.
