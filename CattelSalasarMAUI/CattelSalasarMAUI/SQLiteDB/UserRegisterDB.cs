using CattelSalasarMAUI.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CattelSalasarMAUI.SQLiteDB
{
    public class UserRegisterDB
    {
        public UserRegisterDB()
        {
            Init();
        }

        public const string DatabaseFilename = "Cattel.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;



        public static string DatabasePath =>
           Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
        SQLiteAsyncConnection Database;


        async Task Init()
        {
            try
            {
                if (Database is not null)
                    return;

                Database = new SQLiteAsyncConnection(DatabasePath, Flags);
                var result = await Database.CreateTableAsync<UserRegisterModel>();
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<List<UserRegisterModel>> GetUserRegisterDetails()
        {
            var data = await Database.Table<UserRegisterModel>().ToArrayAsync();
            return (from u in data
                    select u).ToList();
        }

        public async Task<string> AddUserRegisterDetails(UserRegisterModel userRegisterModel)
        {
            await Init();
            var data = await Database.Table<UserRegisterModel>().ToArrayAsync();

            if (data != null)
            {
                await Database.InsertAsync(userRegisterModel);

                return "Sucessfully Add User";
            }
            return null;
        }

    }
}
