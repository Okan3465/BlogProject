using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    public class CommentController : Controller
    {
        CommentManager cm = new CommentManager(new EfCommentRepository());
        public IActionResult Index()
        {
            return View();
        }

       
       
        public IActionResult AddComment(Comment p)
        {
            p.CommentDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            p.ComentStatus = true;
            p.BlogID = 3;
            cm.CommentAdd(p);
            return Redirect($"/Blog/BlogReadAll/{p.BlogID}");
        }

        public PartialViewResult CommentListByBlog(int id)
        {
            var values = cm.GetList(id);
           return PartialView(values);
        }

    }
}
