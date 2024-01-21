using Classes;
using ServerApi.Database;

namespace ServerApi.Functions
{
    public class GeneralFunctions
    {
        public static int GenerateRandomCode()
        {
            Random random = new Random();
            int code = random.Next(100000, 1000000);
            return code;
        }

        public static byte[] ConvertImageToByteArray(IFormFile imageFile)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                imageFile.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public static string UpdateGuid(DataContext db, int id)
        {
            // Update or insert into Session table
            var existingProcess = db.Session
                .Where(p => p.Id_User == id)
                .FirstOrDefault();

            Guid myGuid = Guid.NewGuid();
            string guid = myGuid.ToString();

            if (existingProcess != null)
            {
                // Update existing record in Session table
                existingProcess.Guid = guid;
                existingProcess.Last_Update = DateTime.Now;
            }
            else
            {
                // Insert data into Session table
                db.Session.Add(new Session
                {
                    Guid = guid,
                    Id_User = id,
                    Last_Update = DateTime.Now
                });
            }

            db.SaveChanges();
            return guid;
        }
    }
}
