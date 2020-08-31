using Newtonsoft.Json.Linq;
using SampleProject.Backend.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestNewParser.Abstract.Model {
    class ParsAlbum : Parsing {
        public async override Task<object> ParsingJson(string id) {
            var json = JObject.Parse(await _client.GetStringAsync($"https://www.langitmusik.co.id/rest/detail/album2/{id}"));
            var album = new Album() {
                Id = json["albumId"].ToString(),
                Title = json["albumName"].ToString(),
                Artist = json["mainArtistName"].ToString(),
                ImageLink = $"https://www.langitmusik.co.id/image.do?fileuid={json["albumSImgPath"].ToString()}",
                ReleaseDate = json["issueDate"].ToString()
            };
            json = JObject.Parse(await _client.GetStringAsync($"https://www.langitmusik.co.id/rest/song/album/{id}/1/100"));
            album.AllTracks = new List<Track>();
            foreach (var js in (JArray)json["dataList"])
                album.AllTracks.Add(new Track() {
                    Id = js["songId"].ToString(),
                    Title = js["songName"].ToString(),
                    Description = js["songName"].ToString(),
                    Album = js["albumName"].ToString(),
                    AlbumId = js["albumId"].ToString(),
                    Artist = js["artistName"].ToString(),
                    ArtistId = js["artistId"].ToString(),
                    ImageLink = album.ImageLink,
                    Duration = strDuration(js["playtime"].ToObject<int>() / 60, js["playtime"].ToObject<int>() % 60)
                });
            return album;
        }
    }
}
