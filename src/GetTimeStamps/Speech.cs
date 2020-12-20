using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace GetTimeStamps
{
    class Speech
    {
        [JsonProperty("DisplayText")]
        public string DisplayText { get; set; }

        [JsonProperty("Duration")]
        public long Duration { get; set; }

        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("NBest")]
        public NBest[] NBest { get; set; }

        [JsonProperty("Offset")]
        public long Offset { get; set; }

        [JsonProperty("RecognitionStatus")]
        public string RecognitionStatus { get; set; }
    }

    public partial class NBest : IComparable<NBest>
    {
        public int CompareTo(NBest other)
        {
            if (this.Confidence == other.Confidence) return 0;
            if (this.Confidence > other.Confidence) return 1;
            return -1;
        }

        [JsonProperty("Confidence")]
        public double Confidence { get; set; }

        [JsonProperty("Display")]
        public string Display { get; set; }

        [JsonProperty("ITN")]
        public string Itn { get; set; }

        [JsonProperty("Lexical")]
        public string Lexical { get; set; }

        [JsonProperty("MaskedITN")]
        public string MaskedItn { get; set; }

        [JsonProperty("Words")]
        public Word[] Words { get; set; }
    }

    public partial class Word
    {
        [JsonProperty("Duration")]
        public long Duration { get; set; }

        [JsonProperty("Offset")]
        public long Offset { get; set; }

        [JsonProperty("Word")]
        public string WordWord { get; set; }
    }
}
