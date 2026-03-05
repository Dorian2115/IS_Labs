using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace IS_Lab1_XML
{
    internal class XMLReadWithSAXApproach
    {
        public static Dictionary<string, HashSet<string>> Read(string xmlpath)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            settings.IgnoreProcessingInstructions = true;
            settings.IgnoreWhitespace = true;

            XmlReader reader = XmlReader.Create(xmlpath, settings);

            int count = 0;
            string postac = "";
            string sc = "";

            reader.MoveToContent();

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "produktLeczniczy")
                {
                    postac = reader.GetAttribute("nazwaPostaciFarmaceutycznej");
                    sc = reader.GetAttribute("nazwaPowszechnieStosowana");
                    if (postac == "Krem" && sc == "Mometasoni furoas")
                    {
                        count++;
                    }
                }
            }
            Console.WriteLine("Liczba produktów leczniczych w postaci kremu, których jedyną substancją czynną jest Mometasoni furoas {0} ", count);

            // --- ZADANIE 1.3.2 ---
            //XmlReaderSettings settings = new XmlReaderSettings();
            //settings.IgnoreComments = true;
            //settings.IgnoreProcessingInstructions = true;
            //settings.IgnoreWhitespace = true;

            XmlReader reader2 = XmlReader.Create(xmlpath, settings);

            reader2.MoveToContent();

            Dictionary<string, HashSet<string>> preparaty = new Dictionary<string, HashSet<string>>();

            while (reader2.Read())
            {
                string p = reader2.GetAttribute("nazwaPostaciFarmaceutycznej");
                string s = reader2.GetAttribute("nazwaPowszechnieStosowana");

                if (!string.IsNullOrEmpty(s) && !string.IsNullOrEmpty(p))
                {
                    if (!preparaty.ContainsKey(s))
                    {
                        preparaty.Add(s, new HashSet<string>());
                    }
                    preparaty[s].Add(p);
                }
            }

            int wielopostaciowe = 0;
            foreach (var lek in preparaty)
            {
                if (lek.Value.Count >= 2)
                {
                    wielopostaciowe++;
                }
            }
            Console.WriteLine("Liczba preparatów występujących pod więcej niż jedną postacią: {0}", wielopostaciowe);

            XmlReader reader3 = XmlReader.Create(xmlpath, settings);
            reader3.MoveToContent();

            Dictionary<string, HashSet<string>> panstwaWytworcy = new Dictionary<string, HashSet<string>>();

            while (reader3.Read())
            {
                string kraj = reader3.GetAttribute("krajWytworcyImportera");
                string nazwa = reader3.GetAttribute("nazwaWytworcyImportera");

                if (!string.IsNullOrEmpty(kraj) && !string.IsNullOrEmpty(nazwa))
                {
                    if (!panstwaWytworcy.ContainsKey(kraj))
                    {
                        panstwaWytworcy[kraj] = new HashSet<string>();
                    }
                    panstwaWytworcy[kraj].Add(nazwa);
                }
            }

            var top5Panstw = panstwaWytworcy.OrderByDescending(p => p.Value.Count).Take(5);

            Console.WriteLine("\n--- TOP 5 PAŃSTW ---");
            foreach (var panstwo in top5Panstw)
            {
                Console.WriteLine($"Państwo: {panstwo.Key} (Liczba wytwórców: {panstwo.Value.Count})");
                foreach (var wytworca in panstwo.Value.Take(3))
                {
                    Console.WriteLine($" - {wytworca}");
                }
            }

            return preparaty;
        }
    }
}
