using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OmdbPracticaNetCore.Models;

namespace OmdbPracticaNetCore.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index(string id)
        {
            return View();
        }
        [HttpPost]
        public IActionResult response(string id)
        {
            try
            {
                string busqueda = Request.Form["nombre"].ToString();
                Movies mov = new Movies();
                string url = "http://www.omdbapi.com/?t="+busqueda+"&plot=full&apikey=7dd00300";
                var json = new WebClient().DownloadString(url);
                mov = JsonConvert.DeserializeObject<Movies>(json);
                //mov = XmlConvert.EncodeLocalName<Movies>(json);
                //XmlSerializer xml = new XmlSerializer(typeof(Movies));
                ViewData["Info"] = mov.Plot;
                ViewData["img"] = mov.Poster;
                ViewData["Actors"] = mov.Actors;
                ViewData["titulo"] = mov.Title;
                ViewData["id"] = id;
                return View();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return View();
        }
    }
}