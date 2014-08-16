***** Airlink Readme *****

This is a basic personnel management app focused on military front offices. I built most of it during a class and have been working on it sporadically since then. It is very much a work in progress and feedback is appreciated.
The app uses a layered architecture with Presentation, Business, Service, & Domain layers working together using a factory. It's overkill for such a small app, but in the class we had to build it that way.
The front end uses WPF and is set up using the MVVM pattern to display the menus.

---Other project notes---
The project uses basic file serialization to persist the data. The file name is 'airlink.dat', and is located in the Service layer's app.config file, along with the rest of the key/value pairs for service implementations. The rest of the projects have links to that specific app.config file for their use.

Right now the testing and main project build don't have any data conflicts, because they are separate projects, and each makes a local version of 'airlink.dat' in their bin/Debug folder. This seems to work for now, although it's not ideal.

Now, the separate DbCollections class contains all the code to load and later save the data to a file. It is a singleton, and has a static initializer that deserializes the data into static collections for all service implementations to use. Then it uses a finalizer to serialize the data to the file when it is garbage collected.

***** Airlink Readme Doc *****