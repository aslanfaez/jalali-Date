using System;
using System.Linq;
using System.Globalization;

namespace adliTM.persianDate
{
    public sealed class persianDate
    {
        PersianCalendar _perCal;
        DateTime _cal;

        string[] _daynames = new string[] { "یکشنبه", "دوشنبه", "سه شنبه", "چهار شنبه", "پنج شنبه", "جمعه", "شنبه" };

        public int second { get { return _perCal.GetSecond(_cal); } }
        public int minute { get { return _perCal.GetMinute(_cal); } }
        public int hour { get { return _perCal.GetHour(_cal); } }
        public int day { get { return _perCal.GetDayOfMonth(_cal); } }
        public int month { get { return _perCal.GetMonth(_cal); } }
        public int year { get { return _perCal.GetYear(_cal); } }

        public int dayOfYear { get { return _perCal.GetDayOfYear(_cal); } }
        public int WeekofYear { get { return _perCal.GetWeekOfYear(_cal, CalendarWeekRule.FirstDay, DayOfWeek.Saturday); } }
        public bool isLeapYear { get { return _perCal.IsLeapYear(year, PersianCalendar.PersianEra); } }
        public string dayname { get { return _daynames[(_perCal.GetDayOfWeek(_cal).GetHashCode())]; } }


        public string date { get { return string.Concat(year, "/", format(month), "/", format(day)); } }
        public string time { get { return string.Concat(format(hour), ":", format(minute), ":", format(second)); } }
        public string dateTime { get { return string.Concat(date, "-", time); } }
        public DateTime englishDate { get { return _cal; } }
        private string format(int digit)
        {
            if (digit < 10)
                return string.Concat("0", digit);
            else
                return digit.ToString();
        }

        public void initializeCalender()
        {
            _cal = DateTime.Now;


        }
        public void initializeCalender(int _year, int _month, int _day, int _hour, int _minutes, int _second)
        {

            _cal = _perCal.ToDateTime(_year, _month, _day, _hour, _minutes, _second, 0);

        }
        public void initializeCalender(string PersianDate)
        {
            string[] dates = new string[] { "0", "0", "0" };
            string[] times = new string[] { "0", "0", "0" };
            if (PersianDate.Contains('-'))
            {
                var temp = splitDateTime(PersianDate);
                dates = splitDate(temp[0]);
                times = splitTime(temp[1]);
            }
            else
            {
                dates = splitDate(PersianDate);
            }
            if (dates.Count() == 3 && times.Count() == 3)
                initializeCalender(
                          int.Parse(dates[0]),
                          int.Parse(dates[1]),
                          int.Parse(dates[2]),
                          int.Parse(times[0]),
                          int.Parse(times[1]),
                          int.Parse(times[2]));
            else if (dates.Count() == 3 && times.Count() == 2)
                initializeCalender(
                          int.Parse(dates[0]),
                          int.Parse(dates[1]),
                          int.Parse(dates[2]),
                          int.Parse(times[0]),
                          int.Parse(times[1]),
                          0);
            else
                initializeCalender();


        }
        public void initializeCalender(DateTime EnglishDate)
        {

            _cal = EnglishDate;

        }

        string[] splitDate(string Date)
        {
            return Date.Split('/');
        }
        string[] splitTime(string Time)
        {
            return Time.Split(':');
        }
        string[] splitDateTime(string DateTime)
        {
            return DateTime.Split('-');
        }

        public persianDate()
        {
            _perCal = new PersianCalendar();
            initializeCalender();
        }
        public persianDate(string DateTime)
        {
            _perCal = new PersianCalendar();
            initializeCalender(DateTime);
        }
        public persianDate(int year, int month, int day, int hour, int minutes, int second)
        {
            _perCal = new PersianCalendar();
            initializeCalender(year, month, day, hour, minutes, second);
        }
        public persianDate(DateTime EnglishDate)
        {
            _perCal = new PersianCalendar();
            initializeCalender(EnglishDate);
        }
        public persianDate(persianDate perDate)
        {
            _perCal = new PersianCalendar();
            initializeCalender(perDate._cal);
        }

        public override string ToString()
        {
            return dateTime;
        }

        public void addseconds(int seconds)
        {
            _cal = _cal.AddSeconds(seconds);
        }
        public void addminutes(int minutes)
        {
            _cal = _cal.AddMinutes(minutes);
        }
        public void addhour(int hours)
        {
            _cal = _cal.AddHours(hours);
        }
        public void adddays(int days)
        {
            _cal = _cal.AddDays(days);
        }
        public void addmonths(int months)
        {
            _cal = _cal.AddMonths(months);
        }
        public void addyears(int years)
        {
            _cal = _cal.AddYears(years);
        }

        public persianDate clone()
        {
            return new persianDate(this);
        }

        public static persianDate operator +(persianDate d, TimeSpan t)
        {
            d._cal = d._cal + t;
            return d;
        }
        public static persianDate operator -(persianDate d, TimeSpan t)
        {
            d._cal = d._cal - t;
            return d;
        }
        public static TimeSpan operator -(persianDate d1, persianDate d2)
        {

            return (d1._cal - d2._cal);
        }
        public static bool operator ==(persianDate d1, persianDate d2)
        {
            return (d1._cal == d2._cal);
        }
        public static bool operator !=(persianDate d1, persianDate d2)
        {
            return (d1._cal != d2._cal);
        }
        public static bool operator >(persianDate d1, persianDate d2)
        {
            return (d1._cal > d2._cal);
        }
        public static bool operator >=(persianDate d1, persianDate d2)
        {
            return (d1._cal >= d2._cal);
        }
        public static bool operator <(persianDate d1, persianDate d2)
        {
            return (d1._cal < d2._cal);
        }
        public static bool operator <=(persianDate d1, persianDate d2)
        {
            return (d1._cal <= d2._cal);
        }

        public static persianDate Parse(string s)
        {
            return (new persianDate(s));
        }



    }

    public static class dateExtensions
    {
        public static string toPersian(this DateTime dtt)
        {
            persianDate dt = new persianDate(dtt);
            return dt.dateTime;
        }
        public static string toPersian(this DateTime? dtt)
        {
            if (dtt != null)
            {
                persianDate dt = new persianDate(dtt.Value);
                return dt.dateTime;
            }
            else
            {
                return "";
            }
        }
    }
}