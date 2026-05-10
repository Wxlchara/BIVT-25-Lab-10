using System;

namespace Lab9.Green
{
    public abstract class Green
    {
        private string _input;

        public string Input
        {
            get { return _input; }
        }

        protected Green(string text)
        {
            _input = text;
        }

        public abstract void Review();

        public virtual void ChangeText(string text)
        {
            _input = text;
            Review();
        }
    }
}