using Classes;
using Microsoft.AspNetCore.Mvc;
using ServerApi.Database;
using ServerApi.Mails;
using System;
using System.Net;

namespace ServerApi.Functions
{
    public class UserFunctions
    {

        public static async Task Resend(string email)
        {
            Database.General db = Database.General.GetInstance;
            string code = GeneralFunctions.GenerateRandomCode().ToString("D6");
            try
            {
                Mails.General general = Mails.General.GetInstance;
                await general.Send(email, Templates.SignInTemplate(code), "Verification");
            }
            catch (Exception ex)
            {
            }
        }
        public static List<Post> GetPosts(string username)
        {
            using (DataContext db = new DataContext())
            {
                var posts = db.Post.Where(x => x.Username == username).OrderByDescending(x => x.Date).ToList();
                return posts;
            }
        }

        public static List<Post>? GetImages(string guid)
        {
            using (DataContext db = new DataContext())
            {
                var posts = db.Post
                    .OrderByDescending(x => x.Date)
                    .ToList();
                return posts;
            }
        }

        public static bool UpdateProfile(string guid, string name, string username, string date, string? bio, byte[]? picture)
        {
            using (DataContext db = new DataContext())
            {
                var userId = db.Session.Where(x => x.Guid == guid).FirstOrDefault()?.Id_User;
                var user = db.User.Where(x => x.Id == userId).FirstOrDefault();
                if (user != null)
                {
                    user.Username = username;
                    user.Bio = bio;
                    user.Picture = picture;
                    user.Name = name;
                    user.Date = DateTime.Parse(date);
                    db.Update(user);
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public static int NumberOfFollowing(string username)
        {
            using (DataContext db = new DataContext())
            {

                var us = db.User.ToList();
                var user = db.User.Where(x => x.Username == username).FirstOrDefault();
                if (user != null)
                {
                    int id = user.Id;
                    int followings = db.Follow.Where(x => x.Id_Follower == id).Count();
                    return followings;
                }
                else
                {
                    return -1;
                }
            }
        }

        public static string? ChangeFollow(string guid, string username)
        {
            using (var context = new DataContext())
            {
                var follower = context.Session.Where(x => x.Guid == guid).FirstOrDefault()?.Id_User;
                var following = context.User.Where(x => x.Username == username).FirstOrDefault()?.Id;
                if (context.Follow.Where(x => x.Id_Follower == follower && x.Id_Following == following).Count() > 0)
                {
                    var fol = context.Follow.Where(x => x.Id_Follower == follower && x.Id_Following == following).First();
                    context.Follow.Remove(fol);
                    context.SaveChanges();
                    return "Follow";
                }
                else
                {
                    var fol = new Follow();
                    fol.Id_Follower = (int)follower;
                    fol.Id_Following = (int)following!;
                    context.Follow.Add(fol);
                    context.SaveChanges();
                    return "Unfollow";
                }
            }
        }

        public static string GetFollowersNumber(string username)
        {
            using (DataContext db = new DataContext())
            {
                var userId = db.User.Where(x => x.Username == username).FirstOrDefault()?.Id;
                return db.Follow.Where(x => x.Id_Following == userId).Count().ToString();
            }
        }

        public static string GetFollowingsNumber(string username)
        {
            using (DataContext db = new DataContext())
            {
                var userId = db.User.Where(x => x.Username == username).FirstOrDefault()?.Id;
                return db.Follow.Where(x => x.Id_Follower == userId).Count().ToString();
            }
        }
        public static bool IsUsernameFree(string username, string guid)
        {
            using (DataContext db = new DataContext())
            {
                var userId = db.Session.Where(x => x.Guid == guid).FirstOrDefault()!.Id_User;
                var user = db.User.Where(x => x.Username == username && x.Id != userId).FirstOrDefault();
                return (user == null);
            }
        }
        public static string? DeleteUser(string guid)
        {
            using (var context = new DataContext())
            {
                var user = context.Session.Where(x => x.Guid == guid).FirstOrDefault();
                var userToDelete = context.User.FirstOrDefault(u => u.Id == user.Id);
                if (userToDelete != null)
                {
                    var follow = context.Follow.Where(x => x.Id_Follower == user.Id || x.Id_Following == user.Id);
                    foreach (var item in follow)
                    {
                        context.Follow.Remove(item);
                    }
                    var likes = context.Like.Where(x => x.Username == userToDelete.Username);
                    foreach (var item in likes)
                    {
                        context.Remove(item);
                    }
                    var posts = context.Post.Where(x => x.Username == userToDelete.Username);
                    foreach (var item in posts)
                    {
                        context.Remove(item);
                    }
                    context.Session.Remove(user);
                    context.User.Remove(userToDelete);
                    context.SaveChanges();

                    return "User deleted successfully.";
                }
                else
                {
                    return null;
                }
            }
        }
        public static string? DeletePost(string postId)
        {
            int id;
            if (int.TryParse(postId, out id))
            {
                using (var context = new DataContext())
                {
                    var postToDelete = context.Post.FirstOrDefault(u => u.Id == id);

                    if (postToDelete != null)
                    {
                        context.Post.Remove(postToDelete);
                        context.SaveChanges();

                        return "Post deleted successfully.";
                    }
                }
            }
            return null;
        }
        public static string? IsLiked(string postId, string guid)
        {
            int id;
            if (int.TryParse(postId, out id))
            {
                using (var context = new DataContext())
                {
                    var post = context.Post.FirstOrDefault(u => u.Id == id);
                    var userId = context.Session.Where(x => x.Guid == guid).FirstOrDefault()?.Id_User;
                    var username = context.User.Where(x => x.Id == userId).FirstOrDefault()?.Username;
                    if (post != null)
                    {
                        if (context.Like.Where(x => x.PostId == id && x.Username == username).Count() > 0)
                        {
                            return "Dislike";
                        }
                        else
                        {
                            return "Like";
                        }
                    }
                }
            }
            return null;
        }
        public static string? ChangeLike(string postId, string guid)
        {
            int id;
            if (int.TryParse(postId, out id))
            {
                using (var context = new DataContext())
                {
                    var post = context.Post.FirstOrDefault(u => u.Id == id);
                    var userId = context.Session.Where(x => x.Guid == guid).FirstOrDefault()?.Id_User;
                    var username = context.User.Where(x => x.Id == userId).FirstOrDefault()?.Username;
                    if (post != null)
                    {
                        if (context.Like.Where(x => x.PostId == id && x.Username == username).Count() > 0)
                        {
                            Like l = context.Like.Where(x => x.PostId == id && x.Username == username).FirstOrDefault();
                            context.Like.Remove(l);
                            post.LikeCount--;
                            context.SaveChanges();
                            return "Like";
                        }
                        else
                        {
                            Like l = new Like();
                            l.Username = username;
                            l.PostId = id;
                            context.Like.Add(l);
                            post.LikeCount++;
                            context.SaveChanges();
                            return "Dislike";
                        }
                    }
                }
            }
            return null;
        }

        public static string? GetLikes(string postId)
        {
            int id;
            if (int.TryParse(postId, out id))
            {
                using (var context = new DataContext())
                {
                    return context.Post.FirstOrDefault(u => u.Id == id)?.LikeCount.ToString();
                }
            }
            return null;
        }

        public static bool NewPost(string guid, string caption, string location, IFormFile? image1, IFormFile? image2, IFormFile? image3)
        {
            using (var context = new DataContext())
            {
                var session = context.Session.Where(x => x.Guid == guid).FirstOrDefault();
                if (session != null)
                {
                    var userId = session.Id_User;
                    var username = context.User.Where(x => x.Id == userId).FirstOrDefault()!.Username;
                    var newPost = new Post
                    {
                        Username = username,
                        Caption = caption,
                        Location = location,
                        Date = DateTime.Now
                    };

                    if (image1 != null)
                    {
                        newPost.Image1 = GeneralFunctions.ConvertImageToByteArray(image1);
                    }

                    if (image2 != null)
                    {
                        newPost.Image2 = GeneralFunctions.ConvertImageToByteArray(image2);
                    }

                    if (image3 != null)
                    {
                        newPost.Image3 = GeneralFunctions.ConvertImageToByteArray(image3);
                    }

                    // Add the new post to the database
                    context.Post.Add(newPost);
                    context.SaveChanges();
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        public static User? GetUser(string guid, string username)
        {
            using (var context = new DataContext())
            {
                var user = context.User.FirstOrDefault(u => u.Username == username);
                return user;
            }
        }
        public static bool IsMe(string guid, string username)
        {
            using (var context = new DataContext())
            {
                var user = context.User.FirstOrDefault(u => u.Username == username);
                var userId = context.Session.Where(x => x.Guid == guid).FirstOrDefault()?.Id_User;
                return (user.Id == userId);
            }
        }
        public static bool IsFollowing(string guid, string username)
        {
            using (var context = new DataContext())
            {
                var following = context.User.FirstOrDefault(u => u.Username == username)?.Id;
                var userId = context.Session.Where(x => x.Guid == guid).FirstOrDefault()?.Id_User;
                return (context.Follow.Where(x => x.Id_Following == following && x.Id_Follower == userId).Count() > 0);
            }
        }
        public static User? GetUser(string guid)
        {
            using (var context = new DataContext())
            {
                var userProcess = context.Session.FirstOrDefault(u => u.Guid == guid);
                if (userProcess != null)
                {
                    var user = context.User.FirstOrDefault(u => u.Id == userProcess.Id_User);
                    return user;
                }

            }
            return null;
        }
    }
}
