using static System.DateTime;

namespace Lunha.DevKit.TypesAndData
{
    [System.Serializable]
    public class TimestampData
    {
        const string DATE_TIME_FORMAT = "yyyy-MM-dd HH:mm:ss";

        string _value;
        public string format { get; private set; }

        public string value
        {
            get => _value;
            set => _value = value;
        }

        public TimestampData(string format = "")
        {
            this.format = string.IsNullOrEmpty(format) ? DATE_TIME_FORMAT : format;
            SetDateTime(UtcNow);
        }

        public void SetDateTime(System.DateTime dateTime) => _value = dateTime.ToString(format);

        public System.DateTime GetDateTime() => string.IsNullOrEmpty(_value)
            ? UtcNow
            : ParseExact(_value, format, null);
    }
}