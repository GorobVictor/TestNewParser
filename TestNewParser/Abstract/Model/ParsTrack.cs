using Newtonsoft.Json.Linq;
using SampleProject.Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNewParser.Abstract.Model {
    class ParsTrack : Parsing {
        public async override Task<object> ParsingJson(string id) {
            var json = JArray.Parse(await _client.GetStringAsync($"https://www.langitmusik.co.id/rest/song/set?songId={id}"));
            var result = new Track() {
                Id = json[0]["songId"].ToString(),
                Title = json[0]["songName"].ToString(),
                Album = json[0]["albumName"].ToString(),
                AlbumId = json[0]["albumId"].ToString(),
                Artist = json[0]["artistName"].ToString(),
                ArtistId = json[0]["artistId"].ToString(),
                Duration = strDuration(json[0]["playtime"].ToObject<int>() / 60, json[0]["playtime"].ToObject<int>() % 60)
            };
            return result;
        }
    }
}
