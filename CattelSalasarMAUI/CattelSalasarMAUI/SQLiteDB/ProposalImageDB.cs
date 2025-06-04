using CattelSalasarMAUI.Models;
using SQLite;

namespace CattelSalasarMAUI.SQLiteDB
{
    public class ProposalImageDB
    {
        public ProposalImageDB()
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
                var result = await Database.CreateTableAsync<ProposalImages>();
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<List<ProposalImages>> GetAnimalImage()
        {
            var data = await Database.Table<ProposalImages>().ToArrayAsync();
            return (from u in data
                    select u).ToList();
        }

        public async Task<string> AddAnimalImage(ProposalImages ProposalImages)
        {
            await Init();
            var data = await Database.Table<ProposalImages>().ToArrayAsync();

            if (data != null)
            {
                await Database.InsertAsync(ProposalImages);

                return "Sucessfully Added";
            }
            return null;
        }

        public async Task<List<ProposalImages>> GetImageDetailsByAnimalId(int ProposalId, int AnimalId)
        {
            try
            {
                await Init();

                string animalIdString = AnimalId.ToString().Trim();

                // Fetch data from the database
                var ImageData = await Database.Table<ProposalImages>().ToArrayAsync();

                // Check if ImageData is null
                if (ImageData == null)
                {
                    throw new InvalidOperationException("Query result is null.");
                }

                var result = ImageData.Where(x => x.PropId == ProposalId && x.CompassDegrees == animalIdString).ToList();

                // Log the number of items in the result
                Console.WriteLine($"Number of items in result: {result.Count}");

                // Create a new list to hold the results
                List<ProposalImages> finalResult = new List<ProposalImages>();

                foreach (var item in result)
                {
                    ProposalImages imgProposal = new ProposalImages()
                    {
                        PropId = item.PropId,
                        CompassDegrees = item.CompassDegrees,
                        CreatedDate = item.CreatedDate,
                        CustomerMobileNo = item.CustomerMobileNo,
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


        //public async Task<List<ProposalImages>> GetImageDetailsByAnimalId(int ProposalId, int AnimalId)
        //{
        //    try
        //    {
        //        List<ProposalImages> result = new List<ProposalImages>();
        //        string animalIdString = AnimalId.ToString().Trim();
        //        var data = Database.Table<ProposalImages>();
        //        result =await data.Where(x => x.PropId == ProposalId && x.CompassDegrees == animalIdString).ToListAsync();
        //        foreach (var item in result)
        //        {
        //            ProposalImages imgProposal = new ProposalImages()
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

        public async Task<bool> UpdateImage(string Name, string ImagePath, string mobile, string createdDate)
        {
            var data = Database.Table<ProposalImages>();

            var d1 = await data.Where(x => x.ImageName == Name && x.CustomerMobileNo == mobile && x.CreatedDate == createdDate).ToListAsync();
            foreach (var item in d1)
            {
                if (item.ImageName == Name)
                {
                    item.ImgesPath = ImagePath;
                    item.CustomerMobileNo = mobile;
                    item.CreatedDate = createdDate;
                    await Database.UpdateAsync(item);
                    return true;
                }
            }

            return false;
        }

        public async Task<bool> UpdateImageDetailsByProposalId(int PropId, string CustomerMobile)
        {
            try
            {
                var data = Database.Table<ProposalImages>();
                //var d1 = data.Where(x => x.CustomerMobileNo == CustomerMobile).FirstOrDefault();
                var d1 =await data.Where(x => x.CustomerMobileNo == CustomerMobile).ToListAsync(); 
                foreach (var item in d1)
                {
                    if (item.CustomerMobileNo == CustomerMobile)
                    {
                        item.PropId = PropId;
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

        public async Task<bool> UpdateImageDetailsByCatelFlag(int Cattelflag, int PropId)
        {
            try
            {
                var data = Database.Table<ProposalImages>();
                //var d1 = data.Where(x => x.CustomerMobileNo == CustomerMobile).FirstOrDefault();
                var d1 =await data.Where(x => x.PropId == PropId && x.CompassDegrees == "").ToListAsync();
                foreach (var item in d1)
                {
                    if (item.PropId == PropId)
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
