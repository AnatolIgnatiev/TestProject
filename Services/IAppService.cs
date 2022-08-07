using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Models;

namespace TestProject.Services
{
    public interface IAppService
    {
        /// <summary>
        ///  add edit Incedent
        /// </summary>
        /// <param name="incedentModel"></param>
        /// <returns></returns>
        ResponseModel SaveIncedent(Incedent incedentModel);

        /// <summary>
        /// get list of all Accounts
        /// </summary>
        /// <returns></returns>
        List<Account> GetAccountsList(List<Account> accounts);


        /// <summary>
        ///  add edit Account
        /// </summary>
        /// <param name="accountModel"></param>
        /// <returns></returns>
        ResponseModel SaveAccount(Account accountModel);

        /// <summary>
        /// get Contact with specific Email
        /// </summary>
        /// <returns></returns>
        Contact GetContactByEmail(string eMail);

        /// <summary>
        /// get Contact with specific Email
        /// </summary>
        /// <returns></returns>
        List<Contact> GetContactList(List<Contact> contacts);


        /// <summary>
        ///  add edit Contact
        /// </summary>
        /// <param name="contactModel"></param>
        /// <returns></returns>
        ResponseModel SaveContact(Contact contactModel);


    }
}
