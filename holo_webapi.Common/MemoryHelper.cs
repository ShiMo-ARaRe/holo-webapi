//这是一个用于管理内存缓存的辅助类 MemoryHelper

using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace holo_webapi.Common
{
    public class MemoryHelper
    {
        //定义了一个私有的静态字段 _memoryCache，用于保存内存缓存的实例
        private static IMemoryCache _memoryCache = null;
        /*  IMemoryCache 接口提供了一组方法，用于在应用程序中实现轻量级的内存缓存。
            它允许将对象存储在内存中，并提供了一些常见的缓存操作，如添加、获取和移除缓存项，以及设置缓存项的过期策略等。*/

        static MemoryHelper()
            /*  静态构造函数，它在类第一次被使用时执行。在该构造函数中，首先检查 _memoryCache 是否为 null。
                如果是 null，即表示内存缓存尚未初始化，那么创建一个新的 MemoryCache 实例并将其赋值给 _memoryCache。
                这样可以确保只有一个内存缓存实例被创建和使用。*/
        {
            if (_memoryCache == null)
            {
                //MemoryCacheOptions 是用于配置内存缓存的选项类。
                //通过使用 new MemoryCacheOptions()，创建了一个默认的选项实例，它使用默认的配置来创建内存缓存。
                _memoryCache = new MemoryCache(new MemoryCacheOptions());
            }
        }
        public static void SetMemory(string key, object value, int expireMins)
        {
            //将对象存储到内存缓存中。
            //它接受三个参数：key 表示缓存项的键，value 表示要存储的对象，expireMins 表示缓存项的过期时间（以分钟为单位）。
            _memoryCache.Set(key, value, TimeSpan.FromMinutes(expireMins));
        }

        public static object GetMemory(string key)
        {
            return _memoryCache.Get(key);// 返回键对应的缓存项
        }
    }
}
