using System.Reflection.Metadata;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        //// загрузка из файла
        XDocument document = XDocument.Load("path/to/file.xml");

        //// загрузка из строки
        //string xmlString = @"<Root><Element>Value</Element></Root>";
        //XDocument document2 = XDocument.Parse(xmlString);

        // создание
        XDocument contactsDoc = new XDocument(
         new XElement("Contacts",
            new XElement("Contact",
                new XElement("Name", "Tom"),
                 new XElement("Phone", "123456789")
             ),
             new XElement("Contact",
                new XElement("Name", "Bob"),
                new XElement("Phone", "987654321")
              )
          )
        );

        // Сохранение в файл
        contactsDoc.Save("contacts.xml");

        //Descendants
        document.Descendants("Name");

        //Elements
        document.Elements("")

    }
}