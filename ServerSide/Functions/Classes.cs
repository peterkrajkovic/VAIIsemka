

namespace ServerApi.Functions
{
    public class Classes
    {
        public class User
        {
            public string Image { get; set; }
            public string Username { get; set; }

            public User(string image, string username)
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