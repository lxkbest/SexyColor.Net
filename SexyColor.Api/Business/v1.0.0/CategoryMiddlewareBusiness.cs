using SexyColor.Api.Models;
using AutoMapper;
using SexyColor.BusinessComponents;
using SexyColor.Infrastructure;
using SexyColor.CommonComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SexyColor.Api.Business.v1_0_0
{
    public class CategoryMiddlewareBusiness
    {
        private readonly IGoodsCategoryService goodsCategoryService = DIContainer.Resolve<IGoodsCategoryService>();
        private readonly IGoodsInfoService goodsInfoService = DIContainer.Resolve<IGoodsInfoService>();

        public async Task<List<CategoryDto>> GetCategory()
        {
            List<CategoryDto> list = new List<CategoryDto>();
            await Task.Run(() =>
            {
                var categorys = goodsCategoryService.GetGoodsCategoryAll();
                foreach (var item in categorys)
                {
                    CategoryDto dto = new CategoryDto();
                    dto.cat_name = item.CategoryName;

                    IEnumerable<GoodsCategoryInItem> categoryInItems = goodsCategoryService.GetGoodsCategoryInItemByCategoryId(item.CategoryId);
                    List<int> ids = new List<int>();
                    foreach (var inItem in categoryInItems)
                    {
                        ids.Add(inItem.CategoryItemId);
                    }
                    var categoryItems = goodsCategoryService.GetCategoryItemByIds(ids);

                    Mapper.Initialize(c => c.CreateMap<CategoryItem, CategoryItemDto>()
                           .ForMember(dest => dest.cat_img, opt => opt.MapFrom(src => src.ImgUrl))
                           .ForMember(dest => dest.link_url, opt => opt.MapFrom(src => src.LinkUrl))
                           .ForMember(dest => dest.cat_name, opt => opt.MapFrom(src => src.CategoryItemName))
                           .ForMember(dest => dest.is_hot, opt => opt.MapFrom(src => src.IsHot))
                    );

                    var sub_category = Mapper.Map<IEnumerable<CategoryItem>, IEnumerable<CategoryItemDto>>(categoryItems);
                    if (sub_category != null && sub_category.Count() > 0)
                        dto.sub_category = sub_category;
                    list.Add(dto);
                }
            });
            return list;
        }

        public async Task<GoodsInfoCategoryListDto> GetCategoryByParams(GoodsInfoCategoryParam param = null)
        {
            if (param == null)
                return null;

            GoodsInfoCategoryListDto dto = new GoodsInfoCategoryListDto();
            ScreeningDto screenDto = new ScreeningDto();
            screenDto.price_grade = new List<GradeDto>();

            List<int> ids = new List<int>();

            await Task.Run(() =>
            {
                IEnumerable<CategoryItemInBrands> inBrands = goodsCategoryService.GetCategoryItemInBrandsByCategoryItemId(param.CategoryItemid);
                foreach (var inBrandItem in inBrands)
                {
                    ids.Add(inBrandItem.BrandsId);
                }
                var brands = goodsCategoryService.GetCategoryItemBrandsIds(ids);
                ids.Clear();
                Mapper.Initialize(c => c.CreateMap<CategoryItemBrands, BrandDto>()
                           .ForMember(dest => dest.brand_id, opt => opt.MapFrom(src => src.BrandsId))
                           .ForMember(dest => dest.brand_name, opt => opt.MapFrom(src => src.BrandsName))
                    );
                screenDto.brands = Mapper.Map<IEnumerable<CategoryItemBrands>, List<BrandDto>>(brands);
            });

            await Task.Run(() => 
            {
                IEnumerable<CategoryItemInChara> inCharas = goodsCategoryService.GetCategoryItemInCharaByCategoryItemId(param.CategoryItemid);
                foreach (var inCharaItem in inCharas)
                {
                    ids.Add(inCharaItem.CharaId);
                }
                var charas = goodsCategoryService.GetCategoryItemCharaIds(ids);
                ids.Clear();
                Mapper.Initialize(c => c.CreateMap<CategoryItemChara, CharaDto>()
                           .ForMember(dest => dest.chara, opt => opt.MapFrom(src => src.CharaId))
                           .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.CharaName))
                    );
                screenDto.characteristic = Mapper.Map<IEnumerable<CategoryItemChara>, List<CharaDto>>(charas);
            });



            await Task.Run(() =>
            {
                PagingDataSet<GoodsInfo> pageing = goodsInfoService.GetPageGoodsInfoAPI(ParamsToKeyValue(param), ParamsSortValue(param.Sort), param.PageIndex, param.PageSize);
                dto.page_total = pageing.PageCount;
                dto.c_total = (int)pageing.TotalRecords;
                dto.c_page_next = param.PageIndex + 1;

                var pageings = pageing.ToList();
                Mapper.Initialize(c => c.CreateMap<GoodsInfo, GoodsInfoCategoryDto>()
                           .ForMember(dest => dest.is_vedio, opt => opt.MapFrom(src => false))
                           .ForMember(dest => dest.goods_id, opt => opt.MapFrom(src => src.Goodsid))
                           .ForMember(dest => dest.goods_name, opt => opt.MapFrom(src => src.GoodsName))
                           .ForMember(dest => dest.app_price, opt => opt.MapFrom(src => src.GoodsRealPrice))
                           .ForMember(dest => dest.comment_num, opt => opt.MapFrom(src => src.CommenCount))
                           .ForMember(dest => dest.img_url, opt => opt.MapFrom(src => src.ImageUrl))
                           .ForMember(dest => dest.is_free_shipping, opt => opt.MapFrom(src => true))
                           .ForMember(dest => dest.is_new, opt => opt.MapFrom(src => false))
                           .ForMember(dest => dest.is_promote, opt => opt.MapFrom(src => false))
                           .ForMember(dest => dest.sold_num, opt => opt.MapFrom(src => src.BuyCount))
                           .ForMember(dest => dest.is_hot, opt => opt.MapFrom(src => src.IsHot))
                    );
               dto.goods_list = Mapper.Map<List<GoodsInfo>, List<GoodsInfoCategoryDto>>(pageings);
            });

            dto.screening = screenDto;
            return dto;
        }

        private Dictionary<string, object> ParamsToKeyValue(GoodsInfoCategoryParam param)
        {
            Dictionary<string, object> paramCollection = new Dictionary<string, object>();
            if (param.CategoryItemid > 0)
                paramCollection.Add("CategoryItemid", param.CategoryItemid);
            if (param.Brandsid > 0)
                paramCollection.Add("Brandsid", param.Brandsid);
            if (param.Charaid > 0)
                paramCollection.Add("Charaid", param.Charaid);
            if (param.Max > 0)
                paramCollection.Add("Max", param.Max);
            if (param.Min > 0)
                paramCollection.Add("Min", param.Min);
            return paramCollection;
        }

        private string ParamsSortValue(GoodsInfoCategoryAPISortBy sortBy)
        {
            string sortString = string.Empty;
            switch (sortBy)
            {
                case GoodsInfoCategoryAPISortBy.Default:
                    sortString = "commen_count desc, real_buy_count desc, is_hot desc";
                    break;
                case GoodsInfoCategoryAPISortBy.Sale:
                    sortString = "buy_count desc";
                    break;
                case GoodsInfoCategoryAPISortBy.Peice_Desc:
                    sortString = "goods_real_price desc";
                    break;
                case GoodsInfoCategoryAPISortBy.Peice_Asc:
                    sortString = "goods_real_price asc";
                    break;
            }
            return sortString;
        }
    }
}
