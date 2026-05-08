namespace Lab9.Blue;

public class Task1 : Blue
{
    private string[] output;

    public string[] Output
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
    
    public Task1(string text) : base(text)
    {
        Output = new string[0];
        Review();
    }

    private bool IsSpace(char c)
    {
        return c == ' ';
    }
    
    public override void Review()
    {
        string text = Input;
        if (text == null || text.Length == 0)
        {
            Output = new string[0];
            return;
        }
        
        int linesCount = 0;
        int currentLineLen = 0;
        int i = 0;
        while (i < text.Length)
        {
            while (i < text.Length && IsSpace(text[i]))
            {
                i++;
            }

            if (i >= text.Length)
            {
                return;
            }

            int wordStart = i;
            while (i < text.Length && !IsSpace(text[i]))
            {
                i++;
            }
            int wordLen = i - wordStart;

            if (currentLineLen == 0)
            {
                currentLineLen = wordLen;
            }
            else
            {
                if (currentLineLen + 1 + wordLen <= 50)
                {
                    currentLineLen = currentLineLen + 1 + wordLen;
                }
                else
                {
                    linesCount++;
                    currentLineLen = wordLen;
                }
            }
        }

        if (currentLineLen > 0)
        {
            linesCount++;
        }
        
        string[] result = new string[linesCount];
        int lineIndex = 0;
        currentLineLen = 0;
        char[] lineBuffer = new char[50];
        int bufferPos = 0;
        i = 0;
        while (i < text.Length)
        {
            while (i < text.Length && IsSpace(text[i]))
            {
                i++;
            }

            if (i >= text.Length)
            {
                return;
            }

            int wordStart = i;
            while (i < text.Length && !IsSpace(text[i]))
            {
                i++;
            }
            int wordLen = i - wordStart;

            if (bufferPos == 0)
            {
                for (int j = 0; j < wordLen; j++)
                {
                    lineBuffer[bufferPos++] = text[wordStart + j];
                }
                currentLineLen = wordLen;
            }
            else
            {
                if (currentLineLen + 1 + wordLen <= 50)
                {
                    lineBuffer[bufferPos++] = ' ';
                    for (int j = 0; j < wordLen; j++)
                    {
                        lineBuffer[bufferPos++] = text[wordStart + j];
                    }
                    currentLineLen = currentLineLen + 1 + wordLen;
                }
                else
                {
                    result[lineIndex] = new string(lineBuffer, 0, bufferPos);
                    lineIndex++;
                    bufferPos = 0;
                    for (int j = 0; j < wordLen; j++)
                    {
                        lineBuffer[bufferPos++] = text[wordStart + j];
                    }
                    currentLineLen = wordLen;
                }
            }
        }
        if (bufferPos > 0)
        {
            result[lineIndex] = new string(lineBuffer, 0, bufferPos);
        }

        Output = result;
    }

    public override string ToString()
    {
        if (Output == null || Output.Length == 0)
        {
            return "";
        }
        
        string result = "";
        for (int i = 0; i < Output.Length; i++)
        {
            result += Output[i];
            if (i != Output.Length - 1)
            {
                result += "\n";
            }
        }
        return result;
    }
}
