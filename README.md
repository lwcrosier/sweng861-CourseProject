# sweng861-CourseProject
Course Project for sweng861
To start the program:
First clone the repository
	Optionally, you can build the project yourself by running 'dotnet build --configuration release' from the root directory of the project

You then need to make sure you have set the following Environment Variable:
	ASPNETCORE_ENVIRONMENT = Production
	
Next, you need to modify the 'appsettings.json' in the release directory
	Change the 'DefaultDb' key under 'ConnectionStrings' to the full path to the SQLite DB that can be found in the cloned repository

Finally, from the Release Directory, you can run 'FlightBooking.exe'

With the program running, we can open our browser and navigate to:
http://localhost:5000

