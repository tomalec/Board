using Starcounter;
using Concepts.Ring1;
using Concepts.Ring2;

[AddThreadPage_json]
partial class AddThreadPage : Page
{
    void Handle(Input.Save input)
    {
        //get somebody out of email
            Somebody somebody = null;


            EMailAddress emailRelation = Db.SQL<EMailAddress>("SELECT o FROM EMailAddress o Where o.Name=?", this.Email).First;
            if (emailRelation != null && emailRelation.ToWhat is Somebody)
            {
                somebody = (Somebody)emailRelation.ToWhat;
            }
            else
            {
                somebody = new Somebody();
                somebody.Name = this.Email;

                EMailAddress emailRel = new EMailAddress();
                emailRel.SetToWhat(somebody);
                emailRel.EMail = this.Email;
            }
            Board.AuthorRelation.CreateAuthorRelation(somebody, (Board.Thread)this.Data);


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

