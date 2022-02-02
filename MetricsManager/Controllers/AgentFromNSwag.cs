namespace MetricsManager.Controllers
{
    using System = global::System;

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "13.10.9.0 (NJsonSchema v10.4.1.0 (Newtonsoft.Json v11.0.0.0))")]
    public partial interface IClient
    {
        System.Threading.Tasks.Task ApiMetricsCpuCreateAsync(CpuMetricModelFromNSwag body);

        void ApiMetricsCpuCreate(CpuMetricModelFromNSwag body);

        System.Threading.Tasks.Task ApiMetricsCpuCreateAsync(CpuMetricModelFromNSwag body, System.Threading.CancellationToken cancellationToken);
        System.Threading.Tasks.Task ApiMetricsCpuAllAsync();

        void ApiMetricsCpuAll();

        System.Threading.Tasks.Task ApiMetricsCpuAllAsync(System.Threading.CancellationToken cancellationToken);
        System.Threading.Tasks.Task ApiMetricsCpuFromToAsync(System.DateTimeOffset fromTime, System.DateTimeOffset toTime);

        void ApiMetricsCpuFromTo(System.DateTimeOffset fromTime, System.DateTimeOffset toTime);

        System.Threading.Tasks.Task ApiMetricsCpuFromToAsync(System.DateTimeOffset fromTime, System.DateTimeOffset toTime, System.Threading.CancellationToken cancellationToken);
        System.Threading.Tasks.Task ApiMetricsCpuFromToPercentilesAsync(System.DateTimeOffset fromTime, System.DateTimeOffset toTime, PercentileFromNSwag percentile);

        void ApiMetricsCpuFromToPercentiles(System.DateTimeOffset fromTime, System.DateTimeOffset toTime, PercentileFromNSwag percentile);

        System.Threading.Tasks.Task ApiMetricsCpuFromToPercentilesAsync(System.DateTimeOffset fromTime, System.DateTimeOffset toTime, PercentileFromNSwag percentile, System.Threading.CancellationToken cancellationToken);
        System.Threading.Tasks.Task ApiMetricsDotnetFromToAsync(System.DateTimeOffset fromTime, System.DateTimeOffset toTime);

        void ApiMetricsDotnetFromTo(System.DateTimeOffset fromTime, System.DateTimeOffset toTime);

        System.Threading.Tasks.Task ApiMetricsDotnetFromToAsync(System.DateTimeOffset fromTime, System.DateTimeOffset toTime, System.Threading.CancellationToken cancellationToken);
        System.Threading.Tasks.Task ApiMetricsHddAllAsync();

        void ApiMetricsHddAll();

        System.Threading.Tasks.Task ApiMetricsHddAllAsync(System.Threading.CancellationToken cancellationToken);
        System.Threading.Tasks.Task ApiMetricsHddFromToAsync(System.DateTimeOffset fromTime, System.DateTimeOffset toTime);

        void ApiMetricsHddFromTo(System.DateTimeOffset fromTime, System.DateTimeOffset toTime);

        System.Threading.Tasks.Task ApiMetricsHddFromToAsync(System.DateTimeOffset fromTime, System.DateTimeOffset toTime, System.Threading.CancellationToken cancellationToken);
        System.Threading.Tasks.Task ApiMetricsNetworkFromToAsync(System.DateTimeOffset fromTime, System.DateTimeOffset toTime);

        void ApiMetricsNetworkFromTo(System.DateTimeOffset fromTime, System.DateTimeOffset toTime);

        System.Threading.Tasks.Task ApiMetricsNetworkFromToAsync(System.DateTimeOffset fromTime, System.DateTimeOffset toTime, System.Threading.CancellationToken cancellationToken);
        System.Threading.Tasks.Task ApiMetricsRamAllAsync();

        void ApiMetricsRamAll();

        System.Threading.Tasks.Task ApiMetricsRamAllAsync(System.Threading.CancellationToken cancellationToken);
        System.Threading.Tasks.Task ApiMetricsRamFromToAsync(System.DateTimeOffset fromTime, System.DateTimeOffset toTime);

        void ApiMetricsRamFromTo(System.DateTimeOffset fromTime, System.DateTimeOffset toTime);

        System.Threading.Tasks.Task ApiMetricsRamFromToAsync(System.DateTimeOffset fromTime, System.DateTimeOffset toTime, System.Threading.CancellationToken cancellationToken);
    }

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "13.10.9.0 (NJsonSchema v10.4.1.0 (Newtonsoft.Json v11.0.0.0))")]
    public partial class Client : IClient
    {
        private string _baseUrl = "";
        private System.Net.Http.HttpClient _httpClient;
        private System.Lazy<Newtonsoft.Json.JsonSerializerSettings> _settings;

        public Client(string baseUrl, System.Net.Http.HttpClient httpClient)
        {
            BaseUrl = baseUrl;
            _httpClient = httpClient;
            _settings = new System.Lazy<Newtonsoft.Json.JsonSerializerSettings>(CreateSerializerSettings);
        }

        private Newtonsoft.Json.JsonSerializerSettings CreateSerializerSettings()
        {
            var settings = new Newtonsoft.Json.JsonSerializerSettings();
            UpdateJsonSerializerSettings(settings);
            return settings;
        }

        public string BaseUrl
        {
            get { return _baseUrl; }
            set { _baseUrl = value; }
        }

        protected Newtonsoft.Json.JsonSerializerSettings JsonSerializerSettings { get { return _settings.Value; } }

        partial void UpdateJsonSerializerSettings(Newtonsoft.Json.JsonSerializerSettings settings);


        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, string url);
        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, System.Text.StringBuilder urlBuilder);
        partial void ProcessResponse(System.Net.Http.HttpClient client, System.Net.Http.HttpResponseMessage response);

        public System.Threading.Tasks.Task ApiMetricsCpuCreateAsync(CpuMetricModelFromNSwag body)
        {
            return ApiMetricsCpuCreateAsync(body, System.Threading.CancellationToken.None);
        }

        public void ApiMetricsCpuCreate(CpuMetricModelFromNSwag body)
        {
            System.Threading.Tasks.Task.Run(async () => await ApiMetricsCpuCreateAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public async System.Threading.Tasks.Task ApiMetricsCpuCreateAsync(CpuMetricModelFromNSwag body, System.Threading.CancellationToken cancellationToken)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/metrics/cpu/create");

            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    var content_ = new System.Net.Http.StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(body, _settings.Value));
                    content_.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
                    request_.Content = content_;
                    request_.Method = new System.Net.Http.HttpMethod("POST");

                    PrepareRequest(client_, request_, urlBuilder_);

                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);

                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            return;
                        }
                        else
                        if (status_ == 400)
                        {
                            string responseText_ = (response_.Content == null) ? string.Empty : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("\u041e\u0448\u0438\u0431\u043a\u0430 \u0432 \u0437\u0430\u043f\u0440\u043e\u0441\u0435", status_, responseText_, headers_, null);
                        }
                        else
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

        /// <summary>Получение всех метрик CPU</summary>
        /// <returns>Удачное выполнение запроса</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task ApiMetricsCpuAllAsync()
        {
            return ApiMetricsCpuAllAsync(System.Threading.CancellationToken.None);
        }

        /// <summary>Получение всех метрик CPU</summary>
        /// <returns>Удачное выполнение запроса</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public void ApiMetricsCpuAll()
        {
            System.Threading.Tasks.Task.Run(async () => await ApiMetricsCpuAllAsync(System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Получение всех метрик CPU</summary>
        /// <returns>Удачное выполнение запроса</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task ApiMetricsCpuAllAsync(System.Threading.CancellationToken cancellationToken)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/metrics/cpu/all");

            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("GET");

                    PrepareRequest(client_, request_, urlBuilder_);

                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);

                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            return;
                        }
                        else
                        if (status_ == 400)
                        {
                            string responseText_ = (response_.Content == null) ? string.Empty : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("\u041e\u0448\u0438\u0431\u043a\u0430 \u0432 \u0437\u0430\u043f\u0440\u043e\u0441\u0435", status_, responseText_, headers_, null);
                        }
                        else
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

        /// <summary>Получение всех метрик CPU в заданном диапазоне времени</summary>
        /// <param name="fromTime">начальная метка времени в формате DateTimeOffset</param>
        /// <param name="toTime">конечная метка времени в формате DateTimeOffset</param>
        /// <returns>Удачное выполнение запроса</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task ApiMetricsCpuFromToAsync(System.DateTimeOffset fromTime, System.DateTimeOffset toTime)
        {
            return ApiMetricsCpuFromToAsync(fromTime, toTime, System.Threading.CancellationToken.None);
        }

        /// <summary>Получение всех метрик CPU в заданном диапазоне времени</summary>
        /// <param name="fromTime">начальная метка времени в формате DateTimeOffset</param>
        /// <param name="toTime">конечная метка времени в формате DateTimeOffset</param>
        /// <returns>Удачное выполнение запроса</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public void ApiMetricsCpuFromTo(System.DateTimeOffset fromTime, System.DateTimeOffset toTime)
        {
            System.Threading.Tasks.Task.Run(async () => await ApiMetricsCpuFromToAsync(fromTime, toTime, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Получение всех метрик CPU в заданном диапазоне времени</summary>
        /// <param name="fromTime">начальная метка времени в формате DateTimeOffset</param>
        /// <param name="toTime">конечная метка времени в формате DateTimeOffset</param>
        /// <returns>Удачное выполнение запроса</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task ApiMetricsCpuFromToAsync(System.DateTimeOffset fromTime, System.DateTimeOffset toTime, System.Threading.CancellationToken cancellationToken)
        {
            if (fromTime == null)
                throw new System.ArgumentNullException("fromTime");

            if (toTime == null)
                throw new System.ArgumentNullException("toTime");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/metrics/cpu/from/{fromTime}/to/{toTime}");
            urlBuilder_.Replace("{fromTime}", System.Uri.EscapeDataString(fromTime.ToString("s", System.Globalization.CultureInfo.InvariantCulture)));
            urlBuilder_.Replace("{toTime}", System.Uri.EscapeDataString(toTime.ToString("s", System.Globalization.CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("GET");

                    PrepareRequest(client_, request_, urlBuilder_);

                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);

                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            return;
                        }
                        else
                        if (status_ == 400)
                        {
                            string responseText_ = (response_.Content == null) ? string.Empty : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("\u041e\u0448\u0438\u0431\u043a\u0430 \u0432 \u0437\u0430\u043f\u0440\u043e\u0441\u0435", status_, responseText_, headers_, null);
                        }
                        else
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

        /// <summary>Получение заданного перцентиля для метрик CPU в заданном диапазоне времени</summary>
        /// <param name="fromTime">начальная метка времени в формате DateTimeOffset</param>
        /// <param name="toTime">конечная метка времени в формате DateTimeOffset</param>
        /// <param name="percentile">требуемый перцентиль из Enum Percentile</param>
        /// <returns>Удачное выполнение запроса</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task ApiMetricsCpuFromToPercentilesAsync(System.DateTimeOffset fromTime, System.DateTimeOffset toTime, PercentileFromNSwag percentile)
        {
            return ApiMetricsCpuFromToPercentilesAsync(fromTime, toTime, percentile, System.Threading.CancellationToken.None);
        }

        /// <summary>Получение заданного перцентиля для метрик CPU в заданном диапазоне времени</summary>
        /// <param name="fromTime">начальная метка времени в формате DateTimeOffset</param>
        /// <param name="toTime">конечная метка времени в формате DateTimeOffset</param>
        /// <param name="percentile">требуемый перцентиль из Enum Percentile</param>
        /// <returns>Удачное выполнение запроса</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public void ApiMetricsCpuFromToPercentiles(System.DateTimeOffset fromTime, System.DateTimeOffset toTime, PercentileFromNSwag percentile)
        {
            System.Threading.Tasks.Task.Run(async () => await ApiMetricsCpuFromToPercentilesAsync(fromTime, toTime, percentile, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Получение заданного перцентиля для метрик CPU в заданном диапазоне времени</summary>
        /// <param name="fromTime">начальная метка времени в формате DateTimeOffset</param>
        /// <param name="toTime">конечная метка времени в формате DateTimeOffset</param>
        /// <param name="percentile">требуемый перцентиль из Enum Percentile</param>
        /// <returns>Удачное выполнение запроса</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task ApiMetricsCpuFromToPercentilesAsync(System.DateTimeOffset fromTime, System.DateTimeOffset toTime, PercentileFromNSwag percentile, System.Threading.CancellationToken cancellationToken)
        {
            if (fromTime == null)
                throw new System.ArgumentNullException("fromTime");

            if (toTime == null)
                throw new System.ArgumentNullException("toTime");

            if (percentile == null)
                throw new System.ArgumentNullException("percentile");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/metrics/cpu/from/{fromTime}/to/{toTime}/percentiles/{percentile}");
            urlBuilder_.Replace("{fromTime}", System.Uri.EscapeDataString(fromTime.ToString("s", System.Globalization.CultureInfo.InvariantCulture)));
            urlBuilder_.Replace("{toTime}", System.Uri.EscapeDataString(toTime.ToString("s", System.Globalization.CultureInfo.InvariantCulture)));
            urlBuilder_.Replace("{percentile}", System.Uri.EscapeDataString(ConvertToString(percentile, System.Globalization.CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("GET");

                    PrepareRequest(client_, request_, urlBuilder_);

                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);

                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            return;
                        }
                        else
                        if (status_ == 400)
                        {
                            string responseText_ = (response_.Content == null) ? string.Empty : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("\u041e\u0448\u0438\u0431\u043a\u0430 \u0432 \u0437\u0430\u043f\u0440\u043e\u0441\u0435", status_, responseText_, headers_, null);
                        }
                        else
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

        /// <summary>Получение всех метрик DotNet в заданном диапазоне времени</summary>
        /// <param name="fromTime">начальная метка времени в формате DateTimeOffset</param>
        /// <param name="toTime">конечная метка времени в формате DateTimeOffset</param>
        /// <returns>Удачное выполнение запроса</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task ApiMetricsDotnetFromToAsync(System.DateTimeOffset fromTime, System.DateTimeOffset toTime)
        {
            return ApiMetricsDotnetFromToAsync(fromTime, toTime, System.Threading.CancellationToken.None);
        }

        /// <summary>Получение всех метрик DotNet в заданном диапазоне времени</summary>
        /// <param name="fromTime">начальная метка времени в формате DateTimeOffset</param>
        /// <param name="toTime">конечная метка времени в формате DateTimeOffset</param>
        /// <returns>Удачное выполнение запроса</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public void ApiMetricsDotnetFromTo(System.DateTimeOffset fromTime, System.DateTimeOffset toTime)
        {
            System.Threading.Tasks.Task.Run(async () => await ApiMetricsDotnetFromToAsync(fromTime, toTime, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Получение всех метрик DotNet в заданном диапазоне времени</summary>
        /// <param name="fromTime">начальная метка времени в формате DateTimeOffset</param>
        /// <param name="toTime">конечная метка времени в формате DateTimeOffset</param>
        /// <returns>Удачное выполнение запроса</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task ApiMetricsDotnetFromToAsync(System.DateTimeOffset fromTime, System.DateTimeOffset toTime, System.Threading.CancellationToken cancellationToken)
        {
            if (fromTime == null)
                throw new System.ArgumentNullException("fromTime");

            if (toTime == null)
                throw new System.ArgumentNullException("toTime");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/metrics/dotnet/from/{fromTime}/to/{toTime}");
            urlBuilder_.Replace("{fromTime}", System.Uri.EscapeDataString(fromTime.ToString("s", System.Globalization.CultureInfo.InvariantCulture)));
            urlBuilder_.Replace("{toTime}", System.Uri.EscapeDataString(toTime.ToString("s", System.Globalization.CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("GET");

                    PrepareRequest(client_, request_, urlBuilder_);

                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);

                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            return;
                        }
                        else
                        if (status_ == 400)
                        {
                            string responseText_ = (response_.Content == null) ? string.Empty : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("\u041e\u0448\u0438\u0431\u043a\u0430 \u0432 \u0437\u0430\u043f\u0440\u043e\u0441\u0435", status_, responseText_, headers_, null);
                        }
                        else
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

        /// <summary>Получение всех метрик Hdd</summary>
        /// <returns>Удачное выполнение запроса</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task ApiMetricsHddAllAsync()
        {
            return ApiMetricsHddAllAsync(System.Threading.CancellationToken.None);
        }

        /// <summary>Получение всех метрик Hdd</summary>
        /// <returns>Удачное выполнение запроса</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public void ApiMetricsHddAll()
        {
            System.Threading.Tasks.Task.Run(async () => await ApiMetricsHddAllAsync(System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Получение всех метрик Hdd</summary>
        /// <returns>Удачное выполнение запроса</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task ApiMetricsHddAllAsync(System.Threading.CancellationToken cancellationToken)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/metrics/hdd/all");

            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("GET");

                    PrepareRequest(client_, request_, urlBuilder_);

                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);

                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            return;
                        }
                        else
                        if (status_ == 400)
                        {
                            string responseText_ = (response_.Content == null) ? string.Empty : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("\u041e\u0448\u0438\u0431\u043a\u0430 \u0432 \u0437\u0430\u043f\u0440\u043e\u0441\u0435", status_, responseText_, headers_, null);
                        }
                        else
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

        /// <summary>Получение всех метрик Hdd в заданном диапазоне времени</summary>
        /// <param name="fromTime">начальная метка времени в формате DateTimeOffset</param>
        /// <param name="toTime">конечная метка времени в формате DateTimeOffset</param>
        /// <returns>Удачное выполнение запроса</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task ApiMetricsHddFromToAsync(System.DateTimeOffset fromTime, System.DateTimeOffset toTime)
        {
            return ApiMetricsHddFromToAsync(fromTime, toTime, System.Threading.CancellationToken.None);
        }

        /// <summary>Получение всех метрик Hdd в заданном диапазоне времени</summary>
        /// <param name="fromTime">начальная метка времени в формате DateTimeOffset</param>
        /// <param name="toTime">конечная метка времени в формате DateTimeOffset</param>
        /// <returns>Удачное выполнение запроса</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public void ApiMetricsHddFromTo(System.DateTimeOffset fromTime, System.DateTimeOffset toTime)
        {
            System.Threading.Tasks.Task.Run(async () => await ApiMetricsHddFromToAsync(fromTime, toTime, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Получение всех метрик Hdd в заданном диапазоне времени</summary>
        /// <param name="fromTime">начальная метка времени в формате DateTimeOffset</param>
        /// <param name="toTime">конечная метка времени в формате DateTimeOffset</param>
        /// <returns>Удачное выполнение запроса</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task ApiMetricsHddFromToAsync(System.DateTimeOffset fromTime, System.DateTimeOffset toTime, System.Threading.CancellationToken cancellationToken)
        {
            if (fromTime == null)
                throw new System.ArgumentNullException("fromTime");

            if (toTime == null)
                throw new System.ArgumentNullException("toTime");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/metrics/hdd/from/{fromTime}/to/{toTime}");
            urlBuilder_.Replace("{fromTime}", System.Uri.EscapeDataString(fromTime.ToString("s", System.Globalization.CultureInfo.InvariantCulture)));
            urlBuilder_.Replace("{toTime}", System.Uri.EscapeDataString(toTime.ToString("s", System.Globalization.CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("GET");

                    PrepareRequest(client_, request_, urlBuilder_);

                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);

                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            return;
                        }
                        else
                        if (status_ == 400)
                        {
                            string responseText_ = (response_.Content == null) ? string.Empty : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("\u041e\u0448\u0438\u0431\u043a\u0430 \u0432 \u0437\u0430\u043f\u0440\u043e\u0441\u0435", status_, responseText_, headers_, null);
                        }
                        else
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

        /// <summary>Получение всех метрик Network в заданном диапазоне времени</summary>
        /// <param name="fromTime">начальная метка времени в формате DateTimeOffset</param>
        /// <param name="toTime">конечная метка времени в формате DateTimeOffset</param>
        /// <returns>Удачное выполнение запроса</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task ApiMetricsNetworkFromToAsync(System.DateTimeOffset fromTime, System.DateTimeOffset toTime)
        {
            return ApiMetricsNetworkFromToAsync(fromTime, toTime, System.Threading.CancellationToken.None);
        }

        /// <summary>Получение всех метрик Network в заданном диапазоне времени</summary>
        /// <param name="fromTime">начальная метка времени в формате DateTimeOffset</param>
        /// <param name="toTime">конечная метка времени в формате DateTimeOffset</param>
        /// <returns>Удачное выполнение запроса</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public void ApiMetricsNetworkFromTo(System.DateTimeOffset fromTime, System.DateTimeOffset toTime)
        {
            System.Threading.Tasks.Task.Run(async () => await ApiMetricsNetworkFromToAsync(fromTime, toTime, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Получение всех метрик Network в заданном диапазоне времени</summary>
        /// <param name="fromTime">начальная метка времени в формате DateTimeOffset</param>
        /// <param name="toTime">конечная метка времени в формате DateTimeOffset</param>
        /// <returns>Удачное выполнение запроса</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task ApiMetricsNetworkFromToAsync(System.DateTimeOffset fromTime, System.DateTimeOffset toTime, System.Threading.CancellationToken cancellationToken)
        {
            if (fromTime == null)
                throw new System.ArgumentNullException("fromTime");

            if (toTime == null)
                throw new System.ArgumentNullException("toTime");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/metrics/network/from/{fromTime}/to/{toTime}");
            urlBuilder_.Replace("{fromTime}", System.Uri.EscapeDataString(fromTime.ToString("s", System.Globalization.CultureInfo.InvariantCulture)));
            urlBuilder_.Replace("{toTime}", System.Uri.EscapeDataString(toTime.ToString("s", System.Globalization.CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("GET");

                    PrepareRequest(client_, request_, urlBuilder_);

                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);

                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            return;
                        }
                        else
                        if (status_ == 400)
                        {
                            string responseText_ = (response_.Content == null) ? string.Empty : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("\u041e\u0448\u0438\u0431\u043a\u0430 \u0432 \u0437\u0430\u043f\u0440\u043e\u0441\u0435", status_, responseText_, headers_, null);
                        }
                        else
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

        /// <summary>Получение всех метрик Ram</summary>
        /// <returns>Удачное выполнение запроса</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task ApiMetricsRamAllAsync()
        {
            return ApiMetricsRamAllAsync(System.Threading.CancellationToken.None);
        }

        /// <summary>Получение всех метрик Ram</summary>
        /// <returns>Удачное выполнение запроса</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public void ApiMetricsRamAll()
        {
            System.Threading.Tasks.Task.Run(async () => await ApiMetricsRamAllAsync(System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Получение всех метрик Ram</summary>
        /// <returns>Удачное выполнение запроса</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task ApiMetricsRamAllAsync(System.Threading.CancellationToken cancellationToken)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/metrics/ram/all");

            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("GET");

                    PrepareRequest(client_, request_, urlBuilder_);

                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);

                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            return;
                        }
                        else
                        if (status_ == 400)
                        {
                            string responseText_ = (response_.Content == null) ? string.Empty : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("\u041e\u0448\u0438\u0431\u043a\u0430 \u0432 \u0437\u0430\u043f\u0440\u043e\u0441\u0435", status_, responseText_, headers_, null);
                        }
                        else
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

        /// <summary>Получение всех метрик Ram в заданном диапазоне времени</summary>
        /// <param name="fromTime">начальная метка времени в формате DateTimeOffset</param>
        /// <param name="toTime">конечная метка времени в формате DateTimeOffset</param>
        /// <returns>Удачное выполнение запроса</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task ApiMetricsRamFromToAsync(System.DateTimeOffset fromTime, System.DateTimeOffset toTime)
        {
            return ApiMetricsRamFromToAsync(fromTime, toTime, System.Threading.CancellationToken.None);
        }

        /// <summary>Получение всех метрик Ram в заданном диапазоне времени</summary>
        /// <param name="fromTime">начальная метка времени в формате DateTimeOffset</param>
        /// <param name="toTime">конечная метка времени в формате DateTimeOffset</param>
        /// <returns>Удачное выполнение запроса</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public void ApiMetricsRamFromTo(System.DateTimeOffset fromTime, System.DateTimeOffset toTime)
        {
            System.Threading.Tasks.Task.Run(async () => await ApiMetricsRamFromToAsync(fromTime, toTime, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Получение всех метрик Ram в заданном диапазоне времени</summary>
        /// <param name="fromTime">начальная метка времени в формате DateTimeOffset</param>
        /// <param name="toTime">конечная метка времени в формате DateTimeOffset</param>
        /// <returns>Удачное выполнение запроса</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task ApiMetricsRamFromToAsync(System.DateTimeOffset fromTime, System.DateTimeOffset toTime, System.Threading.CancellationToken cancellationToken)
        {
            if (fromTime == null)
                throw new System.ArgumentNullException("fromTime");

            if (toTime == null)
                throw new System.ArgumentNullException("toTime");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/metrics/ram/from/{fromTime}/to/{toTime}");
            urlBuilder_.Replace("{fromTime}", System.Uri.EscapeDataString(fromTime.ToString("s", System.Globalization.CultureInfo.InvariantCulture)));
            urlBuilder_.Replace("{toTime}", System.Uri.EscapeDataString(toTime.ToString("s", System.Globalization.CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("GET");

                    PrepareRequest(client_, request_, urlBuilder_);

                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);

                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            return;
                        }
                        else
                        if (status_ == 400)
                        {
                            string responseText_ = (response_.Content == null) ? string.Empty : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("\u041e\u0448\u0438\u0431\u043a\u0430 \u0432 \u0437\u0430\u043f\u0440\u043e\u0441\u0435", status_, responseText_, headers_, null);
                        }
                        else
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

        protected struct ObjectResponseResult<T>
        {
            public ObjectResponseResult(T responseObject, string responseText)
            {
                this.Object = responseObject;
                this.Text = responseText;
            }

            public T Object { get; }

            public string Text { get; }
        }

        public bool ReadResponseAsString { get; set; }

        protected virtual async System.Threading.Tasks.Task<ObjectResponseResult<T>> ReadObjectResponseAsync<T>(System.Net.Http.HttpResponseMessage response, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers, System.Threading.CancellationToken cancellationToken)
        {
            if (response == null || response.Content == null)
            {
                return new ObjectResponseResult<T>(default(T), string.Empty);
            }

            if (ReadResponseAsString)
            {
                var responseText = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                try
                {
                    var typedBody = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseText, JsonSerializerSettings);
                    return new ObjectResponseResult<T>(typedBody, responseText);
                }
                catch (Newtonsoft.Json.JsonException exception)
                {
                    var message = "Could not deserialize the response body string as " + typeof(T).FullName + ".";
                    throw new ApiException(message, (int)response.StatusCode, responseText, headers, exception);
                }
            }
            else
            {
                try
                {
                    using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    using (var streamReader = new System.IO.StreamReader(responseStream))
                    using (var jsonTextReader = new Newtonsoft.Json.JsonTextReader(streamReader))
                    {
                        var serializer = Newtonsoft.Json.JsonSerializer.Create(JsonSerializerSettings);
                        var typedBody = serializer.Deserialize<T>(jsonTextReader);
                        return new ObjectResponseResult<T>(typedBody, string.Empty);
                    }
                }
                catch (Newtonsoft.Json.JsonException exception)
                {
                    var message = "Could not deserialize the response body stream as " + typeof(T).FullName + ".";
                    throw new ApiException(message, (int)response.StatusCode, string.Empty, headers, exception);
                }
            }
        }

        private string ConvertToString(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value == null)
            {
                return "";
            }

            if (value is System.Enum)
            {
                var name = System.Enum.GetName(value.GetType(), value);
                if (name != null)
                {
                    var field = System.Reflection.IntrospectionExtensions.GetTypeInfo(value.GetType()).GetDeclaredField(name);
                    if (field != null)
                    {
                        var attribute = System.Reflection.CustomAttributeExtensions.GetCustomAttribute(field, typeof(System.Runtime.Serialization.EnumMemberAttribute))
                            as System.Runtime.Serialization.EnumMemberAttribute;
                        if (attribute != null)
                        {
                            return attribute.Value != null ? attribute.Value : name;
                        }
                    }

                    var converted = System.Convert.ToString(System.Convert.ChangeType(value, System.Enum.GetUnderlyingType(value.GetType()), cultureInfo));
                    return converted == null ? string.Empty : converted;
                }
            }
            else if (value is bool)
            {
                return System.Convert.ToString((bool)value, cultureInfo).ToLowerInvariant();
            }
            else if (value is byte[])
            {
                return System.Convert.ToBase64String((byte[])value);
            }
            else if (value.GetType().IsArray)
            {
                var array = System.Linq.Enumerable.OfType<object>((System.Array)value);
                return string.Join(",", System.Linq.Enumerable.Select(array, o => ConvertToString(o, cultureInfo)));
            }

            var result = System.Convert.ToString(value, cultureInfo);
            return result == null ? "" : result;
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.4.1.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class CpuMetricModelFromNSwag
    {
        [Newtonsoft.Json.JsonProperty("id", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int Id { get; set; }

        [Newtonsoft.Json.JsonProperty("value", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int Value { get; set; }

        [Newtonsoft.Json.JsonProperty("time", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.DateTimeOffset Time { get; set; }


    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.4.1.0 (Newtonsoft.Json v11.0.0.0)")]
    public enum PercentileFromNSwag
    {
        _50 = 50,

        _75 = 75,

        _90 = 90,

        _95 = 95,

        _99 = 99,

    }

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "13.10.9.0 (NJsonSchema v10.4.1.0 (Newtonsoft.Json v11.0.0.0))")]
    public partial class ApiException : System.Exception
    {
        public int StatusCode { get; private set; }

        public string Response { get; private set; }

        public System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> Headers { get; private set; }

        public ApiException(string message, int statusCode, string response, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers, System.Exception innerException)
            : base(message + "\n\nStatus: " + statusCode + "\nResponse: \n" + ((response == null) ? "(null)" : response.Substring(0, response.Length >= 512 ? 512 : response.Length)), innerException)
        {
            StatusCode = statusCode;
            Response = response;
            Headers = headers;
        }

        public override string ToString()
        {
            return string.Format("HTTP Response: \n\n{0}\n\n{1}", Response, base.ToString());
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "13.10.9.0 (NJsonSchema v10.4.1.0 (Newtonsoft.Json v11.0.0.0))")]
    public partial class ApiException<TResult> : ApiException
    {
        public TResult Result { get; private set; }

        public ApiException(string message, int statusCode, string response, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers, TResult result, System.Exception innerException)
            : base(message, statusCode, response, headers, innerException)
        {
            Result = result;
        }
    }

}
