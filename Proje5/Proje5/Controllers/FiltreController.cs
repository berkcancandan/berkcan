using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proje5.Controllers
{
    public class FiltreController : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if(Session["username"]==null && Session["adminname"] == null) // bilinmeyen kimse home harici dolaaşmasın
            {

                filterContext.Result = new HttpNotFoundResult();
            }
            base.OnActionExecuted(filterContext);
        }


    }
}