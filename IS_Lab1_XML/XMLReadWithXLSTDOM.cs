using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;

namespace IS_Lab1_XML
{
    internal class XMLReadWithXLSTDOM
    {

        public static Dictionary<string, HashSet<string>> Read(string xmlpath)
        {
            XPathDocument document = new XPathDocument(xmlpath);
            XPathNavigator navigator = document.CreateNavigator();

            XmlNamespaceManager manager = new XmlNamespaceManager(navigator.NameTable);
            manager.AddNamespace("x", "http://rejestry.ezdrowie.gov.pl/rpl/eksport-danych-v6.0.0");

            XPathExpression query = navigator.Compile("/x:produktyLecznicze/x:produktLeczniczy[@nazwaPostaciFarmaceutycznej = 'Krem' and @nazwaPowszechnieStosowana = 'Mometasoni furoas']");
            query.SetContext(manager);

            int count = navigator.Select(query).Count;

            Console.WriteLine("Liczba produktów leczniczych w postaci kremu, których jedyną substancją czynną jest Mometasoni furoas {0}", count);

            // --- ZADANIE 1.4.2 (Część 1) - Leki wielopostaciowe ---
            XPathExpression allDrugsQuery = navigator.Compile("/x:produktyLecznicze/x:produktLeczniczy");
            allDrugsQuery.SetContext(manager);
            XPathNodeIterator drugsIterator = navigator.Select(allDrugsQuery);

            Dictionary<string, HashSet<string>> preparatyXPath = new Dictionary<string, HashSet<string>>();

            while (drugsIterator.MoveNext())
            {
                string postac = drugsIterator.Current.GetAttribute("nazwaPostaciFarmaceutycznej", "");
                string sc = drugsIterator.Current.GetAttribute("nazwaPowszechnieStosowana", "");

                if (!string.IsNullOrEmpty(postac) && !string.IsNullOrEmpty(sc))
                {
                    if (!preparatyXPath.ContainsKey(sc))
                    {
                        preparatyXPath[sc] = new HashSet<string>();
                    }
                    preparatyXPath[sc].Add(postac);
                }
            }

            int wielopostaciowe = 0;
            foreach (var lek in preparatyXPath)
            {
                if (lek.Value.Count >= 2)
                {
                    wielopostaciowe++;
                }
            }
            Console.WriteLine("Liczba preparatów występujących pod więcej niż jedną postacią: {0}", wielopostaciowe);

            // --- ZADANIE 1.4.2 (Część 2) - TOP 5 Państw ---
            XPathExpression allDrugsQuery2 = navigator.Compile("//x:wytworcy");
            allDrugsQuery2.SetContext(manager);
            XPathNodeIterator wytworcaIterator = navigator.Select(allDrugsQuery2);

            Dictionary<string, HashSet<string>> panstwaWytworcy = new Dictionary<string, HashSet<string>>();

            while (wytworcaIterator.MoveNext())
            {
                string kraj = wytworcaIterator.Current.GetAttribute("krajWytworcyImportera", "");
                string nazwa = wytworcaIterator.Current.GetAttribute("nazwaWytworcyImportera", "");
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

            // --- ZADANIE 1.5 (Zaawansowane) - Jedna vs Wiele substancji czynnych ---
            //XPathExpression productsQuery = navigator.Compile("//*[local-name()='produktLeczniczy']");
            //productsQuery.SetContext(manager);
            //XPathNodeIterator productsIterator = navigator.Select(productsQuery);

            //int jednoSubstancjowe = 0;
            //int wieloSubstancjowe = 0;
            //int bezSubstancji = 0;

            //while (productsIterator.MoveNext())
            //{
            //    XPathNavigator product = productsIterator.Current.Clone();
            //    XPathExpression substancesQuery = product.Compile(".//*[local-name()='substancjaCzynna']");

            //    int liczbaSubstancji = product.Select(substancesQuery).Count;
            //    if (liczbaSubstancji == 1)
            //    {
            //        jednoSubstancjowe++;
            //    }
            //    else if (liczbaSubstancji > 1)
            //    {
            //        wieloSubstancjowe++;
            //    }
            //    else
            //    {
            //        bezSubstancji++;
            //    }
            //}

            //Console.WriteLine("\n--- Substancje czynne ---");
            //Console.WriteLine($"Produkty z jedną substancją czynną: {jednoSubstancjowe}");
            //Console.WriteLine($"Produkty z kilkoma substancjami czynnymi (leki złożone): {wieloSubstancjowe}");
            //Console.WriteLine($"Produkty bez określonej substancji czynnej w tagach: {bezSubstancji}");
            return preparatyXPath;
        }
    }
}
