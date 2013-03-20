using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RazorEngine;
using System.Web.Mvc;
using ChronoSpark.Data.Entities;
using ChronoSpark.Logic;
using RazorEngine.Templating;
using ChronoSpark.Data;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;



namespace ChronoSpark.Service
{
    class EventController : ApiController
    {
        public static List<EventModel> listOfEvents = new List<EventModel>();
        
        [System.Web.Http.HttpGet]
        public List<EventModel> CheckEventList()
        {
            if (listOfEvents.Count > 0) {
                var savedList = listOfEvents;
                listOfEvents.Clear();
                return savedList;
            }
            else{
                return listOfEvents;
            }
        }

        public static void RegisterEvent(EventModel eventToRegister) 
        {
            listOfEvents.Add(eventToRegister);
        }

    }
}
