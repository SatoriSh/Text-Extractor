using Microsoft.Win32;
using System.Text;
using System.Windows;
using Tesseract;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TextExtractor
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                image.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        private void btnExtract_Click(object sender, RoutedEventArgs e)
        {
            string language = "eng"; // По умолчанию английский язык
            // Получаем выбранный язык из ComboBox
            if (cmbLanguage.SelectedIndex == 0)
                language = "eng";
            else
                language = "rus";
            if (image.Source != null)
            {
                using (var engine = new TesseractEngine(@"./tessdata", language, EngineMode.Default))
                {
                    var imagePath = ((BitmapImage)image.Source).UriSource.LocalPath;

                    using (var img = Pix.LoadFromFile(imagePath))
                    {
                        using (var page = engine.Process(img))
                        {
                            string recognizedText = page.GetText();
                            txtContent.Text = recognizedText;
                        }
                    }
                }
            }
        }
    }
}
