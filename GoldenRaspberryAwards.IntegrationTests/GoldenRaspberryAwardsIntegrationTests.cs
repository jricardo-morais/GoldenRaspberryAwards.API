using FluentAssertions;
using GoldenRaspberryAwards.Domain.Entities;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;

namespace GoldenRaspberryAwards.IntegrationTests
{
    public class GoldenRaspberryAwardsIntegrationTests
    {
        [Fact]
        public async Task GetMovies_ReturnsOkStatusCode()
        {
            await using var application = new GoldenRaspberryAwardsApplication();

            var url = "api/movies";

            var client = application.CreateClient();

            var response = await client.GetAsync(url);
            var movies = await client.GetFromJsonAsync<List<Movie>>(url);

            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType!.ToString());
        }

        [Fact]
        public async Task GetAwardIntervals_ReturnsOkStatusCode()
        {
            await using var application = new GoldenRaspberryAwardsApplication();

            var url = "api/awardsintervals/intervals";

            var client = application.CreateClient();

            var response = await client.GetAsync(url);

            var awardIntervals = await client.GetFromJsonAsync<AwardIntervals>(url);

            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType!.ToString());
        }


        [Fact]
        public async Task ProcessCsvFile_WhenValidFile_Returns_Ok()
        {
            //Arrange
            var response = await UploadCsvFile();
            response.EnsureSuccessStatusCode();

            //Assert
            var result = await response.Content.ReadAsStringAsync();
            result.Should().Contain("Upload realizado com sucesso");
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType!.ToString());
        }

        private async Task<HttpResponseMessage> UploadCsvFile()
        {
            await using var application = new GoldenRaspberryAwardsApplication();

            var url = "api/Movies/Upload";

            var client = application.CreateClient();

            var fileContent = @"year;title;studios;producers;winner
                                1999;Movie 1;Studio 1;Producer 1;yes
                                2000;Movie 2;Studio 2;Producer 2;no
                                2001;Movie 1;Studio 3;Producer 1;yes
                                2010;Movie 2;Studio 4;Producer 2;no";

            var content = new MultipartFormDataContent();
            var fileContentBytes = Encoding.UTF8.GetBytes(fileContent);

            var file = new ByteArrayContent(fileContentBytes);
            file.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "file",
                FileName = "movieslist.csv"
            };
            content.Add(file);

            var response = await client.PostAsync(url, content);

            return response;
        }

        [Fact]
        public async Task GetAwardsIntervals_ShouldReturnCorrectIntervals()
        {

            // Act
            await using var application = new GoldenRaspberryAwardsApplication();
            var url = "api/AwardsIntervals/Intervals";
            var client = application.CreateClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            // Assert
            var result = await response.Content.ReadFromJsonAsync<AwardIntervals>();

            result.Should().NotBeNull();
            result?.Min.Should().HaveCount(1);
            result?.Min.Should().Contain(p => p.Producer == "Joel Silver" && p.Interval == 1);

            result?.Max.Should().HaveCount(1);
            result?.Max.Should().Contain(p => p.Producer == "Matthew Vaughn" && p.Interval == 13);
        }

    }
}