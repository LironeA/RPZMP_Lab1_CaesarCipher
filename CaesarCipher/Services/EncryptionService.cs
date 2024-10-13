using System;

namespace CaesarCipher.Services
{
    public class EncryptionService
    {
        // Алфавіти
        private const string EnglishAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        private const string UkrainianAlphabet = "АБВГҐДЕЄЖЗИІЙКЛМНОПРСТУФХЦЧШЩЬЮЯабвгґдежзийклмнопрстуфхцчшщьюя";

        // Шифр Цезаря
        public string CaesarEncrypt(string text, int shift)
        {
            return ShiftText(text, shift);
        }

        public string CaesarDecrypt(string cipherText, int shift)
        {
            return ShiftText(cipherText, -shift);
        }

        private string ShiftText(string text, int shift)
        {
            char[] buffer = text.ToCharArray();
            char[] alphabet = GetUkrainianAlphabet();

            for(int i = 0; i < buffer.Length; i++)
            {
                char letter = buffer[i];

                if(char.IsLetter(letter))
                {
                    char offset = char.IsUpper(letter) ? 'А' : 'а';
                    int alphabetLength = alphabet.Length;
                    int index = Array.IndexOf(alphabet, letter);

                    if(index >= 0) // Літера в українському алфавіті
                    {
                        // Циклічне зміщення
                        index = (index + shift + alphabetLength) % alphabetLength;
                        buffer[i] = alphabet[index];
                    }
                }
            }

            return new string(buffer);
        }

        // Шифр Віженера
        public string VigenereEncrypt(string text, string key)
        {
            if(string.IsNullOrEmpty(key))
            {
                return text;
            }

            key = key.ToLower();
            char[] buffer = new char[text.Length];
            char[] ukrainianAlphabet = UkrainianAlphabet.ToCharArray();
            int alphabetLength = ukrainianAlphabet.Length;

            for(int i = 0, j = 0; i < text.Length; i++)
            {
                char letter = text[i];

                if(UkrainianAlphabet.Contains(letter)) // Перевірка на український алфавіт
                {
                    char offset = char.IsUpper(letter) ? 'А' : 'а'; // Визначення початкової букви
                    int letterIndex = Array.IndexOf(ukrainianAlphabet, letter);
                    int shift = Array.IndexOf(ukrainianAlphabet, key[j % key.Length]);

                    // Шифрування з урахуванням зсуву
                    int encryptedIndex = (letterIndex + shift) % alphabetLength;
                    buffer[i] = ukrainianAlphabet[encryptedIndex];
                    j++;
                }
                else
                {
                    buffer[i] = letter; // Якщо не буква, залишити незміненою
                }
            }

            return new string(buffer);
        }

        public string VigenereDecrypt(string cipherText, string key)
        {
            if(string.IsNullOrEmpty(key))
            {
                return cipherText;
            }

            key = key.ToLower();
            char[] buffer = new char[cipherText.Length];
            char[] ukrainianAlphabet = UkrainianAlphabet.ToCharArray();
            int alphabetLength = ukrainianAlphabet.Length;

            for(int i = 0, j = 0; i < cipherText.Length; i++)
            {
                char letter = cipherText[i];

                if(UkrainianAlphabet.Contains(letter)) // Перевірка на український алфавіт
                {
                    char offset = char.IsUpper(letter) ? 'А' : 'а'; // Визначення початкової букви
                    int letterIndex = Array.IndexOf(ukrainianAlphabet, letter);
                    int shift = Array.IndexOf(ukrainianAlphabet, key[j % key.Length]);

                    // Розшифрування з урахуванням зсуву
                    int decryptedIndex = (letterIndex - shift + alphabetLength) % alphabetLength;
                    buffer[i] = ukrainianAlphabet[decryptedIndex];
                    j++;
                }
                else
                {
                    buffer[i] = letter; // Якщо не буква, залишити незміненою
                }
            }

            return new string(buffer);
        }

        // Методи для отримання алфавіту
        public string GetOriginalAlphabet()
        {
            return UkrainianAlphabet;
        }

        public string GetTransformedCaesarAlphabet(int shift)
        {
            char[] alphabet = GetUkrainianAlphabet();
            char[] transformedAlphabet = new char[alphabet.Length];

            for(int i = 0; i < alphabet.Length; i++)
            {
                transformedAlphabet[i] = ShiftText(alphabet[i].ToString(), shift)[0];
            }

            return new string(transformedAlphabet);
        }

        public string GetTransformedVignereAlphabet(string key)
        {
            char[] alphabet = GetUkrainianAlphabet();
            char[] transformedAlphabet = new char[alphabet.Length];

            for(int i = 0; i < alphabet.Length; i++)
            {
                transformedAlphabet[i] = VigenereEncrypt(alphabet[i].ToString(), key)[0];
            }

            return new string(transformedAlphabet);
        }

        // Допоміжний метод для отримання українського алфавіту
        private char[] GetUkrainianAlphabet()
        {
            return UkrainianAlphabet.ToCharArray();
        }
    }
}
