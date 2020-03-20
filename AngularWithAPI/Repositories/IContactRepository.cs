using AngularWithAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularWithAPI.Repositories
{
    public interface IContactRepository
    {
        Persone Persones(int personeId);
        Persone GetPersoneById(int personeId, int contactId);

        void AddContact(int PersoneId, Contact contact);
        void UpdateContact(int personeId, int ContactId, Contact newContact);
        void DeleteContact(int personeId, int contactId);

    }
}
