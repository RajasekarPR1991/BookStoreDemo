using System.Net.Http;
using System.Threading.Tasks;
using BookEntityDemo.IntegrationTests.Helpers;
using BookEntityDemo.Models.Request;
using BookEntityDemo.Models.Response;
using Newtonsoft.Json;
using Xunit;

namespace BookEntityDemo.IntegrationTests
{
    public class BookTests : IClassFixture<TestFixture<Startup>>
    {
        private HttpClient Client;

        public BookTests(TestFixture<Startup> fixture)
        {
            Client = fixture.Client;
        }

        [Fact]
        public async Task TestGetAllBooks()
        {
            // Arrange
            var request = "/api/book/lists";

            // Act
            var response = await Client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetBookById()
        {
            // Arrange
            var request = "/api/book/details?id=0c2a4feb-a31f-4807-9155-70bdd6775c24";

            // Act
            var response = await Client.GetAsync(request);

            // Assert
            Assert.Equal("NotFound", response.StatusCode.ToString());
        }

        [Fact]
        public async Task TestAddBookItem()
        {
            // Arrange
            var request = new
            {
                Url = "/api/book/add",
                Body = new BookRequestModel
                {
                    Name = "test",
                    AuthorName = "test1"
                }
            };

            // Act
            var response = await Client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            var value = await response.Content.ReadAsStringAsync();
            var responseModel = JsonConvert.DeserializeObject<BaseResponseModel>(value);

            // Assert
            Assert.True(responseModel.Success);
            Assert.Null(responseModel.Message);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestUpdateBookItem()
        {
            // Arrange
            var request = new
            {
                Url = "/api/book/update",
                Body = new BookUpdateRequestModel
                {
                    Id = "0c2a4feb-a31f-4807-9155-70bdd6775c24",
                    Name = "test",
                    AuthorName = "test1"
                }
            };

            // Act
            var response = await Client.PutAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            var value = await response.Content.ReadAsStringAsync();
            var responseModel = JsonConvert.DeserializeObject<BaseResponseModel>(value);

            // Assert
            Assert.False(responseModel.Success);
            Assert.NotNull(responseModel.Message);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestDeleteBookById()
        {
            // Arrange
            var request = "/api/book/delete?id=0c2a4feb-a31f-4807-9155-70bdd6775c24";

            // Act
            var response = await Client.DeleteAsync(request);
            var value = await response.Content.ReadAsStringAsync();
            var responseModel = JsonConvert.DeserializeObject<BaseResponseModel>(value);

            // Assert
            Assert.False(responseModel.Success);
            Assert.NotNull(responseModel.Message);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetBookById_BadRequest()
        {
            // Arrange
            var request = "/api/book/details?id=";

            // Act
            var response = await Client.GetAsync(request);

            // Assert
            Assert.Equal("BadRequest", response.StatusCode.ToString());
        }

        [Fact]
        public async Task TestAddBookItem_BadRequest()
        {
            // Arrange
            var request = new
            {
                Url = "/api/book/add"
            };

            // Act
            var response = await Client.PostAsync(request.Url, ContentHelper.GetStringContent(null));

            // Assert
            Assert.Equal("BadRequest", response.StatusCode.ToString());
        }

        [Fact]
        public async Task TestUpdateBookItem_BadRequest()
        {
            // Arrange
            var request = new
            {
                Url = "/api/book/update"
            };

            // Act
            var response = await Client.PutAsync(request.Url, ContentHelper.GetStringContent(null));

            // Assert
            Assert.Equal("BadRequest", response.StatusCode.ToString());
        }

        [Fact]
        public async Task DeleteBookById_BadRequest()
        {
            // Arrange
            var request = "/api/book/delete?id=";

            // Act
            var response = await Client.DeleteAsync(request);

            // Assert
            Assert.Equal("BadRequest", response.StatusCode.ToString());
        }
    }
}
