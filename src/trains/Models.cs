using System;
using System.Collections.Generic;

namespace TrainClient.Models
{
    public class SearchResult
    {
        public LocationDetail Location { get; set; }
        public IEnumerable<LocationContainer> Services { get; set; }
    }

    public class LocationContainer
    {
        public LocationDetail LocationDetail { get; set; }
        public string ServiceUid { get; set; }
        public DateTime RunDate { get; set; }
        public string TrainIdentity { get; set; }
        public string RunningIdentity { get; set; }
        public string AtocCode { get; set; }
        public string AtocName { get; set; }
        public string ServiceType { get; set; }
        public bool IsPassenger { get; set; }
    }

    public class LocationDetail
    {
        public string Name { get; set; }
        public bool RealtimeActivated { get; set; }
        public string Crs { get; set; }
        public string Description { get; set; }
        public string GbttBookedArrival { get; set; }
        public string GbttBookedDeparture { get; set; }
        public IEnumerable<Pair> Origin { get; set; }
        public IEnumerable<Pair> Destination { get; set; }
        public bool IsCall { get; set; }
        public bool IsPublicCall { get; set; }
        public string RealtimeArrival { get; set; }
        public bool RealtimeArrivalActual { get; set; }
        public bool RealtimeArrivalNoReport { get; set; }
        public string realtimeGbttArrivalLateness { get; set; }
        public string RealtimeDeparture { get; set; }
        public bool RealtimeDepartureActual { get; set; }
        public bool RealtimeDeparturelActual { get; set; }
        public bool RealtimeDepartureNoReport { get; set; }
        public string realtimeGbttDepartureLateness { get; set; }
        public string Platform { get; set; }
        public bool PlatformConfirmed { get; set; }
        public bool PlatformChanged { get; set; }
        public string CancelReasonCode { get; set; }
        public string CancelReasonShortText { get; set; }
        public string CancelReasonLongText { get; set; }
        public string DisplayAs { get; set; }
        public string ServiceLocation { get; set; }
    }

    public class Pair
    {
        public string Tiploc { get; set; }
        public string Description { get; set; }
        public string WorkingTime { get; set; }
        public string PublicTime { get; set; }
    }
}
