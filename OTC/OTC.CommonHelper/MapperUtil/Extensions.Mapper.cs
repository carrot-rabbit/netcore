﻿using AutoMapper;
using System;
using System.Globalization;
using System.Reflection;

namespace OTC.CommonHelper.MapperUtil
{
    /// <summary>
    /// 系统扩展 - 对象映射
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// 同步锁
        /// </summary>
        private static readonly object Sync = new object();

        /// <summary>
        /// 将源对象映射到目标对象
        /// </summary>
        /// <typeparam name="TSource">源类型</typeparam>
        /// <typeparam name="TDestination">目标类型</typeparam>
        /// <param name="source">源对象</param>
        /// <param name="destination">目标对象</param>
        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return MapTo<TDestination>(source, destination);
        }

        /// <summary>
        /// 将源对象映射到目标对象
        /// </summary>
        /// <typeparam name="TDestination">目标类型</typeparam>
        /// <param name="source">源对象</param>
        public static TDestination MapTo<TDestination>(this object source) where TDestination : new()
        {
            return MapTo(source, new TDestination());
        }

        /// <summary>
        /// 将源对象映射到目标对象
        /// </summary>
        private static TDestination MapTo<TDestination>(object source, TDestination destination)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));
            var sourceType = GetType(source);
            var destinationType = GetType(destination);
            var map = GetMap(sourceType, destinationType);
            if (map != null)
                return Mapper.Map(source, destination);
            lock (Sync)
            {
                map = GetMap(sourceType, destinationType);
                if (map != null)
                    return Mapper.Map(source, destination);
                InitMaps(sourceType, destinationType);
            }
            return Mapper.Map(source, destination);
        }

        /// <summary>
        /// 获取类型
        /// </summary>
        private static Type GetType(object obj)
        {
            var type = obj.GetType();
            if ((obj is System.Collections.IEnumerable) == false)
                return type;
            if (type.IsArray)
                return type.GetElementType();
            var genericArgumentsTypes = type.GetTypeInfo().GetGenericArguments();
            if (genericArgumentsTypes == null || genericArgumentsTypes.Length == 0)
                throw new ArgumentException("泛型类型参数不能为空");
            return genericArgumentsTypes[0];
        }

        /// <summary>
        /// 获取映射配置
        /// </summary>
        private static TypeMap GetMap(Type sourceType, Type destinationType)
        {
            try
            {
                return Mapper.Configuration.FindTypeMapFor(sourceType, destinationType);
            }
            catch (InvalidOperationException)
            {
                lock (Sync)
                {
                    try
                    {
                        return Mapper.Configuration.FindTypeMapFor(sourceType, destinationType);
                    }
                    catch (InvalidOperationException)
                    {
                        InitMaps(sourceType, destinationType);
                    }
                    return Mapper.Configuration.FindTypeMapFor(sourceType, destinationType);
                }
            }
        }

        /// <summary>
        /// 初始化映射配置
        /// </summary>
        private static void InitMaps(Type sourceType, Type destinationType)
        {
            try
            {
                var maps = Mapper.Configuration.GetAllTypeMaps();
                Mapper.Initialize(config => {
                    ClearConfig();
                    foreach (var item in maps)
                        config.CreateMap(item.SourceType, item.DestinationType);
                    config.CreateMap(sourceType, destinationType);
                });
            }
            catch (InvalidOperationException)
            {
                Mapper.Initialize(config => {
                    config.CreateMap(sourceType, destinationType);
                });
            }
        }

        /// <summary>
        /// 清空配置
        /// </summary>
        private static void ClearConfig()
        {
            var typeMapper = typeof(Mapper).GetTypeInfo();
            var configuration = typeMapper.GetDeclaredField("_configuration");
            configuration.SetValue(null, null, BindingFlags.Static, null, CultureInfo.CurrentCulture);
        }
        
    }
}
