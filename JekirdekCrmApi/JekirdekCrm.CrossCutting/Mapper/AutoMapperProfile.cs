using AutoMapper;
using JekirdekCrm.Domain.Dto.Request;
using JekirdekCrm.Domain.Dto.Response;
using JekirdekCrm.Domain.Entity;
using JekirdekCrm.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JekirdekCrm.CrossCutting.Mapper
{
    /// <summary>
    /// Mapper ile Nesneler Dönüşümü Tek Satırda Yapılcak
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// Mapler Çift Yönlüde Tek Yönlüde Olabilir Belirtmek Gerekir
        /// </summary>
        public AutoMapperProfile()
        {

            //User Db ile App Nesnesi Map
            CreateMap<UserModel, User>();
            CreateMap<User, UserModel>();

            //Customer Db ile App Nesnesi Map
            CreateMap<CustomerModel, Customer>();
            CreateMap<Customer, CustomerModel>();

            //Customer Ui Req ile App Nesnesi Map
            CreateMap<CustomerModel, CustomerRequest>();
            CreateMap<CustomerRequest, CustomerModel>();

            //Customer Ui Res ile App Nesnesi Map
            CreateMap<CustomerModel, CustomerResponse>();
            CreateMap<CustomerResponse, CustomerModel>();
        }
    }
}
