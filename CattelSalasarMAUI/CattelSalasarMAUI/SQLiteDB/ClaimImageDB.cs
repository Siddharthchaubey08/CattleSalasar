using CattelSalasarMAUI.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CattelSalasarMAUI.SQLiteDB
{
    public class ClaimImageDB
    {
        public ClaimImageDB()
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

        public async Task Init()
        {
            try
            {
                if (Database is not null)
                    return;

                Database = new SQLiteAsyncConnection(DatabasePath, Flags);
                var result = await Database.CreateTableAsync<ClaimIntimationImageModel>();
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<List<ClaimIntimationImageModel>> GetClaimIntimationImage()
        {
            var data = await Database.Table<ClaimIntimationImageModel>().ToArrayAsync();
            return (from u in data
                    select u).ToList();
        }
        
        
        public async Task<List<ClaimIntimationImageModel>> GetUploadImageDetails(string LeadNumber, string ClaimProposalId, string TagNumber)
        {
            try
            {
                await Init();

                // Fetch data from the database
                var ImageData = await Database.Table<ClaimIntimationImageModel>().ToArrayAsync();
                //&& x.ImageId == int.Parse(ClaimProposalId)
                var result = ImageData.Where(x => x.LeadNumber == LeadNumber  && x.TagNumber== TagNumber).ToList();

                // Create a new list to hold the results
                List<ClaimIntimationImageModel> finalResult = new List<ClaimIntimationImageModel>();

                foreach (var item in result)
                {
                    ClaimIntimationImageModel imgProposal = new ClaimIntimationImageModel()
                    {
                        ClaimIntimationId = item.ClaimIntimationId,
                        CompassDegrees = item.CompassDegrees,
                        CreatedDate = item.CreatedDate,
                        ImageCapson = item.ImageCapson,
                        ImageId = item.ImageId,
                        ImageName = item.ImageName,
                        ImgesPath = item.ImgesPath,
                        Latitude = item.Latitude,
                        Longitude = item.Longitude,
                        TimeStamp = item.TimeStamp,
                        ClaimProposalId = item.ClaimProposalId,
                        LeadNumber = item.LeadNumber,
                        TagNumber = item.TagNumber,
                        TypeOfAnimal = item.TypeOfAnimal
                    };

                    finalResult.Add(imgProposal);
                }

                return finalResult;
            }
            catch (Exception ex)
            {
                // Log the exception message
                Console.WriteLine($"Error in GetImageDetailsByAnimalId: {ex.Message}");
                throw;
            }
        }


        public async Task<string> AddClaimIntimationImage(ClaimIntimationImageModel ClaimIntimationImageModel)
        {
            await Init();
            var data = await Database.Table<ClaimIntimationImageModel>().ToArrayAsync();

            if (data != null)
            {
                await Database.InsertAsync(ClaimIntimationImageModel);

                return "Sucessfully Added";
            }
            return null;
        }

        public async Task<string> DeleteImageDetailsByAnimalId(string LeadNumber, string ClaimProposalId, string TagNumber)
        {
            try
            {
                await Init();

               // string animalIdString = AnimalId.ToString().Trim();

                // Fetch data from the database
                var ImageData = await Database.Table<ClaimIntimationImageModel>().ToArrayAsync();

                // Check if ImageData is null
                if (ImageData == null)
                {
                    throw new InvalidOperationException("Query result is null.");
                }
                var result = ImageData.Where(x => x.LeadNumber == LeadNumber && x.TagNumber == TagNumber);
                //  var result = ImageData.Where(x =>x.LeadNumber==LeadNumber && x.ClaimIntimationId == ProposalId && x.CompassDegrees == animalIdString).ToList();
                //  var result = ImageData.Where(x =>x.LeadNumber==LeadNumber && x.ClaimProposalId == ClaimProposalId && x.TagNumber == TagNumber).ToList();
                if (result != null)
                {
                    var data= await Database.DeleteAsync(result.FirstOrDefault(x => x.LeadNumber == LeadNumber && x.TagNumber == TagNumber));
                    return "Delete data Successfuly";
                }
                
                return "";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetImageDetailsByAnimalId: {ex.Message}");
                throw;
            }
        }


        public async Task<List<ClaimIntimationImageModel>> GetImageDetailsByAnimalId(int ProposalId, int AnimalId)
        {
            try
            {
                await Init();

                string animalIdString = AnimalId.ToString().Trim();

                // Fetch data from the database
                var ImageData = await Database.Table<ClaimIntimationImageModel>().ToArrayAsync();

                // Check if ImageData is null
                if (ImageData == null)
                {
                    throw new InvalidOperationException("Query result is null.");
                }

                var result = ImageData.Where(x => x.ClaimIntimationId == ProposalId && x.CompassDegrees == animalIdString).ToList();

                // Log the number of items in the result
                Console.WriteLine($"Number of items in result: {result.Count}");

                // Create a new list to hold the results
                List<ClaimIntimationImageModel> finalResult = new List<ClaimIntimationImageModel>();

                foreach (var item in result)
                {
                    ClaimIntimationImageModel imgProposal = new ClaimIntimationImageModel()
                    {
                        ClaimIntimationId = item.ClaimIntimationId,
                        CompassDegrees = item.CompassDegrees,
                        CreatedDate = item.CreatedDate,
                        ImageCapson = item.ImageCapson,
                        ImageId = item.ImageId,
                        ImageName = item.ImageName,
                        ImgesPath = item.ImgesPath,
                        Latitude = item.Latitude,
                        Longitude = item.Longitude,
                        TimeStamp = item.TimeStamp
                    };

                    finalResult.Add(imgProposal);
                }

                return finalResult;
            }
            catch (Exception ex)
            {
                // Log the exception message
                Console.WriteLine($"Error in GetImageDetailsByAnimalId: {ex.Message}");
                throw;
            }
        }


        //public async Task<List<ClaimIntimationImageModel>> GetImageDetailsByAnimalId(int ProposalId, int AnimalId)
        //{
        //    try
        //    {
        //        List<ClaimIntimationImageModel> result = new List<ClaimIntimationImageModel>();
        //        string animalIdString = AnimalId.ToString().Trim();
        //        var data = Database.Table<ClaimIntimationImageModel>();
        //        result =await data.Where(x => x.PropId == ProposalId && x.CompassDegrees == animalIdString).ToListAsync();
        //        foreach (var item in result)
        //        {
        //            ClaimIntimationImageModel imgProposal = new ClaimIntimationImageModel()
        //            {
        //                PropId = item.PropId,
        //                CompassDegrees = item.CompassDegrees,
        //                CreatedDate = item.CreatedDate,
        //                CustomerMobileNo = item.CustomerMobileNo,
        //                ImageCapson = item.ImageCapson,
        //                ImageId = item.ImageId,
        //                ImageName = item.ImageName,
        //                ImgesPath = item.ImgesPath,
        //                Latitude = item.Latitude,
        //                Longitude = item.Longitude,
        //                TimeStamp = item.TimeStamp
        //            };

        //            result.Add(imgProposal);
        //        }
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }

        //}

        public async Task<bool> UpdateClaimImage(string Name, string ImagePath, string mobile, string createdDate)
        {
            var data = Database.Table<ClaimIntimationImageModel>();

            var d1 = await data.Where(x => x.ImageName == Name && x.CreatedDate == createdDate).ToListAsync();
            foreach (var item in d1)
            {
                if (item.ImageName == Name)
                {
                    item.ImgesPath = ImagePath;
                    item.CreatedDate = createdDate;
                    await Database.UpdateAsync(item);
                    return true;
                }
            }

            return false;
        }

        public async Task<bool> UpdateImageDetailsByProposalId(int ClaimPropId, string TagNo)
        {
            try
            {
                var data = Database.Table<ClaimIntimationImageModel>();
                //var d1 = data.Where(x => x.CustomerMobileNo == CustomerMobile).FirstOrDefault();
                var d1 = await data.Where(x => x.TagNumber == TagNo).ToListAsync();
                foreach (var item in d1)
                {
                    if (item.TagNumber == TagNo)
                    {
                        item.ClaimIntimationId = ClaimPropId;
                        Database.UpdateAsync(item);

                    }
                }
                return true;
            }
            catch (System.Exception ex)
            {
                ex.Message.ToString();
                throw;
            }
        }

        public async Task<bool> UpdateImageDetailsByCatelFlag(int Cattelflag, int ClaimPropId)
        {
            try
            {
                var data = Database.Table<ClaimIntimationImageModel>();
                //var d1 = data.Where(x => x.CustomerMobileNo == CustomerMobile).FirstOrDefault();
                var d1 = await data.Where(x => x.ClaimIntimationId == ClaimPropId && x.CompassDegrees == "").ToListAsync();
                foreach (var item in d1)
                {
                    if (item.ClaimIntimationId == ClaimPropId)
                    {
                        item.CompassDegrees = Cattelflag.ToString();
                        Database.UpdateAsync(item);

                    }
                }
                return true;
            }
            catch (System.Exception ex)
            {
                ex.Message.ToString();
                throw;
            }
            return false;
        }

    }
}
