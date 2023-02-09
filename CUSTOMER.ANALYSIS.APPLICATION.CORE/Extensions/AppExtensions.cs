using AutoMapper;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.AppServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.QueryServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Extensions
{
    public static class AppExtensions
    {
        internal static RolAppResultDto MapToRolesAppResultDto(this RolesQueryDto obj)
        {
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<RolesQueryDto, RolAppResultDto>());
            var mapper = configuration.CreateMapper();
            return mapper.Map<RolAppResultDto>(obj);
        }


    }
}
