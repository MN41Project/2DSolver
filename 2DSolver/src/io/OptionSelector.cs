using System;

namespace DSolver
{
    public class OptionSelector
    {
        public string[] Options { get; private set; }
        public string Explanation { get; private set; }
        public int Selected { get; private set; }

        public OptionSelector()
        {
        }

        public OptionSelector WithOptions(string[] options)
        {
            this.Options = options;
            return this;
        }

        public OptionSelector WithExplanation(string explanation)
        {
            this.Explanation = explanation;
            return this;
        }

        public int PickAnOption()
        {
            bool chosen = false;

            while (!chosen)
            {
                this.Display();
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (this.Selected > 0)
                        {
                            this.Selected--;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (this.Selected < this.Options.Length - 1)
                        {
                            this.Selected++;
                        }
                        break;
                    case ConsoleKey.Enter:
                        chosen = true;
                        break;
                    default:
                        break;
                }
            }

            Console.Clear();

            return this.Selected;
        }

        private void Display()
        {
            Console.Clear();
            Console.WriteLine("{0} (use keyboard arrows, ENTER to select):", this.Explanation);
            for (var i = 0; i < this.Options.Length; i++)
            {
                if (i == this.Selected)
                {
                    Console.Write("> ");
                }
                else
                {
                    Console.Write("  ");
                }
                Console.Write(this.Options[i]);
                Console.WriteLine();
            }
        }
    }
}

