using System;
using AutoMapper;
using System.Collections;
using System.Collections.Generic;

namespace XSchool.Helpers
{
    public static class AutoMapperExt
    {
        /// <summary>
        ///  类型映射
        /// </summary>
        public static T MapTo<T>(this object obj)
        {
            if (obj == null) return default(T);

            var config = new MapperConfiguration(cfg => cfg.CreateMap(obj.GetType(), typeof(T)));
            var mapper = config.CreateMapper();
            return mapper.Map<T>(obj);
        }

        /// <summary>
        /// 类型映射,可指定映射字段的配置信息
        /// </summary>
        /// <typeparam name="TSource">源数据：要被转化的实体对象</typeparam>
        /// <typeparam name="TDestination">目标数据：转换后的实体对象</typeparam>
        /// <param name="source">任何引用类型对象</param>
        /// <param name="cfgExp">可为null，则自动一一映射</param>
        /// <returns></returns>
        public static TDestination MapTo<TSource, TDestination>(this TSource source, Action<IMapperConfigurationExpression> cfgExp)
         where TDestination : class
         where TSource : class
        {
            if (source == null) return default(TDestination);
            var config = new MapperConfiguration(cfgExp != null ? cfgExp : cfg => cfg.CreateMap<TSource, TDestination>());
            var mapper = config.CreateMapper();
            return mapper.Map<TDestination>(source);
        }

        /// <summary>
        /// 集合列表类型映射
        /// </summary>
        public static List<TDestination> MapToList<TDestination>(this IEnumerable source)
        {
            Type sourceType = source.GetType().GetGenericArguments()[0];  //获取枚举的成员类型
            var config = new MapperConfiguration(cfg => cfg.CreateMap(sourceType, typeof(TDestination)));
            var mapper = config.CreateMapper();

            return mapper.Map<List<TDestination>>(source);
        }

        /// <summary>
        /// 集合列表类型映射
        /// </summary>
        public static List<TDestination> MapToList<TSource, TDestination>(this IEnumerable<TSource> source)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap(typeof(TSource), typeof(TDestination)));
            var mapper = config.CreateMapper();

            return mapper.Map<List<TDestination>>(source);
        }

        /// <summary>
        /// 类型映射
        /// </summary>
        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
            where TSource : class
            where TDestination : class
        {
            if (source == null) return destination;

            var config = new MapperConfiguration(cfg => cfg.CreateMap(typeof(TSource), typeof(TDestination)));
            var mapper = config.CreateMapper();
            return mapper.Map<TDestination>(source);
        }

    }
}
