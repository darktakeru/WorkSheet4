using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml;

namespace WcfServiceBookstore
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class ServiceBookstore : IServiceBookstore
    {

        string filePath = AppDomain.CurrentDomain.BaseDirectory.ToString() + @"App_Data\bookstore.xml";

        public void AddBook(Book newBook)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            XmlNode root = doc.SelectSingleNode("/bookstore");

            XmlElement livro = doc.CreateElement("book");
            livro.SetAttribute("category", newBook.Category.ToString());
            XmlElement titulo = doc.CreateElement("title");
            titulo.InnerText = newBook.Title;
            XmlElement autor = doc.CreateElement("author");
            autor.InnerText = newBook.Author;
            XmlElement ano = doc.CreateElement("year");
            ano.InnerText = newBook.Year.ToString();
            XmlElement preco = doc.CreateElement("price");
            preco.InnerText = newBook.Price.ToString();

            livro.AppendChild(titulo);
            livro.AppendChild(autor);
            livro.AppendChild(ano);
            livro.AppendChild(preco);

            root.AppendChild(livro);

            doc.Save(filePath);
        }

        public bool DeleteBook(string title)
        {
            bool deleted = false;
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            XmlNode filtro = doc.SelectSingleNode($"/bookstore/book[title='{title}']");

            if(filtro != null)
            {
                XmlNode pai = filtro.ParentNode;
                pai.RemoveChild(filtro);
                deleted = true;
                doc.Save(filePath);
            }
            return deleted;
        }

        public Book GetBookByTitle(string title)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            XmlNode filtro = doc.SelectSingleNode($"/bookstore/book[title='{title}']");

                Book filter = new Book();
                filter.Title = filtro["title"].InnerText;
                filter.Author = filtro["author"].InnerText;
                filter.Year = int.Parse(filtro["year"].InnerText);
                filter.Price = float.Parse(filtro["price"].InnerText, System.Globalization.NumberFormatInfo.InvariantInfo);
                filter.Category = (BookCategory)Enum.Parse(typeof(BookCategory), filtro.Attributes["category"].Value);

            return filter;
        }

        public List<Book> GetBooks()
        {
            List<Book> livros = new List<Book>();
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            XmlNodeList filtro = doc.SelectNodes("/bookstore/book");

            foreach (XmlNode item in filtro)
            {
                Book b = new Book();
                b.Title = item["title"].InnerText;
                b.Author = item["author"].InnerText;
                b.Year = int.Parse(item["year"].InnerText);
                b.Price = float.Parse(item["price"].InnerText, System.Globalization.NumberFormatInfo.InvariantInfo);
                b.Category = (BookCategory)Enum.Parse(typeof(BookCategory), item.Attributes["category"].Value);

                livros.Add(b);
            }
            return livros;
        }

        public List<Book> GetBooksByCategory(BookCategory category)
        {
            List<Book> livros = new List<Book>();
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            XmlNodeList filtro = doc.SelectNodes($"/bookstore/book[@category= '{category}']");

            foreach (XmlNode item in filtro)
            {
                Book b = new Book();
                b.Title = item["title"].InnerText;
                b.Author = item["author"].InnerText;
                b.Year = int.Parse(item["year"].InnerText);
                b.Price = float.Parse(item["price"].InnerText, System.Globalization.NumberFormatInfo.InvariantInfo);
                b.Category = (BookCategory)Enum.Parse(typeof(BookCategory), item.Attributes["category"].Value);

                livros.Add(b);
            }
            return livros;
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }
    }
}
