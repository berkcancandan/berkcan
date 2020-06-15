using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Proje5.Models;


namespace Proje5.Helpers
{
    public class Ortak
    {
        KitapDB db = new KitapDB();

        public static bool Editizni(int id,Misafir user)
        {

            if(user.Mkayitno==id)
            {
                return true;

            }
            return false;
        }

    }
}