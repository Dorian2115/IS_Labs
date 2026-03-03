namespace IS_Lab1_XML
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string xmlpath = Path.Combine("Assets", "data.xml");

            //Console.WriteLine("XML loaded by DOM Approach");
            //XMLReadWithDOMApproach.Read(xmlpath);

            Console.WriteLine("XML loaded with XPath");
            XMLReadWithDOMApproach.Read(xmlpath);

            Console.ReadLine();
        }
    }
}
