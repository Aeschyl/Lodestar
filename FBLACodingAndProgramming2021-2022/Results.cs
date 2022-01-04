namespace Json
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Text;
    using FBLACodingAndProgramming2021_2022;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Results
    {
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("features", NullValueHandling = NullValueHandling.Ignore)]
        public List<Feature> Features { get; set; }
        public static string  jsonString { get; set; }
        public static void GetJsonFromGeoApi()
        {
            string html;
            string url = @"https://api.geoapify.com/v2/places/?";


            StringBuilder builder = new StringBuilder();
            builder.Append(url);
            builder.Append("categories=");
            builder.Append(Parameters.category);
            if (!Parameters.subcategory.Equals(string.Empty))
            {
                builder.Append("." + Parameters.subcategory);
            }

            builder.Append("&");

            var conditions = new StringBuilder();

            if (Parameters.amenities.Count > 1)
            {
                foreach (string temp in Parameters.amenities)
                {
                    conditions.Append(temp + ", ");
                }
                conditions.Remove(conditions.Length - 1, conditions.Length);
                builder.Append("conditions=");
                builder.Append(conditions);
                builder.Append("&");
            }
            else if (Parameters.amenities.Count == 1)
            {
                conditions.Append(Parameters.amenities[0]);
                builder.Append("conditions=");
                builder.Append(conditions);
                builder.Append("&");

            }

            builder.Append("filter=circle:");
            builder.Append(Parameters.Longitude + ",");
            builder.Append(Parameters.Latitude + ",");
            builder.Append(int.Parse(Parameters.radius) * 1609);

            builder.Append("&");
            builder.Append("bias=proximity:");
            builder.Append(Parameters.Longitude);
            builder.Append("," + Parameters.Latitude);

            builder.Append("&");
            builder.Append("limit=10");

            builder.Append("&");
            builder.Append("apiKey=");
            //Need to hide this somehow
            builder.Append("233315ef9be94184b7addc54c006bbde");


            var request = (HttpWebRequest)WebRequest.Create(builder.ToString());

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }
            jsonString = html;
        }
    }
    public partial class Results
    {
        public static Results FromJson(string json) => JsonConvert.DeserializeObject<Results>(json, Json.Converter.Settings);
    }
        public partial class Feature
    {
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public FeatureType? Type { get; set; }

        [JsonProperty("properties", NullValueHandling = NullValueHandling.Ignore)]
        public Properties Properties { get; set; }

        [JsonProperty("geometry", NullValueHandling = NullValueHandling.Ignore)]
        public Geometry Geometry { get; set; }
    }

    public partial class Geometry
    {
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public GeometryType? Type { get; set; }

        [JsonProperty("coordinates", NullValueHandling = NullValueHandling.Ignore)]
        public List<double> Coordinates { get; set; }
    }

    public partial class Properties
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("street", NullValueHandling = NullValueHandling.Ignore)]
        public string Street { get; set; }

        [JsonProperty("suburb", NullValueHandling = NullValueHandling.Ignore)]
        public District? Suburb { get; set; }

        [JsonProperty("district", NullValueHandling = NullValueHandling.Ignore)]
        public District? District { get; set; }

        [JsonProperty("city", NullValueHandling = NullValueHandling.Ignore)]
        public City? City { get; set; }

        [JsonProperty("state", NullValueHandling = NullValueHandling.Ignore)]
        public City? State { get; set; }

        [JsonProperty("postcode", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? Postcode { get; set; }

        [JsonProperty("country", NullValueHandling = NullValueHandling.Ignore)]
        public Country? Country { get; set; }

        [JsonProperty("country_code", NullValueHandling = NullValueHandling.Ignore)]
        public CountryCode? CountryCode { get; set; }

        [JsonProperty("lon", NullValueHandling = NullValueHandling.Ignore)]
        public double? Lon { get; set; }

        [JsonProperty("lat", NullValueHandling = NullValueHandling.Ignore)]
        public double? Lat { get; set; }

        [JsonProperty("formatted", NullValueHandling = NullValueHandling.Ignore)]
        public string Formatted { get; set; }

        [JsonProperty("address_line1", NullValueHandling = NullValueHandling.Ignore)]
        public string AddressLine1 { get; set; }

        [JsonProperty("address_line2", NullValueHandling = NullValueHandling.Ignore)]
        public string AddressLine2 { get; set; }

        [JsonProperty("categories", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Categories { get; set; }

        [JsonProperty("details", NullValueHandling = NullValueHandling.Ignore)]
        public List<Detail> Details { get; set; }

        [JsonProperty("datasource", NullValueHandling = NullValueHandling.Ignore)]
        public Datasource Datasource { get; set; }

        [JsonProperty("distance", NullValueHandling = NullValueHandling.Ignore)]
        public long? Distance { get; set; }

        [JsonProperty("place_id", NullValueHandling = NullValueHandling.Ignore)]
        public string PlaceId { get; set; }

        [JsonProperty("housenumber", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? Housenumber { get; set; }
    }

    public partial class Datasource
    {
        [JsonProperty("sourcename", NullValueHandling = NullValueHandling.Ignore)]
        public Sourcename? Sourcename { get; set; }

        [JsonProperty("attribution", NullValueHandling = NullValueHandling.Ignore)]
        public Attribution? Attribution { get; set; }

        [JsonProperty("license", NullValueHandling = NullValueHandling.Ignore)]
        public License? License { get; set; }

        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public Uri Url { get; set; }
    }

    public enum GeometryType { Point };

    public enum City { Berlin };

    public enum Country { Germany };

    public enum CountryCode { De };

    public enum Attribution { OpenStreetMapContributors };

    public enum License { OpenDatabaseLicence };

    public enum Sourcename { Openstreetmap };

    public enum Detail { Details, DetailsCatering, DetailsContact, DetailsFacilities };

    public enum District { Mitte };

    public enum FeatureType { Feature };

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                GeometryTypeConverter.Singleton,
                CityConverter.Singleton,
                CountryConverter.Singleton,
                CountryCodeConverter.Singleton,
                AttributionConverter.Singleton,
                LicenseConverter.Singleton,
                SourcenameConverter.Singleton,
                DetailConverter.Singleton,
                DistrictConverter.Singleton,
                FeatureTypeConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class GeometryTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(GeometryType) || t == typeof(GeometryType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "Point")
            {
                return GeometryType.Point;
            }
            throw new Exception("Cannot unmarshal type GeometryType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (GeometryType)untypedValue;
            if (value == GeometryType.Point)
            {
                serializer.Serialize(writer, "Point");
                return;
            }
            throw new Exception("Cannot marshal type GeometryType");
        }

        public static readonly GeometryTypeConverter Singleton = new GeometryTypeConverter();
    }

    internal class CityConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(City) || t == typeof(City?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "Berlin")
            {
                return City.Berlin;
            }
            throw new Exception("Cannot unmarshal type City");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (City)untypedValue;
            if (value == City.Berlin)
            {
                serializer.Serialize(writer, "Berlin");
                return;
            }
            throw new Exception("Cannot marshal type City");
        }

        public static readonly CityConverter Singleton = new CityConverter();
    }

    internal class CountryConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Country) || t == typeof(Country?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "Germany")
            {
                return Country.Germany;
            }
            throw new Exception("Cannot unmarshal type Country");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Country)untypedValue;
            if (value == Country.Germany)
            {
                serializer.Serialize(writer, "Germany");
                return;
            }
            throw new Exception("Cannot marshal type Country");
        }

        public static readonly CountryConverter Singleton = new CountryConverter();
    }

    internal class CountryCodeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(CountryCode) || t == typeof(CountryCode?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "de")
            {
                return CountryCode.De;
            }
            throw new Exception("Cannot unmarshal type CountryCode");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (CountryCode)untypedValue;
            if (value == CountryCode.De)
            {
                serializer.Serialize(writer, "de");
                return;
            }
            throw new Exception("Cannot marshal type CountryCode");
        }

        public static readonly CountryCodeConverter Singleton = new CountryCodeConverter();
    }

    internal class AttributionConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Attribution) || t == typeof(Attribution?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "© OpenStreetMap contributors")
            {
                return Attribution.OpenStreetMapContributors;
            }
            throw new Exception("Cannot unmarshal type Attribution");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Attribution)untypedValue;
            if (value == Attribution.OpenStreetMapContributors)
            {
                serializer.Serialize(writer, "© OpenStreetMap contributors");
                return;
            }
            throw new Exception("Cannot marshal type Attribution");
        }

        public static readonly AttributionConverter Singleton = new AttributionConverter();
    }

    internal class LicenseConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(License) || t == typeof(License?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "Open Database Licence")
            {
                return License.OpenDatabaseLicence;
            }
            throw new Exception("Cannot unmarshal type License");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (License)untypedValue;
            if (value == License.OpenDatabaseLicence)
            {
                serializer.Serialize(writer, "Open Database Licence");
                return;
            }
            throw new Exception("Cannot marshal type License");
        }

        public static readonly LicenseConverter Singleton = new LicenseConverter();
    }

    internal class SourcenameConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Sourcename) || t == typeof(Sourcename?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "openstreetmap")
            {
                return Sourcename.Openstreetmap;
            }
            throw new Exception("Cannot unmarshal type Sourcename");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Sourcename)untypedValue;
            if (value == Sourcename.Openstreetmap)
            {
                serializer.Serialize(writer, "openstreetmap");
                return;
            }
            throw new Exception("Cannot marshal type Sourcename");
        }

        public static readonly SourcenameConverter Singleton = new SourcenameConverter();
    }

    internal class DetailConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Detail) || t == typeof(Detail?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "details":
                    return Detail.Details;
                case "details.catering":
                    return Detail.DetailsCatering;
                case "details.contact":
                    return Detail.DetailsContact;
                case "details.facilities":
                    return Detail.DetailsFacilities;
            }
            throw new Exception("Cannot unmarshal type Detail");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Detail)untypedValue;
            switch (value)
            {
                case Detail.Details:
                    serializer.Serialize(writer, "details");
                    return;
                case Detail.DetailsCatering:
                    serializer.Serialize(writer, "details.catering");
                    return;
                case Detail.DetailsContact:
                    serializer.Serialize(writer, "details.contact");
                    return;
                case Detail.DetailsFacilities:
                    serializer.Serialize(writer, "details.facilities");
                    return;
            }
            throw new Exception("Cannot marshal type Detail");
        }

        public static readonly DetailConverter Singleton = new DetailConverter();
    }

    internal class DistrictConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(District) || t == typeof(District?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "Mitte")
            {
                return District.Mitte;
            }
            throw new Exception("Cannot unmarshal type District");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (District)untypedValue;
            if (value == District.Mitte)
            {
                serializer.Serialize(writer, "Mitte");
                return;
            }
            throw new Exception("Cannot marshal type District");
        }

        public static readonly DistrictConverter Singleton = new DistrictConverter();
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }

    internal class FeatureTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(FeatureType) || t == typeof(FeatureType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "Feature")
            {
                return FeatureType.Feature;
            }
            throw new Exception("Cannot unmarshal type FeatureType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (FeatureType)untypedValue;
            if (value == FeatureType.Feature)
            {
                serializer.Serialize(writer, "Feature");
                return;
            }
            throw new Exception("Cannot marshal type FeatureType");
        }

        public static readonly FeatureTypeConverter Singleton = new FeatureTypeConverter();
    }
}
