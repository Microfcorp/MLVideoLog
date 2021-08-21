using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicLantern.MLLog
{
    /// <summary>
    /// Поиск каталогов и файлов
    /// </summary>
    public static class MLSearchLogFile
    {
        /// <summary>
        /// Возвращает путь к файло лога по пути на видео файл
        /// </summary>
        /// <param name="VideoPath">Путь к видео файлу</param>
        /// <returns></returns>
        public static string GetMLPath(string VideoPath)
        {
            string txtfile = Path.ChangeExtension(VideoPath, "log");
            return File.Exists(txtfile) ? txtfile : null;
        }

        /// <summary>
        /// Имеется ли файл с LOG Данными
        /// </summary>
        /// <param name="VideoPath">Путь к видео файлу</param>
        /// <returns></returns>
        public static bool IsLogFile(string VideoPath)
        {
            return GetMLPath(VideoPath) != null;
        }
    }
}
