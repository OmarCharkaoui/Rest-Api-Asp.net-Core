using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularWithAPI.Data;
using AngularWithAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AngularWithAPI.Repositories
{
    public class MockRepositoryContact : IContactRepository
    {
        private readonly EfDbContext context;

        public MockRepositoryContact(EfDbContext context)
        {
            this.context = context;
        }

        public Persone Persones(int personeId)
        {
            Persone p1 = context.Persones.FirstOrDefault(x => x.Id == personeId);
            if (p1 != null)
            {
                context.Entry(p1).Collection(x => x.Contacts).Load();
                return p1;
            }
            return null;
        }

        public Persone GetPersoneById(int personeId, int contactId)
        {
            Persone p1 = context.Persones.FirstOrDefault(x => x.Id == personeId);

            if (p1 != null)
            {
                context.Entry(p1).Collection(x => x.Contacts).Query()
                    .FirstOrDefault(x => x.PersoneId==p1.Id && x.Id==contactId);
                if (!p1.Contacts.Any())
                {
                    return null;
                }
                return p1;
            }

            return null;
        }

        public void AddContact(int PersoneId, Contact contact)
        {
            if (contact == null) { return; }

            var findPersone = context.Persones.FirstOrDefault(x => x.Id == PersoneId);
            
            if (findPersone == null) { return; }

            findPersone.Contacts = new List<Contact>();
            findPersone.Contacts.Add(contact);

            context.SaveChanges();
        }

        public void UpdateContact(int personeId, int ContactId, Contact newContact)
        {

            var findPersone = context.Persones.Find(personeId);

            var oldContact = context.Entry(findPersone).Collection(x => x.Contacts).Query().
                FirstOrDefault(x => x.PersoneId == findPersone.Id && x.Id == ContactId);

            if (oldContact == null) { return; }

            oldContact.Name = newContact.Name;
            oldContact.Company = newContact.Company;
            oldContact.Email = newContact.Email;
            oldContact.PersoneId = newContact.PersoneId;

            context.SaveChanges();



        }

        public void DeleteContact(int personeId, int contactId)
        {
            Contact findContact = context.Contacts
                .FirstOrDefault(x => x.PersoneId == personeId && x.Id == contactId);
            if (findContact==null) { return; }

            context.Contacts.Remove(findContact);
            context.SaveChanges();

        }
    }
}
