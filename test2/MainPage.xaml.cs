using System.Reflection;
using System.Xml.Linq;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Core.Primitives;

namespace test2
{
    public partial class MainPage : ContentPage
    {
        private ICameraProvider cameraProvider;
        //start timestamp for later
        String timeStamp = "";
        public MainPage(ICameraProvider cameraProvider)
        {
            InitializeComponent();
            this.cameraProvider = cameraProvider;
            DateTime utcDate = DateTime.UtcNow;
        }
        void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            string oldText = e.OldTextValue;
            string newText = e.NewTextValue;
            string myText = ((Entry)sender).Text;
        }
        void OnEntryCompleted(object sender, EventArgs e)
        {
            string text = ((Entry)sender).Text;
        }
        void OnSaveButtonClicked(object sender, EventArgs e)
        {
            string docPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string[] lines = { entry1.Text, entry2.Text, entry3.Text, entry4.Text, entry5.Text, entry6.Text, check_no_prec.IsChecked.ToString(), check_lo_prec.IsChecked.ToString(), check_no_cloud.IsChecked.ToString(), check_lo_cloud.IsChecked.ToString(), entry7.Text, entry8.Text, entry9.Text, entry10.Text, entry11.Text, entry12.Text, entry13.Text, entry14.Text, entry15.Text, entry16.Text, entry17.Text, entry18.Text, entry19.Text, entry20.Text, entry21.Text, entry22.Text, entry23.Text, entry24.Text, entry25.Text, entry26.Text, entry27.Text, entry28.Text, entry29.Text, entry30.Text, entry31.Text, entry32.Text, entry33.Text, entry34.Text, entry35.Text, entry36.Text, entry37.Text };
            using (StreamWriter outputFile = new StreamWriter(System.IO.Path.Combine(docPath, $"{DateTime.UtcNow}.txt")))
            {
                foreach (string line in lines)
                    outputFile.WriteLine(line);
            }
        }
        void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            string docPath = "";
            if (File.Exists(docPath + "/BossTest.txt"))
            {
                File.Delete(docPath + "/BossTest.txt");
            }
        }
        void OnEditorTextChanged(object sender, TextChangedEventArgs e)
        {
            string oldText = e.OldTextValue;
            string newText = e.NewTextValue;
            string myText = ((Editor)sender).Text;
        }
        void OnEditorCompleted(object sender, EventArgs e)
        {
            string text = ((Editor)sender).Text;
        }
        //values for coordinate polarity
        string NS = "";
        string EW = "";
        //button for taking picture
        private async void TakePicture(object sender, EventArgs e)
        {
            await MyCamera.CaptureImage(CancellationToken.None);
        }
        //the following pile of functions relates to the in-app camera. I rushed a bit, so some of it's probably unecessary
        private void MyCamera_MediaCaptured(object? sender, CommunityToolkit.Maui.Views.MediaCapturedEventArgs e)
        {
            if (Dispatcher.IsDispatchRequired)
            {
                Dispatcher.Dispatch(() => MyImage.Source = ImageSource.FromStream(() => e.Media));
                return;
            }

            MyImage.Source = ImageSource.FromStream(() => e.Media);
        }
        protected async override void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);

            await cameraProvider.RefreshAvailableCameras(CancellationToken.None);
            MyCamera.SelectedCamera = cameraProvider.AvailableCameras
                .Where(c => c.Position == CameraPosition.Front).FirstOrDefault();
        }
        protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
        {
            base.OnNavigatedFrom(args);

            MyCamera.MediaCaptured -= MyCamera_MediaCaptured;
            MyCamera.Handler?.DisconnectHandler();
        }
        //the following pile of functions is used to take geolocation data. I took them from microsoft, I might know how they work.
        //I will remove the redundant ones once i figure out which ones are redundant
        private CancellationTokenSource _cancelTokenSource;
        private bool _isCheckingLocation;

        //did this in a rush
        //to do: generalized solution. This code is bad. This code works. Thus is life
        private async void GetLocationDate1(object sender, EventArgs e)
        {
            try
            {
                _isCheckingLocation = true;

                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

                _cancelTokenSource = new CancellationTokenSource();

                Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

                if (location != null)
                    //NS = "S";
                    //EW = "W";
                    //if (location.Latitude < 1)
                    //    location.Latitude = -location.Latitude;
                    //    NS = "N";
                    //if (location.Longitude < 1)
                    //    location.Longitude = -location.Longitude;
                    //    EW = "E";
                    timeStamp = $"{DateTime.UtcNow}";
                label_highfrac_1_start.Text = $"{location.Latitude},{location.Longitude},{timeStamp}";

            }
            // Catch one of the following exceptions:
            //   FeatureNotSupportedException
            //   FeatureNotEnabledException
            //   PermissionException
            catch (Exception ex)
            {
                // Unable to get location
            }
            finally
            {
                _isCheckingLocation = false;
            }
        }
        private async void GetLocationDate2(object sender, EventArgs e)
        {
            try
            {
                _isCheckingLocation = true;

                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

                _cancelTokenSource = new CancellationTokenSource();

                Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

                if (location != null)
                    //NS = "S";
                    //EW = "W";
                    //if (location.Latitude < 1)
                    //    location.Latitude = -location.Latitude;
                    //    NS = "N";
                    //if (location.Longitude < 1)
                    //    location.Longitude = -location.Longitude;
                    //    EW = "E";
                    timeStamp = $"{DateTime.UtcNow}";
                label_highfrac_1_end.Text = $"{location.Latitude},{location.Longitude},{timeStamp}";

            }
            // Catch one of the following exceptions:
            //   FeatureNotSupportedException
            //   FeatureNotEnabledException
            //   PermissionException
            catch (Exception ex)
            {
                // Unable to get location
            }
            finally
            {
                _isCheckingLocation = false;
            }
        }
        private async void GetLocationDate3(object sender, EventArgs e)
        {
            try
            {
                _isCheckingLocation = true;

                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

                _cancelTokenSource = new CancellationTokenSource();

                Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

                if (location != null)
                    //NS = "S";
                    //EW = "W";
                    //if (location.Latitude < 1)
                    //    location.Latitude = -location.Latitude;
                    //    NS = "N";
                    //if (location.Longitude < 1)
                    //    location.Longitude = -location.Longitude;
                    //    EW = "E";
                    timeStamp = $"{DateTime.UtcNow}";
                label_highfrac_2_start.Text = $"{location.Latitude},{location.Longitude},{timeStamp}";

            }
            // Catch one of the following exceptions:
            //   FeatureNotSupportedException
            //   FeatureNotEnabledException
            //   PermissionException
            catch (Exception ex)
            {
                // Unable to get location
            }
            finally
            {
                _isCheckingLocation = false;
            }
        }
        private async void GetLocationDate4(object sender, EventArgs e)
        {
            try
            {
                _isCheckingLocation = true;

                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

                _cancelTokenSource = new CancellationTokenSource();

                Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

                if (location != null)
                    //NS = "S";
                    //EW = "W";
                    //if (location.Latitude < 1)
                    //    location.Latitude = -location.Latitude;
                    //    NS = "N";
                    //if (location.Longitude < 1)
                    //    location.Longitude = -location.Longitude;
                    //    EW = "E";
                    timeStamp = $"{DateTime.UtcNow}";
                label_highfrac_2_end.Text = $"{location.Latitude},{location.Longitude},{timeStamp}";

            }
            // Catch one of the following exceptions:
            //   FeatureNotSupportedException
            //   FeatureNotEnabledException
            //   PermissionException
            catch (Exception ex)
            {
                // Unable to get location
            }
            finally
            {
                _isCheckingLocation = false;
            }
        }
        private async void GetLocationDate5(object sender, EventArgs e)
        {
            try
            {
                _isCheckingLocation = true;

                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

                _cancelTokenSource = new CancellationTokenSource();

                Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

                if (location != null)
                    //NS = "S";
                    //EW = "W";
                    //if (location.Latitude < 1)
                    //    location.Latitude = -location.Latitude;
                    //    NS = "N";
                    //if (location.Longitude < 1)
                    //    location.Longitude = -location.Longitude;
                    //    EW = "E";
                    timeStamp = $"{DateTime.UtcNow}";
                label_lowfrac_1_start.Text = $"{location.Latitude},{location.Longitude},{timeStamp}";

            }
            // Catch one of the following exceptions:
            //   FeatureNotSupportedException
            //   FeatureNotEnabledException
            //   PermissionException
            catch (Exception ex)
            {
                // Unable to get location
            }
            finally
            {
                _isCheckingLocation = false;
            }
        }
        private async void GetLocationDate6(object sender, EventArgs e)
        {
            try
            {
                _isCheckingLocation = true;

                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

                _cancelTokenSource = new CancellationTokenSource();

                Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

                if (location != null)
                    //NS = "S";
                    //EW = "W";
                    //if (location.Latitude < 1)
                    //    location.Latitude = -location.Latitude;
                    //    NS = "N";
                    //if (location.Longitude < 1)
                    //    location.Longitude = -location.Longitude;
                    //    EW = "E";
                    timeStamp = $"{DateTime.UtcNow}";
                label_lowfrac_1_end.Text = $"{location.Latitude},{location.Longitude},{timeStamp}";

            }
            // Catch one of the following exceptions:
            //   FeatureNotSupportedException
            //   FeatureNotEnabledException
            //   PermissionException
            catch (Exception ex)
            {
                // Unable to get location
            }
            finally
            {
                _isCheckingLocation = false;
            }
        }
        private async void GetLocationDate7(object sender, EventArgs e)
        {
            try
            {
                _isCheckingLocation = true;

                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

                _cancelTokenSource = new CancellationTokenSource();

                Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

                if (location != null)
                    //NS = "S";
                    //EW = "W";
                    //if (location.Latitude < 1)
                    //    location.Latitude = -location.Latitude;
                    //    NS = "N";
                    //if (location.Longitude < 1)
                    //    location.Longitude = -location.Longitude;
                    //    EW = "E";
                    timeStamp = $"{DateTime.UtcNow}";
                label_lowfrac_2_start.Text = $"{location.Latitude},{location.Longitude},{timeStamp}";

            }
            // Catch one of the following exceptions:
            //   FeatureNotSupportedException
            //   FeatureNotEnabledException
            //   PermissionException
            catch (Exception ex)
            {
                // Unable to get location
            }
            finally
            {
                _isCheckingLocation = false;
            }
        }
        private async void GetLocationDate8(object sender, EventArgs e)
        {
            try
            {
                _isCheckingLocation = true;

                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

                _cancelTokenSource = new CancellationTokenSource();

                Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

                if (location != null)
                    //NS = "S";
                    //EW = "W";
                    //if (location.Latitude < 1)
                    //    location.Latitude = -location.Latitude;
                    //    NS = "N";
                    //if (location.Longitude < 1)
                    //    location.Longitude = -location.Longitude;
                    //    EW = "E";
                    timeStamp = $"{DateTime.UtcNow}";
                label_lowfrac_2_end.Text = $"{location.Latitude},{location.Longitude},{timeStamp}";

            }
            // Catch one of the following exceptions:
            //   FeatureNotSupportedException
            //   FeatureNotEnabledException
            //   PermissionException
            catch (Exception ex)
            {
                // Unable to get location
            }
            finally
            {
                _isCheckingLocation = false;
            }
        }
        public void CancelRequest()
        {
            if (_isCheckingLocation && _cancelTokenSource != null && _cancelTokenSource.IsCancellationRequested == false)
                _cancelTokenSource.Cancel();
        }
    }

}
