using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Green
{
    public class Task2 : Green
    {
        private char[] _output;

        public char[] Output
        {
            get { return _output; }
        }

        public Task2(string text) : base(text)
        {
            _output = null;
        }

        public override void Review()
        {
            string text = Input;

            if (text == null)
            {
                _output = new char[0];
                return;
            }

            char[] letters = new char[text.Length];
            int[] counts = new int[text.Length];
            int size = 0;

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

                    char first = '\0';

                    for (int j = start; j < i; j++)
                    {
                        if (IsLetter(text[j]))
                        {
                            first = ToLower(text[j]);
                            break;
                        }
                    }

                    if (first != '\0')
                    {
                        int index = Find(letters, size, first);

                        if (index == -1)
                        {
                            letters[size] = first;
                            counts[size] = 1;
                            size++;
                        }
                        else
                        {
                            counts[index]++;
                        }
                    }
                }
                else
                {
                    i++;
                }
            }

            // сортировка (без LINQ!)
            for (int a = 0; a < size - 1; a++)
            {
                for (int b = 0; b < size - 1 - a; b++)
                {
                    bool swap = false;

                    if (counts[b] < counts[b + 1])
                    {
                        swap = true;
                    }
                    else if (counts[b] == counts[b + 1] && letters[b] > letters[b + 1])
                    {
                        swap = true;
                    }

                    if (swap)
                    {
                        char tempC = letters[b];
                        letters[b] = letters[b + 1];
                        letters[b + 1] = tempC;

                        int tempN = counts[b];
                        counts[b] = counts[b + 1];
                        counts[b + 1] = tempN;
                    }
                }
            }

            _output = new char[size];

            for (int k = 0; k < size; k++)
            {
                _output[k] = letters[k];
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
                    result += ", ";

                result += _output[i];
            }

            return result;
        }

        // ===== вспомогательные методы =====

        private int Find(char[] arr, int length, char c)
        {
            for (int i = 0; i < length; i++)
            {
                if (arr[i] == c)
                    return i;
            }
            return -1;
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