REMSLogic
=========

To work on this solution, you will need the follwoing items:
* Visual Studio 2013 (2012 may work)
* NuGet Package Manager
* The Git client of your choice

The following environments are setup:
* **Development** `http://dev.remslogic.com`
* **Production** `http://www.remslogic.com`

Database Updates
-----------------

Team City is setup to run FluentMigrator with each build.  As long as you properly code your database changes as migration in the `RemsLogic.Migrations` project the **Development** and **Production** database will automatically get updated.  Please refer to that project for example migrations.

To update your local database you'll simply need to run the migration project on your local box.  To do this:
1. Launch visual Studio **Developer Command Prompt** (*this will ensure MSBuild is in your path*)
2. navigate to the folder you're keeping your solution in
3. Modify the connection string in `Migration.msbuild` **MigrateDv** taget to match your setup
4. Run `msbuild /v:d /target:MigrateDev Migration.msbuild`

It might be a good idea to just add a new target and put your connection string there.
