using Newtonsoft.Json.Linq;
using SampleProject.Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNewParser.Abstract.Model {
    class ParsPlaylist : Parsing {
        public async override Task<object> ParsingJson(string id) {
            var json = JObject.Parse(await _client.GetStringAsync($"https://www.langitmusik.co.id/rest/playlist/details?playlistId={id}&page=1&limit=100"));
            var result = new Playlist() {
                Id = json["detail"]["playlistId"].ToString(),
                Title = json["detail"]["playlistName"].ToString(),
                Artist = json["detail"]["creatorNickname"].ToString(),
                ImageLink = $"https://www.langitmusik.co.id/image.do?fileuid={json["detail"]["playlistSImgPath"].ToString()}",
            };

            var listSound = (JArray)json["list"]["dataList"];
            result.AllTracks = listSound.Select(x => new Track() {
                Id = x["songId"].ToString(),
                Title = x["songName"].ToString(),
                Album = x["albumName"].ToString(),
                AlbumId = x["albumId"].ToString(),
                Artist = x["artistName"].ToString(),
                ArtistId = x["artistId"].ToString(),
                Duration = strDuration(x["playtime"].ToObject<int>() / 60, x["playtime"].ToObject<int>() % 60)}).ToList();

            return result;
        }
    }
}
