using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Business_Logic_Layer;

namespace UnitTests
{
    public static class MappingClass
    {
        public static Mapper CreateMapper() 
        {
            var profile = new MapperConfigurationClass();
            var mapperConfiguration = new MapperConfiguration(c => c.AddProfile(profile));
            return new Mapper(mapperConfiguration);
        }
    }
}
