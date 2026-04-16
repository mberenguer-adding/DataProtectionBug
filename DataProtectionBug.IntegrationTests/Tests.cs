using System.Net.Http.Json;
using FluentAssertions;

namespace DataProtectionBug.IntegrationTests;

public class Tests : BaseIntegrationTest
{
    public Tests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task TestDataProtection()
    {
        var response = await HttpClient.GetAsync("/?payload=testing");
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadFromJsonAsync<TestProtectionResponse>();
        body.Should().NotBeNull();
        body.Payload.Should().Be(body.UnprotectedPayload);
        body.UnprotectedPayload.Should().NotBeEmpty();
    }
}