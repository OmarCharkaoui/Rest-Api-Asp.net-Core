using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularWithAPI.Data;
using AngularWithAPI.Models;
using AngularWithAPI.Pagination;

namespace AngularWithAPI.Repositories
{
    public class MockRepositoryPersone : IPersoneRepository
    {
        private readonly EfDbContext context;

        public MockRepositoryPersone( EfDbContext context )
        {
            this.context = context;
        }

        public IEnumerable<Persone> AllPersones()
        {
            return context.Persones.ToList();
        }

        public Persone AllPersoneWithContact(int id, bool includeContact = false)
        {
            var existPersone = context.Persones.FirstOrDefault(x => x.Id == id);
            if( existPersone!=null && includeContact)
            {
                context.Entry(existPersone).Collection(x => x.Contacts).Load();
            }
            return existPersone;
        }

        public void AddPersone(Persone persone)
        {
            if(persone!=null)
            {
                context.Add(persone);
                context.SaveChanges();
            }
        }

        public void DeletePersone(Persone persone)
        {
            var personeDeleted = context.Persones.Find(persone.Id);
            if (personeDeleted == null) { return; }

            context.Entry(personeDeleted).Collection(x => x.Contacts).Load();
      
            foreach (var contact in personeDeleted.Contacts)
            {
                context.Contacts.Remove(contact);

            }
            context.Persones.Remove(personeDeleted);
            context.SaveChanges();
        }

        public void UpdatePersone(int id, Persone persone)
        {
            var personeExist = context.Persones.FirstOrDefault(x=>x.Id==id);
            if (personeExist!=null)
            {
                personeExist.Name = persone.Name;
                personeExist.Phone = persone.Phone;
                context.SaveChanges();
            }
        }

        public Persone PersoneById(int id)
        {
            var findPer = context.Persones.FirstOrDefault(x => x.Id == id);

            if(findPer != null)
            {
                return findPer;
            }
            return null;

        }

        public Pagination<Persone> GetPersones(int pageNumber,int pagesize)
        {
            return Pagination<Persone>.ToPagingList(AllPersones(), pageNumber, pagesize);
        }
    }
}
