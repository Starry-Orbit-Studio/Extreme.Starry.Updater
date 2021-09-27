using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

#if !NET45
using System.Text.Json;
#else
using Newtonsoft.Json;
#endif

namespace Extreme.Starry.Updater.Client
{
    public delegate void DownloadProgressCallback(long process, long? length);
    public sealed class Client : HttpClient
    {

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="uri">下载地址</param>
        /// <param name="output">输出流</param>
        /// <param name="from">开始</param>
        /// <param name="to">结束</param>
        /// <param name="bufferSize">缓冲区大小</param>
        /// <param name="callback">进度回调</param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public async Task Download(
            Uri uri,
            Stream output,
            long? from = null,
            long? to = null,
            int bufferSize = 128,
            DownloadProgressCallback callback = null,
            CancellationTokenSource cancellation = default
            )
        {
            bool dispose = false;
            try
            {
                if (cancellation is null)
                {
                    cancellation = new CancellationTokenSource();
                    dispose = true;
                }
                using (HttpRequestMessage requese = new HttpRequestMessage
                {
                    RequestUri = uri,
                    Method = HttpMethod.Get
                })
                {
                    if (from.HasValue || to.HasValue)
                        requese.Headers.Range = new RangeHeaderValue(from, to);

                    var response = await SendAsync(requese, cancellation.Token).ConfigureAwait(false);
                    int bytesRead;
                    long? length = response.Content.Headers.ContentLength;
                    if ((response.Content.Headers.ContentRange?.To.HasValue ?? false) && (response.Content.Headers.ContentRange?.From.HasValue ?? false))
                        length = response.Content.Headers.ContentRange.To - response.Content.Headers.ContentRange.From;
                    long process = 0;
                    byte[] buffer = new byte[bufferSize];


                    using (var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    {
                        while ((bytesRead =
                            await stream.ReadAsync(buffer, 0, bufferSize, cancellation.Token)
                                        .ConfigureAwait(false)) > 0)
                        {
                            await output.WriteAsync(buffer, 0, bytesRead, cancellation.Token)
                                        .ConfigureAwait(false);

                            process += bytesRead;
                            callback?.Invoke(process, length);
                        }
                    }
                }
            }
            finally
            {
                if (dispose)
                    cancellation?.Dispose();
            }
        }


        public async Task<T> GetObjectFromJson<T>(Uri url, CancellationTokenSource cancellation = default)
        {

            bool dispose = false;
            try
            {
                if (cancellation is null)
                {
                    cancellation = new CancellationTokenSource();
                    dispose = true;
                }
#if NET45
                using (var ms = new MemoryStream())
                {
                    await Download(url, ms, cancellation: cancellation).ConfigureAwait(false);
                    await ms.FlushAsync().ConfigureAwait(false);
                    var str = Encoding.UTF8.GetString(ms.ToArray());
                    Debug.WriteLine(str);
                    return JsonConvert.DeserializeObject<T>(str);
                }
#else
                using var response = await GetAsync(url, cancellation.Token).ConfigureAwait(false);
                await using var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
#if DEBUG
                using StreamReader sr = new(stream);
                Debug.WriteLine(sr.ReadToEnd());
                stream.Seek(0, SeekOrigin.Begin);
#endif
                return await JsonSerializer.DeserializeAsync<T>(stream, cancellationToken: cancellation.Token);
#endif
            }
            finally
            {
                if (dispose)
                    cancellation?.Dispose();
            }
        }

        /// <summary>
        /// Get Http Header(Empty Response Body)
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async Task<HttpContentHeaders> GetUrlHead(Uri uri)
        {
            using (HttpRequestMessage requese = new HttpRequestMessage
            {
                RequestUri = uri,
                Method = HttpMethod.Head
            })
            {
                var response = await SendAsync(requese).ConfigureAwait(false);
                return response.Content.Headers;
            }
        }
    }
}
