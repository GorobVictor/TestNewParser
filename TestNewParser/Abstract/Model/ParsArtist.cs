using Newtonsoft.Json.Linq;
using SampleProject.Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNewParser.Abstract.Model {
    class ParsArtist : Parsing {
        public async override Task<object> ParsingJson(string id) {
            var json = JObject.Parse(await _client.GetStringAsync($"https://www.langitmusik.co.id/rest/artist/detail/{id}"));
            var artist = new Artist() {
                Id = json["artistId"].ToString(),
                ArtistName = json["artistName"].ToString(),
                Description = json["profile"].ToString(),
                Fans = json["followerCnt"].ToString(),
                ImageLink = $"https://www.langitmusik.co.id/image.do?fileuid={json["artistSImgPath"].ToString()}"
            };
            return artist;
        }
    }
}
