using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
    public class AccountService
    {
        private readonly AccountsRepository _repo;
        public AccountService(AccountsRepository repo)
        {
            _repo = repo;
        }

        internal string GetProfileEmailById(string id)
        {
            return _repo.GetById(id).Email;
        }
        internal Account GetProfileByEmail(string email)
        {
            return _repo.GetByEmail(email);
        }
        internal List<Profile> GetProfiles()
        {
            return _repo.GetProfiles();
        }
        internal Profile GetProfile(string id)
        {
            return _repo.GetProfile(id);
        }
        internal Account GetOrCreateProfile(Account userInfo)
        {
            Account profile = _repo.GetById(userInfo.Id);
            if (profile == null)
            {
                return _repo.Create(userInfo);
            }
            return profile;
        }

        internal Account Edit(Account editData, string userEmail)
        {
            Account original = GetProfileByEmail(userEmail);
            original.Name = editData.Name.Length > 0 ? editData.Name : original.Name;
            original.Picture = editData.Picture.Length > 0 ? editData.Picture : original.Picture;
            return _repo.Edit(original);
        }
        internal Profile Update(Profile updatedAccount)
        {
        Profile original = GetProfile(updatedAccount.Id);
        original.Name = updatedAccount.Name ?? original.Name;
        original.Picture = updatedAccount.Picture ?? original.Picture;
        _repo.Update(original);
        return original;
        }
    }
}