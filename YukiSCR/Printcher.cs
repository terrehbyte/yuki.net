using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace YukiSCR
{
    public class Printcher
    {
        public float charactersPerMinute;
        public string text;

        private float totalRuntime;
        private int lastIndex;

        StringBuilder currentText;
        public string displayText
        {
            get
            {
                return currentText.ToString();
            }
        }

        public Printcher(string queuedText)
        {
            text = queuedText;
            currentText = new StringBuilder(text.Length);
        }

        public float charactersPerSecond
        {
            get
            {
                return charactersPerMinute / 60.0f;
            }
            set
            {
                charactersPerMinute = value * 60.0f;
            }
        }
        public float wordsPerMinute
        {
            get
            {
                return charactersPerMinute / 5.0f;
            }
            set
            {
                charactersPerMinute = value * 5.0f;
            }
        }

        public bool IsFinished { get { return lastIndex == text.Length; } }
        public float SecondsBetweenLetters { get { return 1 / charactersPerSecond; } }

        public void Advance(float deltaTime)
        {
            Debug.Assert(deltaTime >= 0.0f, "The delta time should be greater or equal to zero.");

            if (IsFinished) { return; }

            totalRuntime += deltaTime;

            float lastPrintTime = lastIndex * SecondsBetweenLetters;
            int potentialPrintCount = (int)Math.Floor((totalRuntime - lastPrintTime) / SecondsBetweenLetters);

            int startIndex = lastIndex;

            lastIndex += potentialPrintCount;
            lastIndex = Math.Min(lastIndex, text.Length);

            currentText.Append(text.Substring(startIndex, lastIndex - startIndex));
        }
    }
}
