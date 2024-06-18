using CommunityToolkit.Mvvm.Messaging;
using ProyectoP2.Utilities;
using Camera.MAUI;
using Camera.MAUI.ZXingHelper;


namespace ProyectoP2.Views
{
    public partial class BarcodePage : ContentPage
    {
        public BarcodePage()
        {
            InitializeComponent();
            cameraView.BarCodeOptions = new Camera.MAUI.ZXingHelper.BarcodeDecodeOptions()
            {
                TryHarder = true,
                PossibleFormats = { ZXing.BarcodeFormat.All_1D }
            };
        }

        private void cameraView_CamerasLoaded(object sender, EventArgs e)
        {
            if (cameraView.Cameras.Count > 0)
            {
                cameraView.Camera = cameraView.Cameras.First();
                MainThread.BeginInvokeOnMainThread(new Action(async () =>
                {
                    await cameraView.StopCameraAsync();
                    await cameraView.StartCameraAsync();
                }));
            }
        }

        private void cameraView_BarcodeDetected(object sender, Camera.MAUI.ZXingHelper.BarcodeEventArgs args)
        {
            BarcodeResult barcodeResult = new BarcodeResult { BarcodeValue = args.Result[0].Text };
            WeakReferenceMessenger.Default.Send(new BarcodeScannedMessage(barcodeResult));

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Shell.Current.Navigation.PopModalAsync();
            });
        }
    }
}
