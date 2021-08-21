using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicLantern.MLLog
{
    public class MLLogFile
    {
        private string path;

        /// <summary>
        /// Путь к файлу с лог-ом
        /// </summary>
        public string Path
        {
            get => path;
            set 
            {
                path = value;
                TextData = File.ReadAllText(value);
                Data = TextData.Split('\n');
            }
        }

        /// <summary>
        /// Создает класс с указанием пути к файлу
        /// </summary>
        /// <param name="path"></param>
        public MLLogFile(string path)
        {
            Path = path;
        }

        /// <summary>
        /// Текстовый доккумент, представляющий собой данные
        /// </summary>
        public string TextData
        {
            get;
            private set;
        }
        /// <summary>
        /// Набор строк, представляющие данные файла
        /// </summary>
        public string[] Data
        {
            get;
            private set;
        }

        public override string ToString()
        {
            return TextData.Replace("\n", Environment.NewLine);
        }

        /// <summary>
        /// Версия Magic Lantern
        /// </summary>
        public string MLVersion
        {
            get
            {
                return Data[0].Substring(2);
            }
        }
        /// <summary>
        /// Время запуска съемки
        /// </summary>
        public string StartTime
        {
            get
            {
                return GetParameters(Data[2]);
            }
        }
        /// <summary>
        /// Статические данные
        /// </summary>
        public MLLogStaticData StaticData
        {
            get
            {
                return new MLLogStaticData(Data);
            }
        }

        /// <summary>
        /// Динамические данные
        /// </summary>
        public MLLogDynamicData[] DynamicData
        {
            get
            {
                return Data.Skip(16).Where(tmp => tmp.Length > 10).Select(tmp => new MLLogDynamicData(tmp.Split(','))).ToArray();
            }
        }

        /// <summary>
        /// Возвращает значение параметра из строки
        /// </summary>
        /// <param name="line">Строка</param>
        /// <returns></returns>
        public static string GetParameters(string line)
        {
            return line.Substring(17);
        }
    }
}
