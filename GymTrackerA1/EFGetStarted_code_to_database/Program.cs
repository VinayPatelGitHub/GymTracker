using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace GymTracker
{
    class Program
    {
        static void Main()
        {
            using (var db = new GymTrackerContext())
            {
                ////Create
                //Console.WriteLine("Inserting a new blog");
                //var selectuser = db.Users.Where(u => u.UserId == 3).FirstOrDefault();
                //var newUser = new User()
                //{
                //    UserName = "James",
                //    UserAddress = "London"
                //};

                //var selectBlog = db.Blogs.Where(b => b.BlogId == 2).FirstOrDefault();
                //var newBlog = new Blog()
                //{
                //    BlogTitle = "Music",
                //    BlogDescription = "Blog about Music"
                //};
                //var selectPost = db.Posts.Where(p => p.PostId == 4).FirstOrDefault();
                //var newPost = new Post()
                //{
                //    PostTitle = "Fav instrument",
                //    CreationDate = "27.08.20",
                //    Content = "My favourite instrument is the triangle",
                //    Blog = selectBlog
                //};
                //var newWebsite = new Website()
                //{
                //    WebsiteAddress = "Facebook",
                //    Post = selectPost
                //};
                //db.Users.Add(newUser);
                //db.Blogs.Add(newBlog);
                //db.Posts.Add(newPost);
                //selectPost.User = selectuser;
                //db.Websites.Add(newWebsite);
                //db.SaveChanges();

                //Edit
                //var blog = db.Blogs.Where(b => b.BlogId == 1).FirstOrDefault();
                ////Console.WriteLine(blog.BlogTitle);
                //blog.BlogDescription = "Blog About The Weather";
                //db.SaveChanges();

                //adding extra requires selecting - new post, same user, same blog
                //var selectUser = db.Users.Where(u=>u.UserName == "Vinay Patel").FirstOrDefault();
                //var selectBlog = db.Blogs.Where(b => b.BlogTitle == "Weather").FirstOrDefault();
                //var newPost = new Post()
                //{
                //    PostTitle = "Yesterdays Weather",
                //    CreationDate = "27.08.20",
                //    Content = "Rainy and grey",
                //    Blog = selectBlog
                //};
                //var newUserBlog = new UserBlog()
                //{
                //    User = selectUser,
                //    Post = newPost
                //};
                //db.Posts.Add(newPost);
                //db.UserPosts.Add(newUserPost);
                //db.SaveChanges();

                //used for connecting stuff after correction database
                //////var selectUser = db.Users.Where(u=>u.UserId == 1).FirstOrDefault();
                //////var selectPost = db.Posts.Where(p => p.PostId == 1).FirstOrDefault();
                //////var newUserPost = new UserPost()
                //////{
                //////    User = selectUser,
                //////    Post = selectPost,
                //////};
                //////db.UserPosts.Add(newUserPost);
                //////db.SaveChanges();










                //// Create
                //Console.WriteLine("Inserting a new blog");
                ////db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
                //db.SaveChanges();


                //// Read
                //Console.WriteLine("Querying for a blog");
                //var blog = db.Blogs
                //    .OrderBy(b => b.BlogId)
                //    .First();


                //// Update
                //Console.WriteLine("Updating the blog and adding a post");
                //blog.Url = "https://devblogs.microsoft.com/dotnet";
                //blog.Posts.Add(
                //    new Post
                //    {
                //        Title = "Hello World",
                //        Content = "I wrote an app using EF Core!"
                //    });
                //db.SaveChanges();


                //// Delete
                //Console.WriteLine("Delete the blog");
                //db.Remove(blog);
                //db.SaveChanges();
            }
        }
    }
}