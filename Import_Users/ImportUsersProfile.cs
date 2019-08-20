using AutoMapper;
using ProductShop.Models;
using System;
using System.Collections.Generic;
using System.Text;

public class ImportUsersProfile : Profile
{
    public  ImportUsersProfile()
    {
        this.CreateMap<User, UserImportDto>();
    }
}

