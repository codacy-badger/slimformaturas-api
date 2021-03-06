﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using SlimFormaturas.Domain.Dto.Address;
using SlimFormaturas.Domain.Entities;
using SlimFormaturas.Domain.Interfaces.Repository;
using SlimFormaturas.Domain.Interfaces.Service;
using SlimFormaturas.Domain.Notifications;
using SlimFormaturas.Domain.Validators;
using SlimFormaturas.Infra.Data.Repository;

namespace SlimFormaturas.Service.Services
{
    public class AddressService : BaseService<Address>, IAddressService
    {
        protected readonly IAddressRepository _addressRepository;
        protected readonly NotificationHandler _notifications;
        protected readonly ITypeGenericRepository _typeGenericRepository;
        protected readonly IGraduateRepository _graduateRepository;
        readonly IMapper _mapper;

        public AddressService(
            IGraduateRepository graduateRepository,
            IAddressRepository addressRepository,
            NotificationHandler notifications,
            IMapper mapper,
            ITypeGenericRepository typeGenericRepository) : base(addressRepository) {
            _addressRepository = addressRepository;
            _notifications = notifications;
            _typeGenericRepository = typeGenericRepository;
            _mapper = mapper;
            _graduateRepository = graduateRepository;
        }

        public async Task<Address> Insert(Address address) {
            address.Validate(address, new AddressValidator());
            _notifications.AddNotifications(address.ValidationResult);
            //Address address = new Address(obj.Cep, obj.Street, obj.Number, obj.Complement, obj.Neighborhood, obj.City, obj.Uf, await _typeGenericRepository.GetById(obj.TypeGenericId));

            if (address.GraduateId != null) {
                address.Graduate = await _graduateRepository.GetById(address.GraduateId);
            }

            if (address.Invalid) {
                _notifications.AddNotifications(address.ValidationResult);
            }

            if (!_notifications.HasNotifications) {
                await Post(address);
            }

            return address;
        }

        public async Task<Address> Update(AddressDto addressDto) {

            Address address = await _addressRepository.GetById(addressDto.AddressId);

            if (address != null) {
                
                _mapper.Map(addressDto, address);

                address.Validate(address, new AddressValidator());
                _notifications.AddNotifications(address.ValidationResult);

                if (!_notifications.HasNotifications) {
                    await Put(address);
                }
            } else {
                _notifications.AddNotification("404", "AddressId", "Address with id = " + addressDto.AddressId + " not found");
            }



            /*
            if (address != null) {

                address.Cep = obj.Cep;
                address.Street = obj.Street;
                address.Number = obj.Number;
                address.Complement = obj.Complement;
                address.Neighborhood = obj.Neighborhood;
                address.City = obj.City;
                address.Uf = obj.Uf;
                address.TypeGeneric = await _typeGenericRepository.GetById(obj.TypeGenericId);

                address.Validate(address, new AddressValidator());

                if (address.Valid) {
                    await Put(address);
                } else {
                    _notifications.AddNotifications(address.ValidationResult);
                }
            } else {
                _notifications.AddNotification("404", "AddressId", "Graduate with id = " + obj.AddressId + " not found");
            }
            */

            return address;
        }
    }
}
