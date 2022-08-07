using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestTask.Models;
using TestTask.Models.Persistance;

namespace TestTask.Controllers
{
    [RoutePrefix("api/v1/Car")]
    public class CarController : ApiController
    {
        static readonly ICarRepository CarRepository = new CarRepository();

        public IEnumerable<CarModel> GetCar()
        {
            return CarRepository.GetAll();
        }

        [Route("GetBalanceById")]
        [HttpGet]
        public IHttpActionResult GetBalanceById(int id)
        {
            try
            {
                var car = CarRepository.Get(id);

                if (car == null)
                    return Content(HttpStatusCode.NotFound, $"Ошибка! Автомобиль по id: {id}, не найден!");

                decimal Balance = car.Balance;
                return Ok(Balance);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Route("GetBalanceByGovNumber")]
        [HttpGet]
        public IHttpActionResult GetBalanceByGovNumber(string GovNumber)
        {
            try
            {
                var car = CarRepository.GetByGovNumber(GovNumber);

                if (car == null)
                    return Content(HttpStatusCode.NotFound, $"Ошибка! Автомобиль по гос. номеру: {GovNumber}, не найден!");
                
                return Ok(car.Select(s=>new { s.GovNumber, s.Balance }).ToList());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Route("Add")]
        [HttpPost]
        public IHttpActionResult Add(CarModel car)
        {
            try
            {
                int? id = CarRepository.Add(car).ID;

                if(id == null)
                    return Content(HttpStatusCode.NotFound, $"Ошибка! Автомобиль по гос. номеру: {car.GovNumber}, не создан!");

                return Ok($"Автомобиль с ID: {id}, успешно добавлен!");
            }
            catch(Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Route("UpdateBalance")]
        [HttpPost]
        public void UpdateBalance(int id, CarModel Car)
        {
            Car.ID = id;

            if (!CarRepository.Update(Car))
                throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        public void DeleteCar(int id)
        {
            var Car = CarRepository.Get(id);

            if (Car == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            CarRepository.Delete(id);
        }
    }
}
