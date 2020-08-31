using System;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace TestNewParser.Abstract {
    /// <summary>
    /// Класс парсинга
    /// </summary>
    public abstract class Parsing {
        protected static HttpClient _client = new HttpClient();
        protected static WebClient _webclient = new WebClient();
        public abstract Task<object> ParsingJson(string id);
        /// <summary>
        /// Шаблон обработки ошибок
        /// </summary>
        /// <param name="id">принимаемый id объекта</param>
        /// <returns></returns>
        public async Task<object> GetObject(string id) {
            int _try = 0;
            while (_try < 5)
                try {
                    return await ParsingJson(id);
                }
                catch (Exception ex) {
                    if (ex.Message.IndexOf("404") != -1)
                        return null;
                    else if (ex.Message.IndexOf("501") != -1) {
                        _try++;
                        continue;
                    }
                    else if (ex.Message.IndexOf("503") != -1) {
                        _try++;
                        continue;
                    }
                    else return null;
                }
            return null;
        }
        public async Task<object> GetObject(int id) => await GetObject(id.ToString());
        /// <summary>
        /// Скачать фото
        /// </summary>
        /// <param name="url">ссылка на фото</param>
        /// <returns></returns>
        public static async Task<Bitmap> SetBitmap(string url) {
            try {
                Bitmap result = null;
                var img = await _webclient.OpenReadTaskAsync(new Uri(url));
                await Task.Run(() => { result = new Bitmap(img); });
                return result;
            }
            catch { return new Bitmap("Resources\\Why.jpg"); }
        }
        public static string strDuration(int minute, int second) {
            string duration = "";
            if (minute < 10)
                duration += $"0{minute}:";
            else duration += $"{minute}:";
            if (second < 10)
                duration += $"0{second}";
            else duration += second;
            return duration;
        }
    }
}
