using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicLantern.MLLog
{
    /// <summary>
    /// Представляет статичные данные
    /// </summary>
    public struct MLLogStaticData
    {
        /// <summary>
        /// Название объектива
        /// </summary>
        public string LensName;
        /// <summary>
        /// Значение ISO
        /// </summary>
        public uint ISO;
        /// <summary>
        /// Выдержка
        /// </summary>
        public string Shutter;
        /// <summary>
        /// Диафрагма
        /// </summary>
        public double Apperture;
        /// <summary>
        /// Фокусное расстояние
        /// </summary>
        public uint FocalLength;
        /// <summary>
        /// Фокусная дистанция
        /// </summary>
        public long FocusDistance;
        /// <summary>
        /// Баланс белого
        /// </summary>
        public string WhiteBalance;
        /// <summary>
        /// Стиль изображения
        /// </summary>
        public string PictureStyle;
        /// <summary>
        /// Частота кадров
        /// </summary>
        public double FPS;
        /// <summary>
        /// Коэффициент скорости потока
        /// </summary>
        public string BitRate;

        internal MLLogStaticData(string[] data)
        {
            LensName = MLLogFile.GetParameters(data[3]);
            ISO = uint.Parse(MLLogFile.GetParameters(data[4]));
            Shutter = MLLogFile.GetParameters(data[5]).Replace("s", "");
            Apperture = double.Parse(MLLogFile.GetParameters(data[6]).Substring(3).Replace('.', ','));
            FocalLength = uint.Parse(MLLogFile.GetParameters(data[7]).Split(' ').FirstOrDefault());
            FocusDistance = long.Parse(MLLogFile.GetParameters(data[8]).Split(' ').FirstOrDefault());
            WhiteBalance = MLLogFile.GetParameters(data[9]);
            PictureStyle = MLLogFile.GetParameters(data[10]);
            FPS = double.Parse(MLLogFile.GetParameters(data[11]).Replace('.',','));
            BitRate = MLLogFile.GetParameters(data[12]);
        }
        
    }
}
