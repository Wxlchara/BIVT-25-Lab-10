using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Green
{
    public class Task3 : Green
    {
        private string _sequence;
        private string[] _output;

        public string[] Output
        {
            get { return _output; }
        }

        public Task3(string text, string sequence) : base(text)
        {
            _sequence = sequence;
            _output = null;
        }

        public override void Review()
        {
            string text = Input;

            if (text == null)
            {
                _output = new string[0];
                return;
            }

            string seq = _sequence == null ? "" : _sequence;

            string[] words = new string[text.Length];
            int count = 0;

            int i = 0;

            while (i < text.Length)
            {
                if (IsWordChar(text[i]))
                {
                    int start = i;

                    while (i < text.Length && IsWordChar(text[i]))
                    {
                        i++;
                    }

                    string word = text.Substring(start, i - start);

                    if (ContainsIgnoreCase(word, seq) &&
                        !AlreadyExists(words, count, word))
                    {
                        words[count] = word;
                        count++;
                    }
                }
                else
                {
                    i++;
                }
            }

            _output = new string[count];

            for (int j = 0; j < count; j++)
            {
                _output[j] = words[j];
            }
        }

        public override string ToString()
        {
            if (_output == null || _output.Length == 0)
                return "";

            string result = "";

            for (int i = 0; i < _output.Length; i++)
            {
                if (i > 0)
                    result += "\n";

                result += _output[i];
            }

            return result;
        }

   

        private bool AlreadyExists(string[] arr, int length, string word)
        {
            for (int i = 0; i < length; i++)
            {
                if (EqualsIgnoreCase(arr[i], word))
                    return true;
            }
            return false;
        }

        private bool ContainsIgnoreCase(string text, string pattern)
        {
            if (pattern.Length == 0)
                return true;

            if (text.Length < pattern.Length)
                return false;

            for (int i = 0; i <= text.Length - pattern.Length; i++)
            {
                bool ok = true;

                for (int j = 0; j < pattern.Length; j++)
                {
                    if (ToLower(text[i + j]) != ToLower(pattern[j]))
                    {
                        ok = false;
                        break;
                    }
                }

                if (ok) return true;
            }

            return false;
        }

        private bool EqualsIgnoreCase(string a, string b)
        {
            if (a.Length != b.Length)
                return false;

            for (int i = 0; i < a.Length; i++)
            {
                if (ToLower(a[i]) != ToLower(b[i]))
                    return false;
            }

            return true;
        }

        private bool IsWordChar(char c)
        {
            return IsLetter(c) || c == '-' || c == '\'';
        }

        private bool IsLetter(char c)
        {
            return (c >= 'А' && c <= 'Я') ||
                   (c >= 'а' && c <= 'я') ||
                   c == 'Ё' || c == 'ё' ||
                   (c >= 'A' && c <= 'Z') ||
                   (c >= 'a' && c <= 'z');
        }

        private char ToLower(char c)
        {
            if (c >= 'А' && c <= 'Я')
                return (char)(c - 'А' + 'а');

            if (c == 'Ё')
                return 'ё';

            if (c >= 'A' && c <= 'Z')
                return (char)(c - 'A' + 'a');

            return c;
        }
    }
}
