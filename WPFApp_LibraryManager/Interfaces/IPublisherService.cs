using System.Collections.Generic;
using WPFApp_LibraryManager.Models;

namespace WPFApp_LibraryManager.Interfaces
{
    public interface IPublisherService
    {
        List<Publisher> GetPublishers();
    }
}
