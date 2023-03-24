namespace MJU23v_D10_inl_sveng
{
    //Inlämning Hannes Paulsson
    internal class Program
    {
        static List<SweEngGloss> dictionary;
        class SweEngGloss
        {
            public string word_swe, word_eng;
            public SweEngGloss(string word_swe, string word_eng)
            {
                this.word_swe = word_swe; this.word_eng = word_eng;
            }
            public SweEngGloss(string line)
            {
                string[] words = line.Split('|');
                this.word_swe = words[0]; this.word_eng = words[1];
            }
        }
        static void Main(string[] args)
        {
            string defaultFile = "..\\..\\..\\dict\\sweeng.lis";
            Console.WriteLine("Welcome to the dictionary app! \nType 'help' for available commands.");
            do
            {
                Console.Write("> ");
                //Tror att spliten här gör så att 'load x' inte fungerar. testa med split " "?
                string[] argument = Console.ReadLine().Split();
                string command = argument[0];
                if (command == "quit")
                {
                    Console.WriteLine("Goodbye!");
                    break;
                }
                else if (command == "load")
                {
                    try
                    {
                        //kan inte ladda min testfil med 'load C:\Users\Hanne\Desktop\Datalogiskt tänkande och Problemlösning\Vecka 2\test.txt'
                        //tror det är för att pathen innehåller mellanslag, vilket splittar 'argument'
                        if (argument.Length == 2)
                        {
                            LoadFile(argument[1]);
                        }
                        else if (argument.Length == 1)
                        {
                            //extraherade innehållet till en metod
                            LoadFile(defaultFile);
                        }
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        Console.WriteLine($"ERROR: Kan inte hitta den filen!");
                    }
                    catch (System.IO.DirectoryNotFoundException)
                    {
                        Console.WriteLine($"ERROR: Kunde inte hitta mappen!");
                    }
                    catch (System.UnauthorizedAccessException)
                    {
                        //Fick detta error: System.UnauthorizedAccessException: 'Access to the path 'C:\Users\Hanne\Desktop\test' is denied.'
                        Console.WriteLine($"ERROR: Access denied?");
                    }
                }
                else if (command == "list")
                {
                    //Märkte att debuggern stannar, även om man har en try-catch sats.
                    //Dock fungerar programmet fortfarande om man kör 'continue'.
                    try
                    {
                        foreach (SweEngGloss gloss in dictionary)
                        {
                            Console.WriteLine($"{gloss.word_swe,-10}  - {gloss.word_eng,-10}");
                        }
                    }
                    catch (NullReferenceException)
                    {
                        Console.WriteLine($"ERROR: Listan är tom!");
                    }
                }
                //Tror inte new behöver en metod, eftersom den bara har en rad som är dubblett
                else if (command == "new")
                {
                    //Samma try catch som med 'list', eftersom det var samma error
                    try
                    {
                        if (argument.Length == 3)
                        {
                            dictionary.Add(new SweEngGloss(argument[1], argument[2]));
                            //Om man bara skriver in ett ord händer ingenting, men programmet fortsätter köra
                        }
                        else if (argument.Length == 1)
                        {
                            Console.WriteLine("Write word in Swedish: ");
                            string swedish = Console.ReadLine();
                            Console.Write("Write word in English: ");
                            string english = Console.ReadLine();
                            dictionary.Add(new SweEngGloss(swedish, english));
                        }
                    }
                    catch(NullReferenceException )
                    {
                        Console.WriteLine($"ERROR: Listan är inte initialiserad!");
                    }
                }
                else if (command == "delete")
                {
                    //Har två catch, eftersom den hade två olika exceptions.
                    try
                    {
                        if (argument.Length == 3)
                        {
                            DeleteWord(argument[1], argument[2]);
                        }
                        else if (argument.Length == 1)
                        {
                            //samma namnbyte som med 'new'
                            Console.WriteLine("Write word in Swedish: ");
                            string swedish = Console.ReadLine();
                            Console.Write("Write word in English: ");
                            string english = Console.ReadLine();
                            //Gjorde koden efter readlines till en metod, eftersom den är lika mellan if satserna.
                            DeleteWord(swedish, english);
                        }
                    }
                    catch (NullReferenceException)
                    {
                        Console.WriteLine($"ERROR: Listan är inte initialiserad!");
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Console.WriteLine($"ERROR: Den kombinationen finns inte i listan!");
                    }
                }
                else if (command == "translate")
                {
                    //Samma try catch som med 'list' och 'new', eftersom det var samma error
                    try
                    {
                        if (argument.Length == 2)
                        {
                            //Gjorde en metod istället
                            TranslateWord(argument[1]);
                        }
                        else if (argument.Length == 1)
                        {
                            Console.WriteLine("Write word to be translated: ");
                            string input = Console.ReadLine();
                            //Gjorde en metod istället
                            TranslateWord(input);
                        }
                    }
                    catch (NullReferenceException)
                    {
                        Console.WriteLine($"ERROR: Listan är inte initialiserad!");
                    }
                }
                //Tror inte help behöver en metod, eftersom den inte har koddubbletter
                else if (command == "help")
                {
                    Console.WriteLine("Följande kommandon finns:");
                    Console.WriteLine("quit - gör vad du tror");
                    Console.WriteLine("load - hämtar ord från sweeng.lis \n load <path> laddar från en valfri fil.");
                    Console.WriteLine("list - skriver ut ordlistan");
                    Console.WriteLine("new - lägger till ett nytt ord \n new <svenska> <engelska> kan användas istället.");
                    Console.WriteLine("delete - tar bort ett ord \n delete <svenska> <engelska> tar bort ett ord om det finns i listan.");
                    Console.WriteLine("translate - översätter ett ord från svenska till engelska, eller tvärtom \n translate <ord> kan också användas.");
                    Console.WriteLine("help - visar alla kommandon");

                }
                else
                {
                    Console.WriteLine($"Unknown command: '{command}'");
                }
            }
            while (true);
        }

        private static void TranslateWord(string word)
        {
            foreach (SweEngGloss gloss in dictionary)
            {
                //blir ingen utskrift om man försöker med ett ord som inte finns, fast programmet fortsätter köra
                if (gloss.word_swe == word)
                    Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                if (gloss.word_eng == word)
                    Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
            }
        }

        private static void DeleteWord(string swedish, string english)
        {
            int index = -1;
            for (int i = 0; i < dictionary.Count; i++)
            {
                SweEngGloss gloss = dictionary[i];
                //Inget ord tas bort om man bara skriver på svenska
                if (gloss.word_swe == swedish && gloss.word_eng == english)
                    index = i;
            }
            dictionary.RemoveAt(index);
        }

        private static void LoadFile(string filePath)
        {
            using (StreamReader textFile = new StreamReader(filePath))
            {
                dictionary = new List<SweEngGloss>(); // Empty it!
                string line = textFile.ReadLine();
                while (line != null)
                {
                    SweEngGloss gloss = new SweEngGloss(line);
                    dictionary.Add(gloss);
                    line = textFile.ReadLine();
                }
            }
        }
    }
}