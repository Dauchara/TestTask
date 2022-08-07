using System.Collections.Generic;

namespace TestTask.Models.Persistance
{
    public interface ICarRepository
    {
        CarModel Get(int id);
        IEnumerable<CarModel> GetByGovNumber(string govNumber);
        IEnumerable<CarModel> GetAll();
        CarModel Add(CarModel Car);
        void Delete(int id);
        bool Update(CarModel Car);

    }
}
