using Starcounter;

[AddThreadPage_json]
partial class AddThreadPage : Page
{
    void Handle(Input.Save input)
    {
        Transaction.Commit();
        RedirectUrl = "/launcher/workspace/board/threads/add";
        Data = new Board.Thread();
    }
    protected override string UriFragment
    {
        get
        {
            return "/board/threads/" + Data.GetObjectID();
        }
    }
}

