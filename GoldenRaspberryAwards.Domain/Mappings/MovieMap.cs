using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using GoldenRaspberryAwards.Domain.Entities;

namespace GoldenRaspberryAwards.Domain.Mappings;

public class MovieMap : ClassMap<Movie>
{
	public MovieMap()
	{
        Map(movie => movie.Id).Ignore();
        Map(movie => movie.Year).Name("year");
        Map(movie => movie.Title).Name("title");
        Map(movie => movie.Studios).Name("studios");
        Map(movie => movie.Producers).Name("producers");
        Map(movie => movie.Winner).Name("winner").TypeConverter<YesNoToBoolConverter>();
    }
}

public class YesNoToBoolConverter : ITypeConverter
{
    public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        return text.Trim().ToLower().Equals("yes");
    }

    public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
    {
        return value.ToString();
    }
}
