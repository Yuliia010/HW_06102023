using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMorty_API_CORE.Data.API
{
    public class RAM_API_ENGINE : IAPI
    {
        private string BaseUri = "https://rickandmortyapi.com/api/";
        private HttpClient client { set; get; }
        public RAM_API_ENGINE() {
            client = new HttpClient();
            client.BaseAddress = new Uri(BaseUri);
        }

        public string GetPage(int page)
        {
            var response = client.SendAsync(new HttpRequestMessage(HttpMethod.Get, BaseUri + "character?page=" + page));
            //if(response.Result.StatusCode == HttpStatusCode.OK)
            //{
            //    result += "Status Code 200 - OK";
            //    result += "\n";
            //}
            var content = response.Result.Content.ReadAsStringAsync();
            return content.Result.ToString();
        }
        void IAPI.GetCharacter(int id)
        {
            throw new NotImplementedException();
        }

        void IAPI.GetEpisode(int id)
        {
            throw new NotImplementedException();
        }

        void IAPI.GetLocation(int id)
        {
            throw new NotImplementedException();
        }

       
    }
}
