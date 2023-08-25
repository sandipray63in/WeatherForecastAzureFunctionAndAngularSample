using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.DomainServices.DayMessageBuilders;

namespace WeatherForecast.DomainServices
{
    public static class DayMessageBuilderFactory
    {
        private static IDictionary<DayMessageBuilderType,IDayMessageBuilder> _dayMessageBuildersList = new Dictionary<DayMessageBuilderType,IDayMessageBuilder>();
        static DayMessageBuilderFactory()
        {
            _dayMessageBuildersList.Add(DayMessageBuilderType.Rainy, new RainyDayMessageBuilder());
            _dayMessageBuildersList.Add(DayMessageBuilderType.Hot, new HotDayMessageBuilder());
            _dayMessageBuildersList.Add(DayMessageBuilderType.Windy, new WindyDayMessageBuilder());
            _dayMessageBuildersList.Add(DayMessageBuilderType.Thunderstorm, new ThunderstormDayMessageBuilder());
        }

        public static IList<IDayMessageBuilder> GetDayMessageBuilders(params DayMessageBuilderType[] dayMessagesBuilderTypes)
        {
            IList<IDayMessageBuilder> dayMessageBuildersList = new List<IDayMessageBuilder>();
            foreach (DayMessageBuilderType dayMessagesBuilderType in dayMessagesBuilderTypes)
            {
                dayMessageBuildersList.Add(_dayMessageBuildersList[dayMessagesBuilderType]);
            }
            return dayMessageBuildersList;
        }
    }
}
