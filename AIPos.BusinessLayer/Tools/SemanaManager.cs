using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIPos.BusinessLayer.Tools
{
    public class SemanaManager
    {
        
        public List<Semana> RecuperarSemanaAnio()
        {
            List<Semana> semanas = new List<Semana>();
            for (int i = 1; i <= 52; i++)
            {
                semanas.Add(new Semana { Nombre = "Semana " + i.ToString(), Numero = i });
            }
            return semanas;
        }

        public int SemanaActual()
        {
            return System.Globalization.DateTimeFormatInfo.CurrentInfo.Calendar.GetWeekOfYear(DateTime.Now, 
                System.Globalization.DateTimeFormatInfo.CurrentInfo.CalendarWeekRule, System.Globalization.DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek);
        }
    }

    public class Semana
    {
        public string Nombre { get; set; }
        public int Numero { get; set; }

    }
}
