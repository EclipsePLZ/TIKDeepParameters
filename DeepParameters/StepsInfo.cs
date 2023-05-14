﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepParameters {
    internal static class StepsInfo {
        public static string Step1 { get; } = "Загрузка данных из файла (excel, csv) и выбор аварии для исследования";

        public static string Step2 { get; } = "На данном этапе происходит вычисление интервала надежности вибросигнала.\n" +
            "Максимальный вибросигнал, при котором наблюдается нормальная работа,\nрассчитывается как среднее значение + k-стандартных отклонений.\n" +
            "Далее каждому значению вибросигнала в соответствие ставится значение надежности из рассчитанного интервала.";
    }
}
