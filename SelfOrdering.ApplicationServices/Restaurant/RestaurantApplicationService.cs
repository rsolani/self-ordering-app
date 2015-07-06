﻿using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Bson;
using SelfOrdering.ApplicationServices.Contracts;
using SelfOrdering.ApplicationServices.DTO;
using SelfOrdering.Domain.Contracts.Repositories;
using SelfOrdering.Domain.Contracts.Services;

namespace SelfOrdering.ApplicationServices.Restaurant
{
    public class RestaurantApplicationService : ApplicationServiceBase<Domain.Restaurant.Restaurant>, IRestaurantApplicationService
    {
        private readonly IRestaurantService _restaurantService;
     
        public RestaurantApplicationService(IBaseRepository<Domain.Restaurant.Restaurant> repository, 
            IRestaurantService restaurantService) 
            : base(repository)
        {
            _restaurantService = restaurantService;
        }

        public async Task<IReadOnlyList<RestaurantDTO>> GetAll()
        {
            var entity = await Repository.GetAllAsync();
            return Mapper.Map(entity, new List<RestaurantDTO>());
        }

        public async Task<RestaurantDTO> GetById(string id)
        {
            var entity = await Repository.GetByIdAsync(new ObjectId(id));
            return Mapper.Map(entity, new RestaurantDTO());
        }

        public async Task<IEnumerable<RestaurantDTO>> GetNearRestaurants(double longitude, double latitude)
        {
            var maxDistanceinKilometers = 
                double.Parse(ConfigurationManager.AppSettings["MaxDistanceToFindPlacesInKilometers"]);
            var maxDistanceinMeters = maxDistanceinKilometers * 1000; //Transforma em metros

            var nearRestaurants = await _restaurantService.GetNearRestaurants(longitude, latitude, maxDistanceinMeters);
            return Mapper.Map(nearRestaurants, new List<RestaurantDTO>());
        }
    }
}