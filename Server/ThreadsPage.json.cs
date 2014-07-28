using Starcounter;

[ThreadsPage_json]
partial class ThreadsPage : Page
{
}


[ThreadsPage_json.Threads]
partial class ThreadsPageThreads : Page, IBound<Board.Thread>
{
    // void Handle(Input.Name input)
    // {
    //     Name = input.Value; 
    //     //you need to assign value explicitly to the property before Transaction.Commit, because the default assignment done by Starcounter is made AFTER Handler is executed
    //     //alternatively you can imply auto saving using (rather undocumented) HasChanged function - see https://github.com/Starcounter/TodoDemo/blob/master/TodoListModel.json.cs
    //     //we can bring it up on status meeting
        
    //     Transaction.Commit();
    // }
    // void Handle(Input.Amount input)
    // {
    //     Amount = input.Value;
    //     Transaction.Commit();
    // }
}
