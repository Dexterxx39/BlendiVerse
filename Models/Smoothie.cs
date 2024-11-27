using Google.Cloud.Firestore;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BlendiVerseApp.Models
{
    [FirestoreData] // Mark this class as Firestore data model
    public partial class Smoothie : ObservableObject
    {
        [FirestoreProperty]
        public string Name { get; set; }

        [FirestoreProperty]
        public string Image { get; set; }

        [FirestoreProperty]
        public double Price { get; set; }

        [ObservableProperty, NotifyPropertyChangedFor(nameof(Amount))]
        private int _cartQuantity;

        // This property is computed based on CartQuantity and Price
        public double Amount => CartQuantity * Price;

        // Clone method to create a copy of the smoothie
        public Smoothie Clone() => MemberwiseClone() as Smoothie;

        // Method to get the image path from local resources
        public string GetLocalImagePath()
        {
            return $"Resources/Images/{Image}";
        }
    }
}
