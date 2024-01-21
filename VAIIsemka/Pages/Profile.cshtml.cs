using Classes;
using ClientApp.Functions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata.Ecma335;

namespace ClientApp.Pages
{
    [BindProperties]
    public class ProfileModel : PageModel
    {
        public string? Name { get; set; }
        public string? Username { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Bio { get; set; }
        public IFormFile? ProfilePicture { get; set; }
        public bool ShowPopUp { get; set; } = false;
        public string Guid { get; set; }

        public async Task<IActionResult?> OnGet(string guid, bool? firstTime)
        {
            if (!String.IsNullOrEmpty(guid) && await Calls.CheckGuid(guid))
            {
                Guid = guid;
                var user = await Calls.GetUser(guid);
                if (user != null)
                {
                    Name = user.Name;
                    Username = user.Username;
                    Bio = user.Bio;
                    DateOfBirth = user.Date;
                    if (user.Picture != null && user.Picture.Length > 0)
                    {
                        ProfilePicture = ConvertToFormFile(user.Picture);
                    }
                }
                if (firstTime != null && (bool)firstTime)
                {
                    ShowPopUp = true;
                }
                else
                {
                    ShowPopUp = false;
                }
                TempData["Guid"] = Guid;
                return null;
            }
            else
            {
                return RedirectToPage("/SignIn");
            }
        }
        private IFormFile ConvertToFormFile(byte[] data)
        {
            // Create a MemoryStream from the byte array
            using (var stream = new MemoryStream(data))
            {
                // Create an IFormFile from the MemoryStream
                return new FormFile(stream, 0, data.Length, "ProfilePicture", "ProfilePicture.jpg");
            }
        }

        public async Task<IActionResult?> OnPostSubmit()
        {
            if (Guid == null)
            {
                Guid = TempData["Guid"].ToString();
            }
            bool proper = true;
            if (!String.IsNullOrEmpty(Name) && !String.IsNullOrEmpty(Username) && DateOfBirth.HasValue)
            {
                string? valid = InputHandler.IsUsernameValid(Username, Guid);
                if (!String.IsNullOrEmpty(valid))
                {
                    TempData["MessageUsername"] = valid;
                    proper = false;
                }

                valid = InputHandler.IsNameValid(Name);
                if (!String.IsNullOrEmpty(valid))
                {
                    TempData["MessageName"] = valid;
                    proper = false;
                }
            }
            else
            {
                proper = false;
            }

            if (proper)
            {
                byte[]? data = null;
                if (ProfilePicture != null && ProfilePicture.Length > 0)
                {
                    // Read the file content into a byte array
                    using (var stream = new MemoryStream())
                    {
                        await ProfilePicture.CopyToAsync(stream);
                        data = stream.ToArray();
                    }
                }
                if (Bio == null)
                {
                    Bio = string.Empty;
                }

                using (var formData = new MultipartFormDataContent())
                {
                    formData.Add(new StringContent(Guid!), "guid");
                    formData.Add(new StringContent(Name!), "name");
                    formData.Add(new StringContent(Username!), "username");
                    formData.Add(new StringContent(DateOfBirth.ToString()!), "date");
                    formData.Add(new StringContent(Bio!), "bio");
                    if (data != null)
                    {
                        formData.Add(new ByteArrayContent(data), "image", "image.jpg");
                    }
                    await Calls.UpdateProfile(formData);
                    return RedirectToPage("/User", new { guid = Guid });
                }
                
            }
            TempData["Guid"] = Guid;
            return null;
        }

        public async Task<IActionResult?> UsernameChangedAsync()
        {
            if (!string.IsNullOrEmpty(Username))
            {
                var result = await Calls.IsUsernameFree(Username, Guid);
                if (!result)
                {
                    TempData["MessageUsername"] = "Username is not available.";
                    
                }
            }
            TempData["Guid"] = Guid;
            return null;
        }
    }
}
