namespace Simulator.DataStore.Stores
{
    using Simulator.DataObjects;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    public class TripStore : BaseStore//, IBaseStore<Trip>
    {
        public TripStore(string EndPoint)
        {
            base.InitializeStore(EndPoint);
        }

        public async Task<Trip> GetItemAsync(string id)
        {
            Trip trip = null;
            HttpResponseMessage response = await Client.GetAsync($"/api/trips/{id}");
            if (response.IsSuccessStatusCode)
            {
                response.Content.Headers.ContentType.MediaType = "application/json";
                trip = await response.Content.ReadFromJsonAsync<Trip>();
            }
            return trip;
        }

        public async Task<List<Trip>> GetItemsAsync()
        {
            List<Trip> trips = null;
            HttpResponseMessage response = await Client.GetAsync("/api/trips");
            if (response.IsSuccessStatusCode)
            {
                response.Content.Headers.ContentType.MediaType = "application/json";
                trips = await response.Content.ReadFromJsonAsync<List<Trip>>();
            }
            return trips;
        }

        

    }
}