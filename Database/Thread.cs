using Concepts.Ring1;
using Concepts.Ring2;
using Starcounter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Board {

    [Database]
    public class Thread : Message
    {
        public string title;

        public Somebody Author {

            get {
                return Db.SQL<Somebody>("SELECT o.WhoIs FROM AuthorRelation o WHERE o.ToWhat=?", this).First;
            }
        }



        /// <summary>
        /// Delete author relations
        /// </summary>
        public void DeleteRelations() {
            // Cleanup Author releation
            IEnumerable authors = Db.SQL<AuthorRelation>("SELECT o FROM AuthorRelation o WHERE o.ToWhat=?", this);
            if (authors != null) {
                IEnumerator enumerator = authors.GetEnumerator();
                while (enumerator.MoveNext()) {
                    enumerator.Current.Delete();
                }
            }
        }

    }
}
