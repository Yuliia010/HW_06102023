using Newtonsoft.Json.Linq;
using RickAndMorty_API_CORE.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMorty_API_CORE.Domain.ProviderJson
{
    public sealed class JsonProvider
    {
        public static List<Character> FromJsonToCharacterList(string json)
        {
            List<Character> characters = new List<Character>();
            JObject keyValuePairs = JObject.Parse(json);
            IList<JToken> results = keyValuePairs["results"].Children().ToList();
            List<string> class_params = new List<string>();
            foreach (JToken token in results)
            {
                int c = 0;
                foreach (var child in token.Children())
                {
                    if(c == 7)
                    {
                        string s = child.First.First.Last.ToString();
                        class_params.Add(s);
                    }
                    else
                    {
                        class_params.Add(child.Last.ToString());
                    }
                    c++;
                }
                Character item = new Character();
                item.Id = class_params[0];
                item.Name = class_params[1];
                item.Status = class_params[2];
                item.Species = class_params[3];
                item.Gender = class_params[5];
                item.Location = class_params[7];
                item.ImageURL = class_params[8];
                characters.Add(item);
                class_params.Clear();
            }
            return characters;
        }
    }
}
