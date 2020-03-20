using AngularWithAPI.Models;

using AngularWithAPI.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularWithAPI.Repositories
{
    public interface IPersoneRepository
    {
        Pagination<Persone> GetPersones(int pageNumber, int pagesize);
        // IEnumerable<Persone> AllPersones();
        Persone AllPersoneWithContact(int id, bool includeContact = false);
        Persone PersoneById(int id);  
        
        void AddPersone(Persone persone);
        void UpdatePersone(int id, Persone persone);
        void DeletePersone(Persone persone); 
        

    }
}
