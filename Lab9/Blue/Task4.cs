namespace Lab9.Blue
{
    public class Task4 : Blue
    {
        private int output;

        public int Output
        {
            get
            {
                return output;
            }
            private set
            {
                output = value;
            }
        }

        public Task4(string text) : base(text)
        {
            Output = 0;
            Review();
        }

        public override void Review()
        {
            string text = Input;
            if (text == null || text.Length == 0)
            {
                Output = 0;
                return;
            }

            int sum = 0;
            int i = 0;
            while (i < text.Length)
            {
                while (i < text.Length && !(text[i] >= '0' && text[i] <= '9'))
                    i++;

                if (i >= text.Length)
                {
                    break;
                }

                int start = i;
                while (i < text.Length && (text[i] >= '0' && text[i] <= '9'))
                {
                    i++;
                }

                int number = 0;
                for (int j = start; j < i; j++)
                {
                    number = number * 10 + (text[j] - '0');
                }
                sum += number;
            }

            Output = sum;
        }

        public override string ToString()
        {
            return Output.ToString();
        }
    }
}
