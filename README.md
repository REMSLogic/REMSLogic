REMSLogic
=========

To work on this solution, you will need the follwoing items:
* Visual Studio 2013 (2012 may work)
* NuGet Package Manager
* The Git client of your choice
* NUnit for unit testing
* ReSharper or some sort of integrated test runner is recommended.

The following environments are setup:
* **Development** `http://dev.remslogic.com`
* **Production** `http://www.remslogic.com`

Database Configuration
----------------------

1. Find the file named `Site/ConnectionStrings.config.example`
2. Create a copy of it
3. Rename the copy to `ConnectionStrings.config`
4. Remove the `.example` extension from the copy
5. Open the copy in Visual Studio or your favorite text editor
6. Update the connection string to match your database setup

This file is not managed by the VCS.  This will let you setup a connection string for your setup without messing up settings for other developers on the project.  The web.config transforms take care of setting up everything for deployments.

Database Updates
-----------------

Team City is setup to run FluentMigrator with each build.  As long as you properly code your database changes as migration in the `RemsLogic.Migrations` project the **Development** and **Production** database will automatically get updated.  Please refer to that project for example migrations.

To update your local database you'll simply need to run the migration project on your local box.  To do this:

1. Launch visual Studio **Developer Command Prompt** (*this will ensure MSBuild is in your path*)
2. Build the solution.
2. Navigate to the folder you're keeping your solution (for me it's `c:\my dev\remslogic\website`)
3. Modify the connection string in `Migration.msbuild` **MigrateDv** taget to match your setup
4. Run `msbuild /v:d /target:MigrateDev Migration.msbuild`

It might be a good idea to just add a new target and put your connection string there.  I know I need to further automate this process.  I'd eventually like to just get it added as part of the project build process.  I need to figure out some MSBuild commands to make that happen.
