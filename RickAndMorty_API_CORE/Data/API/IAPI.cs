using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMorty_API_CORE.Data.API
{
    public interface IAPI
    {
        public string GetPage(int page);
        public void GetCharacter(int id);
        public void GetLocation(int id);
        public void GetEpisode(int id);
    }
}
