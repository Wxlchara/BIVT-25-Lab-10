using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Green
{
    public class Task4 : Green
    {
        private string[] _output;

        public string[] Output
        {
            get { return _output; }
        }

        public Task4(string text) : base(text)
        {
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

            string[] items = SplitByComma(text);
            SortStrings(items);
            _output = items;
        }

        public override string ToString()
        {
            if (_output == null || _output.Length == 0)
            {
                return string.Empty;
            }

            string result = string.Empty;

            for (int i = 0; i < _output.Length; i++)
            {
                if (i > 0)
                {
                    result += "\n";
                }

                result += _output[i];
            }

            return result;
        }

        private string[] SplitByComma(string text)
        {
            int count = 1;

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == ',')
                {
                    count++;
                }
            }

            string[] result = new string[count];
            int start = 0;
            int index = 0;

            for (int i = 0; i <= text.Length; i++)
            {
                if (i == text.Length || text[i] == ',')
                {
                    string part = text.Substring(start, i - start);
                    result[index] = TrimSpaces(part);
                    index++;
                    start = i + 1;
                }
            }

            return result;
        }

        private void SortStrings(string[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                string current = array[i];
                int j = i - 1;

                while (j >= 0 && CompareStrings(array[j], current) > 0)
                {
                    array[j + 1] = array[j];
                    j--;
                }

                array[j + 1] = current;
            }
        }

        private int CompareStrings(string a, string b)
        {
            if (a == null && b == null)
            {
                return 0;
            }

            if (a == null)
            {
                return -1;
            }

            if (b == null)
            {
                return 1;
            }

            int minLength = a.Length < b.Length ? a.Length : b.Length;

            for (int i = 0; i < minLength; i++)
            {
                char ca = ToLowerChar(a[i]);
                char cb = ToLowerChar(b[i]);

                if (ca < cb)
                {
                    return -1;
                }

                if (ca > cb)
                {
                    return 1;
                }
            }

            if (a.Length < b.Length)
            {
                return -1;
            }

            if (a.Length > b.Length)
            {
                return 1;
            }

            return 0;
        }

        private string TrimSpaces(string text)
        {
            if (text == null || text.Length == 0)
            {
                return text;
            }

            int left = 0;
            int right = text.Length - 1;

            while (left <= right && text[left] == ' ')
            {
                left++;
            }

            while (right >= left && text[right] == ' ')
            {
                right--;
            }

            if (left > right)
            {
                return string.Empty;
            }

            return text.Substring(left, right - left + 1);
        }

        private char ToLowerChar(char c)
        {
            if (c >= 'А' && c <= 'Я')
            {
                return (char)(c - 'А' + 'а');
            }

            if (c == 'Ё')
            {
                return 'ё';
            }

            if (c >= 'A' && c <= 'Z')
            {
                return (char)(c - 'A' + 'a');
            }

            return c;
        }
    }
}
