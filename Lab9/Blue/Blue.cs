namespace Lab9.Blue
{
    public abstract class Blue
    {
        private string _input;

        public string Input
        {
            get
            {
                return _input;
            }
            private set
            {
                _input = value;
            }
        }

        protected Blue(string text)
        {
            Input = text;
        }

        public abstract void Review();

        public virtual void ChangeText(string text)
        {
            Input = text;
            Review();
        }
    }
}
