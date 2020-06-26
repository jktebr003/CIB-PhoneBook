using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IContactRepository : IRepositoryBase<PhoneBook>
    {
        Task<IEnumerable<PhoneBook>> GetAllContactsAsync();
        Task<PhoneBook> GetContactByIdAsync(int contactId);
        void AddContact(PhoneBook contact);
        void UpdateContact(PhoneBook contact);
        void DeleteContact(PhoneBook contact);
  
    }
}
