***** Airlink Startup Doc *****

Week 8
Update 6/28
I used installshield to deploy the app. Some of the files are in VS online, the actual output is in the dropbox. Everything works fine, expect you get a LOT of errors for the program being from an unknown source and also you have to run the program as an administrator for the serialization to work correctly. 

The project now uses basic file serialization to persist the data. The file name 'airlink.dat', and is located in the Service layer's app.config file, along with the rest of the key/value pairs for service implementations. The rest of the projects have links to that specific app.config file for their use.

Right now the testing and main project build don't have any data conflicts, because they are separate projects, and each makes a local version of 'airlink.dat' in their bin/Debug folder. This seems to work for now, and I won't change it until I'm told to or if I find a better solution.

Now, the separate DbCollections class contains all the code to load and later save the data to a file. It is a singleton, and has a static initializer that deserializes the data into static collections for all service implementations to use. Then it uses a finalizer to serialize the data to the file when it is garbage collected.

***** Airlink Startup Doc *****