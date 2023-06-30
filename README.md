# ValGProject
This is a quick the ValGenesis Public Forum Coding test
You're presented with the main page wich has 2 links one for login and one for registration.
For both you're only asked for a user name and a password (both fields required).
In the login screen it checks if the combination exists on the database and if it does you're redirected to the Public forum page. If not you remain on the login page
For the Registration screen it works the same way, only difference is it checks if the user name exists or not.
In The Public Forum Page you can see all the topics. The ones created by you you'll be able to Edit them or Delete them.
Here you also have a link for the Home Page and the Create a Topic Page.
In the create a Topic page you'll be able to create your own topic!

## Technical
For the db creation I used the EntityFramework Migration (connectionString in appsettings)
