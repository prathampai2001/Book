﻿using BookNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookNetCore.DataAccess.Repository.IRepository
{
     public  interface ICompanyRepository:IRepository<Company>
    {
        void Update(Company obj);
       
    }
}
