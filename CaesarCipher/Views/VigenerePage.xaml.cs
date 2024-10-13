using CaesarCipher.ViewModel;

namespace CaesarCipher.Views;

public partial class VigenerePage : ContentPage
{
    private readonly VigenerePageVM _vm;

    public VigenerePage(VigenerePageVM vm)
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

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        VigenereCipherCanvas.Invalidate();
    }
}