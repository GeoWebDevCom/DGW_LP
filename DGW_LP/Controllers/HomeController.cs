using DGW_LP.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace DGW_LP.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Khơi nguồn sáng tạo - Thổi bùng đam mê";
            ViewBag.Description = "Nhằm tạo sân chơi cho các bạn trẻ sinh viên yêu âm nhạc, khơi nguồn cảm hứng để các bạn có thể được sống với niềm đam mê và có cơ hội tỏa sáng trên sân chơi chung, thêm yêu đời, yêu cuộc sống thông qua những giai điệu âm nhạc, giúp các bạn sảng khoái tinh thần học tập tốt hơn, công ty TNHH HP Việt Nam phối hợp với công ty Cổ Phần Thế Giới Số (Digiworld Corporation) tổ chức cuộc thi với tên gọi: “KHƠI NGUỒN SÁNG TẠO – THỔI BÙNG ĐAM MÊ”";
            ViewBag.Banner = "http://hpkhoinguonsangtao.com.vn/Imgs/metabanner.jpg";

            return View();
        }

        public ActionResult TheLe()
        {
            ViewBag.Title = "Khơi nguồn sáng tạo - Thổi bùng đam mê";
            ViewBag.Description = "Nhằm tạo sân chơi cho các bạn trẻ sinh viên yêu âm nhạc, khơi nguồn cảm hứng để các bạn có thể được sống với niềm đam mê và có cơ hội tỏa sáng trên sân chơi chung, thêm yêu đời, yêu cuộc sống thông qua những giai điệu âm nhạc, giúp các bạn sảng khoái tinh thần học tập tốt hơn, công ty TNHH HP Việt Nam phối hợp với công ty Cổ Phần Thế Giới Số (Digiworld Corporation) tổ chức cuộc thi với tên gọi: “KHƠI NGUỒN SÁNG TẠO – THỔI BÙNG ĐAM MÊ”";
            ViewBag.Banner = "http://hpkhoinguonsangtao.com.vn/Imgs/metabanner.jpg";
            return View();
        }
        public ActionResult BinhChon(int t = 1)
        {
            ViewBag.Title = "Khơi nguồn sáng tạo - Thổi bùng đam mê";
            ViewBag.Description = "Nhằm tạo sân chơi cho các bạn trẻ sinh viên yêu âm nhạc, khơi nguồn cảm hứng để các bạn có thể được sống với niềm đam mê và có cơ hội tỏa sáng trên sân chơi chung, thêm yêu đời, yêu cuộc sống thông qua những giai điệu âm nhạc, giúp các bạn sảng khoái tinh thần học tập tốt hơn, công ty TNHH HP Việt Nam phối hợp với công ty Cổ Phần Thế Giới Số (Digiworld Corporation) tổ chức cuộc thi với tên gọi: “KHƠI NGUỒN SÁNG TẠO – THỔI BÙNG ĐAM MÊ”";
            ViewBag.Banner = "http://hpkhoinguonsangtao.com.vn/Imgs/metabanner.jpg";


            DateTime start;
            DateTime end;
            switch (t)
            {
                case 1:
                    start = new DateTime(2016, 11, 1);
                    end = new DateTime(2016, 11, 7);
                    break;
                case 2:
                    start = new DateTime(2016, 11, 7);
                    end = new DateTime(2016, 11, 14);
                    break;
                case 3:
                    start = new DateTime(2016, 11, 14);
                    end = new DateTime(2016, 11, 21);
                    break;
                default:
                    start = new DateTime(2016, 11, 21);
                    end = new DateTime(2016, 12, 1);
                    break;
            }
           

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                string userId = User.Identity.GetUserId();

                List<VideoVote> video = db.Videos.Where(v => v.createdDate >= start && v.createdDate <= end).OrderByDescending(g => g.createdDate).Select(f => new VideoVote
                {
                    Author = f.AuthorName,
                    Title = f.Title,
                    ThumbImg = f.ThumbImg,
                    Id = f.Id,
                    tmpDate = f.createdDate,
                    Vote = db.Likes.Where(b => b.Video.Id == f.Id).Count()
                }).ToList();

                ViewBag.Total = db.Videos.Where(v => v.createdDate >= start && v.createdDate <= end).Count();

                if (!String.IsNullOrEmpty(userId))
                {
                    var votedObj = db.Likes.Where(m => m.ApplicationUser.Id == userId).Select(f => f.Video.Id).ToList();
                    ViewBag.VotedVideoId = votedObj;

                }
                return View(video);
            }

        }

        [HttpPost]
        public string GetMoreComment(int vId, int page)
        {
            string json = "";
            int threshold = 5;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var comment = db.Comments.Where(m => m.Video.Id == vId).Select(c => new Commenter
                {
                    Content = c.Content,
                    Avatar = c.ApplicationUser.Avatar,
                    Name = c.ApplicationUser.UserName,
                    CreatedDate = c.createdDate
                }).OrderBy(f => f.CreatedDate).Skip((page -1)* threshold).Take(threshold).ToList();
                //int count = db.Comments.Where(m => m.Video.Id == vId).Count();

                json = JsonConvert.SerializeObject(new {
                    data = comment,
                    final = db.Comments.Where(m => m.Video.Id == vId).Count() > threshold*page ? false : true
                });

            }
            return json;
        }

        public ActionResult ClipReview(int cId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var vid = db.Videos.Where(t => t.Id == cId).Select(t => new VideoReview {
                    Id = t.Id,
                    Author = t.AuthorName,
                    Description = t.Description,
                    Title = t.Title,
                    Src = t.Src,
                    createdDate = t.createdDate,
                    Thumb = t.ThumbImg,
                    Vote = db.Likes.Where(b => b.Video.Id == t.Id).Count(),
                    FinalComment = db.Comments.Where(m => m.Video.Id == t.Id).Count() > 5 ? false : true,
                    Commenters = db.Comments.Where(m => m.Video.Id == t.Id).Select(c => new Commenter {
                        Content =  c.Content,
                        Avatar = c.ApplicationUser.Avatar,
                        Name = c.ApplicationUser.UserName,
                        CreatedDate = c.createdDate
                    }).OrderBy(f => f.CreatedDate).Skip(0).Take(5).ToList()
                }).FirstOrDefault();

                string userId = User.Identity.GetUserId();
                if (userId != null)
                {
  
                    var votedObj = db.Likes.Where(m => m.ApplicationUser.Id == userId).Select(f => f.Video.Id).ToList();
                    ViewBag.VotedVideoId = votedObj;

                }
                ViewBag.Next = vid.Id;
                ViewBag.Prev = vid.Id;

                // Get next id
                List<int> nId = db.Videos.Where(t => t.createdDate > vid.createdDate).OrderBy(v => v.createdDate).Select(m => m.Id).ToList();
                
                if (nId.Any())
                {
                    ViewBag.Next = nId[0];
                }
                // Get prev id
                List<int> pId = db.Videos.Where(t => t.createdDate < vid.createdDate).OrderByDescending(v => v.createdDate).Select(m => m.Id).ToList();
                if (pId.Any())
                {
                    ViewBag.Prev = pId[pId.Count()-1];
                }




                ViewBag.Title = vid.Title;
                ViewBag.Description = vid.Description;
                ViewBag.Banner = "http://hpkhoinguonsangtao.com.vn/ClipThumb/" + vid.Thumb;

                return View(vid);
            }
        }

        [HttpPost]
        public string VoteVideo(int videoId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                string userId = User.Identity.GetUserId();
                //bool hasVoted = db.Likes.Where(t => t.ApplicationUser.Id == userId && t.Video.Id == videoId).Any();
                //if (!hasVoted)
                //{
                    
                //}
                Like like = new Like()
                {
                    ApplicationUser = db.Users.FirstOrDefault(t => t.Id == userId),
                    Video = db.Videos.FirstOrDefault(t => t.Id == videoId),
                    createdDate = DateTime.Now
                };
                db.Likes.Add(like);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    return "Error";
                }
            }
            return "OK";
        }

        [HttpPost]
        public string CancelVote(int videoId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                string userId = User.Identity.GetUserId();
                var voteObj = db.Likes.FirstOrDefault(t => t.ApplicationUser.Id == userId && t.Video.Id == videoId);
                if (voteObj != null)
                {
                    db.Likes.Remove(voteObj);
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        return "Error";
                    }
                }
            }



            return "OK";
        }


        [HttpPost]
        public string UploadVideo(string para1)
        {
            // 2 ads video
            //string path1 = MyHelper.getVideoPath() + "Clips/test.mp4";
            //var adsvideo1 = System.IO.File.ReadAllBytes(path1);
            //string path2 = MyHelper.getVideoPath() + "Clips/test.mp4";
            //byte[] adsvideo2 = System.IO.File.ReadAllBytes(path2);

            var myFile = Request.Files[0];
            string title = Request.Params["Title"];
            string author = Request.Params["Author"];
            string description = Request.Params["Desc"];


            byte[] videoBytes = null;

            string time = DateTime.Now.ToString().Replace(':', '-').Replace("/", "-").Replace(" ", "-");
            var fullFilePath = MyHelper.getVideoPath() + "Clips\\" + time + ".mp4";

            using (var binaryReader = new BinaryReader(myFile.InputStream))
            {
                videoBytes = binaryReader.ReadBytes(myFile.ContentLength);
            }
            // Save video temporary to merge
            //var tmpFilePath = MyHelper.getVideoPath() + "Clips\\tmp.mp4";

            try
            {
                using (FileStream str = System.IO.File.OpenWrite(fullFilePath))
                {
                    str.Write(videoBytes, 0, videoBytes.Length);
                }

                // Create video thumb image
                string thumbName = time + "-thumb.jpg";
                string thumbImage = MyHelper.getVideoPath() + "ClipThumb/" + thumbName;
                var ffMpeg = new NReco.VideoConverter.FFMpegConverter();
                ffMpeg.GetVideoThumbnail(fullFilePath, thumbImage, 5);

                Video vid = new Video()
                {
                    AuthorName = author,
                    Description = description,
                    Title = title,
                    createdDate = DateTime.Now,
                    Src = time + ".mp4",
                    ThumbImg = time + "-thumb.jpg"
                };
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    db.Videos.Add(vid);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return "Error";
            }
            return "OK";
        }

        [HttpPost]
        public ActionResult DeleteVideo(int vId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var video = db.Videos.Where(t => t.Id == vId).FirstOrDefault();
                if (video != null)
                {
                    var like = db.Likes.Where(t => t.Video.Id == vId);
                    if (like.Any())
                    {
                        db.Likes.RemoveRange(like);
                    }

                    var comment = db.Comments.Where(t => t.Video.Id == vId);
                    if (comment.Any())
                    {
                        db.Comments.RemoveRange(comment);
                    }

                        db.Videos.Remove(video);
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        return Redirect("/admin");
                    }
                }

            }
            return Redirect("/admin");
        }

        public ActionResult SubmitComment(AddComment ac)
        {
            string userId = User.Identity.GetUserId();
            if (!String.IsNullOrEmpty(userId))
            {
                using (ApplicationDbContext db =new ApplicationDbContext())
                {
                    Comments comment = new Comments() {
                        Content = ac.Content,
                        Video = db.Videos.FirstOrDefault(t => t.Id == ac.VideoId),
                        ApplicationUser = db.Users.FirstOrDefault(t => t.Id == ac.UserId),
                        createdDate = DateTime.Now
                    };
                    db.Comments.Add(comment);
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        return Redirect("/watch/" + ac.VideoId);
                    }
                }
            }
            return Redirect("/watch/" + ac.VideoId); 
        }



        public ActionResult RestrictAdmin()
        {
            return View();
        }

        public ActionResult AdminManage()
        {
            string AdminEmail = "haovtit@gmail.com";

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var userId = User.Identity.GetUserId();
                var adminEmail = db.Users.FirstOrDefault(t => t.Id == userId);
                if (adminEmail != null && adminEmail.Email == AdminEmail)
                {
                    List<VideoManage> video = db.Videos.OrderByDescending(t => t.createdDate).Select(t => new VideoManage
                    {
                        Author = t.AuthorName,
                        Description = t.Description,
                        Title = t.Title,
                        CreatedDate = t.createdDate,
                        Id = t.Id
                    }).Skip(0).Take(10).ToList();
                    return View(video);
                }else
                {
                    return Redirect("/");
                }
            }
 
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}