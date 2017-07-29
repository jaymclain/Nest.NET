// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NestServiceProvider.cs" company="HomeRun Software Systems">
//   Copyright (c) 2017 Jay McLain
//
//   Permission is hereby granted, free of charge, to any person 
//   obtaining a copy of this software and associated documentation
//   files (the "Software"), to deal in the Software without
//   restriction, including without limitation the rights to use,
//   copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the
//   Software is furnished to do so, subject to the following
//   conditions:
//
//   The above copyright notice and this permission notice shall be
//   included in all copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//   EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
//   OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND 
//   NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
//   HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
//   WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
//   FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
//   OTHER DEALINGS IN THE SOFTWARE.
// </copyright>
// <summary>
//   Defines the NestServiceProvider type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Net.Http;
using System.Net.Http.Headers;
using Nest.NET.Service.Infrastructure;
using Nest.NET.Service.Infrastructure.Json;

namespace Nest.NET.Service
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    internal class NestServiceProvider : INestServiceProvider
    {
        private const string ServiceUrl = "https://developer-api.nest.com/";

        private readonly ISerializer _serializer;
        private readonly HttpClient _httpClient;

        public NestServiceProvider(ServiceOptions options)
        {
            _serializer = new JsonSerializer();

            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", options.AccessToken);
        }

        private static string GetQueryStringFromParameterObject(object parameters)
        {
            return parameters.GetType().GetTypeInfo().DeclaredProperties.Aggregate(
                new StringBuilder(),
                (q, p) =>
                    {
                        if (q.Length > 0)
                        {
                            q.Append("&");
                        }

                        return q.Append($"{p.Name}={p.GetValue(parameters)}");
                    }).ToString();
        }
        
        public Task<T> GetAsync<T>(string entity, string entityId, string action, object parameters)
        {
            var url = new Uri(ServiceUrl);
            url = new Uri(url, $"{entity}/");

            if (!string.IsNullOrWhiteSpace(entityId))
            {
                url = new Uri(url, $"{entityId}/");
            }

            if (!string.IsNullOrWhiteSpace(action))
            {
                url = new Uri(url, $"{action}");
            }

            if (parameters != null)
            {
                var uri = new UriBuilder(url) { Query = GetQueryStringFromParameterObject(parameters) };
                url = uri.Uri;
            }

            return GetAsync<T>(url);
        }

        private Task<T> GetAsync<T>(Uri url)
        {
            var response = _httpClient.GetAsync(url).Result;
            if (!response.IsSuccessStatusCode)
            {
                ThrowError(response);
            }

            var content = response.Content.ReadAsStringAsync().Result;
            return Task.Run(() => _serializer.DeserializeObject<T>(content));
        }

        private void ThrowError(HttpResponseMessage response)
        {
            //var content = response.Content.ReadAsStringAsync().Result;
            //var error = _serializer.DeserializeObject<Error>(content);
            //throw new NestServiceException(error.Code, error.Message);

            throw new Exception("An unexpected error occurred.");
        }
    }
}
