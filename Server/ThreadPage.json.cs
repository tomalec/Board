using Starcounter;

[ThreadPage_json]
partial class ThreadPage : Page
{
    protected override string UriFragment
    {
        get
        {
            return Data.GetObjectID();
        }
    }
}

