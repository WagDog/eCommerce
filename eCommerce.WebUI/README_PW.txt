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