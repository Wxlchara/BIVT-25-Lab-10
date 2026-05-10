using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Green
{
    public class Task1 : Green
    {
        private (char, double)[] _output;
        private const string Alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";

        public (char, double)[] Output
        {
            get { return _output; }
        }

        public Task1(string text) : base(text)
        {
            _output = null;
        }

        public override void Review()
        {
            string text = Input;
            if (text == null)
            {
                _output = new (char, double)[0];
                return;
            }

            int[] counts = new int[Alphabet.Length];
            int total = 0;

            for (int i = 0; i < text.Length; i++)
            {
                char c = ToLowerRussian(text[i]);
                int index = GetIndex(c);

                if (index >= 0)
                {
                    counts[index]++;
                    total++;
                }
            }

            int used = 0;
            for (int i = 0; i < counts.Length; i++)
            {
                if (counts[i] > 0)
                {
                    used++;
                }
            }

            _output = new (char, double)[used];
            int pos = 0;

            for (int i = 0; i < counts.Length; i++)
            {
                if (counts[i] > 0)
                {
                    double value = total == 0 ? 0.0 : (double)counts[i] / total;
                    _output[pos] = (Alphabet[i], value);
                    pos++;
                }
            }
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

                result += _output[i].Item1;
                result += ":";
                result += _output[i].Item2.ToString("F4");
            }

            return result;
        }

        private int GetIndex(char c)
        {
            for (int i = 0; i < Alphabet.Length; i++)
            {
                if (Alphabet[i] == c)
                {
                    return i;
                }
            }

            return -1;
        }

        private char ToLowerRussian(char c)
        {
            if (c >= 'А' && c <= 'Я')
            {
                return (char)(c - 'А' + 'а');
            }

            if (c == 'Ё')
            {
                return 'ё';
            }

            return c;
        }
    }
}