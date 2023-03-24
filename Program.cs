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
                string[] argument = Console.ReadLine().Split();
                string command = argument[0];
                //FIXME: quit stänger inte av programmet
                if (command == "quit")
                {
                    Console.WriteLine("Goodbye!");
                }
                else if (command == "load")
                {
                    //FIXME: kan inte ladda min testfil med 'load C:\Users\Hanne\Desktop\Datalogiskt tänkande och Problemlösning\Vecka 2\test.txt'
                    if (argument.Length == 2)
                    {
                        using (StreamReader sr = new StreamReader(argument[1]))
                        {
                            dictionary = new List<SweEngGloss>(); // Empty it!
                            string line = sr.ReadLine();
                            while (line != null)
                            {
                                SweEngGloss gloss = new SweEngGloss(line);
                                dictionary.Add(gloss);
                                line = sr.ReadLine();
                            }
                        }
                    }
                    else if(argument.Length == 1)
                    {
                        using (StreamReader sr = new StreamReader(defaultFile))
                        {
                            dictionary = new List<SweEngGloss>(); // Empty it!
                            string line = sr.ReadLine();
                            while (line != null)
                            {
                                SweEngGloss gloss = new SweEngGloss(line);
                                dictionary.Add(gloss);
                                line = sr.ReadLine();
                            }
                        }
                    }
                }
                else if (command == "list")
                {
                    //FIXME: krashar om man inte har laddat en fil
                    foreach(SweEngGloss gloss in dictionary)
                    {
                        //vad gör -10?
                        Console.WriteLine($"{gloss.word_swe,-10}  - {gloss.word_eng,-10}");
                    }
                }
                else if (command == "new")
                {
                    if (argument.Length == 3)
                    {
                        //Fick error: object reference not set to an instance of an object
                        //om man inte har laddat från en fil
                        dictionary.Add(new SweEngGloss(argument[1], argument[2]));
                    }
                    else if(argument.Length == 1)
                    {
                        Console.WriteLine("Write word in Swedish: ");
                        string s = Console.ReadLine();
                        Console.Write("Write word in English: ");
                        string e = Console.ReadLine();
                        //Fick error: object reference not set to an instance of an object
                        //om man inte har laddat från en fil
                        dictionary.Add(new SweEngGloss(s, e));
                    }
                }
                else if (command == "delete")
                {
                    if (argument.Length == 3)
                    {
                        int index = -1;
                        for (int i = 0; i < dictionary.Count; i++) {
                            SweEngGloss gloss = dictionary[i];
                            if (gloss.word_swe == argument[1] && gloss.word_eng == argument[2])
                                index = i;
                        }
                        //Error om man försöker ta bort ett element som inte finns
                        dictionary.RemoveAt(index);
                    }
                    else if (argument.Length == 1)
                    {
                        Console.WriteLine("Write word in Swedish: ");
                        string s = Console.ReadLine();
                        Console.Write("Write word in English: ");
                        string e = Console.ReadLine();
                        int index = -1;
                        //Error om man inte har laddat en lista
                        for (int i = 0; i < dictionary.Count; i++)
                        {
                            SweEngGloss gloss = dictionary[i];
                            if (gloss.word_swe == s && gloss.word_eng == e)
                                index = i;
                        }
                        dictionary.RemoveAt(index);
                    }
                }
                else if (command == "translate")
                {
                    if (argument.Length == 2)
                    {
                        //error om man inte laddat en lista
                        foreach (SweEngGloss gloss in dictionary)
                        {
                            //blir ingen utskrift om man försöker med ett ord som inte finns, fast programmet fortsätter köra
                            if (gloss.word_swe == argument[1])
                                Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                            if (gloss.word_eng == argument[1])
                                Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                        }
                    }
                    else if (argument.Length == 1)
                    {
                        Console.WriteLine("Write word to be translated: ");
                        string s = Console.ReadLine();
                        //error om man inte laddat en lista
                        foreach (SweEngGloss gloss in dictionary)
                        {
                            //blir ingen utskrift om man försöker med ett ord som inte finns, fast programmet fortsätter köra
                            if (gloss.word_swe == s)
                                Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                            if (gloss.word_eng == s)
                                Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                        }
                    }
                }
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
    }
}