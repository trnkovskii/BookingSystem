﻿using BookingSystem.Models.ViewModels;
using BookingSystem.Storage.Interfaces;

namespace BookingSystem.Storage.Repositories
{
    public class SearchRepository : InMemoryRepository<SearchRes>, ISearchRepository
    {
        public List<SearchRes> GetAllOptions()
        {
            var res = (List<SearchRes>)GetAllData();
            return res;
        }
    }
}
