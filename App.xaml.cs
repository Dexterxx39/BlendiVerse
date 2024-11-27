using Microsoft.Maui.Storage;
using System;
using System.IO;

namespace BlendiVerseApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Set the GOOGLE_APPLICATION_CREDENTIALS environment variable for Firebase
            string firebaseCredentialsPath = Path.Combine(FileSystem.AppDataDirectory, "Resources", "Raw", "blendiverse-8339a-firebase-adminsdk-71rvf-d90b94c079.json");

            // Set the environment variable for Firebase
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", firebaseCredentialsPath);

            // Debugging: Check if the file exists (optional)
            Console.WriteLine("Credentials Path: " + firebaseCredentialsPath);
            Console.WriteLine("File Exists: " + File.Exists(firebaseCredentialsPath));

            // Set the MainPage of the app (typically for navigation purposes)
            MainPage = new AppShell();
        }
    }
}
