using System;
using System.Xml;

namespace IS_Lab1_XML
{
    public class XMLReadWithDOMApproach
    {

        public static Dictionary<string, HashSet<string>> Read(string xmlpath)
        {
            // --- ZADANIE 1.2.3 ---
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlpath);

            string postac;
            string sc;
            int count = 0;

            var drugs = doc.GetElementsByTagName("produktLeczniczy");
            Console.WriteLine($"Wczytano produktów: {drugs.Count}");
            foreach (XmlNode d in drugs)
            {
                postac = d.Attributes.GetNamedItem("nazwaPostaciFarmaceutycznej").Value;
                sc = d.Attributes.GetNamedItem("nazwaPowszechnieStosowana").Value;

                if (postac == "Krem" && sc == "Mometasoni furoas")
                {
                    count++;
                }
            }
            Console.WriteLine("Liczba produktów leczniczych w postaci kremu, których jedyną substancją czynną jest Mometasoni furoas: {0}", count);

            // --- ZADANIE 1.2.4 ---
            Dictionary<string, HashSet<string>> preparaty = new Dictionary<string, HashSet<string>>();

            foreach (XmlNode d in drugs)
            {
                string p = d.Attributes.GetNamedItem("nazwaPostaciFarmaceutycznej")?.Value;
                string s = d.Attributes.GetNamedItem("nazwaPowszechnieStosowana")?.Value;

                if (s != null && p != null)
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
            var dane = doc.GetElementsByTagName("wytworcy");

            Dictionary<string, HashSet<string>> panstwaWytworcy = new Dictionary<string, HashSet<string>>();

            foreach (XmlNode d in dane)
            {
                string kraj = d.Attributes?.GetNamedItem("krajWytworcyImportera")?.Value;
                string nazwa = d.Attributes?.GetNamedItem("nazwaWytworcyImportera")?.Value;

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