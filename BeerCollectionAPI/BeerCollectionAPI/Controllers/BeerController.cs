using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CommonObjects;
using System.Web.Http.Results;
using BeerCollectionAPI.Models;

namespace BeerCollectionAPI.Controllers
{
    public class BeerController : ApiController
    {

        [HttpGet]
        public IEnumerable<BeerViewModel> BeerCollection(string name = null)
        {
            return Models.BeerModel.BeerCollection(name);
        }

        [HttpGet]
        public string CreateBeer(string name, string type, int rating)
        {
            return Models.BeerModel.CreateBeer(name, type, rating);
        }

        [HttpGet]
        public string UpdateBeer(int id, string name, string type, int rating)
        {
            return Models.BeerModel.UpdateBeer(id, name, type, rating);
        }

        [HttpGet]
        public BeerViewModel ViewBeer(int id)
        {
            return Models.BeerModel.ViewBeer(id);
        }

    }
}
