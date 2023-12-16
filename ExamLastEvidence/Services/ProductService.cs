using ExamLastEvidence.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ExamLastEvidence.Services
{
    public class ProductService
    {
        private readonly HttpClient http;
        private readonly string url = "http://localhost:5045";
        public ProductService()
        {
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                url = "http://10.0.2.2:5045";
            }
            http = new HttpClient { BaseAddress = new Uri(url) };
        }
        public async Task<IEnumerable<ProductViewModel>?> Get()
        {

            var data = await this.http.GetFromJsonAsync<IEnumerable<ProductViewModel>>($"api/Products");
            if (data == null) return Enumerable.Empty<ProductViewModel>();
            foreach (var item in data)
            {
                item.RemotePictureUrl = $"{url}/Pictures/{item.Picture}";
            }
            return data;

        }
        public async Task<ProductViewModel?> Get(int id)
        {

            var data = await this.http.GetFromJsonAsync<ProductViewModel>($"api/Products/{id}");
            if (data == null) return null;
            data.RemotePictureUrl   = $"{url}/Pictures/{data.Picture}";
            return data;

        }
    }
}
