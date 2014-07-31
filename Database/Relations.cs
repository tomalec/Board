using Concepts.Ring1;
using Concepts.Ring2;
using Starcounter;

namespace Board {

    [Database]
    public class AuthorRelation : Relation {

        [SynonymousTo("WhatIs")]
        public readonly Somebody WhoIs;
        public void SetWhoIs(Somebody whoIs) {

            SetWhatIs(whoIs);
        }

        [SynonymousTo("ToWhat")]
        public readonly Message Message;
        public void SetMessage(Message message) {

            SetToWhat(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="author"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static AuthorRelation CreateAuthorRelation(Somebody author, Message message) {

            AuthorRelation relation = new AuthorRelation();
            relation.SetWhoIs(author);
            relation.SetMessage(message);

            return relation;
        }
    }



}
