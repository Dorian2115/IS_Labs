namespace IS_Lab1_XML
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string xmlpath = Path.Combine("Assets", "data.xml");

            Console.WriteLine("XML loaded by DOM Approach");
            Dictionary<string, HashSet<string>> s1 = XMLReadWithDOMApproach.Read(xmlpath);
            Console.WriteLine(s1);

            Console.WriteLine("---------------------------\n");

            Console.WriteLine("XML loaded by SAX Approach");
            Dictionary<string, HashSet<string>> s2 = XMLReadWithSAXApproach.Read(xmlpath);
            Console.WriteLine(s2);


            Console.WriteLine("---------------------------\n");

            Console.WriteLine("XML loaded with XPath");
            Dictionary<string, HashSet<string>> s3 = XMLReadWithXLSTDOM.Read(xmlpath);
            Console.WriteLine(s3);

            Console.ReadLine();
        }
    }
}
