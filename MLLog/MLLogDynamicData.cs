using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicLantern.MLLog
{
    /// <summary>
    /// Представляет динамичные данные
    /// </summary>
    public struct MLLogDynamicData
    {
        /// <summary>
        /// Время изменения
        /// </summary>
        public string Time;
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
        public uint FocusDistance;

        internal MLLogDynamicData(string[] data)
        {
            Time = data[0];
            ISO = uint.Parse(data[1]);
            Shutter = "1/" + data[2];
            Apperture = double.Parse(data[3].Replace('.', ','));
            FocalLength = uint.Parse(data[4]);
            FocusDistance = uint.Parse(data[5]);
        }
    }
}
