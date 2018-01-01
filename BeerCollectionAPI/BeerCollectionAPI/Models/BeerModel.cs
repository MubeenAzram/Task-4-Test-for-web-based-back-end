
using CommonObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace BeerCollectionAPI.Models
{
    public class BeerModel
    {

        public static List<BeerViewModel> BeerCollection(string name = null)
        {
            try
            {
                using (var dbContext = new BeerCollectionEntities())
                {
                    var result = dbContext.Beers;

                    if (!string.IsNullOrEmpty(name))
                        return result.Where(beer => beer.Name.Contains(name)).Select(b =>
                            new BeerViewModel
                            {
                                Id = b.Id,
                                Name = b.Name,
                                BeerTypes = b.BeerType.Name,
                                Ratings = b.Ratings
                            }
                            ).ToList<BeerViewModel>();
                    else
                        return result.Select(b =>
                            new BeerViewModel
                            {
                                Id = b.Id,
                                Name = b.Name,
                                BeerTypes = b.BeerType.Name,
                                Ratings = b.Ratings
                            }
                            ).ToList();
                }
            }
            catch (Exception ex)
            {

                return new List<BeerViewModel>();
            }
        }
        public static string CreateBeer(string name, string type, int rating = 0)
        {
            #region Validation

            if (string.IsNullOrEmpty(name))
                return "Beer Name can't be empty";

            if (string.IsNullOrEmpty(type))
                return "Beer Type can't be empty";

            if (rating > 0)
                if (rating > 5 || rating < 0)
                    return "Rating must be between 1-5";

            #endregion

            try
            {

                using (var dbContext = new BeerCollectionEntities())
                {
                    var beer = new Beer();

                    beer.Name = name;
                    if (dbContext.BeerTypes.Any(b => b.Name.ToLower().Contains(type.ToLower())))
                        beer.BeerType = dbContext.BeerTypes.FirstOrDefault(b => b.Name.ToLower().Equals(type.ToLower()));
                    else
                        return "Invalid Beer Type";

                    if (rating > 0)
                        beer.Ratings = rating;

                    dbContext.Beers.Add(beer);
                    return (dbContext.SaveChanges() == 1 ? "Record added successfully" : "Server side error occurred while adding the record");
                }
            }
            catch (Exception ex)
            {

                return "Exception occurred while adding the record";
            }
        }
        public static string UpdateBeer(int id, string name, string type, int rating = 0)
        {
            #region Validation

            if (string.IsNullOrEmpty(name))
                return "Beer name must not be empty";

            if (string.IsNullOrEmpty(type))
                return "Beer type must not be empty";

            if (rating > 0)
                if (rating > 5 || rating < 0)
                    return "Rating must be between 1-5";

            #endregion

            try
            {

                using (var dbContext = new BeerCollectionEntities())
                {
                    var beer = dbContext.Beers.Where(b => b.Id == id).FirstOrDefault();
                    if (beer != null)
                    {
                        dbContext.Beers.Attach(beer);
                        beer.Name = name;

                        if (dbContext.BeerTypes.Any(b => b.Name.ToLower().Contains(type.ToLower())))
                            beer.BeerType = dbContext.BeerTypes.FirstOrDefault(b => b.Name.ToLower().Equals(type.ToLower()));
                        else
                            return "Invalid Beer Type";

                        if (rating > 0)
                            beer.Ratings = (beer.Ratings.Value + rating) / 2;
                        else
                            beer.Ratings = rating;

                        return (dbContext.SaveChanges() == 1 ? "Record updated successfully" : "Server side error occurred while updating the record");
                    }
                    return "Invalid Beer Id";
                }
            }
            catch (Exception ex)
            {
                return "Exception occurred while updating the record";
            }
        }
        public static BeerViewModel ViewBeer(int id)
        {

            try
            {

                using (var dbContext = new BeerCollectionEntities())
                    return dbContext.Beers.Where(br => br.Id == id).Select(b =>
                              new BeerViewModel
                              {
                                  Id = b.Id,
                                  Name = b.Name,
                                  BeerTypes = b.BeerType.Name,
                                  Ratings = b.Ratings
                              }
                             ).ToList<BeerViewModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                return new BeerViewModel();
            }
        }


    }

    public class BeerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> Ratings { get; set; }
        public string BeerTypes { get; set; }
    }
}