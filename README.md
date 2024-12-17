İn this project we are using OpenAI Api
To use the ChatGPT API in your C# project, you’ll need an API access key. Here’s how to get one:

1. Log in to your OpenAI account.
2. Go to the “View API Keys” section.
3. Click on “Create API Key” and give it an appropriate name
4. Copy the API key, as you’ll need it later.



Setting up a new C# project
To create a new C# project, we are using Visual Studio but u can use Visual Studio Code or any other IDE that supports C#. Follow these steps:

-Open your preferred IDE and create a new C# project.
-Choose the “Console App” template and provide a name for your project.
-Click “Create” to generate the project.



İnstalling necessary packages
We’ll need to install some NuGet packages to help us interact with the ChatGPT API:

RestSharp: A library for making HTTP requests.
Newtonsoft.Json: A library for handling JSON data.

To install these packages, run the following commands in your IDE’s package manager console:
- Install-Package RestSharp
- Install-Package Newtonsoft.Json
