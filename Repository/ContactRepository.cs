using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ContactRepository : RepositoryBase<PhoneBook>, IContactRepository
    {
        public ContactRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
        public void AddContact(PhoneBook contact)
        {
            contact.Id = contact.Id;
            Create(contact);
            Save();
        }

        public void DeleteContact(PhoneBook contact)
        {
            Delete(contact);
            Save();
        }

        public async Task<IEnumerable<PhoneBook>> GetAllContactsAsync()
        {
            return await Task.Run(() => FindAll());
        }

        public async Task<PhoneBook> GetContactByIdAsync(int contactId)
        {
            return await Task.Run(() => FindByCondition(contact => contact.Id.Equals(contactId))
            .DefaultIfEmpty(new PhoneBook())
            .FirstOrDefault());
        }

        public void UpdateContact(PhoneBook contact)
        {
           
            Update(contact);
            Save();
        }
    }
}