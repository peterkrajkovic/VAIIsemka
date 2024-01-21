using Classes;
using ClientApp.Functions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace ClientApp.Pages
{
    [BindProperties]
    public class NewPostModel : PageModel
    {
        public string Guid { get; set; }
        public string Caption { get; set; }
        public string Location { get; set; }
        public IFormFile? Picture1 { get; set; }
        public IFormFile? Picture2 { get; set; }
        public IFormFile? Picture3 { get; set; }
        public async Task<IActionResult?> OnGet(string guid)
        {
            if (!String.IsNullOrEmpty(guid) && await Calls.CheckGuid(guid))
            {
                Guid = guid;
                TempData["Guid"] = guid;
                return null;
            }
            else
            {
                return RedirectToPage("/SignIn");
            }
        }
        public async Task<IActionResult> OnPostSubmitAsync()
        {
            if (Guid == null)
            {
                Guid = TempData["Guid"]?.ToString();
            }
            Post post = new Post();
            if (Picture1 != null && Picture1.Length > 0 && !Picture1.FileName.Equals("plus.png", StringComparison.OrdinalIgnoreCase))
            {
                post.Image1 = await ConvertImageToByteArray(Picture1);
            }
            if (Picture2 != null && Picture2.Length > 0 && !Picture2.FileName.Equals("plus.png", StringComparison.OrdinalIgnoreCase))
            {
                post.Image2 = await ConvertImageToByteArray(Picture2);
            }

            if (Picture3 != null && Picture3.Length > 0 && !Picture3.FileName.Equals("plus.png", StringComparison.OrdinalIgnoreCase))
            {
                post.Image3 = await ConvertImageToByteArray(Picture3);
            }
            if (Location == null)
            {
                Location = String.Empty;
            }
            post.Location = Location;
            post.Caption = Caption;

            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(new StringContent(Guid), "guid");

                if (!string.IsNullOrEmpty(post.Caption))
                    formData.Add(new StringContent(post.Caption), "caption");

                if (!string.IsNullOrEmpty(post.Location))
                    formData.Add(new StringContent(post.Location), "location");

                // Add image files
                if (post.Image1 != null)
                    formData.Add(new ByteArrayContent(post.Image1), "image1", "image1.jpg");

                if (post.Image2 != null)
                    formData.Add(new ByteArrayContent(post.Image2), "image2", "image2.jpg");

                if (post.Image3 != null)
                    formData.Add(new ByteArrayContent(post.Image3), "image3", "image3.jpg");

                await ClientApp.Functions.Request.PostAsync<bool>("/api/User/NewPost", formData);
            }
            return RedirectToPage("/User", new { guid = Guid });
        }

        private async Task<byte[]> ConvertImageToByteArray(IFormFile imageFile)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                await imageFile.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
