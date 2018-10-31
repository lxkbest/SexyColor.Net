using SexyColor.Api.Models;
using SexyColor.BusinessComponents;
using SexyColor.Infrastructure;
using AutoMapper;
using System.Collections.Generic;
using SexyColor.Api.Utility;

namespace SexyColor.Api.Business.v1_0_0
{
    public class SitesMiddlewareBusiness
    {
        private readonly IHomeDynamicSettingsService homeDynamicSettingsService = DIContainer.Resolve<IHomeDynamicSettingsService>();
        private readonly IGoodsInfoService goodsInfoService = DIContainer.Resolve<IGoodsInfoService>();


        public SitesDto GetSitesDto()
        {
            SitesDto sitesDto = new SitesDto();

            //轮换广告
            IEnumerable<HomeDynamicSettings> ads = homeDynamicSettingsService.GetHomeDynamicByType((int)DynamicSettingsType.Loop);
            //热门图标
            IEnumerable<HomeDynamicSettings> hots = homeDynamicSettingsService.GetHomeDynamicByType((int)DynamicSettingsType.Classify);
            //楼层专区
            IEnumerable<HomeDynamicSettings> others = homeDynamicSettingsService.GetHomeDynamicByType((int)DynamicSettingsType.Area);

            var products = goodsInfoService.GetGoodsTopNumberByHot(6);

            Mapper.Initialize(c => c.CreateMap<HomeDynamicSettings, HomeDynamicSettingsDto> ()
                       .ForMember(dest => dest.img_url, opt => opt.MapFrom(src => src.ImageUrl))
                       .ForMember(dest => dest.link_url, opt => opt.MapFrom(src => src.RedirectUrl))
                       .ForMember(dest => dest.type, opt => opt.MapFrom(src => src.Type))
                       .ForMember(dest => dest.main_title, opt => opt.MapFrom(src => src.LabelText))
            );

            var top_ads = Mapper.Map<IEnumerable<HomeDynamicSettings>, IEnumerable<HomeDynamicSettingsDto>>(ads);
            var hot_list = Mapper.Map<IEnumerable<HomeDynamicSettings>, IEnumerable<HomeDynamicSettingsDto>>(hots);
            Mapper.Initialize(c => c.CreateMap<HomeDynamicSettings, FloorAreaDto>()
                       .ForMember(dest => dest.subtitle, opt => opt.MapFrom(src => string.Empty))
                       .ForMember(dest => dest.link_url, opt => opt.MapFrom(src => src.RedirectUrl))
                       .ForMember(dest => dest.type, opt => opt.MapFrom(src => src.Type))
                       .ForMember(dest => dest.main_title, opt => opt.MapFrom(src => src.LabelText))
                       .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
                       .ForMember(dest => dest.ad_list, opt => opt.MapFrom(src => new List<HomeDynamicSettingsDto>()))
            );
            var other_recommend_list = Mapper.Map<IEnumerable<HomeDynamicSettings>, IEnumerable<FloorAreaDto>>(others);
            foreach (var item in other_recommend_list)
            {
                var areas = homeDynamicSettingsService.GetHomeDynamicByParentsid(item.id);
                Mapper.Initialize(c => c.CreateMap<HomeDynamicSettings, HomeDynamicSettingsDto>()
                       .ForMember(dest => dest.img_url, opt => opt.MapFrom(src => src.ImageUrl))
                       .ForMember(dest => dest.link_url, opt => opt.MapFrom(src => src.RedirectUrl))
                       .ForMember(dest => dest.type, opt => opt.MapFrom(src => src.Type))
                       .ForMember(dest => dest.main_title, opt => opt.MapFrom(src => src.LabelText))
                );
                var ad_list = Mapper.Map<IEnumerable<HomeDynamicSettings>, IEnumerable<HomeDynamicSettingsDto>>(areas);
                item.ad_list = ad_list;
            }

            Mapper.Initialize(c => c.CreateMap<GoodsInfo, GoodsInfoDto>()
                       .ForMember(dest => dest.goods_id, opt => opt.MapFrom(src => src.Goodsid))
                       .ForMember(dest => dest.goods_attr, opt => opt.MapFrom(src => 0))
                       .ForMember(dest => dest.goods_sn, opt => opt.MapFrom(src => string.Empty))
                       .ForMember(dest => dest.goods_name, opt => opt.MapFrom(src => src.GoodsName))
                       .ForMember(dest => dest.app_price, opt => opt.MapFrom(src => src.GoodsRealPrice))
                       .ForMember(dest => dest.special_price, opt => opt.MapFrom(src => src.GoodsRealPrice))
                       .ForMember(dest => dest.special_type, opt => opt.MapFrom(src => 0))
                       .ForMember(dest => dest.special_id, opt => opt.MapFrom(src => 0))
                       .ForMember(dest => dest.sort_order, opt => opt.MapFrom(src => src.DisplayOrder))
                       .ForMember(dest => dest.start, opt => opt.MapFrom(src => string.Empty))
                       .ForMember(dest => dest.recommend, opt => opt.MapFrom(src => string.Empty))
                       .ForMember(dest => dest.total_comment, opt => opt.MapFrom(src => src.RealBuyCount))
                       .ForMember(dest => dest.market_price, opt => opt.MapFrom(src => src.GoodsPrice))
                       .ForMember(dest => dest.discount, opt => opt.MapFrom(src => 7.7))
                       .ForMember(dest => dest.img_url, opt => opt.MapFrom(src => src.ImageUrl))
                       .ForMember(dest => dest.seller_note, opt => opt.MapFrom(src => string.Empty))
                       .ForMember(dest => dest.difference, opt => opt.MapFrom(src => 0))
                       .ForMember(dest => dest.goods_number, opt => opt.MapFrom(src => 0))
                       .ForMember(dest => dest.user_number, opt => opt.MapFrom(src => 0))
                       .ForMember(dest => dest.sold_number, opt => opt.MapFrom(src => src.BuyCount))
            );

            var products_list = Mapper.Map<IEnumerable<GoodsInfo>, IEnumerable<GoodsInfoDto>>(products);

            sitesDto.top_ads = top_ads;
            sitesDto.hot_list = hot_list;
            sitesDto.other_recommend_list = other_recommend_list;
            sitesDto.product_recommendation = products_list;


            return sitesDto;
        }
    }
}
