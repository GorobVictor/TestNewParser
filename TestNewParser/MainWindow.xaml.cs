using SampleProject.Backend.Model;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TestNewParser.Abstract.Model;

namespace TestNewParser {
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private async void btn_Go_Click(object sender, RoutedEventArgs e) {
            btn_Go.IsEnabled = false;
            if (txt_Search.Text.IndexOf("playlist") != -1) {
                var playlist = (Playlist)await new ParsPlaylist().GetObject(
                        txt_Search.Text.Split('/').Where(x => x.Replace(" ", "") != "" && x != null).Last());
                listview_Viewer.ItemsSource = playlist.AllTracks;
                txt_NameArtist.Content = playlist.Artist;
                txt_NameAlbumOrPlaylist.Content = playlist.Title;
            }
            else if (txt_Search.Text.IndexOf("album") != -1) {
                var album = (Album)await new ParsAlbum().GetObject(
                        txt_Search.Text.Split('/').Where(x => x.Replace(" ", "") != "" && x != null).Last());
                listview_Viewer.ItemsSource = album.AllTracks;
                txt_NameArtist.Content = album.Artist;
                txt_NameAlbumOrPlaylist.Content = album.Title;
            }
            btn_Go.IsEnabled = true;
        }

        private async void listview_Viewer_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            var click = ((FrameworkElement)e.OriginalSource).DataContext as Track;
            if (click == null)
                return;
            var page = new DetailView(click);
            page.Show();
            await page.UpdateAllTrack(click.AlbumId);
            await page.UpdateImageAlbum(click.AlbumId);
            await page.UpdateImageArtist(click.ArtistId);
        }
    }
}
