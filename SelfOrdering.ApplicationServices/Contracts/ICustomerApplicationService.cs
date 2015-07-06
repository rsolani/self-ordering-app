﻿using System.Threading.Tasks;
using SelfOrdering.ApplicationServices.DTO;

namespace SelfOrdering.ApplicationServices.Contracts
{
    public interface ICustomerApplicationService
    {
        Task<CustomerDTO> RegisterOrGetCustomer(CustomerDTO customer);
        Task<CustomerDTO> GetCustomer(string id);

    }
}