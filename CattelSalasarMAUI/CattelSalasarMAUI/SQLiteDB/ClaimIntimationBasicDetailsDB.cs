using CattelSalasarMAUI.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CattelSalasarMAUI.SQLiteDB
{
    public class ClaimIntimationBasicDetailsDB
    {

        public ClaimIntimationBasicDetailsDB()
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
                var result = await Database.CreateTableAsync<ClaimIntimationModel>();
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<List<ClaimIntimationModel>> GetClaimIntimationDetails()
        {
            var data = await Database.Table<ClaimIntimationModel>().ToArrayAsync();
            return (from u in data
                    select u).ToList();
        }

        public async Task<string> AddClaimIntimationDetails(ClaimIntimationModel animalDataModel)
        {
            await Init();
            var data = await Database.Table<ClaimIntimationModel>().ToArrayAsync();

            if (data != null)
            {
                await Database.InsertAsync(animalDataModel);

                return "Sucessfully Added";
            }
            return null;
        }

    }
}
