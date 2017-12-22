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
        public float wordsPerMinute;
        public string text;

        private float totalRuntime;
        private int lastIndex;

        public bool IsFinished { get { return lastIndex == text.Length; } }
        public float SecondsBetweenLetters { get { return wordsPerMinute / 60.0f; } }

        public string Advance(float deltaTime)
        {
            Debug.Assert(deltaTime >= 0.0f, "The delta time should be greater or equal to zero.");

            if (IsFinished) { return string.Empty; }

            totalRuntime += deltaTime;

            float lastPrintTime = lastIndex * SecondsBetweenLetters;
            int potentialPrintCount = (int)Math.Floor((totalRuntime - lastPrintTime) / SecondsBetweenLetters);

            int startIndex = lastIndex;

            lastIndex += potentialPrintCount;
            lastIndex = Math.Min(lastIndex, text.Length);

            return text.Substring(startIndex, lastIndex - startIndex);
        }
    }
}
