using static System.Net.Mime.MediaTypeNames;

namespace ClientApp.Functions
{
    public class Classes
    {
        public class User
        {
            public byte[] Image { get; set; }
            public string Username { get; set; }

            public User(byte[] image, string username)
            { 
                Image = image;
                Username = username;
            }
        }
        public class Post
        {
            public string Caption { get; set; }
            public string Image { get; set; }
            public string Username { get; set; }

            public Post(string caption, string image, string username)
            {
                Image = image;
                Username = username;
                Caption = caption;
            }
        }
    }
}
