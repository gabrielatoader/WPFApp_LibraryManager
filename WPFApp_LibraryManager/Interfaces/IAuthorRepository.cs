using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFApp_LibraryManager.Models;

namespace WPFApp_LibraryManager.Interfaces
{
    public interface IAuthorRepository
    {
        List<Author> GetAuthorList();
    }
}
