using CattelSalasarMAUI.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CattelSalasarMAUI.SQLiteDB
{
    public class AnimalDetailDB
    {
        public AnimalDetailDB()
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
                var result = await Database.CreateTableAsync<AnimalDataModel>();
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public async Task<List<AnimalDataModel>> GetAnimalProposal()
        {
            var data =await Database.Table<AnimalDataModel>().ToArrayAsync();
            return (from u in data
                    select u).ToList();
        }
        public async Task<string> AddAnimalDetails(AnimalDataModel animalDataModel)
        {
            await Init();
            var data =await Database.Table<AnimalDataModel>().ToArrayAsync();
            
            if (data != null)
            {
                await Database.InsertAsync(animalDataModel);

                return "Sucessfully Added";
            }
            return null;
        }


        public async Task<List<AnimalDataModel>> GetAnimalProposalByPropId(string LeadId)
        {
            var data =await Database.Table<AnimalDataModel>().ToListAsync();
            var d1 = (from values in data
                      where values.LeadNumber == LeadId
                      select values).ToList();
            return d1;
        }
        public async Task<List<AnimalDataModel>> GetAnimalProposalByPropIdandSpacies(string LeadId, string Species)
        {
            var data =await Database.Table<AnimalDataModel>().ToListAsync();
            var d1 = (from values in data
                      where values.LeadNumber == LeadId &&
                      values.Species==Species
                      select values).ToList();
            return d1;
        }
        public async Task<bool> updateAnimalProposerDetails(AnimalDataModel animalData)
        {
            var data = await Database.Table<AnimalDataModel>().ToListAsync();
            
            if (data != null)
            {
                await Database.UpdateAsync(animalData);
                return true;
            }
            
             return false;
        }

        public async Task<bool> updateAnimalProposalId(AnimalDataModel animalData)
        {
            var data = await Database.Table<AnimalDataModel>().ToListAsync();
            var d1 = data.Where(values => values.Propid == animalData.Propid).ToList();
            if (d1.Any())
            {
                // Update each matching record
                foreach (var item in d1)
                {
                    item.Propid = animalData.Propid;
                    await Database.UpdateAsync(item);
                }
                return true;
            }
            else
            {
                return false;

            }
        }
        public async Task<bool> updateAnimalProposalSpecies(int Propid, string Species)
        {
            try
            {
                var data =await Database.Table<AnimalDataModel>().ToListAsync();
                var d1 = data.Where(values => values.Propid == Propid && values.Species == "").ToList();
               
                if (d1.Any())
                {
                    foreach (var item in d1)
                    {
                        item.Species = Species;
                        await Database.UpdateAsync(item);
                    }
                    return true;
                   
                }
                else
                    return false;
            }
            catch (Exception ex)
            {

                throw;
            }

        }




    }
}
