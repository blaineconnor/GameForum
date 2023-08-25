using Game.Forum.UI.Exceptions;
using Game.Forum.UI.Models.DTOs.Accounts;
using Game.Forum.UI.Services.Abstraction;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace Game.Forum.UI.Services.Implementation
{
    public class RestService : IRestService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;

        public RestService(IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _configuration = configuration;
            _contextAccessor = contextAccessor;
        }



        #region Post İstekleri

        public async Task<RestResponse<TResponse>> PostAsync<TRequest, TResponse>(TRequest requestModel, string endpointUrl, bool tokenRequired = true)
        {
            var apiUrl = _configuration["Api:Url"];
            var jsonModel = JsonConvert.SerializeObject(requestModel);

            RestClient restClient = new RestClient(apiUrl);
            RestRequest restRequest = new RestRequest(endpointUrl, Method.Post);

            restRequest.AddParameter("application/json", jsonModel, ParameterType.RequestBody);
            restRequest.AddHeader("Accept", "application/json");

            if (tokenRequired && GetToken() != null)
            {
                restRequest.AddHeader("Authorization", $"Bearer {GetToken().Token}");
            }

            var response = await restClient.ExecuteAsync<TResponse>(restRequest);
            CheckResponse(response);
            return response;
        }


        public async Task<RestResponse<TResponse>> PostAsync<TResponse>(string endpointUrl, bool tokenRequired = true)
        {
            var apiUrl = _configuration["Api:Url"];

            RestClient restClient = new RestClient(apiUrl);
            RestRequest restRequest = new RestRequest(endpointUrl, Method.Post);

            restRequest.AddHeader("Accept", "application/json");

            if (tokenRequired && GetToken() != null)
            {
                restRequest.AddHeader("Authorization", $"Bearer {GetToken().Token}");
            }

            var response = await restClient.ExecuteAsync<TResponse>(restRequest);
            CheckResponse(response);
            return response;
        }

        public async Task<RestResponse<TResponse>> PostFormAsync<TResponse>(Dictionary<string, string> parameters, string endpointUrl, bool tokenRequired = true)
        {
            var apiUrl = _configuration["Api:Url"];

            RestClient restClient = new RestClient(apiUrl);
            RestRequest restRequest = new RestRequest(endpointUrl, Method.Post);

            restRequest.AddHeader("content-type", "application/x-www-form-urlencoded");
            restRequest.AddHeader("Accept", "application/json");

            //Modelden gelen bilgiler isteğe key value şeklinde aktarılıyor
            AddFormParametersToRequest(restRequest, parameters);

            if (tokenRequired && GetToken() != null)
            {
                restRequest.AddHeader("Authorization", $"Bearer {GetToken().Token}");
            }

            var response = await restClient.ExecuteAsync<TResponse>(restRequest);
            CheckResponse(response);
            return response;
        }

        #endregion

        #region Get İstekleri

        public async Task<RestResponse<TResponse>> GetAsync<TResponse>(string endpointUrl, bool tokenRequired = true)
        {
            var apiUrl = _configuration["Api:Url"];

            RestClient restClient = new RestClient(apiUrl);
            RestRequest restRequest = new RestRequest(endpointUrl, Method.Get);

            restRequest.AddHeader("Accept", "application/json");

            if (tokenRequired && GetToken() != null)
            {
                restRequest.AddHeader("Authorization", $"Bearer {GetToken().Token}");
            }

            var response = await restClient.ExecuteAsync<TResponse>(restRequest);
            CheckResponse(response);
            return response;
        }

        #endregion

        #region Delete İstekleri

        public async Task<RestResponse<TResponse>> DeleteAsync<TResponse>(string endpointUrl, bool tokenRequired = true)
        {
            var apiUrl = _configuration["Api:Url"];

            RestClient restClient = new RestClient(apiUrl);
            RestRequest restRequest = new RestRequest(endpointUrl, Method.Delete);

            restRequest.AddHeader("Accept", "application/json");

            if (tokenRequired && GetToken() != null)
            {
                restRequest.AddHeader("Authorization", $"Bearer {GetToken().Token}");
            }

            var response = await restClient.ExecuteAsync<TResponse>(restRequest);
            CheckResponse(response);
            return response;
        }

        #endregion

        #region Put İstekleri

        public async Task<RestResponse<TResponse>> PutAsync<TRequest, TResponse>(TRequest requestModel, string endpointUrl, bool tokenRequired = true)
        {
            var apiUrl = _configuration["Api:Url"];
            var jsonModel = JsonConvert.SerializeObject(requestModel);

            RestClient restClient = new RestClient(apiUrl);
            RestRequest restRequest = new RestRequest(endpointUrl, Method.Put);

            restRequest.AddParameter("application/json", jsonModel, ParameterType.RequestBody);
            restRequest.AddHeader("Accept", "application/json");

            if (tokenRequired && GetToken() != null)
            {
                restRequest.AddHeader("Authorization", $"Bearer {GetToken().Token}");
            }

            var response = await restClient.ExecuteAsync<TResponse>(restRequest);
            CheckResponse(response);
            return response;
        }

        #endregion



        #region Private Methods

        private TokenDto GetToken()
        {
            var sessionKey = _configuration["Application:SessionKey"];
            if (_contextAccessor.HttpContext.Session.GetString(sessionKey) is null)
                return null;
            var tokenDto = JsonConvert.DeserializeObject<TokenDto>(_contextAccessor.HttpContext.Session.GetString(sessionKey));
            return tokenDto;
        }

        private void CheckResponse<TResponse>(RestResponse<TResponse> response)
        {
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthenticatedException();
            }
            else if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new UnauthorizedException();
            }
        }

        private void AddFormParametersToRequest(RestRequest request, Dictionary<string, string> parameters)
        {
            foreach (var key in parameters.Keys)
            {
                request.AddParameter(key, parameters[key]);
            }
        }

        #endregion
    }
}
