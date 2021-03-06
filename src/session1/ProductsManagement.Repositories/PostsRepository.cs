﻿using Newtonsoft.Json;
using ProductsManagement.Models;
using ProductsManagement.Models.Responses;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProductsManagement.Repositories
{
    public class PostsRepository : IDisposable
    {
        private string baseUrl = "http://10.10.86.140:8888";
        private readonly HttpClient client = new HttpClient();

        public void Dispose()
        {
            client.Dispose();
        }

        public async Task<List<GetPostResponse>> GetAllPostsAsync()
        {
            HttpResponseMessage response = await client.GetAsync($"{baseUrl}/posts");

            if (!response.IsSuccessStatusCode)
                return null;

            var content = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<List<GetPostResponse>>(content);

            return data;
        }

        public async Task<Post> CreatePostAsync(Post p)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(p));
            var response = await client.PostAsync($"{baseUrl}/posts", content);

            if (!response.IsSuccessStatusCode)
                return null;

            var resultContent = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<Post>(resultContent);
            return data;
        }

        public async Task<List<GetProductResponse>> GetAllProductsAsync()
        {
            var httpMethod = new HttpMethod("GET");
            HttpRequestMessage message = new HttpRequestMessage(httpMethod, $"{baseUrl}/products");
            message.Headers.Add("Accept", "application/json");
            HttpResponseMessage response = await client.SendAsync(message);
            string content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<GetProductResponse>>(content);
            return result;
        }
    }
}
