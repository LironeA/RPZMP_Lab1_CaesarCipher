using CaesarCipher.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace CaesarCipher.ViewModel
{
    public partial class CaesarPageVM : PageBaseViewModel
    {
        [ObservableProperty]
        private string _inputText;

        [ObservableProperty]
        private string _result;

        [ObservableProperty]
        private int _shift;

        [ObservableProperty]
        private float _canvasHeight;

        [ObservableProperty]
        private CaesarCipherDrawable _caesarCipherDrawable;

        private readonly EncryptionService _encryptionService;
        private readonly FileService _fileService;

        public CaesarPageVM(EncryptionService encryptionService, FileService fileService)
        {
            _encryptionService = encryptionService;
            _fileService = fileService;
            CaesarCipherDrawable = new CaesarCipherDrawable(_encryptionService, this);
        }

        public override void Init()
        {
            InputText = string.Empty;
            Result = string.Empty;
            Shift = 0;
            CanvasHeight = 400;
        }

        [RelayCommand]
        private void Encrypt()
        {
            if(!string.IsNullOrEmpty(InputText) && Shift > 0)
            {
                Result = _encryptionService.CaesarEncrypt(InputText, Shift);
            }
        }

        [RelayCommand]
        private void Decrypt()
        {
            if(!string.IsNullOrEmpty(InputText) && Shift > 0)
            {
                Result = _encryptionService.CaesarDecrypt(InputText, Shift);
            }
        }

        // Завантаження тексту з файлу
        [RelayCommand]
        private async Task LoadFromFileAsync()
        {
            InputText = await _fileService.LoadFromFileAsync();
        }

        // Збереження результату у файл
        [RelayCommand]
        private async Task SaveToFileAsync()
        {
            if(!string.IsNullOrEmpty(Result))
            {
                await _fileService.SaveToFileAsync(Result, "encrypted_result.txt");
            }
        }
    }
    public class CaesarCipherDrawable : IDrawable
    {
        private readonly EncryptionService _encryptionService;
        private readonly CaesarPageVM _viewModel;

        public CaesarCipherDrawable(EncryptionService encryptionService, CaesarPageVM viewModel)
        {
            _encryptionService = encryptionService;
            _viewModel = viewModel;
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.FillColor = Colors.White;
            canvas.FillRectangle(dirtyRect);

            float cellSize = 40f;
            float padding = 10f;
            float leftOffset = 50f;

            // Отримати початковий та зашифрований алфавіт
            string originalAlphabet = _encryptionService.GetOriginalAlphabet();
            string transformedAlphabet = _encryptionService.GetTransformedCaesarAlphabet(_viewModel.Shift);

            canvas.FontSize = 16;

            for(int i = 0; i < originalAlphabet.Length; i++)
            {
                // Малюємо квадратики
                var x = leftOffset;
                var y = i * (cellSize + padding);

                // Малюємо прямокутники
                canvas.StrokeColor = Colors.Black;
                canvas.StrokeSize = 1;
                canvas.DrawRectangle(x, y, cellSize, cellSize);

                // Малюємо оригінальні літери
                var originalLetter = originalAlphabet[i].ToString();
                canvas.DrawString(originalLetter, x + 20, y + 20, HorizontalAlignment.Center);

                // Малюємо шифровані літери
                var transformedLetter = transformedAlphabet[i].ToString();
                canvas.DrawString(transformedLetter, x + cellSize + 20, y + 20 , HorizontalAlignment.Center);
            }

            _viewModel.CanvasHeight = originalAlphabet.Length * (cellSize + padding);
        }
    }

}
