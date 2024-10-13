using CaesarCipher.ViewModel;

namespace CaesarCipher.Views
{
    public partial class CaesarPage : ContentPage
    {

        private readonly CaesarPageVM _vm;

        public CaesarPage(CaesarPageVM vm)
        {
            _vm = vm;
            this.BindingContext = _vm;
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);
            _vm.Init();
        }

        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            CaesarCipherCanvas.Invalidate();
        }
    }

}
