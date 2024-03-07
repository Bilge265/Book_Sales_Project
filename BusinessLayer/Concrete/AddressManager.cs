﻿using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class AddressManager : IAddressService
    {
        IAddressDal _addressDal;

        public AddressManager(IAddressDal addressDal)
        {
            _addressDal = addressDal;
        }

        public void TAdd(Address t)
        {
           _addressDal.Insert(t);
        }

        public void TDelete(Address t)
        {
           _addressDal.Delete(t);
        }

        public Address TGetByID(int id)
        {
           return _addressDal.GetById(id);
        }

        public List<Address> TGetList()
        {
            return _addressDal.GetList();
        }

        public void TUpdate(Address t)
        {
          _addressDal.Update(t);
        }
    }
}
