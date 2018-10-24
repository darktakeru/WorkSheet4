using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfServiceBookstore
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IServiceBookstore
    {
        [OperationContract]
        void AddBook(Book newBook);

        [OperationContract]
        List<Book> GetBooks();

        [OperationContract]
        List<Book> GetBooksByCategory(BookCategory category);

        [OperationContract]
        Book GetBookByTitle(string title);

        [OperationContract]
        bool DeleteBook(string title);
    }

    public enum BookCategory
    {
        WEB, CHILDREN,SCIENCE, BIOGRAPHIES, ROMANCE, OTHER
    }

    [DataContract]

    public class Book
    {
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Author { get; set; }
        [DataMember]
        public int Year { get; set; }
        [DataMember]
        public float Price { get; set; }
        [DataMember]
        public BookCategory Category { get; set; }
    }
}
