using SampleProject.Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TestNewParser.Abstract;
using TestNewParser.Abstract.Model;

namespace TestNewParser {
    /// <summary>
    /// Логика взаимодействия для DetailView.xaml
    /// </summary>
    public partial class DetailView : Window {
        public DetailView(Track track) {
            InitializeComponent();
            labl_NameSong.Content = track.Title;
            labl_NameAlbum.Content = track.Album;
            labl_NameArtist.Content = track.Artist;
        }
        public async Task UpdateImageArtist(string artistid) {
            var artist = (Artist)await new ParsArtist().GetObject(artistid);
            var image = await Parsing.SetBitmap(artist.ImageLink);
            img_ImageArtist.Source =
                Imaging.CreateBitmapSourceFromHBitmap(image.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }
        public async Task UpdateImageAlbum(string albumid) {
            var album = (Album)await new ParsAlbum().GetObject(albumid);
            var image = await Parsing.SetBitmap(album.ImageLink);
            img_ImageAlbum.Source =
                Imaging.CreateBitmapSourceFromHBitmap(image.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }
        public async Task UpdateAllTrack(string albumid) {
            listview_Viewer.ItemsSource = ((Album)await new ParsAlbum().GetObject(albumid)).AllTracks;
        }
    }
}
