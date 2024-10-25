namespace WPFApp_LibraryManager.Utils
{
    public enum QueryMode
    { 
        All, 
        Authors, 
        Books, 
        Categories, 
        Publishers 
    };
    public enum QuerySearchLocation 
    { 
        None, 
        Authors, 
        Categories, 
        ISBNs, 
        Titles, 
        Publishers 
    };
    public enum QueryFilter 
    { 
        None, 
        Authors, 
        Categories, 
        Publishers 
    };
}