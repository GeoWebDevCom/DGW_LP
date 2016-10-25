using DGW_LP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DGW_LP.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TheLe()
        {
            return View();
        }
        public ActionResult BinhChon()
        {
            return View();
        }

        public ActionResult ClipReview(int cId)
        {
            //// Test get video thumb image
            //string videoPath = MyHelper.getVideoPath() + "Clips/test.mp4";
            ////string thumbName = DateTime.Now.ToString().Replace(':', '-').Replace("/", "-").Replace(" ", "-") + "-thumb.jpg";
            //string thumbName = "test-thumb.jpg";
            //string thumbImage = MyHelper.getVideoPath() + "ClipThumb/" + thumbName;

            //var ffMpeg = new NReco.VideoConverter.FFMpegConverter();

            //ffMpeg.GetVideoThumbnail(videoPath, thumbImage,5);


            return View();
        }









        public ActionResult AdminLogin()
        {
            return View();
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