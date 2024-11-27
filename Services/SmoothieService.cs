using BlendiVerseApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlendiVerseApp.Services
{
    public class SmoothieService
    {
        private readonly FirestoreService _firestoreService;

        public SmoothieService(FirestoreService firestoreService)
        {
            _firestoreService = firestoreService;
        }

        // Fetch all smoothies from Firestore
        public async Task<IEnumerable<Smoothie>> GetAllSmoothiesAsync()
        {
            return await _firestoreService.GetSmoothiesAsync();
        }

        // Fetch popular smoothies from Firestore (random selection)
        public async Task<IEnumerable<Smoothie>> GetPopularSmoothiesAsync(int count = 6)
        {
            var smoothies = await GetAllSmoothiesAsync();
            return smoothies.OrderBy(p => Guid.NewGuid())
                            .Take(count);
        }

        // Search smoothies in Firestore
        public async Task<IEnumerable<Smoothie>> SearchSmoothiesAsync(string searchTerm)
        {
            var smoothies = await GetAllSmoothiesAsync();
            return string.IsNullOrWhiteSpace(searchTerm)
                ? smoothies
                : smoothies.Where(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }

        internal IEnumerable<Smoothie> GetPopularSmoothies()
        {
            throw new NotImplementedException();
        }
    }
}
