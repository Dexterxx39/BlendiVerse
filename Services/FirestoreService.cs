#if ANDROID
using Android.Content;
using Android.App;
#endif

#if IOS
using Foundation;
using UIKit;
#endif

using Google.Cloud.Firestore;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlendiVerseApp.Services
{
    public class FirestoreService
    {
        private readonly FirestoreDb _firestoreDb;

        public FirestoreService()
        {
#if ANDROID
            var context = Android.App.Application.Context;
            var fileName = "blendiverse-8339a-firebase-adminsdk-71rvf-d90b94c079.json";

            // The path to store the credentials file in the app's internal storage
            var filePath = Path.Combine(context.FilesDir.AbsolutePath, fileName);

            // Check if the file already exists in the app's internal storage
            if (!File.Exists(filePath))
            {
                // Copy the file from Resources/raw to the internal storage
                using (var assetStream = context.Assets.Open(fileName))
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    assetStream.CopyTo(fileStream);
                }
            }

            // Set the environment variable to tell Firebase SDK to use this file
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filePath);
#endif

#if IOS
            var fileName = "blendiverse-8339a-firebase-adminsdk-71rvf-d90b94c079.json";
            var documentFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); // Get Documents directory path on iOS
            var filePath = Path.Combine(documentFolder, fileName);

            // Check if the file already exists in the app's documents directory
            if (!File.Exists(filePath))
            {
                // Get the file from Resources/Raw
                var resourcePath = NSBundle.MainBundle.PathForResource(fileName, null);
                if (resourcePath != null)
                {
                    // Copy the file from Resources/Raw to the Documents directory
                    File.Copy(resourcePath, filePath);
                }
            }

            // Set the environment variable to tell Firebase SDK to use this file
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filePath);
#endif

            // Initialize Firestore with your project ID
            _firestoreDb = FirestoreDb.Create("blendiverse-8339a"); // Replace with your Firebase project ID
        }

        // Fetch smoothies from Firestore
        public async Task<List<Smoothie>> GetSmoothiesAsync()
        {
            var querySnapshot = await _firestoreDb.Collection("smoothies").GetSnapshotAsync();
            return querySnapshot.Documents
                                .Where(doc => doc.Exists)
                                .Select(doc => doc.ConvertTo<Smoothie>())
                                .ToList();
        }
    }
}
