using Starcounter;                                  // Most stuff relating to the database, JSON and communication is in this namespace
using Concepts.Ring1;
using Starcounter.Templates;

[Master_json]                                       // This attribute tells Starcounter that the class corresponds to an object in the JSON-by-example file.
partial class Master : Page {

    /// <summary>
    /// Every application in Starcounter works like a console application. They have an .EXE ending. They have a Main() function and
    /// they can do console output. However, they are run inside the scope of a database rather than connecting to it.
    /// </summary>
    static void Main()
    {
        // Mapper
        HandlerOptions h1 = new HandlerOptions() { HandlerLevel = 1 };
        HandlerOptions h0 = new HandlerOptions() { HandlerLevel = 0 };

        Handle.GET("/board/partials/author/{?}", (string objectId) =>
        {
            return (Json)X.GET("/societyobjects/ring1/person/" + objectId);
        }, h0);

        Handle.GET("/societyobjects/ring1/person/{?}", (string objectId) =>
        {
            return (Json)X.GET("/board/partials/author/" + objectId, 0, h1);
        }, h0);

        // Setting default handler level to 1.
        HandlerOptions.DefaultHandlerLevel = 1;
        Handlers.AddExtraHandlerLevel();
        
        // Handle.POST("/init-demo-data", () => {      // The Handle class is where you register new handlers for incomming requests.
        //     DemoData.Create();                      // Will create some demo data.
        //     return 201;                             // Returning an integer is the shortcut for returning a response with a status code.
        // });

        // Handle.GET("/master", () =>
        // {
        //     Master m = new Master()
        //     {                                       // This is the root view model for our application. A view model is a JSON object/tree.
        //         Html = "/master.html",              // This is just a field we added to allow the client to know what Html to load. No magic.
        //     };
        //     m.Session = new Session();              // Save the JSON on the server to be accessed using GET/PATCH to allow it to be used as a view model in web components.
        //     return m;                               // Return the JSON or the HTML depending on the type asked for. See Page.json on how Starcounter knowns what to return.
        // });

        // Handle.GET("/", () => {                     // The root page of our website.
        //     return PrimaryApp.GET("/primary");      // Redirecting root to Tab 1
        // });

        // Handle.GET("/primary", () =>                // The main screen of the app
        // {
        //     var m = Master.GET("/master");          // Create the view model for the main application frame.
        //     PrimaryApp p = new PrimaryApp();        // The email application also consists of a view model.
        //     p.Html = "/primary.html";               // Starcounter is a generic server and does not know of Html, so this is a variable we create in Page.json
        //     p.AddSomeNiceMenuItems(m);              // Adds some menu items in the main menu (by modifying the master view model)
        //     m.ApplicationPage = p;                  // Place the email applications view model inside the main application frame as its subpage.
        //     m.TabName = "My Form";            // Used to highlight the current tab in the client
        //     return p;                               // Returns the home page. As you can see in Page.json, we taught it how to serve both HTML and the JSON view model without any extra work.
        // });

        // Handle.GET("/primary/create", () => 
        // {
        //     var p = PrimaryApp.GET("/primary");
        //     p.FocusedPage = new PrimaryPage()
        //     {
        //         Html = "/primary-create.html"
        //     };
        //     return p;
        // });        


        // Polyjuice hadlers
        // Note that all handlers could be mapped so serve content for different URLs

        // Dashboard 
        // Usually brief summary, or basic feature to be shown on a main screen.
        Handle.GET("/dashboard", () =>
        {
            var page = new DashboardPage()
            {
                //Html = "/dashboard.html" // will fail as other app does also use `/dashboard.html`
                Html = "/board-dashboard.html"
            };

            var threads = SQL<Board.Thread>("SELECT t FROM Board.Thread t LIMIT ?", 5);
            page.Threads.Data = threads;

            page.Transaction = new Transaction();
            return page;
        });

        // Menu
        // Launcher navigation menu
        Handle.GET("/menu", () =>
        {
            var p = new Page()
            {
                Html = "/board-menu.html"
            };
            return p;
        });


        // Threads list
        Handle.GET("/board/threads", () =>
        {
            var page = new ThreadsPage()
            {
                Html = "/board-threads.html"
            };
            page.Transaction = new Transaction();
            page.Session = Session.Current;

            // var threads = SQL<Starcounter.Json>("SELECT t, SUBSTR(t.body, 0, 255) as shortBody FROM Board.Thread t");
            var threads = SQL<Board.Thread>("SELECT t FROM Board.Thread t");

            page.Threads.Data = threads;
            
            return page;
        });

        // New thread 
        Handle.GET("/board/threads/add", () =>
        {
            var page = new AddThreadPage()
            {
                Html = "/board-threads-add.html"
            };
            page.Transaction = new Transaction();
            page.Transaction.Add(() =>
            {
                page.Data = new Board.Thread();
            });
            page.Session = Session.Current;
            return page;
        });

        // Thread page
        Handle.GET("/board/threads/{?}", (string objectId) =>
        {
            ThreadPage c = new ThreadPage()
            {
                Html = "/board-thread-full.html"
            };
            var thread = SQL<Board.Thread>("SELECT t FROM Board.Thread t WHERE ObjectId = ?", objectId).First;
            c.Data = thread;
            c.Transaction = new Transaction();
            c.Session = Session.Current;

            c.Author = (Page)X.GET("/board/partials/author/" + thread.Author.GetObjectID());

            return c;
        });

        
        // Author partial
        Handle.GET("/board/partials/author/{?}", (string objectId) =>
        {
            Page page = new AuthorPage()
            {
                Html = "/board-author.html"
            };
            var author = SQL<Somebody>("SELECT t FROM Somebody t WHERE ObjectId = ?", objectId).First;

            page.Transaction = new Transaction();
            if (author == null)
            {
                page.Transaction.Add(() =>
                {
                    author = new Somebody()
                    {
                    };
                    page.Data = author;
                });
            }
            else
            {

                page.Data = author;
            }
            // if (author.Email == "")
            // {
            //     page.Editing = true;
            // }


            //page.Uri = "/launcher/workspace/supercrm/companies/" + objectId;
            page.Session = Session.Current;

            return page;
        }
        );


        // Map workspace call to list, and add tiles 
        Handle.GET("/board", () =>
        {
            return (Json)X.GET("/board/threads/add");
        });
        // Fails due to 
        // https://github.com/Starcounter/Starcounter/issues/2192
        //
        // Handle.GET("/board", () =>
        // {
        //    return (Json)X.GET("/board/threads");
        // });
    }
}




