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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WebcamViewerX.Home
{
    public partial class CameraView : UserControl
    {
        public CameraView()
        {
            InitializeComponent();

            errorGrid.Opacity = 0;

            anim_in = new DoubleAnimation(1, TimeSpan.FromSeconds(.3));
            anim_out = new DoubleAnimation(0, TimeSpan.FromSeconds(.3));
        }

        public BitmapImage Image
        {
            get
            {
                Image image = (Image)FindName("image");
                return (BitmapImage)image.Source;
            }
            set
            {
                IsLoading = false;

                Image newImage = new Image() { Name = "newImage", Stretch = Stretch.Uniform, Source = value, Opacity = 0 };

                imageGrid.Children.Add(newImage);

                AnimateImages(newImage);
            }
        }

        DoubleAnimation anim_in;
        DoubleAnimation anim_out;

        async void AnimateImages(Image newImage)
        {
            Image oldImage = (Image)imageGrid.Children[0];

            oldImage.BeginAnimation(OpacityProperty, anim_out);
            newImage.BeginAnimation(OpacityProperty, anim_in);

            await Task.Delay(TimeSpan.FromSeconds(.3));

            imageGrid.Children.RemoveAt(0);
            newImage.Name = "image";
        }

        bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;

                if (value)
                    ProgressGrid.BeginAnimation(OpacityProperty, anim_in);
                else
                    ProgressGrid.BeginAnimation(OpacityProperty, anim_out);
            }
        }

        bool _isError;
        public bool IsError
        {
            get { return _isError; }
            set
            {
                _isError = value;

                imageGrid.BeginAnimation(OpacityProperty, value ? anim_out : anim_in);
                errorGrid.BeginAnimation(OpacityProperty, value ? anim_in : anim_out);
            }
        }

        public event RoutedEventHandler Click;

        private void rippleEffect_Click(object sender, RoutedEventArgs e)
        {
            Click?.Invoke(this, null);
        }
    }
}
