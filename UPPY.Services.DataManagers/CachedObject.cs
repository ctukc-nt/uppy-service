using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPPY.Services.DataManagers
{
    sealed class CachedObject<T>
    {
        /// <summary>
        /// Кэшированный объект
        /// </summary>
        public T Object { get; set; }

        /// <summary>
        /// Точка кэширования
        /// </summary>
        public int CachedPointTime { get; set; }
    }
}
