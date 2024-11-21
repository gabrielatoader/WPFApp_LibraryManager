namespace WPFApp_LibraryManager.Models
{
    public class BookFilters
    {
        public int AuthorId { get; set; }

        public int CategoryId { get; set; }

        public int PublisherId { get; set; }

        public string SearchString { get; set; }

        public bool SearchInTitle { get; set; }

        public bool SearchInAuthor { get; set; }

        public bool SearchInPublisher { get; set; }

        public bool SearchInISBN { get; set; }

        public bool SearchInCategory { get; set; }

        public bool SearchLocationIsUndefined => !SearchInTitle && !SearchInAuthor && !SearchInPublisher && !SearchInISBN && !SearchInCategory;
    }
}
