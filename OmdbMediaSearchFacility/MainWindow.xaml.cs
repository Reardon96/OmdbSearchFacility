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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FilmLibraryProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ApiHelper.InitializeClient();
            InitializeQueue();
        }

        LimitedQueue<string> RecentSearchesQueue = new LimitedQueue<string>(5);

        private void InitializeQueue()
        {
            int count = 0;
            while (count < 5)
            {
                RecentSearchesQueue.Enqueue("");
                count++;
            }
        }

        private async void MovieInputButton_Click(object sender, RoutedEventArgs e)
        {
            var movieInfo = await OmdbProcessor.LoadMovie(MovieTitleInputBox.Text);
            try
            {
                // assign media poster 
                var uriSource = new Uri(movieInfo.Poster, UriKind.Absolute);
                MoviePoster.Source = new BitmapImage(uriSource);

                // assign media details
                MovieTitle.Content = $"Title: {movieInfo.Title}";
                MovieYear.Content = $"Year: {movieInfo.Year}";
                MovieDirector.Content = $"Director: {movieInfo.Director}";
                MovieGenre.Content = $"Genre: {movieInfo.Genre}";
                MovieRuntime.Content = $"Runtime: {movieInfo.Runtime}";
                MovieImdbRating.Content = $"Imdb Rating: {movieInfo.ImdbRating}";
                MovieMetascoreRating.Content = $"Metascore Rating: {movieInfo.Metascore}";

                // display search result status
                ExceptionLabel.Foreground = new SolidColorBrush(Colors.Black);
                ExceptionLabel.Content = "Media found";

                // update recent seaches
                RecentSearchesQueue.Enqueue(MovieTitleInputBox.Text);

                // update recent searches combobox
                string[] recentSearchesArray = new string[5];
                recentSearchesArray = RecentSearchesQueue.ToArray();

                ComboRecentOne.Content = recentSearchesArray[4];
                ComboRecentTwo.Content = recentSearchesArray[3];
                ComboRecentThree.Content = recentSearchesArray[2];
                ComboRecentFour.Content = recentSearchesArray[1];
                ComboRecentFive.Content = recentSearchesArray[0];

                SetComboVisibility();
            }
            catch (Exception)
            {
                // display search result status
                ExceptionLabel.Foreground = new SolidColorBrush(Colors.Red);
                ExceptionLabel.Content = "Media not found";
            }
        }

        private void ComboRecent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var item = (ComboBoxItem)e.AddedItems[0];
                MovieTitleInputBox.Text = item.Content.ToString();
            }
            catch (Exception)
            {

            }
        }
        
        // set visibilty if recent searches less than 5 - refactor to reduce code
        private void SetComboVisibility()
        {
            if (ComboRecentOne.Content.ToString() == "")
            {
                ComboRecentOne.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                ComboRecentOne.Visibility = System.Windows.Visibility.Visible;
            }
            if (ComboRecentTwo.Content.ToString() == "")
            {
                ComboRecentTwo.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                ComboRecentTwo.Visibility = System.Windows.Visibility.Visible;
            }
            if (ComboRecentThree.Content.ToString() == "")
            {
                ComboRecentThree.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                ComboRecentThree.Visibility = System.Windows.Visibility.Visible;
            }
            if (ComboRecentFour.Content.ToString() == "")
            {
                ComboRecentFour.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                ComboRecentFour.Visibility = System.Windows.Visibility.Visible;
            }
            if (ComboRecentFive.Content.ToString() == "")
            {
                ComboRecentFive.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                ComboRecentFive.Visibility = System.Windows.Visibility.Visible;
            }
            ComboRecent.Text = "";
        }
    }
}
