using TestTask.Helper;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestTask.Models.Persistance
{
    public class CarRepository : ICarRepository
    {
        public CarRepository()
        {

        }

        public CarModel Get(int id)
        {
            using (var session = NHibernateHelper.OpenSession())
                return session.Get<CarModel>(id);
        }

        public IEnumerable<CarModel> GetByGovNumber(string govNumber)
        {
            using (var session = NHibernateHelper.OpenSession())
                return session.Query<CarModel>().Where(w => w.GovNumber == govNumber).ToList();
        }

        public IEnumerable<CarModel> GetAll()
        {
            using (var session = NHibernateHelper.OpenSession())
                return session.Query<CarModel>().ToList();
        }

        public CarModel Add(CarModel Car)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(Car);
                    transaction.Commit();
                }
                return Car;
            }
        }

        public void Delete(int id)
        {
            var Car = Get(id);

            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Delete(Car);
                    transaction.Commit();
                }
            }

        }

        public bool Update(CarModel Car)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(Car);
                    try
                    {
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                }
                return true;
            }
        }
    }
}