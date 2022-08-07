using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Models;

namespace TestProject.Services
{
    public class AppService : IAppService
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public AppService(IDbContextFactory<AppDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public Contact GetContactByEmail(string eMail)
        {
            Contact contact = new Contact();
            try
            {
                using (var context = _contextFactory.CreateDbContext())
                {
                    contact = context.Contacts.FirstOrDefault(c => c.Email == eMail);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return contact;
        }
        public bool CheckIfAccountExists(string accountName)
        {
            bool result = false;
            try
            {
                using (var context = _contextFactory.CreateDbContext())
                {
                    var account = context.Accounts.FirstOrDefault(a => a.AccountName == accountName);
                    if (account != null)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
        public List<Account> GetAccountsList(List<Account> accounts)
        {
            List<Account> _accounts = new List<Account>();
            try
            {
                using (var context = _contextFactory.CreateDbContext())
                {
                    _accounts = context.Set<Account>().AsEnumerable().Where(a => accounts.Any(an => an.AccountName == a.AccountName)).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return _accounts;
        }
        public List<Contact> GetContactList(List<Contact> contacts)
        {
            List<Contact> _contacts = new List<Contact>();
            try
            {
                if (contacts.Any(c => c != null))
                {
                    using (var context = _contextFactory.CreateDbContext())
                    {
                        _contacts = context.Set<Contact>().AsEnumerable().Where(c => contacts.Any(cs => cs.Email == c.Email)).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return _contacts;
        }

        public ResponseModel SaveContact(Contact contactModel)
        {
            ResponseModel model = new ResponseModel();
            Contact _temp = GetContactByEmail(contactModel.Email);
            try
            {
                using (var context = _contextFactory.CreateDbContext())
                {
                    if (_temp != null)
                    {
                        _temp.Email = contactModel.Email;
                        _temp.FirstNme = contactModel.FirstNme;
                        _temp.LastName = contactModel.LastName;
                        context.Update<Contact>(_temp);
                        model.Messsage = "Contact updateted";
                    }
                    else
                    {
                        context.Add<Contact>(contactModel);
                        model.Messsage = "New Contact created";
                    }
                    context.SaveChanges();
                    model.IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Messsage = "Error : " + ex.Message;
            }
            return model;
        }
        public ResponseModel SaveAccount(Account accountModel)
        {
            ResponseModel model = new ResponseModel();
            List<Contact> foundContacts = new List<Contact>();
            try
            {
                using (var context = _contextFactory.CreateDbContext())
                {
                    foundContacts = GetContactList(accountModel.Contacts.ToList());

                    if (foundContacts.Count > 0)
                    {
                        foreach (var foundContact in foundContacts)
                        {
                            context.Update(accountModel);
                        }

                        model.Messsage = "Account created, Contacts updated";
                    }
                    else if (CheckIfAccountExists(accountModel.AccountName))
                    {
                        foreach (var contact in accountModel.Contacts)
                        {
                            context.Add(contact);
                        }

                        context.Update(accountModel);
                        model.Messsage = "Account updated, new contact was linked";
                        model.IsSuccess = true;
                    }
                    else
                    {
                        context.Add(accountModel);
                        model.Messsage = "Account created";
                    }
                    context.SaveChanges();
                    model.IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Messsage = "Error : " + ex.Message;
            }
            return model;
        }

        public ResponseModel SaveIncedent(Incedent incedentModel)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                using (var context = _contextFactory.CreateDbContext())
                {
                    foreach (var account in incedentModel.Accounts)
                    {
                        if (CheckIfAccountExists(account.AccountName))
                        {
                            foreach (var contact in account.Contacts)
                            {
                                context.Update(contact);
                            }
                            context.Update(account);
                        }
                        else
                        {
                            model.Messsage = "Incedent was not created";
                            model.IsSuccess = false;
                            return model;
                        }
                    }
                    context.Add(incedentModel);
                    model.Messsage = "Incedent created";
                    context.SaveChanges();
                    model.IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Messsage = "Error : " + ex.Message;
            }
            return model;
        }
    }
}
