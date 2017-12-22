using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace YukiNET
{
    class Printcher
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

    class Program
    {
        static void Main(string[] args)
        {
            Printcher test = new Printcher();
            test.wordsPerMinute = 1.0f;
            test.text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.\nDonec interdum magna neque, vel porta lorem pellentesque fermentum.\nIn placerat volutpat nunc.";

            DateTime startTime = DateTime.Now;
            DateTime lastTime = startTime;
            do
            {
                DateTime currentTime = DateTime.Now;
                float deltaTime = (float)((currentTime - lastTime).TotalSeconds);
                lastTime = currentTime;

                Console.Write(test.Advance(deltaTime));
            } while (true);
        }
    }
}
