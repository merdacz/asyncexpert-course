using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwaitExercises.Core
{
    public class AsyncHelpers
    {
        public static Task<string> GetStringWithRetries(HttpClient client, string url, int maxTries = 3, CancellationToken token = default)
        {  
            if (maxTries < 2)
            {
                throw new ArgumentException();
            }

            return GetStringWithRetriesInternal(client, url, maxTries, token);
        }

        public static async Task<string> GetStringWithRetriesInternal(HttpClient client, string url, int maxTries = 3, CancellationToken token = default)
        {
            Exception last = default;
            int attempt = 1;

            while (attempt <= maxTries)
            {
                try
                {
                    var response = await client.GetAsync(url, token);
                    response.EnsureSuccessStatusCode();
                    string result = await response.Content.ReadAsStringAsync();
                    return result;
                }
                catch (Exception ex)
                {
                    last = ex;
                    int nextDelay = (int)Math.Pow(2, attempt - 1);
                    TimeSpan delay = TimeSpan.FromSeconds(nextDelay);
                    await Task.Delay(delay, token);
                    attempt++;
                }
            }

            throw last ?? new InvalidOperationException("Should not really happen. ");
        }
    }
}
