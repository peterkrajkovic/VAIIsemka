using ClientApp.Functions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics.Metrics;

namespace ClientApp.Pages
{
    public class ProfileModel : PageModel
    {
        [BindProperty]
        public string? Name { get; set; }
        [BindProperty]
        public string? Username { get; set; }
        [BindProperty]
        public DateTime? Date { get; set; }
        [BindProperty]
        public string? Country { get; set; }

        [BindProperty]
        public IFormFile? ProfilePicture { get; set; }
        public bool ShowPopUp { get; set; } = false;
        public List<string> Countries { get; set; } = new List<string>
        {
            "Andorra", "United Arab Emirates", "Afghanistan", "Antigua and Barbuda", "Albania", "Armenia", "Angola", "Argentina", "Austria", "Australia", "Azerbaijan", "Bosnia and Herzegovina",
            "Barbados", "Bangladesh", "Belgium", "Burkina Faso", "Bulgaria", "Bahrain", "Burundi", "Benin", "Brunei Darussalam", "Bolivia (Plurinational State of)", "Brazil", "Bahamas", "Bhutan", "Botswana",
            "Belarus", "Belize", "Canada", "Democratic Republic of the Congo", "Central African Republic", "Congo", "Switzerland", "Côte d'Ivoire", "Chile", "Cameroon", "China", "Colombia", "Costa Rica",
            "Cuba", "Cape Verde", "Cyprus", "Czech Republic", "Germany", "Djibouti", "Denmark", "Dominica", "Dominican Republic", "Algeria", "Ecuador", "Estonia", "Egypt", "Eritrea", "Spain", "Ethiopia",
            "Finland", "Fiji", "Micronesia (Federated States of)", "France", "Gabon", "United Kingdom of Great Britain and Northern Ireland", "Grenada", "Georgia", "Ghana", "Gambia", "Guinea", "Equatorial Guinea",
            "Greece", "Guatemala", "Guinea-Bissau", "Guyana", "Honduras", "Croatia", "Haiti", "Hungary", "Indonesia", "Ireland", "Israel", "India", "Iraq", "Iran (Islamic Republic of)", "Iceland", "Italy",
            "Jamaica", "Jordan", "Japan", "Kenya", "Kyrgyzstan", "Cambodia", "Kiribati", "Comoros", "Saint Kitts and Nevis", "Democratic People's Republic of Korea", "Republic of Korea", "Kuwait", "Kazakhstan",
            "Lao People's Democratic Republic", "Lebanon", "Saint Lucia", "Liechtenstein", "Sri Lanka", "Liberia", "Lesotho", "Lithuania", "Luxembourg", "Latvia", "Libyan Arab Jamahiriya", "Morocco", "Monaco",
            "Republic of Moldova", "Montenegro", "Madagascar", "Marshall Islands", "The former Yugoslav Republic of Macedonia", "Mali", "Myanmar", "Mongolia", "Mauritania", "Malta", "Mauritius", "Maldives",
            "Malawi", "Mexico", "Malaysia", "Mozambique", "Namibia", "Niger", "Nigeria", "Nicaragua", "Netherlands", "Norway", "Nepal", "Nauru", "New Zealand", "Oman", "Panama", "Peru", "Papua New Guinea",
            "Philippines", "Pakistan", "Poland", "Portugal", "Palau", "Paraguay", "Qatar", "Romania", "Serbia", "Russian Federation", "Rwanda", "Saudi Arabia", "Solomon Islands", "Seychelles", "Sudan", "Sweden",
            "Singapore", "Slovenia", "Slovakia", "Sierra Leone", "San Marino", "Senegal", "Somalia", "Suriname", "South Sudan", "Sao Tome and Principe", "El Salvador", "Syrian Arab Republic", "Swaziland", "Chad",
            "Togo", "Thailand", "Tajikistan", "Timor-Leste", "Turkmenistan", "Tunisia", "Tonga", "Turkey", "Trinidad and Tobago", "Tuvalu", "United Republic of Tanzania", "Ukraine", "Uganda", "United States of America",
            "Uruguay", "Uzbekistan", "Saint Vincent and the Grenadines", "Venezuela (Bolivarian Republic of)", "Viet Nam", "Vanuatu", "Samoa", "Yemen", "South Africa", "Zambia", "Zimbabwe"
        };
        private string guid;
        public async Task<IActionResult> OnGet(string guid, bool firstTime)
        {
            if (!String.IsNullOrEmpty(guid) &&await Calls.CheckGuid(guid))
            {
                this.guid = guid;
                if (firstTime)
                {
                    this.ShowPopUp = true;
                } else 
                { 
                    this.ShowPopUp = false; 
                }
                return Page();
            }
            else
            {
                return RedirectToPage("/Error", new { message = "You need to login to access this site." });
            }
        }

        public async Task<IActionResult?> OnPostSubmit()
        {
            bool proper = true;
            if (!String.IsNullOrEmpty(Name) && !String.IsNullOrEmpty(Username) && Date.HasValue)
            {
                string? valid = InputHandler.IsUsernameValid(Username);
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
            } else
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
                if (await Calls.UpdateProfile(guid, Name!, Username!, (DateTime)Date!, Country!, data))
                {
                    return RedirectToPage("/Home");
                }
            }
            return null;
        }
    }
}
