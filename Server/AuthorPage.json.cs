using Starcounter;

[AuthorPage_json]
partial class AuthorPage : Page
{
    protected override string UriFragment
    {
        get
        {
            return Data.GetObjectID();
        }
    }
}

